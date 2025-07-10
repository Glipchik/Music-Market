using AnalyticsService.Business.Consumers.Instrument;
using AnalyticsService.Business.Consumers.User;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Options;

namespace AnalyticsService.Business.Extensions;

public static class MassTransitExtensions
{
    public static IServiceCollection AddMessaging(this IServiceCollection services, IConfiguration configuration)
    {
        var rabbitMqOptions = configuration.GetSection(nameof(RabbitMqOptions)).Get<RabbitMqOptions>()!;

        services.AddMassTransit(options =>
        {
            options.AddConsumer<InstrumentCreatedConsumer>();
            options.AddConsumer<InstrumentDeletedConsumer>();
            options.AddConsumer<InstrumentViewedConsumer>();
            options.AddConsumer<InstrumentBookmarkedConsumer>();
            options.AddConsumer<InstrumentContactViewedConsumer>();

            options.AddConsumer<UserInstrumentCreatedConsumer>();
            options.AddConsumer<UserLoggedInConsumer>();

            options.UsingRabbitMq((context, config) =>
            {
                config.Host(rabbitMqOptions.Host, "/", hostConfigurator =>
                {
                    hostConfigurator.Username(rabbitMqOptions.User);
                    hostConfigurator.Password(rabbitMqOptions.Password);
                });

                config.ReceiveEndpoint("analytics-instrument-created", endpoint =>
                    endpoint.ConfigureConsumer<InstrumentCreatedConsumer>(context));

                config.ReceiveEndpoint("analytics-instrument-deleted", endpoint =>
                    endpoint.ConfigureConsumer<InstrumentDeletedConsumer>(context));

                config.ReceiveEndpoint("analytics-instrument-viewed", endpoint =>
                    endpoint.ConfigureConsumer<InstrumentViewedConsumer>(context));

                config.ReceiveEndpoint("analytics-instrument-bookmarked", endpoint =>
                    endpoint.ConfigureConsumer<InstrumentBookmarkedConsumer>(context));

                config.ReceiveEndpoint("analytics-instrument-contact-viewed", endpoint =>
                    endpoint.ConfigureConsumer<InstrumentContactViewedConsumer>(context));

                config.ReceiveEndpoint("analytics-user-logged-in", endpoint =>
                    endpoint.ConfigureConsumer<UserLoggedInConsumer>(context));

                config.ReceiveEndpoint("analytics-user-instrument-viewed", endpoint =>
                    endpoint.ConfigureConsumer<UserInstrumentCreatedConsumer>(context));
            });
        });

        return services;
    }
}