using BE.Domain.Entities;

namespace BE.Domain.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllProducts();
        Task<Product> GetProductById(Guid id);
        Task Add(Product product);
        Task Update(Product product);
        Task Delete(Guid id);
    }
}
