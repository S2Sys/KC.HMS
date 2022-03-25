using Microsoft.AspNetCore.Mvc;

namespace KC.HMS.WebAPI.Controllers
{
    //[Authorize(RoleKind.SuperAdministrator.ToString())]
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;
        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [HttpPost("addrole")]
        public async Task<IActionResult> AddRoleAsync(AddRoleModel model)
        {
            var result = await _adminService.AddRoleAsync(model);
            return Ok(result);
        }

    }
}
