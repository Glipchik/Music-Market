using InstrumentService.Business.Models;
using InstrumentService.Business.Models.Request;
using InstrumentService.Business.Models.Response;
using InstrumentService.DataAccess.Constants;
using InstrumentService.IntegrationTests.Constants;

namespace InstrumentService.IntegrationTests.Builders;

using System.Collections.Generic;

public class InstrumentResponseModelBuilder
{
    private string _id = "some-default-id";
    private string _name = "Default Instrument";
    private decimal _price = 100.00m;
    private string _manufacturer = "Default Corp";
    private string? _description = "A default description.";
    private List<string> _photoUrls = [];
    private string _type = "generic";
    private List<PropertyModel> _properties = [];
    private string _minioEndpoint = "localhost:9000";

    public InstrumentResponseModelBuilder WithId(string id) => Set(ref _id, id);
    public InstrumentResponseModelBuilder WithName(string name) => Set(ref _name, name);
    public InstrumentResponseModelBuilder WithPrice(decimal price) => Set(ref _price, price);
    public InstrumentResponseModelBuilder WithManufacturer(string manufacturer) => Set(ref _manufacturer, manufacturer);
    public InstrumentResponseModelBuilder WithDescription(string? description) => Set(ref _description, description);
    public InstrumentResponseModelBuilder WithPhotoUrls(List<string> photoUrls) => Set(ref _photoUrls, photoUrls);
    public InstrumentResponseModelBuilder WithType(string type) => Set(ref _type, type);

    public InstrumentResponseModelBuilder WithProperties(List<PropertyModel> properties) =>
        Set(ref _properties, properties);

    public InstrumentResponseModelBuilder WithMinioEndpoint(string minioEndpoint) =>
        Set(ref _minioEndpoint, minioEndpoint);

    public InstrumentResponseModelBuilder AddProperty(string label, object value)
    {
        _properties.Add(new PropertyModel(label, value));
        return this;
    }

    public InstrumentResponseModelBuilder FromGuitarRequest(GuitarRequestModel request)
    {
        WithName(request.Name);
        WithPrice(request.Price);
        WithManufacturer(request.Manufacturer);
        WithDescription(request.Description);
        WithPhotoUrls(request.PhotoNames
            .Select(name => $"http://{_minioEndpoint}/{MinioTestConstants.MinioTestBucket}/{name}").ToList());
        WithType(InstrumentTypes.Guitar);
        _properties.Clear();
        AddProperty("Number of Strings", request.NumberOfStrings);
        AddProperty("Top Wood", request.TopWood);
        AddProperty("Body Wood", request.BodyWood);
        AddProperty("Hand Orientation", request.HandOrientation);
        AddProperty("Body Shape", request.BodyShape);
        AddProperty("Nut Width", request.NutWidth);

        return this;
    }

    public InstrumentResponseModel Build()
    {
        return new InstrumentResponseModel
        {
            Id = _id,
            Name = _name,
            Price = _price,
            Manufacturer = _manufacturer,
            Description = _description,
            PhotoUrls = _photoUrls,
            Type = _type,
            Properties = _properties
        };
    }

    private InstrumentResponseModelBuilder Set<T>(ref T field, T value)
    {
        field = value;
        return this;
    }
}