using InstrumentService.Business.Abstractions;
using InstrumentService.Business.Factories;
using InstrumentService.Business.Services;
using InstrumentService.Business.Strategies;
using Microsoft.Extensions.DependencyInjection;

namespace InstrumentService.Business.Extensions;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddBusinessServices(this IServiceCollection services)
    {
        services.AddScoped<IInstrumentResponseStrategy, CelloResponseStrategy>();
        services.AddScoped<IInstrumentResponseStrategy, GuitarResponseStrategy>();
        services.AddScoped<IInstrumentResponseStrategy, PianoResponseStrategy>();
        services.AddScoped<IInstrumentResponseStrategy, DrumResponseStrategy>();

        services.AddScoped<IInstrumentResponseModelFactory, InstrumentResponseModelFactory>();

        services.AddScoped<IInstrumentFormMetadataService, InstrumentFormMetadataService>();
        services.AddScoped<IInstrumentService, Services.InstrumentService>();
        services.AddScoped<IFileService, FileService>();

        return services;
    }
}