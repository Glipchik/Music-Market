using MongoDB.Bson.Serialization.Attributes;

namespace InstrumentService.DataAccess.Entities;

public class InstrumentFormMetadata
{
    [BsonId] public string Id { get; set; } = null!;
    public List<FormFieldDescriptor> Fields { get; set; } = [];
}