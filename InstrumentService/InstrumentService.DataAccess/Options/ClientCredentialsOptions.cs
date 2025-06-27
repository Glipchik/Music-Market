namespace InstrumentService.DataAccess.Options;

public class ClientCredentialsOptions
{
    public string ClientId { get; set; } = default!;
    public string ClientSecret { get; set; } = default!;
    public string Scope { get; set; } = default!;
}