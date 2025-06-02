namespace AnalyticsService.Business.Models;

public record InstrumentDailyStatResult(Guid InstrumentId, DateOnly Date, int Views);