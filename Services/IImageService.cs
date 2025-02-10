namespace CatalogService.Services
{
    public interface IImageService
    {
        Task<string?> UploadImageAsync(IFormFile file);
        string GetImageUrl(string fileName);
    }
}
