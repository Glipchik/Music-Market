namespace AnalyticsService.Business.Models;

public record UserStatResult(Guid UserId, int TotalLogins, int InstrumentsCreated);