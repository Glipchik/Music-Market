namespace AnalyticsService.DataAccess.Entities;

public class UserStat
{
    public string UserId { get; set; } = null!;
    public int TotalLogins { get; set; }
    public int InstrumentsCreated { get; set; }
}