using AnalyticsService.Business.Abstractions;
using AnalyticsService.DataAccess.Data;
using AnalyticsService.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AnalyticsService.DataAccess.Extensions;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddDataAccessServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<IInstrumentStatRepository, InstrumentStatRepository>();
        services.AddScoped<IInstrumentDailyStatRepository, InstrumentDailyStatRepository>();
        services.AddScoped<IUserStatRepository, UserStatRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        
        return services;
    }
}