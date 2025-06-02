namespace AnalyticsService.Business.Entities;

public class InstrumentDailyStat(Guid instrumentId, DateOnly date, int views)
{
    public Guid InstrumentId { get; set; } = instrumentId;
    public DateOnly Date { get; set; } = date;
    public int Views { get; set; } = views;
}