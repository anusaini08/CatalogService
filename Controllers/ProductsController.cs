using CatalogService.Models;
using CatalogService.Services;
using Microsoft.AspNetCore.Mvc;

namespace CatalogService.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IImageService _imageService;

        public ProductsController(IProductService productService, IImageService imageService)
        {
            _productService = productService;
            _imageService = imageService;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            return Ok(await _productService.GetProductsAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            return product != null ? Ok(product) : NotFound();
        }

        [HttpPost("Image")]
        public async Task<IActionResult> UploadImage(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No file provided");
            }

            var imageUrl = await _imageService.UploadImageAsync(file);
            return Ok(new { ImageUrl = imageUrl });
        }

        [HttpGet("Images/{imageName}")]
        public IActionResult GetImageUrl(string imageName)
        {
            var imageUrl = _imageService.GetImageUrl(imageName);
            return Ok(new { ImageUrl = imageUrl });
        }


        [HttpPost]
        public async Task<IActionResult> AddProduct([FromBody] ProductDto product)
        {
            var createdProduct = await _productService.AddProductAsync(product);
            return CreatedAtAction(nameof(GetProductById), new { id = createdProduct.Id }, createdProduct);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] ProductDto updatedProduct)
        {
            var result = await _productService.UpdateProductAsync(id, updatedProduct);
            return result ? NoContent() : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var result = await _productService.DeleteProductAsync(id);
            return result ? NoContent() : NotFound();
        }
    }
}
