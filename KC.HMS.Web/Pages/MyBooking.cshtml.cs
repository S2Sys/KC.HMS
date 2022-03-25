using KC.HMS.Core.Extentions;
using KC.HMS.Core.ViewModel;
using KC.HMS.Web.Infrastructure.Contracts;
using Microsoft.AspNetCore.Authorization;

namespace KC.HMS.Web.Pages
{
    [Authorize]
    public class MyBookingModel : BasePageModel
    {

        public MyBookingModel(ILogger<IndexModel> logger,
            IExternalServices externalServicecs,
             UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager) :
            base(logger, externalServicecs, userManager, signInManager)
        { }
        public List<BookRoomViewModel> Listing { get; set; } = new List<BookRoomViewModel>();

        public void OnGet(Guid referenceId, string status, BookingStatusKind showStatus)
        {
            if (referenceId != Guid.Empty)
                StatusMessage = status;
            Load(showStatus);
        }

        private void Load(BookingStatusKind status)
        {
            string token = User.GetToken();

            TempData["BookingStatusKind"] = status;

            var userId = ApplicationUser.Id;
            var userRoles = _userManager.GetRolesAsync(ApplicationUser);
            if (userRoles.Result.Any(i => (i == RoleKind.Administrator.ToString() || i == RoleKind.SuperAdministrator.ToString())))
            {
                userId = Guid.Empty.ToString();
                ViewData["Title"] = $"Booking History > {status}";
            }
            else
            {
                ViewData["Title"] = $"My Booking > {status} ";
            }

            Listing = _externalServicecs.GetMyBooking(userId, status, token);
        }

        public async Task<IActionResult> OnGetCancelBooking(Guid id)
        {
            var status = (BookingStatusKind)TempData["BookingStatusKind"];
            TempData.Keep();
            string token = User.GetToken();
            var deleted = _externalServicecs.GetCancelBooking(id, ApplicationUser.Id, token);
            if (deleted)
                Load(status);

            return Page();
        }
    }
}
