using MongoDB.Bson.Serialization.Attributes;

namespace InstrumentService.DataAccess.Entities;

public class InstrumentType
{
    [BsonId] public string Id { get; set; } = null!;
    public string Label { get; set; } = null!;
    public string IconPath { get; set; } = null!;
}