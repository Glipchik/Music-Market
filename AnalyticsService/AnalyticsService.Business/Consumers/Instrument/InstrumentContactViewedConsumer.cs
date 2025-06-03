using AnalyticsService.DataAccess.Abstractions;
using AnalyticsService.DataAccess.Entities;
using MassTransit;
using Shared.Messaging.Contracts.Events.Instrument;

namespace AnalyticsService.Business.Consumers.Instrument;

public class InstrumentContactViewedConsumer(IUnitOfWork unitOfWork) : IConsumer<InstrumentContactViewed>
{
    public async Task Consume(ConsumeContext<InstrumentContactViewed> context)
    {
        var instrumentId = context.Message.InstrumentId;

        var instrumentStat = await unitOfWork.InstrumentStatRepository
            .GetByIdAsync(instrumentId, context.CancellationToken);

        if (instrumentStat is null)
        {
            instrumentStat = new InstrumentStat
            {
                InstrumentId = instrumentId,
                ContactViews = 1
            };
            await unitOfWork.InstrumentStatRepository.AddAsync(instrumentStat, context.CancellationToken);
        }
        else
        {
            instrumentStat.ContactViews++;
        }

        await unitOfWork.SaveChangesAsync(context.CancellationToken);
    }
}