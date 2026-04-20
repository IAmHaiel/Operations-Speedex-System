using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace OTMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SecuredAPIController : ControllerBase
    {
        // Secured APIs
        [Authorize]
        [HttpGet]
        public IActionResult AuthenticatedOnlyEndpoint()
        {
            return Ok("You are authenticated");
        }
    }
}
