using AnalyticsService.Business.Models;
using AnalyticsService.DataAccess.Entities;

namespace AnalyticsService.IntegrationTests.Builders;

public class InstrumentStatResultExpectationBuilder
{
    private InstrumentStatResult _result = new(
        InstrumentId: "default-id",
        Views: 0,
        ContactViews: 0,
        Bookmarks: 0
    );

    public InstrumentStatResultExpectationBuilder FromEntity(InstrumentStat stat)
    {
        _result = new InstrumentStatResult(
            stat.InstrumentId,
            stat.Views,
            stat.ContactViews,
            stat.Bookmarks
        );
        return this;
    }

    public InstrumentStatResultExpectationBuilder WithId(string id)
    {
        _result = _result with { InstrumentId = id };
        return this;
    }

    public InstrumentStatResultExpectationBuilder WithViews(int views)
    {
        _result = _result with { Views = views };
        return this;
    }

    public InstrumentStatResultExpectationBuilder WithContactViews(int contactViews)
    {
        _result = _result with { ContactViews = contactViews };
        return this;
    }

    public InstrumentStatResultExpectationBuilder WithBookmarks(int bookmarks)
    {
        _result = _result with { Bookmarks = bookmarks };
        return this;
    }

    public InstrumentStatResult Build() => _result;
}