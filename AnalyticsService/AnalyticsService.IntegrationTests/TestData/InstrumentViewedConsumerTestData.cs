using AnalyticsService.DataAccess.Entities;
using AnalyticsService.IntegrationTests.Builders;
using AnalyticsService.IntegrationTests.Constants;

namespace AnalyticsService.IntegrationTests.TestData;

public static class InstrumentViewedConsumerTestData
{
    public static InstrumentStat CreateStat(string? id = null) =>
        new InstrumentStatBuilder()
            .WithId(id ?? TestConstants.InstrumentId)
            .WithViews(10)
            .Build();

    public static InstrumentDailyStat CreateDailyStat(string? id = null, DateOnly? date = null) =>
        new InstrumentDailyStatBuilder()
            .WithId(id ?? TestConstants.InstrumentId)
            .WithViews(3)
            .WithDate(date ?? TestConstants.GetDate())
            .Build();
}