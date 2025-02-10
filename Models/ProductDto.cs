﻿namespace CatalogService.Models
{
    public class ProductDto
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public string? ImageUrl {  get; set; } // Add ImageUrl to store S3 URL
    }
}
