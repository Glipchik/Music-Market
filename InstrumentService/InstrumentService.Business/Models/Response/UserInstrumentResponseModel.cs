namespace InstrumentService.Business.Models.Response;

public record UserInstrumentResponseModel
{
    public string Id { get; init; } = null!;
    public string Name { get; init; } = null!;
    public decimal Price { get; init; }
    public string Manufacturer { get; init; } = null!;
    public string? Description { get; set; }
    public List<string> PhotoUrls { get; set; } = [];
    public string Type { get; init; } = null!;
    public List<PropertyModel> Properties { get; init; } = [];
    public InstrumentStatResponseModel TotalStats { get; init; } = null!;
    public List<InstrumentDailyStatResponseModel> DailyStats { get; init; } = null!;
};