namespace AnalyticsService.DataAccess.Entities;

public class InstrumentDailyStat
{
    public string InstrumentId { get; set; } = null!;
    public DateOnly Date { get; set; }
    public int Views { get; set; }
}