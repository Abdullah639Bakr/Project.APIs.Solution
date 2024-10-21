using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.APIs.Errors;
using Project.Core.Dtos.Products;
using Project.Core.Helper;
using Project.Core.Services.Contract;
using Project.Core.Specifications.Products;

namespace Project.APIs.Controllers
{
   
    public class ProductsController : BaseApiController
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [ProducesResponseType(typeof(PaginationResponse<ProductDto>),StatusCodes.Status200OK)]
        [HttpGet]
        public async Task<ActionResult<PaginationResponse<ProductDto>>> GetAllProducts([FromQuery] ProductSpecPrames productSpec)
        {
            var result = await _productService.GetAllProductSAsync(productSpec);
            return Ok(result);
        }

        [ProducesResponseType(typeof(IEnumerable<TypeBrandDto>), StatusCodes.Status200OK)]
        [HttpGet("brand")]
        public async Task<ActionResult<IEnumerable<TypeBrandDto>>> GetAllBrands()
        {
            var result = await _productService.GetAllBrandsAsync();
            return Ok(result);
        }

        [ProducesResponseType(typeof(IEnumerable<TypeBrandDto>), StatusCodes.Status200OK)]
        [HttpGet("type")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetAllTypes()
        {
            var result = await _productService.GetAllTypesAsync();
            return Ok(result);
        }


        [ProducesResponseType(typeof(TypeBrandDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int? id)
        {
            if(id is null) return BadRequest(new ApiErrorResponse(400));
            var result = await _productService.GetProductById(id.Value);
            if(result is null) return NotFound(new ApiErrorResponse(404, $"The Product With Id :{id} Not Found At DB =( "));
            return Ok(result);
            
        }
    }
}
