using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace InstrumentService.DataAccess.Entities;

[BsonKnownTypes(typeof(Cello), typeof(Piano), typeof(Guitar), typeof(Drum))]
public abstract class Instrument
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;
    public decimal Price { get; set; }
    public string Manufacturer { get; set; } = null!;
    public string? Description { get; set; }
    public List<string> PhotoNames { get; set; } = [];
    public string Type { get; set; } = null!;
    public string OwnerId { get; set; } = null!;
}