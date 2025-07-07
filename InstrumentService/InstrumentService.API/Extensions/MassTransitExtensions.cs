using MassTransit;
using Shared.Options;

namespace InstrumentService.API.Extensions;

public static class MassTransitExtensions
{
    public static IServiceCollection AddRabbitMq(this IServiceCollection services, IConfiguration configuration)
    {
        var rabbitMqOptions = configuration.GetSection(nameof(RabbitMqOptions)).Get<RabbitMqOptions>()!;

        services.AddMassTransit(options =>
        {
            options.UsingRabbitMq((_, config) =>
            {
                config.Host(rabbitMqOptions.Host, "/", host =>
                {
                    host.Username(rabbitMqOptions.User);
                    host.Password(rabbitMqOptions.Password);
                });
            });
        });

        return services;
    }
}