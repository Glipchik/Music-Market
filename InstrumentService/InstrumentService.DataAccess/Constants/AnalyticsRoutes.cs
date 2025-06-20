namespace InstrumentService.DataAccess.Constants;

public static class AnalyticsRoutes
{
    public static string GetInstrumentStat(string instrumentId) =>
        $"/analytics/instruments/{instrumentId}/stats";

    public static string GetInstrumentStatsRange(string instrumentId, DateOnly startDate, DateOnly endDate) =>
        $"/analytics/instruments/{instrumentId}/stats-range" +
        $"?startDate={startDate:yyyy-MM-dd}" +
        $"&endDate={endDate:yyyy-MM-dd}";

    public static string GetTopViewedInstruments(int limit) =>
        $"/analytics/instruments/top?limit={limit}";
}