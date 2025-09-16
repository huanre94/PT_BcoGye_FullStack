using BE.Domain.Dto.SupplierProduct;

namespace BE.Domain.Dto.Products
{
    public class ProductDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public List<SupplierProductDto> Suppliers { get; set; } = new();
    }
}
