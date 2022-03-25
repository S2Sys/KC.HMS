using KC.HMS.Web.Infrastructure.Contracts;

namespace KC.HMS.Web.Pages
{

    public class DashboardModel : BasePageModel
    {
        public DashboardModel(ILogger<IndexModel> logger, IExternalServices externalServicecs, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager) : base(logger, externalServicecs, userManager, signInManager)
        {
        }

        public void OnGet()
        {
          
        }
       
    }
}
