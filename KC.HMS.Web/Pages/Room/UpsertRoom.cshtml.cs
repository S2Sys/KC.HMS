using KC.HMS.Core.ViewModel;
using KC.HMS.Web.Infrastructure.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KC.HMS.Web.Pages.Room
{
    public class UpsertRoomModel : BasePageModel
    {
        public UpsertRoomModel(ILogger<Pages.IndexModel> logger, 
            IExternalServicecs externalServicecs, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager) : 
            base(logger, externalServicecs, userManager, signInManager)
        {
        }

        [BindProperty]
        public RoomViewModel Input { get; set; }

        public void OnGet(string id)
        {
            Input = new RoomViewModel();
        }

        public void OnPost()
        {
            var inserted = Input;
        }
    }
}
