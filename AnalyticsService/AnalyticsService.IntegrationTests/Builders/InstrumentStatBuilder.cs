using AnalyticsService.DataAccess.Entities;

namespace AnalyticsService.IntegrationTests.Builders;

public class InstrumentStatBuilder
{
    private readonly InstrumentStat _stat = new()
    {
        Views = 0,
        ContactViews = 0,
        Bookmarks = 0
    };

    public InstrumentStatBuilder WithId(string id)
    {
        _stat.InstrumentId = id;
        return this;
    }

    public InstrumentStatBuilder WithViews(int count)
    {
        _stat.Views = count;
        return this;
    }

    public InstrumentStatBuilder WithContactViews(int count)
    {
        _stat.ContactViews = count;
        return this;
    }

    public InstrumentStatBuilder WithBookmarks(int count)
    {
        _stat.Bookmarks = count;
        return this;
    }

    public InstrumentStat Build() => _stat;
}