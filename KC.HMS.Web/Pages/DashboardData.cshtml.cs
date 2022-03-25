using KC.HMS.Core.Extentions;
using KC.HMS.Web.Infrastructure.Contracts;

namespace KC.HMS.Web.Pages
{
    public class DashboardDataModel : BasePageModel
    {
        public DashboardDataModel(ILogger<IndexModel> logger, 
            IExternalServices externalServicecs, 
            UserManager<ApplicationUser> userManager, 
            SignInManager<ApplicationUser> signInManager) : base(logger, externalServicecs, userManager, signInManager)
        {
        }

        public JsonResult OnGet()
        {
            string token = User.GetToken();
            var data = _externalServicecs.GetDashboardData(token);

            return new JsonResult(data);
        }
    }
}
