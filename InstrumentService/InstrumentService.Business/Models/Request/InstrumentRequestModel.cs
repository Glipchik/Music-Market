using System.Text.Json.Serialization;
using InstrumentService.DataAccess.Constants;

namespace InstrumentService.Business.Models.Request;

[JsonPolymorphic(TypeDiscriminatorPropertyName = "Type")]
[JsonDerivedType(typeof(GuitarRequestModel), InstrumentTypes.Guitar)]
[JsonDerivedType(typeof(PianoRequestModel), InstrumentTypes.Piano)]
[JsonDerivedType(typeof(CelloRequestModel), InstrumentTypes.Cello)]
[JsonDerivedType(typeof(DrumRequestModel), InstrumentTypes.Drum)]
public abstract record InstrumentRequestModel
{
    public string Name { get; init; } = null!;
    public decimal Price { get; init; }
    public string Manufacturer { get; init; } = null!;
    public string? Description { get; set; }
    public List<string> PhotoNames { get; init; } = [];
}