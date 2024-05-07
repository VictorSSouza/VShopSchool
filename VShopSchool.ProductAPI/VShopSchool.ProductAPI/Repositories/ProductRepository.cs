using Microsoft.EntityFrameworkCore;
using VShopSchool.ProductAPI.Context;
using VShopSchool.ProductAPI.Interfaces;
using VShopSchool.ProductAPI.Models;

namespace VShopSchool.ProductAPI.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ShopDbContext _context;
        public ProductRepository(ShopDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetAll()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Product> GetById(int id)
        {
            return await _context.Products.Where(c => c.Id == id).FirstOrDefaultAsync();
        }
        public async Task<Product> Create(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<Product> Delete(int id)
        {
            var product = await GetById(id);
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<Product> Update(Product product)
        {
            _context.Entry(product).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return product;
        }
    }
}
