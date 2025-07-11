using AnalyticsService.DataAccess.Entities;

namespace AnalyticsService.IntegrationTests.Builders;

public class InstrumentDailyStatBuilder
{
    private readonly InstrumentDailyStat _dailyStat = new();

    public InstrumentDailyStatBuilder WithId(string id)
    {
        _dailyStat.InstrumentId = id;

        return this;
    }

    public InstrumentDailyStatBuilder WithViews(int count)
    {
        _dailyStat.Views = count;

        return this;
    }

    public InstrumentDailyStatBuilder WithDate(DateOnly date)
    {
        _dailyStat.Date = date;

        return this;
    }

    public InstrumentDailyStat Build() => _dailyStat;
}