using AnalyticsService.Business.Abstractions;
using AnalyticsService.Business.Entities;
using MassTransit;
using Shared.Messaging.Contracts.Events.Instrument;

namespace AnalyticsService.Business.Consumers.Instrument;

public class InstrumentBookmarkedConsumer(IUnitOfWork unitOfWork) : IConsumer<InstrumentBookmarked>
{
    public async Task Consume(ConsumeContext<InstrumentBookmarked> context)
    {
        var instrumentId = context.Message.InstrumentId;
        
        var instrumentStat = await unitOfWork.InstrumentStatRepository
            .GetByIdAsync(instrumentId, context.CancellationToken);

        if (instrumentStat is null)
        {
            instrumentStat = new InstrumentStat
            {
                InstrumentId = instrumentId,
                Bookmarks = 1
            };
            await unitOfWork.InstrumentStatRepository.AddAsync(instrumentStat, context.CancellationToken);
        }
        else
        {
            instrumentStat.Bookmarks++;
        }

        await unitOfWork.SaveChangesAsync(context.CancellationToken);
    }
}