namespace BE.Domain.Dto.SupplierProduct
{
    public class UpdateSupplierProductDto
    {
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public string? LotNumber { get; set; }
    }
}
