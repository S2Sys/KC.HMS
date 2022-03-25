using Microsoft.AspNetCore.Authorization;

namespace KC.HMS.Web.API
{
    [Route("api/[controller]")]
    [ApiController] 
    public class DataController : ControllerBase
    {
        [HttpGet("unsecured")]
        
        public async Task<IActionResult> GetUnsecuredData()
        {
            return Ok("This Un-Secured Data is available for all users.");
        }

        [HttpGet("secured")]
        [Authorize]
        public async Task<IActionResult> GetSecuredData()
        {
            return Ok("This Secured Data is available only for Authenticated Users.");
        }
        [HttpGet("postsecured")]
        [Authorize]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> PostSecuredData()
        {
            return Ok("This Secured Data is available only for Administrators.");
        }
    }
}
