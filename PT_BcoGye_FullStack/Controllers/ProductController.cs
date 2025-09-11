using BE.Domain.Entities;
using BE.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BE.Presentation.Controllers
{
    [ApiController]
    //[Authorize]
    [Route("api/products")]
    public class ProductController : ControllerBase
    {
        readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var response = await _productService.GetAllProducts();

            if (!response.Any()) return NotFound(new { Message = "No hay productos disponibles." });

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(Guid id)
        {
            var response = await _productService.GetProductById(id);

            return Ok(new { Message = $"Obteniendo producto con ID: {id}" });
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct([FromBody] Product product)
        {
            var response = await _productService.Add(product);

            if (response == null) return BadRequest(new { Message = "No se pudo crear el producto" });

            return CreatedAtAction(nameof(GetProductById), new { id = response.Id }, response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(Guid id, [FromBody] Product product)
        {
            var response = await _productService.Update(product);

            if (!response) return BadRequest(new { Message = "No se pudo actualizar el producto" });

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            var response = await _productService.Delete(id);

            if (!response) return BadRequest(new { Message = "No se pudo eliminar el producto" });

            return NoContent();
        }
    }
}
