using InstrumentService.DataAccess.Constants;

namespace InstrumentService.DataAccess.Entities;

public class Cello : Instrument
{
    public Cello()
    {
        Type = InstrumentTypes.Cello;
    }

    public string Size { get; set; } = null!;
    public string TopWood { get; set; } = null!;
    public string EndpinType { get; set; } = null!;
    public string VarnishType { get; set; } = null!;
}