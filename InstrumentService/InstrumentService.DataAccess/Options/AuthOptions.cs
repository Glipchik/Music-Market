namespace InstrumentService.DataAccess.Options;

public class AuthOptions
{
    public string Authority { get; set; } = null!;
    public string ValidAudience { get; set; } = null!;
}