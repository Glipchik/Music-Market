using InstrumentService.DataAccess.Constants;

namespace InstrumentService.DataAccess.Entities;

public class Piano : Instrument
{
    public Piano()
    {
        Type = InstrumentTypes.Piano;
    }

    public string ActionType { get; set; } = null!;
    public string CaseWood { get; set; } = null!;
    public int Weight { get; set; }
    public int NumberOfKeys { get; set; }
    public int NumberOfPedals { get; set; }
}