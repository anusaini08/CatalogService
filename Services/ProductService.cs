using CatalogService.DataContext;
using CatalogService.Models;

namespace CatalogService.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<IEnumerable<Product>> GetProductsAsync() => await _productRepository.GetProductsAsync();
        public async Task<Product?> GetProductByIdAsync(int id) => await _productRepository.GetProductByIdAsync(id);
        public async Task<Product> AddProductAsync(ProductDto product) => await _productRepository.AddProductAsync(product);
        public Task<bool> UpdateProductAsync(int id, ProductDto updatedProduct) => _productRepository.UpdateProductAsync(id, updatedProduct);
        public Task<bool> DeleteProductAsync(int id) => _productRepository.DeleteProductAsync(id);
    }
}
