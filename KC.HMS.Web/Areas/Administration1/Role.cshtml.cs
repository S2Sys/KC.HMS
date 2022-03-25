namespace KC.HMS.Web.Areas.Administration
{
    public class RoleModel : PageModel
    {

        private readonly RoleManager<IdentityRole> _roleManager;
        public RoleModel(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        [BindProperty]
        public string Role { get; set; } = string.Empty;

        public IList<IdentityRole> Listing { get; set; } = new List<IdentityRole>();

        public async void OnGet()
        {
            Listing = await _roleManager.Roles.ToListAsync();

        }

        public async void OnPostAsync(string name)
        {
            if (name != null)
            {
                var existingRole = await _roleManager.FindByNameAsync(name);
                if (existingRole == null)
                    await _roleManager.CreateAsync(new IdentityRole(name.Trim()));
            }
        }
    }
}
