using System.Data.Common;
using AnalyticsService.API;
using AnalyticsService.Business.Consumers.Instrument;
using AnalyticsService.DataAccess.Data;
using AnalyticsService.IntegrationTests.Constants;
using MassTransit;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Npgsql;
using Respawn;
using Testcontainers.PostgreSql;

namespace AnalyticsService.IntegrationTests;

public class CustomWebApplicationFactory : WebApplicationFactory<IApiMarker>, IAsyncLifetime
{
    private readonly PostgreSqlContainer _dbContainer = new PostgreSqlBuilder()
        .WithImage("postgres:latest")
        .WithDatabase("test")
        .WithUsername("admin")
        .WithPassword("admin")
        .Build();

    public HttpClient HttpClient { get; private set; } = null!;

    private DbConnection _dbConnection = null!;
    private Respawner _respawner = null!;

    public async Task InitializeAsync()
    {
        await _dbContainer.StartAsync();
        _dbConnection = new NpgsqlConnection(_dbContainer.GetConnectionString());

        HttpClient = CreateClient();

        await _dbConnection.OpenAsync();
        await ApplyMigrationsAsync();
        await InitializeRespawnerAsync();
    }

    public async Task ResetDatabaseAsync()
    {
        await _respawner.ResetAsync(_dbConnection);
    }

    public new async Task DisposeAsync()
    {
        await _dbContainer.DisposeAsync();
        await _dbConnection.DisposeAsync();
    }

    private async Task InitializeRespawnerAsync()
    {
        _respawner = await Respawner.CreateAsync(_dbConnection, new RespawnerOptions
        {
            SchemasToInclude = ["analytics"],
            DbAdapter = DbAdapter.Postgres
        });
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        Environment.SetEnvironmentVariable("ConnectionStrings:DefaultConnection", _dbContainer.GetConnectionString());
        Environment.SetEnvironmentVariable("RabbitMqOptions:User", "guest");
        Environment.SetEnvironmentVariable("RabbitMqOptions:Password", "guest");

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
                        ValidIssuer = TestAuthConstants.Issuer,
                        ValidateAudience = true,
                        ValidAudience = TestAuthConstants.Audience,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = TestAuthConstants.SigningKey
                    };
                });

            services.AddMassTransitTestHarness(configurator =>
            {
                configurator.AddConsumer<InstrumentBookmarkedConsumer>();
                configurator.UsingInMemory((context, cfg) => { cfg.ConfigureEndpoints(context); });
            });
        });
    }

    private async Task ApplyMigrationsAsync()
    {
        using var scope = Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        await dbContext.Database.MigrateAsync();
    }
}