using Microsoft.AspNetCore.Mvc;

namespace OTMS.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequest request)
    {
        if (request is null || string.IsNullOrWhiteSpace(request.Username) || string.IsNullOrWhiteSpace(request.Password))
        {
            return BadRequest(new { success = false, message = "Username and password are required." });
        }

        // Example authentication logic.
        // Replace with your own user store / database lookup.
        if (request.Username == "admin" && request.Password == "password")
        {
            return Ok(new LoginResponse
            (
                Success: true,
                Message: "Login successful.",
                Token: "fake-jwt-token",
                Username: request.Username
            ));
        }

        return Unauthorized(new { success = false, message = "Invalid username or password." });
    }

    public sealed record LoginRequest(string Username, string Password);
    public sealed record LoginResponse(bool Success, string Message, string Token, string Username);
}
