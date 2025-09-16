using BE.Domain.Entities;

namespace BE.Application.Interfaces
{
    public interface ISupplierRepository
    {
        Task<Supplier?> GetByIdAsync(Guid id);
        Task<IEnumerable<Supplier>> GetAllAsync();
        Task AddAsync(Supplier supplier);
        void Update(Supplier supplier);
        void Remove(Supplier supplier);
        Task SaveChangesAsync();
    }
}
