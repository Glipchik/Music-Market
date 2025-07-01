using InstrumentService.DataAccess.Abstractions;
using InstrumentService.DataAccess.Clients.Analytics;
using InstrumentService.DataAccess.Clients.User;
using InstrumentService.DataAccess.Http.Handlers;
using InstrumentService.DataAccess.Http.Policies;
using InstrumentService.DataAccess.Options;
using InstrumentService.DataAccess.Repositories;
using InstrumentService.DataAccess.Services;
using InstrumentService.DataAccess.Storage;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Minio;
using MongoDB.Driver;

namespace InstrumentService.DataAccess.Extensions;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddDataAccessServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<RedisOptions>(configuration.GetSection(nameof(RedisOptions)));
        services.Configure<ClientCredentialsOptions>(configuration.GetSection(nameof(ClientCredentialsOptions)));

        var redisOptions = configuration.GetSection(nameof(RedisOptions)).Get<RedisOptions>();
        if (redisOptions is null)
            throw new InvalidOperationException("RedisOptions section is missing or invalid.");

        var analyticsClientOptions = configuration.GetSection(nameof(AnalyticsClientOptions)).Get<AnalyticsClientOptions>();
        if (analyticsClientOptions is null)
            throw new InvalidOperationException("AnalyticsClientOptions section is missing or invalid.");

        var userClientOptions = configuration.GetSection(nameof(UserClientOptions)).Get<UserClientOptions>();
        if (userClientOptions is null)
            throw new InvalidOperationException("UserClientOptions section is missing or invalid.");

        var authOptions = configuration.GetSection(nameof(AuthOptions)).Get<AuthOptions>();
        if (authOptions is null)
            throw new InvalidOperationException("AuthOptions section is missing or invalid.");

        services.AddStackExchangeRedisCache(options => { options.Configuration = redisOptions.Configuration; });

        services.AddTransient<AccessTokenHandler>();

        services.AddHttpClient<IAnalyticsClient, AnalyticsClient>(client =>
                client.BaseAddress = new Uri(analyticsClientOptions.BaseAddress))
            .AddHttpMessageHandler<AccessTokenHandler>()
            .AddPolicyHandler(PollyPolicies.GetRetryPolicy())
            .AddPolicyHandler(PollyPolicies.GetTimeoutPolicy())
            .AddPolicyHandler(PollyPolicies.GetCircuitBreakerPolicy());

        services.AddHttpClient<IUserClient, UserClient>(client =>
                client.BaseAddress = new Uri(userClientOptions.BaseAddress))
            .AddHttpMessageHandler<AccessTokenHandler>()
            .AddPolicyHandler(PollyPolicies.GetRetryPolicy())
            .AddPolicyHandler(PollyPolicies.GetTimeoutPolicy())
            .AddPolicyHandler(PollyPolicies.GetCircuitBreakerPolicy());

        services.AddHttpClient<ITokenService, TokenService>(client =>
            client.BaseAddress = new Uri(authOptions.Authority));

        services.Decorate<ITokenService, CachedTokenServiceDecorator>();

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