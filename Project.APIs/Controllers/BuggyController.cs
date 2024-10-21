using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.APIs.Errors;
using Project.Repository.Data.Contexts;

namespace Project.APIs.Controllers
{
    
    public class BuggyController : BaseApiController
    {
        private readonly AppDbContext _context;

        public BuggyController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("notfound")]
        public async Task<IActionResult> GetNotFoundRequestError()
        {
            var brand = await _context.Brands.FindAsync(100);
            if (brand is null) return NotFound(new ApiErrorResponse(404,"brand with id : 100 is not found"));
            return Ok(brand);
        }

        [HttpGet("servererror")]
        public async Task<IActionResult> GetServerError()
        {
            var brand = await _context.Brands.FindAsync(100);
            var brandToString = brand.ToString();
            return Ok(brand);
        }

        [HttpGet("badrequest")]
        public async Task<IActionResult> GetBadRequestError()
        {
            return BadRequest(new ApiErrorResponse(404));
        }

        [HttpGet("badrequest/{id}")]
        public async Task<IActionResult> GetBadRequestError(int id)
        {
            return Ok();
        }

        [HttpGet("unauthorized")]
        public async Task<IActionResult> GetUnauthorizedError(int id)
        {
            return Unauthorized(new ApiErrorResponse(401));
        }


    }
}
