using InstrumentService.DataAccess.Constants;

namespace InstrumentService.DataAccess.Entities;

public class Guitar : Instrument
{
    public Guitar()
    {
        Type = InstrumentTypes.Guitar;
    }

    public int NumberOfStrings { get; set; }
    public string TopWood { get; set; } = null!;
    public string BodyWood { get; set; } = null!;
    public string HandOrientation { get; set; } = null!;
    public string BodyShape { get; set; } = null!;
    public int NutWidth { get; set; }
}