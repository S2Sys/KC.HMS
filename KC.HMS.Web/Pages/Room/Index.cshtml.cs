using KC.HMS.Core.Extentions;
using KC.HMS.Core.ViewModel;
using KC.HMS.Web.Infrastructure.Contracts;
using Microsoft.AspNetCore.Authorization;

namespace KC.HMS.Web.Pages.Room
{
    [Authorize]
    public class IndexModel : BasePageModel
    {
        public IndexModel(
            ILogger<Pages.IndexModel> logger,
            IExternalServices externalServicecs,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager) : base(logger, externalServicecs, userManager, signInManager)
        {
        }
        public List<RoomViewModel> Listing { get; set; }

        [BindProperty]
        public RoomViewModel Room { get; set; }
        public void OnGet(string message)
        {
            if (!string.IsNullOrEmpty(message))
                StatusMessage = message;

            string token = User.GetToken();
            Listing = _externalServicecs.GetRoomsListing(token);
        }

        public void OnPostAsync()
        {
            string token = User.GetToken();
            var upsertedEntity = _externalServicecs.UpsertRoom(Room,token);
        }
    }
}
