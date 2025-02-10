namespace CatalogService.Services
{
    public interface IStorageService
    {
        Task<string?> UploadFileAsync(string fileName, Stream fileStream);
        string GetFileUrl(string fileName);
    }
}
