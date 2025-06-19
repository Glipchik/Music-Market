namespace InstrumentService.DataAccess.Options;

public class MinioOptions
{
    public string Host { get; set; } = null!;
    public string Endpoint { get; set; } = null!;
    public string BucketName { get; set; } = null!;
    public string AccessKey { get; set; } = null!;
    public string SecretKey { get; set; } = null!;
}