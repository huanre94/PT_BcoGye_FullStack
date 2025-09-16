

using BE.Application.Interfaces;
using BE.Domain.Dto.Products;
using BE.Domain.Dto.SupplierProduct;
using BE.Domain.Entities;
using BE.Domain.Interfaces;

namespace BE.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepo;
        private readonly ISupplierRepository _supplierRepo;
        private readonly ISupplierProductRepository _spRepo;

        public ProductService(IProductRepository productRepo,
                              ISupplierRepository supplierRepo,
                              ISupplierProductRepository spRepo)
        {
            _productRepo = productRepo;
            _supplierRepo = supplierRepo;
            _spRepo = spRepo;
        }

        public async Task<ProductDto> CreateAsync(CreateProductDto dto)
        {
            var product = new Product { Name = dto.Name };
            await _productRepo.AddAsync(product);
            await _productRepo.SaveChangesAsync();

            return new ProductDto { Id = product.Id, Name = product.Name };
        }

        public async Task<IEnumerable<ProductDto>> GetAllAsync()
        {
            var products = await _productRepo.GetAllAsync();
            var result = products.Select(p => new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Suppliers = p.SupplierProducts.Select(sp => new SupplierProductDto
                {
                    SupplierId = sp.SupplierId,
                    SupplierName = sp.Supplier.Name,
                    Price = sp.Price,
                    Stock = sp.Stock,
                    LotNumber = sp.LotNumber
                }).ToList()
            });
            return result;
        }

        public async Task AddSupplierInfoAsync(Guid productId, AddSupplierProductDto dto)
        {
            var product = await _productRepo.GetByIdAsync(productId) ?? throw new Exception("Product not found");
            var supplier = await _supplierRepo.GetByIdAsync(dto.SupplierId) ?? throw new Exception("Supplier not found");

            var sp = new SupplierProduct
            {
                ProductId = product.Id,
                SupplierId = supplier.Id,
                Price = dto.Price,
                Stock = dto.Stock,
                LotNumber = dto.LotNumber
            };

            await _spRepo.AddAsync(sp);
            await _spRepo.SaveChangesAsync();
        }
    }
}
