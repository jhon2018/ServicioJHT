using Amazon.S3;
using Amazon.S3.Model;
using JHT.Logistics.Application.Interfaces.Services;
using Microsoft.Extensions.Configuration;

namespace JHT.Logistics.Infrastructure.Services;

public class MinioStorageService : IStorageService
{
    private readonly IAmazonS3 _s3Client;
    private readonly string _bucketName;

    public MinioStorageService(IConfiguration configuration)
    {
        var accessKey = configuration["Minio:AccessKey"] ?? throw new ArgumentNullException("Minio:AccessKey no configurado.");
        var secretKey = configuration["Minio:SecretKey"] ?? throw new ArgumentNullException("Minio:SecretKey no configurado.");
        var serviceUrl = configuration["Minio:ServiceUrl"] ?? throw new ArgumentNullException("Minio:ServiceUrl no configurado.");
        _bucketName = configuration["Minio:BucketName"] ?? "jht-logistics-docs";

        var config = new AmazonS3Config
        {
            ServiceURL = serviceUrl,
            ForcePathStyle = true // Requerido para MinIO
        };

        _s3Client = new AmazonS3Client(accessKey, secretKey, config);
    }

    public async Task<string> UploadFileAsync(Stream fileStream, string fileName, string contentType, CancellationToken cancellationToken = default)
    {
        // Asegurar que el bucket exista
        var bucketExists = await Amazon.S3.Util.AmazonS3Util.DoesS3BucketExistV2Async(_s3Client, _bucketName);
        if (!bucketExists)
        {
            await _s3Client.PutBucketAsync(new PutBucketRequest { BucketName = _bucketName });
        }

        var uniqueFileName = $"{Guid.NewGuid()}_{fileName}";
        var objectKey = $"documentos/{DateTime.UtcNow.Year}/{DateTime.UtcNow.Month:D2}/{uniqueFileName}";

        var request = new PutObjectRequest
        {
            BucketName = _bucketName,
            Key = objectKey,
            InputStream = fileStream,
            ContentType = contentType
        };

        await _s3Client.PutObjectAsync(request, cancellationToken);

        return objectKey; // Retornamos el key para almacenarlo en DB
    }

    public async Task<Stream> GetFileAsync(string fileUrl, CancellationToken cancellationToken = default)
    {
        var request = new GetObjectRequest
        {
            BucketName = _bucketName,
            Key = fileUrl
        };

        var response = await _s3Client.GetObjectAsync(request, cancellationToken);
        return response.ResponseStream;
    }

    public async Task<bool> DeleteFileAsync(string fileUrl, CancellationToken cancellationToken = default)
    {
        var request = new DeleteObjectRequest
        {
            BucketName = _bucketName,
            Key = fileUrl
        };

        var response = await _s3Client.DeleteObjectAsync(request, cancellationToken);
        return response.HttpStatusCode == System.Net.HttpStatusCode.NoContent;
    }
}
