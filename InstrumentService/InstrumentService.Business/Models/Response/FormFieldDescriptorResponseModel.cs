namespace InstrumentService.Business.Models.Response;

public record FormFieldDescriptorResponseModel(
    string Name,
    string Label,
    string Type,
    bool IsRequired,
    object? DefaultValue,
    List<string>? Options,
    string? Placeholder);