using CatalogService.Models;
using Microsoft.EntityFrameworkCore;

namespace CatalogService.DataContext
{
    public class CatalogDbContext : DbContext
    {
        public CatalogDbContext(DbContextOptions<CatalogDbContext> options) : base(options) { }
        public DbSet<Product> Products { get; set; }
    }
}
