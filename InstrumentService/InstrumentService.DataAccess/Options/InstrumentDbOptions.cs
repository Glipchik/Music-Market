namespace InstrumentService.DataAccess.Options;

public class InstrumentDbOptions
{
    public string ConnectionString { get; set; } = null!;
    public string DatabaseName { get; set; } = null!;
    public string InstrumentsCollectionName { get; set; } = null!;
    public string InstrumentFormMetadataCollectionName { get; set; } = null!;
    public string InstrumentTypesCollectionName { get; set; } = null!;
}