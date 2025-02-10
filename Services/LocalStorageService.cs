namespace CatalogService.Services
{
    public class LocalStorageService : IStorageService
    {
        private readonly string _localPath;

        public LocalStorageService(IConfiguration configuration)
        {
            _localPath = configuration["Storage:LocalPath"] ?? throw new Exception("Local storage path not configured");
        }

        public async Task<string?> UploadFileAsync(string fileName, Stream fileStream)
        {
            var filePath = Path.Combine(_localPath, fileName);

            // Ensure directory exists
            Directory.CreateDirectory(_localPath);

            using (var fileStreamOutput = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                await fileStream.CopyToAsync(fileStreamOutput);
            return filePath;
        }

        public string GetFileUrl(string fileName)
        {
            return $"{_localPath}\\{fileName}"; // Return local URL
        }
    }
}
