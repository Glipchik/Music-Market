using AnalyticsService.DataAccess.Abstractions;
using AnalyticsService.DataAccess.Entities;
using MassTransit;
using Shared.Messaging.Contracts.Events.Instrument;

namespace AnalyticsService.Business.Consumers.Instrument;

public class InstrumentViewedConsumer(IUnitOfWork unitOfWork) : IConsumer<InstrumentViewed>
{
    public async Task Consume(ConsumeContext<InstrumentViewed> context)
    {
        var instrumentId = context.Message.InstrumentId;

        var instrumentStat = await unitOfWork.InstrumentStatRepository
            .GetByIdAsync(instrumentId, context.CancellationToken);

        if (instrumentStat is null)
        {
            instrumentStat = new InstrumentStat { InstrumentId = instrumentId, Views = 1 };
            await unitOfWork.InstrumentStatRepository.AddAsync(instrumentStat, context.CancellationToken);
        }
        else
        {
            instrumentStat.Views++;
        }

        var date = context.Message.Date;

        var instrumentDailyStat = await unitOfWork.InstrumentDailyStatRepository
            .GetByDateAsync(instrumentId, date, context.CancellationToken);

        if (instrumentDailyStat is null)
        {
            instrumentDailyStat = new InstrumentDailyStat(instrumentId, date, 1);
            await unitOfWork.InstrumentDailyStatRepository.AddAsync(instrumentDailyStat, context.CancellationToken);
        }
        else
        {
            instrumentDailyStat.Views++;
        }

        await unitOfWork.SaveChangesAsync(context.CancellationToken);
    }
}