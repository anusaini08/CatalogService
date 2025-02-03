using CatalogService.DataContext;
using CatalogService.Models;

namespace CatalogService.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IS3Service _s3Service;

        public ProductService(IProductRepository productRepository, IS3Service s3Service)
        {
            _productRepository = productRepository;
            _s3Service = s3Service;
        }

        public async Task<IEnumerable<Product>> GetProductsAsync() => await _productRepository.GetProductsAsync();
        public async Task<Product?> GetProductByIdAsync(int id) => await _productRepository.GetProductByIdAsync(id);
        public async Task<Product> AddProductAsync(Product product, IFormFile? file)
        {
            if (file != null)
            {
                // Generate a unique filename (e.g., using GUID)
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                using (var stream = file.OpenReadStream())
                {
                    var uploadedFileName = await _s3Service.UploadFileAsync(fileName, stream);
                    if (uploadedFileName != null)
                    {
                        product.ImageUrl = _s3Service.GetFileUrl(uploadedFileName);
                    }
                }
            }

            return await _productRepository.AddProductAsync(product);
        }
        public Task<bool> UpdateProductAsync(int id, Product updatedProduct) => _productRepository.UpdateProductAsync(id, updatedProduct);
        public Task<bool> DeleteProductAsync(int id) => _productRepository.DeleteProductAsync(id);
    }
}
