namespace AnalyticsService.IntegrationTests.Constants;

public static class TestConstants
{
    public const string NonExistentInstrumentId = "non-existent-instrument-id";
    public const string InstrumentId = "instrument-id";
    public static DateOnly GetDate() => new DateOnly(2024, 01, 01);
}