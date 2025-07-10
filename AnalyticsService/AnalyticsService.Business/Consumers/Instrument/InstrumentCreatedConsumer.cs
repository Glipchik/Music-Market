using AnalyticsService.DataAccess.Abstractions;
using AnalyticsService.DataAccess.Entities;
using MassTransit;
using Shared.Messaging.Contracts.Events.Instrument;

namespace AnalyticsService.Business.Consumers.Instrument;

public class InstrumentCreatedConsumer(IUnitOfWork unitOfWork) : IConsumer<InstrumentCreated>
{
    public async Task Consume(ConsumeContext<InstrumentCreated> context)
    {
        var instrumentId = context.Message.InstrumentId;

        var instrumentStat = new InstrumentStat { InstrumentId = instrumentId };
        await unitOfWork.InstrumentStatRepository.AddAsync(instrumentStat, context.CancellationToken);

        var date = context.Message.Date;

        var instrumentDailyStat = new InstrumentDailyStat { InstrumentId = instrumentId, Date = date };
        await unitOfWork.InstrumentDailyStatRepository.AddAsync(instrumentDailyStat, context.CancellationToken);

        await unitOfWork.SaveChangesAsync(context.CancellationToken);
    }
}