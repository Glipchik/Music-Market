namespace InstrumentService.Business.Models.Response;

public record InstrumentResponseModel
{
    public string Id { get; init; } = null!;
    public string Name { get; init; } = null!;
    public decimal Price { get; init; }
    public string Manufacturer { get; init; } = null!;
    public string? Description { get; init; }
    public List<string> PhotoUrls { get; init; } = [];
    public string Type { get; init; } = null!;
    public List<PropertyModel> Properties { get; init; } = [];
}