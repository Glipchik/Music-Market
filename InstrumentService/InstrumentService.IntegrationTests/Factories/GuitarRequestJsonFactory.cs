using System.Net.Http.Json;
using InstrumentService.Business.Models.Request;

namespace InstrumentService.IntegrationTests.Factories;

public static class GuitarRequestJsonFactory
{
    public static JsonContent CreateFromGuitarModel(GuitarRequestModel model)
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

        return JsonContent.Create(payload);
    }
}