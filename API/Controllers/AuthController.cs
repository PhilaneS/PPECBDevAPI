using API.Response;
using Application.DTOs;
using Application.Services;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
            return Ok(ApiResponse<string>.SuccessResponse("Registered successfully"));
        }

        [HttpGet("login")]
        public async Task<IActionResult> Login([FromBody] UserDto userDto)
        {
            var data = await _userService.LoginAsync(userDto);
            if (data == null)
            {
                return Unauthorized();
            }
            return Ok(ApiResponse<string>.SuccessResponse(data));
        }
    }
}
