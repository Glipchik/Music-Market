using InstrumentService.Business.Models.Request;

namespace InstrumentService.IntegrationTests.Builders;

public class GuitarRequestModelBuilder
{
    private string _name = "Ibanez RG";
    private string _manufacturer = "Ibanez";
    private decimal _price = 999.99m;
    private string _description = "Cool guitar";
    private List<string> _photoNames = ["1.jpg", "2.jpg"];
    private int _numberOfStrings = 6;
    private string _topWood = "Maple";
    private string _bodyWood = "Mahogany";
    private string _handOrientation = "Right";
    private string _bodyShape = "Strat";
    private int _nutWidth = 42;

    public GuitarRequestModelBuilder WithName(string name) => Set(ref _name, name);
    public GuitarRequestModelBuilder WithManufacturer(string manufacturer) => Set(ref _manufacturer, manufacturer);
    public GuitarRequestModelBuilder WithPrice(decimal price) => Set(ref _price, price);
    public GuitarRequestModelBuilder WithDescription(string description) => Set(ref _description, description);
    public GuitarRequestModelBuilder WithPhotoNames(List<string> photoNames) => Set(ref _photoNames, photoNames);
    public GuitarRequestModelBuilder WithNumberOfStrings(int value) => Set(ref _numberOfStrings, value);
    public GuitarRequestModelBuilder WithTopWood(string value) => Set(ref _topWood, value);
    public GuitarRequestModelBuilder WithBodyWood(string value) => Set(ref _bodyWood, value);
    public GuitarRequestModelBuilder WithHandOrientation(string value) => Set(ref _handOrientation, value);
    public GuitarRequestModelBuilder WithBodyShape(string value) => Set(ref _bodyShape, value);
    public GuitarRequestModelBuilder WithNutWidth(int value) => Set(ref _nutWidth, value);

    public GuitarRequestModel Build() => new()
    {
        Name = _name,
        Manufacturer = _manufacturer,
        Price = _price,
        Description = _description,
        PhotoNames = _photoNames,
        NumberOfStrings = _numberOfStrings,
        TopWood = _topWood,
        BodyWood = _bodyWood,
        HandOrientation = _handOrientation,
        BodyShape = _bodyShape,
        NutWidth = _nutWidth
    };

    private GuitarRequestModelBuilder Set<T>(ref T field, T value)
    {
        field = value;
        return this;
    }
}