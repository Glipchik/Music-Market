using AnalyticsService.Business.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace AnalyticsService.Business.Extensions;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddBusinessServices(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(DependencyInjectionExtensions).Assembly);
        services.AddMessaging();

        services.AddScoped<IAnalyticsService, Services.AnalyticsService>();
        
        return services;
    }
}