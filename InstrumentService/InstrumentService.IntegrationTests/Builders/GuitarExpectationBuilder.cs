using InstrumentService.Business.Models.Request;
using InstrumentService.DataAccess.Constants;
using InstrumentService.DataAccess.Entities;

namespace InstrumentService.IntegrationTests.Builders;

public class GuitarExpectationBuilder
{
    private readonly Guitar _guitar = new();

    public GuitarExpectationBuilder FromRequest(GuitarRequestModel request)
    {
        _guitar.Name = request.Name;
        _guitar.Price = request.Price;
        _guitar.Manufacturer = request.Manufacturer;
        _guitar.Description = request.Description;
        _guitar.PhotoNames = request.PhotoNames;
        _guitar.Type = InstrumentTypes.Guitar;
        _guitar.NumberOfStrings = request.NumberOfStrings;
        _guitar.TopWood = request.TopWood;
        _guitar.BodyWood = request.BodyWood;
        _guitar.HandOrientation = request.HandOrientation;
        _guitar.BodyShape = request.BodyShape;
        _guitar.NutWidth = request.NutWidth;

        return this;
    }

    public GuitarExpectationBuilder WithId(string id)
    {
        _guitar.Id = id;
        return this;
    }

    public GuitarExpectationBuilder WithOwner(string ownerId)
    {
        _guitar.OwnerId = ownerId;
        return this;
    }

    public Guitar Build() => _guitar;
}