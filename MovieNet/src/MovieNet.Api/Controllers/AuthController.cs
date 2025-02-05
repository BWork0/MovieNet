using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieNet.Application.DTOs;
using MovieNet.Application.Interfaces.Services;

namespace MovieNet.Api.Controllers
{
    public record CredentialsDto(string Username, string Password);

    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            var existingUser = await _authService.CheckUserExists(dto.Email);
            if (existingUser)
            {
                return BadRequest("Email is already in use.");
            }

            await _authService.RegisterAsync(dto);

            return Ok(new { message = "Registration successful." });
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] CredentialsDto dto)
        {
            var user = await _authService.LoginAsync(dto.Username, dto.Password);

            if (user == null)
            {
                return Unauthorized("Invalid username or password.");
            }

            var tokenString = _authService.GenerateJwtToken(user);

            return Ok(new { token = tokenString });
        }

        [HttpGet("test")]
        [Authorize(Roles = "Admin")]
        public IActionResult Test()
        {
            return Ok("You are authenticated and authorized!");
        }
    }
}
