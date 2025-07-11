using AnalyticsService.DataAccess.Entities;
using AnalyticsService.IntegrationTests.Builders;

namespace AnalyticsService.IntegrationTests.TestData;

public static class GetInstrumentStatTestData
{
    public static InstrumentStat CreateStat() =>
        new InstrumentStatBuilder()
            .WithId("guitar-123")
            .WithViews(42)
            .WithContactViews(10)
            .WithBookmarks(3)
            .Build();
}