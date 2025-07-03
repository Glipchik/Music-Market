using Microsoft.AspNetCore.Http;

namespace InstrumentService.Business.Abstractions;

public interface IFileService
{
    Task<List<string>> UploadAsync(List<IFormFile> files, string folder, CancellationToken cancellationToken);
}