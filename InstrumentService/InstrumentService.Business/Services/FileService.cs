using InstrumentService.Business.Abstractions;
using InstrumentService.DataAccess.Abstractions;
using Microsoft.AspNetCore.Http;

namespace InstrumentService.Business.Services;

public class FileService(ICloudStorage cloudStorage) : IFileService
{
    public async Task<List<string>> UploadAsync(List<IFormFile> files, string folder, CancellationToken cancellationToken)
    {
        var semaphore = new SemaphoreSlim(5);
        var uploadTasks = new List<Task<string>>();

        foreach (var file in files)
        {
            await semaphore.WaitAsync(cancellationToken);

            var task = Task.Run(async () =>
            {
                try
                {
                    await using var stream = file.OpenReadStream();
                    var fileName = await cloudStorage
                        .UploadFileAsync(file.FileName, folder, stream, file.ContentType, cancellationToken);
                    return fileName;
                }
                finally
                {
                    semaphore.Release();
                }
            }, cancellationToken);

            uploadTasks.Add(task);
        }

        var fileNames = await Task.WhenAll(uploadTasks);
        return fileNames.ToList();
    }
}