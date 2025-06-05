using AnalyticsService.DataAccess.Abstractions;
using MassTransit;
using Shared.Messaging.Contracts.Events.Instrument;

namespace AnalyticsService.Business.Consumers.Instrument;

public class InstrumentDeletedConsumer(IUnitOfWork unitOfWork) : IConsumer<InstrumentDeleted>
{
    public async Task Consume(ConsumeContext<InstrumentDeleted> context)
    {
        var instrumentId = context.Message.InstrumentId;

        await unitOfWork.InstrumentStatRepository
            .DeleteByInstrumentIdAsync(instrumentId, context.CancellationToken);
        await unitOfWork.InstrumentDailyStatRepository
            .DeleteByInstrumentIdAsync(instrumentId, context.CancellationToken);
        await unitOfWork.SaveChangesAsync(context.CancellationToken);
    }
}