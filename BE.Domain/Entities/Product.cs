namespace BE.Domain.Entities
{
    public class Product
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }

        private readonly List<ProductPrice> _prices;
        public IReadOnlyCollection<ProductPrice> Prices => _prices.AsReadOnly();

        private Product() { }

        public Product(string name)
        {
            Id = Guid.NewGuid();
            Name = name;
            _prices = new List<ProductPrice>();
        }

        public void AddPrice(Guid providerId, decimal price, int stock, string lotNumber)
        {
            var productPrice = new ProductPrice(Id, providerId, price, stock, lotNumber);
            _prices.Add(productPrice);
        }
    }
}


