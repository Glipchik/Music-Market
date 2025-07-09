using AnalyticsService.DataAccess.Entities;

namespace AnalyticsService.DataAccess.Abstractions;

public interface IInstrumentStatRepository : IRepository<InstrumentStat, string>
{
    Task DeleteByInstrumentIdAsync(string instrumentId, CancellationToken cancellationToken);
    Task<List<InstrumentStat>> GetTopViewedAsync(int limit, CancellationToken cancellationToken);
}