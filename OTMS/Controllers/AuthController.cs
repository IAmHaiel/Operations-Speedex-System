using Microsoft.AspNetCore.Mvc;
using OTMS.Models;

namespace OTMS.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequest request)
    {
        if (request is null || request.UserId <= 0 || string.IsNullOrWhiteSpace(request.Password))
        {
            return BadRequest(new { success = false, message = "User ID and password are required." });
        }

        // Example authentication logic.
        // Replace with your own user store / database lookup.
        // For demonstration, using hardcoded values.
        if (request.UserId == 1 && request.Password == "password")
        {
            return Ok(new LoginResponse
            (
                Success: true,
                Message: "Login successful.",
                Token: "fake-jwt-token",
                UserId: request.UserId
            ));
        }

        return Unauthorized(new { success = false, message = "Invalid user ID or password." });
    }

    public sealed record LoginRequest(int UserId, string Password);
    public sealed record LoginResponse(bool Success, string Message, string Token, int UserId);
}
