using InstrumentService.DataAccess.Abstractions;
using InstrumentService.DataAccess.Options;
using Microsoft.Extensions.Options;
using Minio;
using Minio.DataModel.Args;

namespace InstrumentService.DataAccess.Storage;

internal class MinioCloudStorage(IMinioClient minioClient, IOptions<MinioOptions> minioOptions) : ICloudStorage
{
    private readonly MinioOptions _minioOptions = minioOptions.Value;

    public async Task<string> UploadFileAsync(string fileName, Stream content, string contentType,
        CancellationToken cancellationToken)
    {
        var uniqueFileName = $"{Guid.NewGuid()}_{fileName}";

        var args = new PutObjectArgs()
            .WithBucket(_minioOptions.BucketName)
            .WithObject(uniqueFileName)
            .WithStreamData(content)
            .WithObjectSize(content.Length)
            .WithContentType(contentType);

        await minioClient.PutObjectAsync(args, cancellationToken);

        return uniqueFileName;
    }

    public async Task DeleteFileAsync(string fileName, CancellationToken cancellationToken)
    {
        var removeObjectArgs = new RemoveObjectArgs()
            .WithBucket(_minioOptions.BucketName)
            .WithObject(fileName);

        await minioClient.RemoveObjectAsync(removeObjectArgs, cancellationToken);
    }

    public async Task DeleteFilesAsync(List<string> fileNames, CancellationToken cancellationToken)
    {
        var removeObjectsArgs = new RemoveObjectsArgs()
            .WithBucket(_minioOptions.BucketName)
            .WithObjects(fileNames);

        await minioClient.RemoveObjectsAsync(removeObjectsArgs, cancellationToken);
    }
}