using BE.Domain.Dto.SupplierProduct;
using BE.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BE.Presentation.Controllers
{
    [ApiController]
    [Route("api/proveedores-productos")]
    public class SupplierProductsController : ControllerBase
    {
        private readonly ISupplierProductService _svc;
        public SupplierProductsController(ISupplierProductService svc) => _svc = svc;

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var item = await _svc.GetByIdAsync(id);
            if (item == null) return NotFound();
            return Ok(item);
        }

        [HttpGet("producto/{productId:guid}")]
        public async Task<IActionResult> GetByProduct(Guid productId)
        {
            var list = await _svc.GetByProductIdAsync(productId);
            return Ok(list);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateSupplierProductDto dto)
        {
            try
            {
                var id = await _svc.CreateAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id }, new { id });
            }
            catch (KeyNotFoundException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateSupplierProductDto dto)
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
