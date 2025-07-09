using InstrumentService.Business.Extensions;
using InstrumentService.Business.Mapping;
using InstrumentService.DataAccess.Data;
using InstrumentService.DataAccess.Extensions;
using InstrumentService.DataAccess.Options;
using Microsoft.Extensions.Options;

namespace InstrumentService.API.Extensions;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddProjectServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<MinioOptions>(configuration.GetSection(nameof(MinioOptions)));

        services.AddAutoMapper((sp, config) =>
        {
            var minioOptions = sp.GetRequiredService<IOptions<MinioOptions>>();
            config.AddProfile(new InstrumentMappingProfile(minioOptions));
        }, AppDomain.CurrentDomain.GetAssemblies());

        services.AddDataAccessServices(configuration);
        services.AddBusinessServices();

        services.AddScoped<Seeder>();

        services.AddControllers();
        services.AddEndpointsApiExplorer();

        return services;
    }
}