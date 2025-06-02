using AnalyticsService.Business.Entities;

namespace AnalyticsService.Business.Abstractions;

public interface IInstrumentDailyStatRepository : IRepository<InstrumentDailyStat>
{
    Task<List<InstrumentDailyStat>> GetByDateRangeAsync(Guid instrumentId, DateOnly startDate, DateOnly endDate,
        CancellationToken cancellationToken);

    Task<InstrumentDailyStat?> GetByDateAsync(Guid instrumentId, DateOnly date, CancellationToken cancellationToken);
    Task DeleteByInstrumentIdAsync(Guid instrumentId, CancellationToken cancellationToken);
}