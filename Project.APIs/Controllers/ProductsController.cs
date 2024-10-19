using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.Core.Services.Contract;

namespace Project.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllProducts([FromQuery] string? sort , [FromQuery] int? brandId, [FromQuery] int? typeId)
        {
            var result = await _productService.GetAllProductSAsync(sort , brandId,typeId);
            return Ok(result);
        }

        [HttpGet("brand")]
        public async Task<IActionResult> GetAllBrands()
        {
            var result = await _productService.GetAllBrandsAsync();
            return Ok(result);
        }

        [HttpGet("type")]
        public async Task<IActionResult> GetAllTypes()
        {
            var result = await _productService.GetAllTypesAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int? id)
        {
            if(id is null) return BadRequest("Invaled Id !!");
            var result = await _productService.GetProductById(id.Value);
            if(result is null) return NotFound($"The Product With Id :{id} Not Found At DB =( ");
            return Ok(result);
        }
    }
}
