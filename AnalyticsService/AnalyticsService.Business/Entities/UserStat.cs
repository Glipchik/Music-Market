namespace AnalyticsService.Business.Entities;

public class UserStat
{
    public Guid UserId { get; set; }
    public int TotalLogins { get; set; }
    public int InstrumentsCreated { get; set; }
}
