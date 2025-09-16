using BE.Domain.Dto.SupplierProduct;

namespace BE.Domain.Interfaces
{
    public interface ISupplierProductService
    {
        Task<SupplierProductDto?> GetByIdAsync(Guid id);
        Task<IEnumerable<SupplierProductDto>> GetByProductIdAsync(Guid productId);
        Task<Guid> CreateAsync(CreateSupplierProductDto dto);
        Task UpdateAsync(Guid id, UpdateSupplierProductDto dto);
        Task DeleteAsync(Guid id);
    }
}
