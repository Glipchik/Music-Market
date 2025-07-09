using AnalyticsService.DataAccess.Entities;

namespace AnalyticsService.DataAccess.Abstractions;

public interface IInstrumentDailyStatRepository : IRepository<InstrumentDailyStat, string>
{
    Task<List<InstrumentDailyStat>> GetByDateRangeAsync(string instrumentId, DateOnly startDate, DateOnly endDate,
        CancellationToken cancellationToken);

    Task<InstrumentDailyStat?> GetByDateAsync(string instrumentId, DateOnly date, CancellationToken cancellationToken);
    Task DeleteByInstrumentIdAsync(string instrumentId, CancellationToken cancellationToken);
}