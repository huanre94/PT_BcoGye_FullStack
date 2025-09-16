using BE.Domain.Dto.Suppliers;

namespace BE.Domain.Interfaces
{
    public interface ISupplierService
    {
        Task<IEnumerable<SupplierDto>> GetAllAsync();
        Task<SupplierDto> CreateAsync(CreateSupplierDto dto);
        Task<SupplierDto?> GetByIdAsync(Guid id);
        Task UpdateAsync(Guid id, CreateSupplierDto dto);
        Task DeleteAsync(Guid id);
    }
}
