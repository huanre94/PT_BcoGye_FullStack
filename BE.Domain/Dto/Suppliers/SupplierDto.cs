using BE.Domain.Dto.SupplierProduct;

namespace BE.Domain.Dto.Suppliers
{
    public class SupplierDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public List<SupplierProductDto> Products { get; set; } = new();
    }
}
