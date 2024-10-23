﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.APIs.Errors;
using Project.Core.Dtos.Auth;
using Project.Core.Repositories.Contract;

namespace Project.APIs.Controllers
{
   
    public class AccountsController : BaseApiController
    {
        private readonly IUserService _userService;

        public AccountsController(IUserService userService)
        {
            _userService = userService;
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
    }
}