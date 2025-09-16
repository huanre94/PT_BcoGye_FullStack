namespace BE.Domain.Entities
{
    public class SupplierProduct : AuditoriaBase
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid ProductId { get; set; }
        public Product Product { get; set; } = null!;

        public Guid SupplierId { get; set; }
        public Supplier Supplier { get; set; } = null!;

        public decimal Price { get; set; }
        public int Stock { get; set; }

        public string? LotNumber { get; set; }
    }
}
