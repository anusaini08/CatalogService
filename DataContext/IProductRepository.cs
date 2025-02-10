using CatalogService.Models;

namespace CatalogService.DataContext
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetProductsAsync();
        Task<Product?> GetProductByIdAsync(int id);
        Task<Product> AddProductAsync(ProductDto product);
        Task<bool> UpdateProductAsync(int id, ProductDto updatedProduct);
        Task<bool> DeleteProductAsync(int id);
    }
}
