using Business.Abstract.UnitOfWorks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
    }
}
