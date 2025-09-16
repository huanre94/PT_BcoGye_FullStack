using BE.Domain.Dto.Products;
using BE.Domain.Dto.SupplierProduct;

namespace BE.Domain.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetAllAsync();
        Task<ProductDto> CreateAsync(CreateProductDto dto);
        Task AddSupplierInfoAsync(Guid productId, AddSupplierProductDto dto);
    }
}
