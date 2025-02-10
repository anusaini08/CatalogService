namespace CatalogService.Services
{
    public class ImageService : IImageService
    {
        private readonly IStorageService _storageService;

        public ImageService(IStorageService storageService)
        {
            _storageService = storageService;
        }

        public async Task<string?> UploadImageAsync(IFormFile file)
        {
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            using var stream = file.OpenReadStream();
            return await _storageService.UploadFileAsync(fileName, stream);
        }

        public string GetImageUrl(string fileName)
        {
            return _storageService.GetFileUrl(fileName);
        }
    }
}
