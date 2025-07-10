using AnalyticsService.Business.Abstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AnalyticsService.Business.Extensions;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddBusinessServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAutoMapper(typeof(DependencyInjectionExtensions).Assembly);
        services.AddMessaging(configuration);

        services.AddScoped<IAnalyticsService, Services.AnalyticsService>();

        return services;
    }
}