namespace AnalyticsService.Business.Entities;

public class InstrumentStat
{
    public Guid InstrumentId { get; set; }
    public int Views { get; set; }
    public int ContactViews { get; set; }
    public int Bookmarks { get; set; }
}