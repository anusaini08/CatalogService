using Amazon.S3;
using Amazon.S3.Model;

namespace CatalogService.Services
{
    public class S3StorageService : IStorageService
    {
        private readonly IAmazonS3 _s3Client;
        private readonly string _bucketName;

        public S3StorageService(IAmazonS3 s3Client, IConfiguration configuration)
        {
            _s3Client = s3Client;
            _bucketName = configuration["AWS:BucketName"];
        }
        public async Task<string> UploadFileAsync(string fileName, Stream fileStream)
        {
            var putRequest = new PutObjectRequest
            {
                BucketName = _bucketName,
                Key = fileName, // You can use GUID to make the filename unique
                InputStream = fileStream,
                ContentType = "image/jpeg" // Adjust content type if necessary
            };

            var response = await _s3Client.PutObjectAsync(putRequest);
            if (response.HttpStatusCode == System.Net.HttpStatusCode.OK)
            {
                return fileName; // Return the file name to store in DB
            }

            return null;
        }

        public string GetFileUrl(string fileName)
        {
            return $"https://{_bucketName}.s3.amazonaws.com/{fileName}";
        }
    }
}
