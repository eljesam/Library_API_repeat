using Library_API_repeat.Api.DTOs.Authentication;

using Library_API_repeat.Api.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Library_API_repeat.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthenticationController(IAuthService authService)
        {
            _authService = authService;
        }

        // POST: api/authentication/register
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDTO dto)
        {
            if (await _authService.EmailExistsAsync(dto.Email))
            {
                return Conflict("A user with this email already exists.");
            }

            var registered = await _authService.RegisterAsync(dto);

            if (!registered)
            {
                return BadRequest("Unable to register user.");
            }

            return StatusCode(
                StatusCodes.Status201Created,
                new
                {
                    message = "User registered successfully."
                });
        }
        // POST: api/authentication/login
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO dto)
        {
            var result = await _authService.LoginAsync(dto);

            if (result == null)
            {
                return Unauthorized("Invalid email or password.");
            }

            return Ok(result);
        }
    }
}