using BE.Application.Interfaces;
using BE.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BE.Infrastructure.Database
{
    public class SupplierProductRepository : ISupplierProductRepository
    {
        private readonly BEContext _db;
        public SupplierProductRepository(BEContext db) => _db = db;

        public async Task AddAsync(SupplierProduct sp) => await _db.SupplierProducts.AddAsync(sp);

        public async Task<IEnumerable<SupplierProduct>> GetByProductIdAsync(Guid productId)
        {
            return await _db.SupplierProducts
                .Where(x => x.ProductId == productId)
                .Include(x => x.Supplier)
                .Include(x => x.Product)
                .ToListAsync();
        }

        public async Task<SupplierProduct?> GetByIdAsync(Guid id)
        {
            return await _db.SupplierProducts
                .Include(x => x.Supplier)
                .Include(x => x.Product)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public void Remove(SupplierProduct sp) => _db.SupplierProducts.Remove(sp);

        public void Update(SupplierProduct sp) => _db.SupplierProducts.Update(sp);

        public Task SaveChangesAsync() => _db.SaveChangesAsync();
    }
}
