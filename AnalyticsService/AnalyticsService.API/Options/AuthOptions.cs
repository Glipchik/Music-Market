namespace AnalyticsService.API.Options;

public class AuthOptions
{
    public string Authority { get; set; } = default!;
    public string[] ValidAudiences { get; set; } = default!;
}