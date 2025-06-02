using AnalyticsService.Business.Entities;

namespace AnalyticsService.Business.Abstractions;

public interface IInstrumentStatRepository : IRepository<InstrumentStat>
{
    Task DeleteByInstrumentIdAsync(Guid instrumentId, CancellationToken cancellationToken);
}