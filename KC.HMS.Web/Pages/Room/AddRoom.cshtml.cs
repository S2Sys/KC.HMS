using KC.HMS.Core.Extentions;
using KC.HMS.Core.ViewModel;
using KC.HMS.Web.Infrastructure.Contracts;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace KC.HMS.Web.Pages.Room
{
    public class AddRoomModel : BasePageModel
    {
        public AddRoomModel(ILogger<Pages.IndexModel> logger,
                  IExternalServices externalServicecs, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager) :
                  base(logger, externalServicecs, userManager, signInManager)
        {
        }

        [BindProperty]
        public RoomViewModel Input { get; set; }

        [BindProperty]
        public List<SelectListItem> Types { get; set; }
        [BindProperty]
        public List<SelectListItem> Hotel { get; set; }

        public void OnGet(int id)
        {
            Load(id);
        }

        private void Load(int id)
        {
            string token = User.GetToken();
            var lookups = _externalServicecs.GetLookups(LookupFormKind.AddRoom, 2);
            Types = lookups.Types;
            Hotel = lookups.Hotels;

            if (id == 0)
            {
                Input = new RoomViewModel();
                ViewData["Title"] = "Room > NEW";
            }
            else
            {
                ViewData["Title"] = "Room > EDIT";
                Input = _externalServicecs.GetRoom(string.Empty, id, token);
            }

        }

        public IActionResult OnPostCreateRoom()
        {

            if (ModelState.IsValid)
            {

                string token = User.GetToken();
                var inserted = Input;
                var updated = _externalServicecs.UpsertRoom(Input, token);


                StatusMessage = $"Room {Input.RoomNumber} is created/updated successfully.";

                return RedirectToPage("/Room/Index", new { status = StatusMessage });

            }

            //Lookups = _externalServicecs.GetLookups(LookupFormKind.AddRoom, 2);

            Load(Input.RoomID);
            return Page();
        }
    }
}
