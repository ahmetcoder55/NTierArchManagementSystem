using Business.Abstract.UnitOfWorks;
using Entities.Concrete.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.ActionFilters;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IServiceManager _services;

        public ProductsController(IServiceManager services)
        {
            _services = services;
        }
        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllProductsAsync()
        {
            return Ok(
                await _services.ProductService.GetAllAsync()
                );
        }
        [HttpPost("create-one")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]

        public async Task<IActionResult> CreateProductAsync(ProductCreateDto productCreateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _services.ProductService.CreateAsync(productCreateDto);
            return Ok(new
            {
                ProductName = productCreateDto.Name,
                ProductDescription = productCreateDto.Description,
            });
        }

        [HttpPut("update-one")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> UpdateProductAsync(ProductUpdateDto productUpdateDto){

            await _services.ProductService.UpdateAsync(productUpdateDto);
            return NoContent();
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetByProductIdAsync([FromRoute(Name ="id")]int id)
        {
            return Ok(
                await _services.ProductService.GetByIdAsync(id)
                );
        }
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteOneProductAsync([FromRoute(Name ="id")]int id)
        {
            await _services.ProductService.DeleteAsync(id);
            return NoContent();
        }

    }
}
