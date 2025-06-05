using AnalyticsService.Business.Models;

namespace AnalyticsService.Business.Abstractions;

public interface IAnalyticsService
{
    Task<InstrumentStatResult> GetInstrumentStatAsync(Guid instrumentId, CancellationToken cancellationToken);

    Task<InstrumentDailyStatResult> GetInstrumentDailyStatByDateAsync(Guid instrumentId, DateOnly date,
        CancellationToken cancellationToken);

    Task<List<InstrumentDailyStatResult>> GetInstrumentDailyStatsByDateRangeAsync(Guid instrumentId, DateOnly startDate,
        DateOnly endDate, CancellationToken cancellationToken);

    Task<UserStatResult> GetUserStatAsync(Guid userId, CancellationToken cancellationToken);
}