namespace BE.Domain.Dto.SupplierProduct
{

    public class AddSupplierProductDto
    {
        public Guid SupplierId { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public string? LotNumber { get; set; }
    }
}
