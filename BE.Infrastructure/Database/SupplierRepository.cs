using BE.Application.Interfaces;
using BE.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BE.Infrastructure.Database
{
    public class SupplierRepository : ISupplierRepository
    {
        private readonly BEContext _db;
        public SupplierRepository(BEContext db) => _db = db;

        public async Task AddAsync(Supplier supplier) => await _db.Suppliers.AddAsync(supplier);

        public async Task<IEnumerable<Supplier>> GetAllAsync()
        {
            return await _db.Suppliers
                .Include(s => s.SupplierProducts)
                    .ThenInclude(sp => sp.Product)
                .ToListAsync();
        }

        public async Task<Supplier?> GetByIdAsync(Guid id)
        {
            return await _db.Suppliers
                .Include(s => s.SupplierProducts)
                    .ThenInclude(sp => sp.Product)
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public void Remove(Supplier supplier) => _db.Suppliers.Remove(supplier);

        public void Update(Supplier supplier) => _db.Suppliers.Update(supplier);

        public Task SaveChangesAsync() => _db.SaveChangesAsync();
    }
}
