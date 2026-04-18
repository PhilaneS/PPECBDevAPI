using Application.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Application.DTOs;
using Application.Common.Models;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserService _userService;

        public AuthController(UserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserDto userDto)
        {
            await _userService.CreateAsync(userDto);
            return Ok();
        }

        [HttpGet("login")]
        public async Task<IActionResult> Login([FromBody] UserDto userDto)
        {
            var token = await _userService.LoginAsync(userDto);
            if (token == null)
            {
                return Unauthorized();
            }
            return Ok(ApiResponse<string>.SuccessResponse(token));
        }
    }
}
