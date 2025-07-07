using System;
using System.Net.Http;
using System.Threading.Tasks;
using InstrumentService.API;
using InstrumentService.IntegrationTests.Constants;
using InstrumentService.IntegrationTests.Consumers;
using MassTransit;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Minio;
using Minio.DataModel.Args;
using MongoDB.Driver;
using Testcontainers.Minio;
using Testcontainers.MongoDb;
using Testcontainers.RabbitMq;
using Xunit;

namespace InstrumentService.IntegrationTests;

public class CustomWebApplicationFactory : WebApplicationFactory<IApiMarker>, IAsyncLifetime
{
    private readonly MongoDbContainer _mongoDbContainer = new MongoDbBuilder()
        .WithImage("mongo:latest")
        .Build();

    private readonly RabbitMqContainer _rabbitMqContainer = new RabbitMqBuilder()
        .WithImage("rabbitmq:3-management")
        .WithPortBinding(8080, true)
        .WithUsername("guest")
        .WithPassword("guest")
        .Build();
    
    private readonly MinioContainer _minioContainer = new MinioBuilder()
        .WithImage("minio/minio")
        .WithPortBinding(9000, true)
        .WithPortBinding(9001, true)
        .WithUsername(MinioTestConstants.MinioUsername)
        .WithPassword(MinioTestConstants.MinioPassword)
        .Build();
    
    
    private const string TestDatabaseName = "InstrumentDb";
    public IMongoClient MongoClient { get; private set; } = null!;
    public IMinioClient MinioClient { get; private set; } = null!;
    public string MinioEndpoint { get; private set; } = null!;
    public HttpClient HttpClient { get; private set; } = null!;

    public IMongoCollection<TDocument> GetCollection<TDocument>(string collectionName)
    {
        return MongoClient.GetDatabase(TestDatabaseName).GetCollection<TDocument>(collectionName);
    }

    public async Task ClearCollectionsAsync(params string[] collectionNamesToClear)
    {
        var database = MongoClient.GetDatabase(TestDatabaseName);

        if (collectionNamesToClear.Length == 0)
        {
            var allCollectionNames = await (await database.ListCollectionNamesAsync()).ToListAsync();
            foreach (var collectionName in allCollectionNames)
            {
                await database.DropCollectionAsync(collectionName);
            }
        }
        else
        {
            foreach (var collectionName in collectionNamesToClear)
            {
                await database.DropCollectionAsync(collectionName);
            }
        }
    }

    public async Task InitializeAsync()
    {
        await _mongoDbContainer.StartAsync();
        await _rabbitMqContainer.StartAsync();
        await _minioContainer.StartAsync();

        var mappedPort = _minioContainer.GetMappedPublicPort(9000);
        MinioEndpoint = $"localhost:{mappedPort}";
        
        MinioClient = new MinioClient()
            .WithEndpoint(_minioContainer.Hostname, mappedPort)
            .WithCredentials(MinioTestConstants.MinioUsername, MinioTestConstants.MinioPassword)
            .Build();
        
        var bucketExists = await MinioClient.BucketExistsAsync(
            new BucketExistsArgs().WithBucket(MinioTestConstants.MinioTestBucket));

        if (!bucketExists)
        {
            await MinioClient.MakeBucketAsync(
                new MakeBucketArgs().WithBucket(MinioTestConstants.MinioTestBucket));
        }
        
        MongoClient = new MongoClient(_mongoDbContainer.GetConnectionString());
        HttpClient = CreateClient();
    }

    public new async Task DisposeAsync()
    {
        await _mongoDbContainer.DisposeAsync();
        await _rabbitMqContainer.DisposeAsync();
        await _minioContainer.DisposeAsync();
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        Environment.SetEnvironmentVariable("InstrumentDbOptions:ConnectionString",
            _mongoDbContainer.GetConnectionString());
        Environment.SetEnvironmentVariable("RabbitMqOptions:Host", _rabbitMqContainer.GetConnectionString());
        Environment.SetEnvironmentVariable("RabbitMqOptions:User", "guest");
        Environment.SetEnvironmentVariable("RabbitMqOptions:Password", "guest");
        
        Environment.SetEnvironmentVariable("MinioOptions:Endpoint", MinioEndpoint);
        Environment.SetEnvironmentVariable("MinioOptions:AccessKey", MinioTestConstants.MinioUsername);
        Environment.SetEnvironmentVariable("MinioOptions:SecretKey", MinioTestConstants.MinioPassword);
        Environment.SetEnvironmentVariable("MinioOptions:Host", $"http://{MinioEndpoint}");
        Environment.SetEnvironmentVariable("MinioOptions:BucketName", MinioTestConstants.MinioTestBucket);
        

        builder.ConfigureTestServices(services =>
        {
            services.RemoveAll<IConfigureOptions<AuthenticationOptions>>();
            services.RemoveAll<IConfigureOptions<JwtBearerOptions>>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = AuthTestConstants.Issuer,
                        ValidateAudience = true,
                        ValidAudience = AuthTestConstants.Audience,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = AuthTestConstants.SigningKey
                    };
                });

            services.AddMassTransitTestHarness(configurator =>
            {
                configurator.AddConsumer<InstrumentCreatedConsumer>();
                
                configurator.AddConsumer<InstrumentDeletedConsumer>();
                configurator.AddConsumer<InstrumentViewedConsumer>();

                configurator.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(new Uri(_rabbitMqContainer.GetConnectionString()), h =>
                    {
                        h.Username("guest");
                        h.Password("guest");
                    });
                    cfg.ConfigureEndpoints(context);
                });
            });
        });
    }
}