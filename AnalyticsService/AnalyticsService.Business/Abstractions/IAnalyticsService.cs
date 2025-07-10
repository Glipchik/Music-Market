using AnalyticsService.Business.Models;

namespace AnalyticsService.Business.Abstractions;

public interface IAnalyticsService
{
    Task<InstrumentStatResult> GetInstrumentStatAsync(string instrumentId, CancellationToken cancellationToken);

    Task<InstrumentDailyStatResult> GetInstrumentDailyStatByDateAsync(string instrumentId, DateOnly date,
        CancellationToken cancellationToken);

    Task<List<InstrumentDailyStatResult>> GetInstrumentDailyStatsByDateRangeAsync(string instrumentId,
        DateOnly startDate,
        DateOnly endDate, CancellationToken cancellationToken);

    Task<UserStatResult> GetUserStatAsync(string userId, CancellationToken cancellationToken);

    Task<List<TopInstrumentModel>> GetTopViewedInstrumentIdsAsync(int limit, CancellationToken cancellationToken);
}