namespace BE.Domain.Entities
{
    public class ProductPrice
    {
        public Guid ProductId { get; private set; }
        public Guid ProviderId { get; private set; }
        public decimal Price { get; private set; }
        public int Stock { get; private set; }
        public string LotNumber { get; private set; }

        private ProductPrice() { }

        public ProductPrice(Guid productId, Guid providerId, decimal price, int stock, string lotNumber)
        {
            ProductId = productId;
            ProviderId = providerId;
            Price = price;
            Stock = stock;
            LotNumber = lotNumber;
        }
    }
}


