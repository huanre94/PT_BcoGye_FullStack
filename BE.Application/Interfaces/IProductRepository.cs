using BE.Domain.Entities;

namespace BE.Application.Interfaces
{
    public interface IProductRepository
    {
        Task<Product?> GetByIdAsync(Guid id);
        Task<IEnumerable<Product>> GetAllAsync();
        Task AddAsync(Product product);
        void Update(Product product);
        void Remove(Product product);
        Task SaveChangesAsync();
    }
}
