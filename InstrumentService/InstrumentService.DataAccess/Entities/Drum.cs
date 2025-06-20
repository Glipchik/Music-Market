using InstrumentService.DataAccess.Constants;

namespace InstrumentService.DataAccess.Entities;

public class Drum : Instrument
{
    public Drum()
    {
        Type = InstrumentTypes.Drum;
    }

    public int NumberOfPieces { get; set; }
    public string ShellMaterial { get; set; } = null!;
    public string Configuration { get; set; } = null!;
    public bool CymbalsIncluded { get; set; }
}