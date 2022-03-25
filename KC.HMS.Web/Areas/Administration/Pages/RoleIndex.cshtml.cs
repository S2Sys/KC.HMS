namespace KC.HMS.Web.Areas.Administration.Pages
{
    public class RoleIndexModel : PageModel
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        public RoleIndexModel(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        public IList<IdentityRole> Listing { get; set; } = new List<IdentityRole>();

        public async Task OnGet()
        {
            Listing = await _roleManager.Roles.ToListAsync();

        }

        public async Task OnPostAsync()
        {
            if (Input.RoleName != null)
            {
                var existingRole = await _roleManager.FindByNameAsync(Input.RoleName);
                if (existingRole == null)
                    await _roleManager.CreateAsync(new IdentityRole(Input.RoleName.Trim()));
                Listing = await _roleManager.Roles.ToListAsync();
            }
            else
            {
                StatusMessage = "Role Name is required";
            }
        }

        public class InputModel
        {
            public string RoleName { get; set; } = string.Empty;
        }
    }
}
