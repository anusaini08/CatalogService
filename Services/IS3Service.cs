namespace CatalogService.Services
{
    public interface IS3Service
    {
        Task<string> UploadFileAsync(string fileName, Stream fileStream);
        string GetFileUrl(string fileName);
    }
}
