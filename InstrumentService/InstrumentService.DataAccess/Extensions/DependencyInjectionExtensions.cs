using InstrumentService.DataAccess.Abstractions;
using InstrumentService.DataAccess.Clients.Analytics;
using InstrumentService.DataAccess.Clients.User;
using InstrumentService.DataAccess.Options;
using InstrumentService.DataAccess.Repositories;
using InstrumentService.DataAccess.Storage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Minio;
using MongoDB.Driver;

namespace InstrumentService.DataAccess.Extensions;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddDataAccessServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        var analyticsClientOptions = configuration.GetSection(nameof(AnalyticsClientOptions)).Get<AnalyticsClientOptions>();
        if (analyticsClientOptions is null)
            throw new InvalidOperationException("AnalyticsClientOptions section is missing or invalid.");

        var userClientOptions = configuration.GetSection(nameof(UserClientOptions)).Get<UserClientOptions>();
        if (userClientOptions is null)
            throw new InvalidOperationException("UserClientOptions section is missing or invalid.");

        services.AddHttpClient<IAnalyticsClient, AnalyticsClient>(client =>
            client.BaseAddress = new Uri(analyticsClientOptions.BaseAddress));

        services.AddHttpClient<IUserClient, UserClient>(client =>
            client.BaseAddress = new Uri(userClientOptions.BaseAddress));

        var mongoOptions = configuration.GetSection(nameof(InstrumentDbOptions)).Get<InstrumentDbOptions>()!;
        services.AddSingleton<IMongoClient>(_ => new MongoClient(mongoOptions.ConnectionString));

        var minioOptions = configuration.GetSection(nameof(MinioOptions)).Get<MinioOptions>()!;

        services.AddMinio(options => options
            .WithEndpoint(minioOptions.Endpoint)
            .WithCredentials(minioOptions.AccessKey, minioOptions.SecretKey)
            .WithSSL(false)
            .Build());

        services.AddScoped<IInstrumentRepository, InstrumentRepository>();
        services.AddScoped<IInstrumentTypeRepository, InstrumentTypeRepository>();
        services.AddScoped<IInstrumentFormMetadataRepository, InstrumentFormMetadataRepository>();

        services.AddScoped<ICloudStorage, MinioCloudStorage>();

        services.Configure<InstrumentDbOptions>(configuration.GetSection(nameof(InstrumentDbOptions)));

        return services;
    }
}