using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KC.HMS.WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class SecuredController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetData()
        {
            return Ok("This Secured Data is available only for Authenticated Users.");
        }
    }

}
