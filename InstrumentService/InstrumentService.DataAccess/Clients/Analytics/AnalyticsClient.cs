using System.Net.Http.Json;
using InstrumentService.DataAccess.Abstractions;
using InstrumentService.DataAccess.Clients.Analytics.Models;
using InstrumentService.DataAccess.Constants;

namespace InstrumentService.DataAccess.Clients.Analytics;

public class AnalyticsClient(HttpClient httpClient) : IAnalyticsClient
{
    public async Task<InstrumentStat> GetInstrumentStatAsync(string instrumentId, CancellationToken cancellationToken)
    {
        var response = await httpClient.GetAsync(AnalyticsRoutes.GetInstrumentStat(instrumentId), cancellationToken);

        return await response.Content.ReadFromJsonAsync<InstrumentStat>(cancellationToken: cancellationToken)
               ?? throw new InvalidOperationException("Failed to deserialize stat response.");
    }

    public async Task<List<InstrumentDailyStat>> GetInstrumentDailyStatsForDateRangeAsync(string instrumentId,
        DateOnly startDate, DateOnly endDate,
        CancellationToken cancellationToken)
    {
        var response =
            await httpClient.GetAsync(AnalyticsRoutes.GetInstrumentStatsRange(instrumentId, startDate, endDate),
                cancellationToken);

        return await response.Content.ReadFromJsonAsync<List<InstrumentDailyStat>>(cancellationToken)
               ?? throw new InvalidOperationException("Failed to deserialize daily stats response.");
    }

    public async Task<List<TopInstrument>> GetTopViewedInstrumentsAsync(int limit, CancellationToken cancellationToken)
    {
        var response = await httpClient.GetAsync(AnalyticsRoutes.GetTopViewedInstruments(limit), cancellationToken);

        return await response.Content.ReadFromJsonAsync<List<TopInstrument>>(cancellationToken)
               ?? throw new InvalidOperationException("Failed to deserialize top viewed stats response.");
    }
}