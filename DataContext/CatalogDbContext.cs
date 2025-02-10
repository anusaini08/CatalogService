using CatalogService.Models;
using Microsoft.EntityFrameworkCore;

namespace CatalogService.DataContext
{
    public class CatalogDbContext : DbContext
    {
        public CatalogDbContext(DbContextOptions<CatalogDbContext> options) : base(options) { }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, Name = "Laptop", Price = 1500, Stock = 10, ImageUrl = "" },
                new Product { Id = 2, Name = "Smartphone", Price = 800, Stock = 20, ImageUrl = "" }
            );
        }
    }
}
