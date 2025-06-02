using AnalyticsService.Business.Consumers.Instrument;
using AnalyticsService.Business.Consumers.User;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;

namespace AnalyticsService.Business.Extensions;

public static class MassTransitExtensions
{
    public static IServiceCollection AddMessaging(this IServiceCollection services)
    {
        services.AddMassTransit(options =>
        {
            options.AddConsumer<InstrumentDeletedConsumer>();
            options.AddConsumer<InstrumentViewedConsumer>();
            options.AddConsumer<InstrumentBookmarkedConsumer>();
            options.AddConsumer<InstrumentContactViewedConsumer>();
        
            options.AddConsumer<UserInstrumentCreatedConsumer>();
            options.AddConsumer<UserLoggedInConsumer>();
        
            options.UsingRabbitMq((context, configuration) =>
            {
                configuration.Host("localhost", "/", hostConfigurator =>
                {
                    hostConfigurator.Username("guest");
                    hostConfigurator.Password("guest");
                });
        
                configuration.ReceiveEndpoint("analytics-instrument-deleted", endpoint =>
                    endpoint.ConfigureConsumer<InstrumentDeletedConsumer>(context));
        
                configuration.ReceiveEndpoint("analytics-instrument-viewed", endpoint =>
                    endpoint.ConfigureConsumer<InstrumentViewedConsumer>(context));
        
                configuration.ReceiveEndpoint("analytics-instrument-bookmarked", endpoint =>
                    endpoint.ConfigureConsumer<InstrumentBookmarkedConsumer>(context));
        
                configuration.ReceiveEndpoint("analytics-instrument-contact-viewed", endpoint =>
                    endpoint.ConfigureConsumer<InstrumentContactViewedConsumer>(context));
        
                configuration.ReceiveEndpoint("analytics-user-logged-in", endpoint =>
                    endpoint.ConfigureConsumer<UserLoggedInConsumer>(context));
        
                configuration.ReceiveEndpoint("analytics-user-instrument-viewed", endpoint =>
                    endpoint.ConfigureConsumer<UserInstrumentCreatedConsumer>(context));
            });
        });

        return services;
    }
}