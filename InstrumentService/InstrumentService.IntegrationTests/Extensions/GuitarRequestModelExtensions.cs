using System.Text;
using InstrumentService.Business.Models.Request;
using InstrumentService.IntegrationTests.Factories;

namespace InstrumentService.IntegrationTests.Extensions;

public static class GuitarRequestModelExtensions
{
    public static HttpContent ToHttpContent(this GuitarRequestModel model)
    {
        var json = GuitarRequestJsonFactory.CreateFromGuitarModel(model);
        return new StringContent(json, Encoding.UTF8, "application/json");
    }
}