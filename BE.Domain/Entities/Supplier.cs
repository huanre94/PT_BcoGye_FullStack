namespace BE.Domain.Entities
{
    public class Supplier : AuditoriaBase
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = null!;
        public ICollection<SupplierProduct> SupplierProducts { get; set; } = new List<SupplierProduct>();
    }
}