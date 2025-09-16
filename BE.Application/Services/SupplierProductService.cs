using BE.Application.Interfaces;
using BE.Domain.Dto.SupplierProduct;
using BE.Domain.Entities;
using BE.Domain.Interfaces;

namespace BE.Application.Services
{
    public class SupplierProductService : ISupplierProductService
    {
        private readonly ISupplierProductRepository _spRepo;
        private readonly IProductRepository _productRepo;
        private readonly ISupplierRepository _supplierRepo;

        public SupplierProductService(ISupplierProductRepository spRepo, IProductRepository productRepo, ISupplierRepository supplierRepo)
        {
            _spRepo = spRepo;
            _productRepo = productRepo;
            _supplierRepo = supplierRepo;
        }

        public async Task<Guid> CreateAsync(CreateSupplierProductDto dto)
        {
            // validaciones básicas
            var product = await _productRepo.GetByIdAsync(dto.ProductId) ?? throw new KeyNotFoundException("Producto no encontrado");
            var supplier = await _supplierRepo.GetByIdAsync(dto.SupplierId) ?? throw new KeyNotFoundException("Proveedor no encontrado");

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

            return sp.Id;
        }

        public async Task DeleteAsync(Guid id)
        {
            var sp = await _spRepo.GetByIdAsync(id) ?? throw new KeyNotFoundException("Registro proveedor-producto no encontrado");
            _spRepo.Remove(sp);
            await _spRepo.SaveChangesAsync();
        }

        public async Task<SupplierProductDto?> GetByIdAsync(Guid id)
        {
            var sp = await _spRepo.GetByIdAsync(id);
            if (sp == null) return null;

            return new SupplierProductDto
            {
                SupplierId = sp.SupplierId,
                SupplierName = sp.Supplier.Name,
                Price = sp.Price,
                Stock = sp.Stock,
                LotNumber = sp.LotNumber
            };
        }

        public async Task<IEnumerable<SupplierProductDto>> GetByProductIdAsync(Guid productId)
        {
            var list = await _spRepo.GetByProductIdAsync(productId);
            return list.Select(sp => new SupplierProductDto
            {
                SupplierId = sp.SupplierId,
                SupplierName = sp.Supplier.Name,
                Price = sp.Price,
                Stock = sp.Stock,
                LotNumber = sp.LotNumber
            });
        }

        public async Task UpdateAsync(Guid id, UpdateSupplierProductDto dto)
        {
            var sp = await _spRepo.GetByIdAsync(id) ?? throw new KeyNotFoundException("Registro proveedor-producto no encontrado");
            sp.Price = dto.Price;
            sp.Stock = dto.Stock;
            sp.LotNumber = dto.LotNumber;
            _spRepo.Update(sp);
            await _spRepo.SaveChangesAsync();
        }
    }
}
