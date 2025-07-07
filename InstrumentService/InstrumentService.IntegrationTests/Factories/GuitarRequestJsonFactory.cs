using System.Text.Json;
using InstrumentService.Business.Models.Request;

namespace InstrumentService.IntegrationTests.Factories;

public static class GuitarRequestJsonFactory
{
    public static string CreateFromGuitarModel(GuitarRequestModel model)
    {
        var payload = new
        {
            Type = "guitar",
            model.Name,
            model.Manufacturer,
            model.Price,
            model.Description,
            model.PhotoNames,
            model.NumberOfStrings,
            model.TopWood,
            model.BodyWood,
            model.HandOrientation,
            model.BodyShape,
            model.NutWidth
        };

        return JsonSerializer.Serialize(payload);
    }
}