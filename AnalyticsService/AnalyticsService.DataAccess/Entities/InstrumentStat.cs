namespace AnalyticsService.DataAccess.Entities;

public class InstrumentStat
{
    public string InstrumentId { get; set; } = null!;
    public int Views { get; set; }
    public int ContactViews { get; set; }
    public int Bookmarks { get; set; }
}