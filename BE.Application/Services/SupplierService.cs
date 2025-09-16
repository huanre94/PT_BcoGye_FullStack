using BE.Application.Interfaces;
using BE.Domain.Dto.SupplierProduct;
using BE.Domain.Dto.Suppliers;
using BE.Domain.Entities;
using BE.Domain.Interfaces;

namespace BE.Application.Services
{
    public class SupplierService : ISupplierService
    {
        private readonly ISupplierRepository _supplierRepo;
        public SupplierService(ISupplierRepository supplierRepo)
        {
            _supplierRepo = supplierRepo;
        }

        public async Task<SupplierDto> CreateAsync(CreateSupplierDto dto)
        {
            var supplier = new Supplier { Name = dto.Name };
            await _supplierRepo.AddAsync(supplier);
            await _supplierRepo.SaveChangesAsync();

            return new SupplierDto { Id = supplier.Id, Name = supplier.Name };
        }

        public async Task DeleteAsync(Guid id)
        {
            var supplier = await _supplierRepo.GetByIdAsync(id)
                ?? throw new KeyNotFoundException("Proveedor no encontrado");

            _supplierRepo.Remove(supplier);
            await _supplierRepo.SaveChangesAsync();
        }

        public async Task<IEnumerable<SupplierDto>> GetAllAsync()
        {
            var suppliers = await _supplierRepo.GetAllAsync();
            return suppliers.Select(s => new SupplierDto
            {
                Id = s.Id,
                Name = s.Name,
                Products = s.SupplierProducts.Select(sp => new SupplierProductDto
                {
                    SupplierId = s.Id,
                    SupplierName = s.Name,
                    Price = sp.Price,
                    Stock = sp.Stock,
                    LotNumber = sp.LotNumber,
                    // optionally product info
                }).ToList()
            });
        }

        public async Task<SupplierDto?> GetByIdAsync(Guid id)
        {
            var s = await _supplierRepo.GetByIdAsync(id);
            if (s == null) return null;

            return new SupplierDto
            {
                Id = s.Id,
                Name = s.Name,
                Products = s.SupplierProducts.Select(sp => new SupplierProductDto
                {
                    SupplierId = s.Id,
                    SupplierName = s.Name,
                    Price = sp.Price,
                    Stock = sp.Stock,
                    LotNumber = sp.LotNumber
                }).ToList()
            };
        }

        public async Task UpdateAsync(Guid id, CreateSupplierDto dto)
        {
            var supplier = await _supplierRepo.GetByIdAsync(id)
                ?? throw new KeyNotFoundException("Proveedor no encontrado");

            supplier.Name = dto.Name;
            _supplierRepo.Update(supplier);
            await _supplierRepo.SaveChangesAsync();
        }
    }
}
