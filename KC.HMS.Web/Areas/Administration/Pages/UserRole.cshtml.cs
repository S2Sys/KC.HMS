namespace KC.HMS.Web.Areas.Administration.Pages
{
    public class UserRoleModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public UserRoleModel(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        [TempData]
        public string StatusMessage { get; set; }
       //public IList<UserRoleViewModel> Listing { get; set; } = new List<UserRoleViewModel>();

        [BindProperty]
        public List<UserRoleViewModel> RolesSelected { get; set; } = new List<UserRoleViewModel>();

        public async Task  OnGet(string userId)
        {
           // ViewBag.userId = userId;
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                StatusMessage = $"Error: User with Id = {userId} cannot be found";
           
            }
            // ViewBag.UserName = user.UserName;
            RolesSelected = new List<UserRoleViewModel>();
            foreach (var role in _roleManager.Roles.ToList())
            {
                var userRolesViewModel = new UserRoleViewModel
                {
                    RoleId = role.Id,
                    RoleName = role.Name
                };
                if (await _userManager.IsInRoleAsync(user, role.Name))
                {
                    userRolesViewModel.Selected = true;
                }
                else
                {
                    userRolesViewModel.Selected = false;
                }
                RolesSelected.Add(userRolesViewModel);
            }
           
        }
        public async Task OnPost(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                StatusMessage = $"Error: User with Id = {userId} cannot be found";
                return;
            }
            var roles = await _userManager.GetRolesAsync(user);
            var result = await _userManager.RemoveFromRolesAsync(user, roles);
            if (!result.Succeeded)
            {
                 StatusMessage = "Error: Cannot remove user existing roles";
                return ;
            }
            result = await _userManager.AddToRolesAsync(user, RolesSelected.Where(x => x.Selected).Select(y => y.RoleName));
            if (!result.Succeeded)
            {
                StatusMessage =  "Error: Cannot add selected roles to user";
                return;
            }
        }
         


        public class UserRoleViewModel
        {
            public string RoleId { get; set; }
            public string RoleName { get; set; }
            public bool Selected { get; set; }
        }
    }
}
