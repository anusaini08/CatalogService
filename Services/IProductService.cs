using CatalogService.Models;

namespace CatalogService.Services
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetProductsAsync();
        Task<Product?> GetProductByIdAsync(int id);
        Task<Product> AddProductAsync(ProductDto product);
        Task<bool> UpdateProductAsync(int id, ProductDto updatedProduct);
        Task<bool> DeleteProductAsync(int id);
    }
}
