namespace InstrumentService.Business.Models.Request;

public record PianoRequestModel : InstrumentRequestModel
{
    public string ActionType { get; init; } = null!;
    public string CaseWood { get; init; } = null!;
    public int Weight { get; init; }
    public int NumberOfKeys { get; init; }
    public int NumberOfPedals { get; init; }
}