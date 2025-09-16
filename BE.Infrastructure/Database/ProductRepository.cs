using BE.Application.Interfaces;
using BE.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BE.Infrastructure.Database
{
    public class ProductRepository : IProductRepository
    {
        readonly BEContext _context;

        public ProductRepository(BEContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Product product) => await _context.Products.AddAsync(product);

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _context.Products
                .Include(p => p.SupplierProducts)
                    .ThenInclude(sp => sp.Supplier)
                .ToListAsync();
        }

        public async Task<Product?> GetByIdAsync(Guid id)
        {
            return await _context.Products
                .Include(p => p.SupplierProducts)
                    .ThenInclude(sp => sp.Supplier)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public void Remove(Product product) => _context.Products.Remove(product);
        public void Update(Product product) => _context.Products.Update(product);
        public Task SaveChangesAsync() => _context.SaveChangesAsync();
    }
}
