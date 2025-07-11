using AnalyticsService.DataAccess.Entities;
using AnalyticsService.IntegrationTests.Builders;

namespace AnalyticsService.IntegrationTests.TestData;

public static class GetTopViewedInstrumentsTestData
{
    public static List<InstrumentStat> CreateStats() =>
    [
        new InstrumentStatBuilder().WithId("i1").WithViews(20).WithContactViews(5).WithBookmarks(1).Build(),
        new InstrumentStatBuilder().WithId("i2").WithViews(40).WithContactViews(2).WithBookmarks(3).Build(),
        new InstrumentStatBuilder().WithId("i3").WithViews(30).WithContactViews(7).WithBookmarks(4).Build(),
        new InstrumentStatBuilder().WithId("i4").WithViews(35).WithContactViews(13).WithBookmarks(2).Build()
    ];
}