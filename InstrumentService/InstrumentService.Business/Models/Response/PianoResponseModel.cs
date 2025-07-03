namespace InstrumentService.Business.Models.Response;

public record PianoResponseModel : InstrumentResponseModel
{
    public int KeysCount { get; init; }
    public string TypeOfAction { get; init; } = null!;
}