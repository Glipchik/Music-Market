using MassTransit;
using Shared.Messaging.Contracts.Events.Instrument;

namespace InstrumentService.IntegrationTests.Consumers;

public class InstrumentDeletedConsumer : IConsumer<InstrumentDeleted>
{
    public Task Consume(ConsumeContext<InstrumentDeleted> context)
    {
        return Task.CompletedTask;
    }
}