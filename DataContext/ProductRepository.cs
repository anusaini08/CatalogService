using CatalogService.Models;
using Microsoft.EntityFrameworkCore;

namespace CatalogService.DataContext
{
    public class ProductRepository : IProductRepository
    {
        private readonly CatalogDbContext _dbContext;

        public ProductRepository(CatalogDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            return await _dbContext.Products.ToListAsync();
        }

        public async Task<Product?> GetProductByIdAsync(int id)
        {
            return await _dbContext.Products.FindAsync(id);
        }

        public async Task<Product> AddProductAsync(ProductDto productDto)
        {
            var product = new Product
            {
                Name = productDto.Name,
                Description = productDto.Description,
                Price = productDto.Price,
                Stock = productDto.Stock,
                ImageUrl = productDto.ImageUrl // Set if available
            };

            _dbContext.Products.Add(product);
            await _dbContext.SaveChangesAsync();
            return product;
        }

        public async Task<bool> UpdateProductAsync(int id, ProductDto updatedProduct)
        {
            var product = await _dbContext.Products.FindAsync(id);
            if (product == null) return false;

            product.Name = updatedProduct.Name;
            product.Price = updatedProduct.Price;
            product.Stock = updatedProduct.Stock;
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            var product = await _dbContext.Products.FindAsync(id);
            if (product == null) return false;

            _dbContext.Products.Remove(product);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
