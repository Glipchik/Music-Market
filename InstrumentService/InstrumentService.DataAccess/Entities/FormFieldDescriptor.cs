namespace InstrumentService.DataAccess.Entities;

public class FormFieldDescriptor
{
    public string Name { get; set; } = null!;
    public string Label { get; set; } = null!;
    public string Type { get; set; } = null!;
    public bool IsRequired { get; set; }
    public object? DefaultValue { get; set; }
    public List<string>? Options { get; set; }
    public string? Placeholder { get; set; }
}