using KC.HMS.Web.Infrastructure.Contracts;

using System.Security.Claims;

namespace KC.HMS.Web.Pages
{
    public class BasePageModel : PageModel
    {

        protected readonly ILogger<IndexModel> _logger;
        protected readonly IExternalServices _externalServicecs;

        protected readonly UserManager<ApplicationUser> _userManager;
        protected readonly SignInManager<ApplicationUser> _signInManager;
        protected ApplicationUser ApplicationUser { get { return GetUserAsync().Result; } }

        protected string ApplicationUserId
        {
            get { return this.User.FindFirstValue(ClaimTypes.NameIdentifier); }
        }

        public BasePageModel(ILogger<IndexModel> logger,
                             IExternalServices externalServicecs,
                             UserManager<ApplicationUser> userManager,
                             SignInManager<ApplicationUser> signInManager)
        {
            _logger = logger;
            _externalServicecs = externalServicecs;
            _userManager = userManager;
            _signInManager = signInManager;

        }


        public bool IsValid<T>(T inputModel)
        {
            var property = this.GetType().GetProperties().Where(x => x.PropertyType == inputModel.GetType()).FirstOrDefault();

            var hasErros = ModelState.Values
                .Where(value => value.GetType().GetProperty("Key").GetValue(value).ToString().Contains(property.Name))
                .Any(value => value.Errors.Any());

            return !hasErros;
        }


        private async Task<ApplicationUser> GetUserAsync()
        {
            return await _userManager.GetUserAsync(User);
        }

        [TempData]
        public string StatusMessage { get; set; }
    }
}
