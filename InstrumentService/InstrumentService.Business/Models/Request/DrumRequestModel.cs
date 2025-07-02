namespace InstrumentService.Business.Models.Request;

public record DrumRequestModel : InstrumentRequestModel
{
    public int NumberOfPieces { get; init; }
    public string ShellMaterial { get; init; } = null!;
    public string Configuration { get; init; } = null!;
    public bool CymbalsIncluded { get; init; }
}