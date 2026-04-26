using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OTMS.Entities.DTOs;
using OTMS.Entities.Models;
using OTMS.Service.Interfaces;
using OTMS.Service.Services;

namespace OTMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class authorizationController(IAuthService authService) : ControllerBase
    {
        // Secured APIs

        /// <summary>
        /// Checks if the User is Authenticated.
        /// </summary>
        /// <response code="200">User is Authenticated.</response>
        /// <response code="401">User is not Authenticated.</response>
        /// <response code="500">Unexpected server error.</response>
        [Authorize]
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(500)]
        public IActionResult AuthenticatedOnlyEndpoint()
        {
            return Ok("You are authenticated");
        }

        // Role-Based Access

        // Changing the Roles to a string array to allow multiple roles
        /// <summary>
        /// Only accessible to users with the "Admin" role.
        /// </summary>
        /// <response code="200">User is an admin and is Authorized.</response>
        /// <response code="401">User is not Authorized.</response>
        /// <response code="500">Unexpected server error.</response>
        [Authorize(Roles = "Admin")]
        [HttpGet("admin-only")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(500)]
        public IActionResult AdminOnlyEndpoint()
        {
            return Ok("You are admin!");
        }

        // Register Account only accessible and used by SuperAdmin
        /// <summary>
        /// Registers a new employee account. Only accessible to users with the "SuperAdmin" role.
        /// </summary>
        /// <response code="200">User is authorized as a Super Admin.</response>
        /// <response code="401">User is not Authorized.</response>
        /// <response code="500">Unexpected server error.</response>
        [Authorize(Roles = "SuperAdmin")]
        [HttpPost("superadmin/register")]
        [ProducesResponseType(typeof(EmployeeRegisterDTO), 200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<Employee>> Register(EmployeeRegisterDTO request)
        {
            var user = await authService.RegisterAsync(request);
            if (user is null)
            {
                return BadRequest("Employee Number already exists.");
            }

            return Ok(user);
        }
    }
}