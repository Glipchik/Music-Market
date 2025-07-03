namespace InstrumentService.Business.Models.Request;

public record GuitarRequestModel : InstrumentRequestModel
{
    public int NumberOfStrings { get; init; }
    public string TopWood { get; init; } = null!;
    public string BodyWood { get; init; } = null!;
    public string HandOrientation { get; init; } = null!;
    public string BodyShape { get; init; } = null!;
    public int NutWidth { get; init; }
}