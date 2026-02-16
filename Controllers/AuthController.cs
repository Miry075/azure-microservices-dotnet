using Microsoft.AspNetCore.Mvc;
using Wpm.Managemnt.Api.Dtos;
using Wpm.Managemnt.Api.Services;

namespace Wpm.Managemnt.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(IJwtTokenService jwtTokenService, IConfiguration configuration) : ControllerBase
{
    /// <summary>
    /// Login endpoint - generates a JWT token if user exists
    /// </summary>
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto request)
    {
        if (string.IsNullOrWhiteSpace(request.Username) || string.IsNullOrWhiteSpace(request.Password))
        {
            return BadRequest("Username and password are required");
        }

        // Validate user and generate token
        var token = await jwtTokenService.GenerateTokenAsync(request.Username);

        if (token == null)
        {
            return Unauthorized("Invalid username or password");
        }

        var expirationMinutes = int.Parse(configuration["Jwt:ExpirationMinutes"] ?? "60");

        return Ok(new LoginResponseDto
        {
            Token = token,
            ExpiresIn = expirationMinutes * 60 // in seconds
        });
    }
}
