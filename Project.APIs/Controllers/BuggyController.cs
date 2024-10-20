using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.Repository.Data.Contexts;

namespace Project.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuggyController : ControllerBase
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
            if (brand is null) return NotFound();
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
            return BadRequest();
        }

        [HttpGet("badrequest/{id}")]
        public async Task<IActionResult> GetBadRequestError(int id)
        {
            return Ok();
        }

        [HttpGet("unauthorized")]
        public async Task<IActionResult> GetUnauthorizedError(int id)
        {
            return Unauthorized();
        }


    }
}
