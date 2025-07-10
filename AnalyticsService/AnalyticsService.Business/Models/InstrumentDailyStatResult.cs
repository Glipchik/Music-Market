namespace AnalyticsService.Business.Models;

public record InstrumentDailyStatResult(string InstrumentId, DateOnly Date, int Views);