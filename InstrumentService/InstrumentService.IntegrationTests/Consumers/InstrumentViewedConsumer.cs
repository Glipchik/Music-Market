using MassTransit;
using Shared.Messaging.Contracts.Events.Instrument;

namespace InstrumentService.IntegrationTests.Consumers;

public class InstrumentViewedConsumer : IConsumer<InstrumentViewed>
{
    public Task Consume(ConsumeContext<InstrumentViewed> context)
    {
        return Task.CompletedTask;
    }
}