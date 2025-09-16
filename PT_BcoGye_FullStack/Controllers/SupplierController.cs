using BE.Application.Interfaces;
using BE.Domain.Dto.Suppliers;
using BE.Domain.Entities;
using BE.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BE.Presentation.Controllers
{
    [ApiController]
    [Route("api/proveedores")]
    public class SuppliersController : ControllerBase
    {
        private readonly ISupplierService _svc;
        public SuppliersController(ISupplierService svc) => _svc = svc;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var list = await _svc.GetAllAsync();
            return Ok(list);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var supplier = await _svc.GetByIdAsync(id);
            if (supplier == null) return NotFound();
            return Ok(supplier);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateSupplierDto dto)
        {
            var created = await _svc.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] CreateSupplierDto dto)
        {
            try
            {
                await _svc.UpdateAsync(id, dto);
                return NoContent();
            }
            catch (KeyNotFoundException) { return NotFound(); }
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _svc.DeleteAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException) { return NotFound(); }
        }
    }
}
