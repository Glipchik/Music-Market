namespace InstrumentService.DataAccess.Abstractions;

public interface ICloudStorage
{
    Task<string> UploadFileAsync(string fileName, string folder, Stream content, string contentType,
        CancellationToken cancellationToken);

    Task DeleteFileAsync(string fileName, CancellationToken cancellationToken);
    Task DeleteFilesAsync(List<string> fileNames, CancellationToken cancellationToken);
}