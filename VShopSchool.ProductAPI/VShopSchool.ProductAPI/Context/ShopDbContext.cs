using Microsoft.EntityFrameworkCore;
using VShopSchool.ProductAPI.Models;

namespace VShopSchool.ProductAPI.Context
{
    public class ShopDbContext : DbContext
    {
        public ShopDbContext(DbContextOptions<ShopDbContext> options) : base(options) { }

        public DbSet<Category>? Categories { get; set; }
        public DbSet<Product>? Products { get; set; }

        protected override void OnModelCreating(ModelBuilder mb)
        {
            // category
            mb.Entity<Category>().HasKey(c => c.CategoryId);
            mb.Entity<Category>().
                 Property(c => c.Name).
                 HasMaxLength(100).
                 IsRequired();

            // product
            mb.Entity<Product>().
                 Property(c => c.Name).
                 HasMaxLength(100).
                    IsRequired();
            mb.Entity<Product>().
                 Property(c => c.Price).
                 HasPrecision(12, 2);
            mb.Entity<Product>().
                 Property(c => c.Description).
                 HasMaxLength(255).
                    IsRequired();
            mb.Entity<Product>().
                 Property(c => c.ImageUrl).
                 HasMaxLength(255).
                    IsRequired();

            // Product per Category list
            mb.Entity<Category>().
                 HasMany(l => l.Products).
                 WithOne(c => c.Category).
                     IsRequired().
                     OnDelete(DeleteBehavior.Cascade);

            // first Data
            mb.Entity<Category>().HasData(
                new Category
                {
                    CategoryId = 1,
                    Name = "Cadernos"
                },
                new Category
                {
                    CategoryId = 2,
                    Name = "Acessórios"
                }
            );
        }

    }
}
