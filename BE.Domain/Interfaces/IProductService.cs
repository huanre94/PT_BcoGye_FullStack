using BE.Domain.Entities;

namespace BE.Domain.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllProducts();
        Task<Product> GetProductById(Guid id);
        Task<Product> Add(Product product);
        Task<bool> Update(Product product);
        Task<bool> Delete(Guid id);
    }
}
