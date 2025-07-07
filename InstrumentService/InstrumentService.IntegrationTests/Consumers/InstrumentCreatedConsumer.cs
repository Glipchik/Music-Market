using MassTransit;
using Shared.Messaging.Contracts.Events.Instrument;

namespace InstrumentService.IntegrationTests.Consumers;

public class InstrumentCreatedConsumer : IConsumer<InstrumentCreated>
{
    public Task Consume(ConsumeContext<InstrumentCreated> context)
    {
        return Task.CompletedTask;
    }
}