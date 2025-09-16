using BE.Domain.Dto.Products;
using BE.Domain.Dto.SupplierProduct;
using BE.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BE.Presentation.Controllers
{
    [ApiController]
    //[Authorize]
    [Route("api/products")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _svc;
        public ProductController(IProductService svc) => _svc = svc;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _svc.GetAllAsync();
            return Ok(products);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateProductDto dto)
        {
            var created = await _svc.CreateAsync(dto);
            return CreatedAtAction(nameof(GetAll), new { id = created.Id }, created);
        }

        [HttpPost("{productId:guid}/proveedores")]
        public async Task<IActionResult> AddSupplierInfo(Guid productId, [FromBody] AddSupplierProductDto dto)
        {
            await _svc.AddSupplierInfoAsync(productId, dto);
            return NoContent();
        }
    }
}
