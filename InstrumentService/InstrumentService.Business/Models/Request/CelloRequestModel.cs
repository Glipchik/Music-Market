namespace InstrumentService.Business.Models.Request;

public record CelloRequestModel : InstrumentRequestModel
{
    public string Size { get; init; } = null!;
    public string TopWood { get; init; } = null!;
    public string EndpinType { get; init; } = null!;
    public string VarnishType { get; init; } = null!;
}