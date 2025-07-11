using AnalyticsService.DataAccess.Entities;

namespace AnalyticsService.IntegrationTests.Expectations;

public class TopInstrumentExpectation
{
    public string InstrumentId { get; init; }
    public int Views { get; init; }

    public TopInstrumentExpectation(InstrumentStat stat)
    {
        InstrumentId = stat.InstrumentId;
        Views = stat.Views;
    }
}