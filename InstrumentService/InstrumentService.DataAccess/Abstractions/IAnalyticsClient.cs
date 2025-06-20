using InstrumentService.DataAccess.Clients.Analytics.Models;

namespace InstrumentService.DataAccess.Abstractions;

public interface IAnalyticsClient
{
    Task<InstrumentStat> GetInstrumentStatAsync(string instrumentId, CancellationToken cancellationToken);

    Task<List<InstrumentDailyStat>> GetInstrumentDailyStatsForDateRangeAsync(
        string instrumentId, DateOnly startDate, DateOnly endDate, CancellationToken cancellationToken);

    Task<List<TopInstrument>> GetTopViewedInstrumentsAsync(int limit, CancellationToken cancellationToken);
}