using BE.Domain.Entities;

namespace BE.Application.Interfaces
{
    public interface ISupplierProductRepository
    {
        Task<SupplierProduct?> GetByIdAsync(Guid id);
        Task<IEnumerable<SupplierProduct>> GetByProductIdAsync(Guid productId);
        Task AddAsync(SupplierProduct sp);
        void Update(SupplierProduct sp);
        void Remove(SupplierProduct sp);
        Task SaveChangesAsync();
    }
}
