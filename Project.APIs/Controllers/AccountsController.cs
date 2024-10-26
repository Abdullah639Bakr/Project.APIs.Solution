using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Project.APIs.Errors;
using Project.APIs.Extensions;
using Project.Core.Dtos.Auth;
using Project.Core.Entities.Identity;
using Project.Core.Repositories.Contract;
using System.Security.Claims;

namespace Project.APIs.Controllers
{
   
    public class AccountsController : BaseApiController
    {
        private readonly IUserService _userService;
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;

        public AccountsController(IUserService userService , 
                                  UserManager<AppUser> userManager,
                                  ITokenService tokenService,
                                  IMapper mapper)
        {
            _userService = userService;
            _userManager = userManager;
            _tokenService = tokenService;
            _mapper = mapper;
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto) 
        {
            var user = _userService.LoginAsync(loginDto);
            if (user is null) return Unauthorized(new ApiErrorResponse(StatusCodes.Status401Unauthorized));
            return Ok(user);
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register (RegisterDto loginDto)
        {
            var user = _userService.RegisterAsync(loginDto);
            if (user is null) return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest, "Invalid Registretion !!"));
            return Ok(user);
        }

        [HttpGet("GetCurrentUser")]
        [Authorize]
        public async Task<ActionResult<UserDto>> GetCurrentUser() 
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            if (userEmail is null) return BadRequest( new ApiErrorResponse(StatusCodes.Status400BadRequest));
            var user = await _userManager.FindByEmailAsync(userEmail);
            if (user is null) return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest));
            return Ok(new UserDto() 
            {

                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = await _tokenService.CreateTokenAsync(user, _userManager)
            });
        }


        [HttpGet("Address")]
        [Authorize]

        public async Task<ActionResult<UserDto>> GetCurrentUserAddress()
        {
            
            var user = await _userManager.FindByEmailWithAddressAsync(User);
            if (user is null) return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest));
            return Ok(_mapper.Map<AddressDto>(user.Address));
          
        }

    }
}
