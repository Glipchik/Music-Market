using System.Text.Json;
using System.Text.Json.Serialization;
using InstrumentService.Business.Models;

namespace InstrumentService.IntegrationTests.Utils;

public class PropertyModelJsonConverter : JsonConverter<PropertyModel>
{
    public override PropertyModel Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using var doc = JsonDocument.ParseValue(ref reader);
        var root = doc.RootElement;

        var label = root.GetProperty("label").GetString();
        var valueElement = root.GetProperty("value");

        object value = valueElement.ValueKind switch
        {
            JsonValueKind.String => valueElement.GetString()!,
            JsonValueKind.Number when valueElement.TryGetInt32(out var intVal) => intVal,
            JsonValueKind.Number when valueElement.TryGetDecimal(out var decVal) => decVal,
            JsonValueKind.True => true,
            JsonValueKind.False => false,
            _ => valueElement.ToString()
        };

        return new PropertyModel(label!, value);
    }

    public override void Write(Utf8JsonWriter writer, PropertyModel value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();
        writer.WriteString("label", value.Label);
        writer.WritePropertyName("value");
        JsonSerializer.Serialize(writer, value.Value, options);
        writer.WriteEndObject();
    }
}