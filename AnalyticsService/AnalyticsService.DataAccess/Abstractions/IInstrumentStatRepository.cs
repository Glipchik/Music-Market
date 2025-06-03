using AnalyticsService.DataAccess.Entities;

namespace AnalyticsService.DataAccess.Abstractions;

public interface IInstrumentStatRepository : IRepository<InstrumentStat>
{
    Task DeleteByInstrumentIdAsync(Guid instrumentId, CancellationToken cancellationToken);
}