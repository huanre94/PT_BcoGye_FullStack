namespace BE.Domain.Dto.SupplierProduct
{
    public class SupplierProductDto
    {
        public Guid SupplierId { get; set; }
        public string SupplierName { get; set; } = null!;
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public string? LotNumber { get; set; }
    }
}
