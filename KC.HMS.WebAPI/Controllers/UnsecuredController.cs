using Microsoft.AspNetCore.Mvc;

namespace KC.HMS.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UnsecuredController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetData()
        {
            return Ok("This Unsecured Data is available only for all Users.");
        }
    }

}
