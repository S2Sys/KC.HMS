using KC.HMS.Core.Validation;
using KC.HMS.Core.ViewModel;
using KC.HMS.Web.Infrastructure.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace KC.HMS.Web.Pages
{
    public class SearchListingModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IExternalServicecs _externalServicecs;

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;


        public SearchListingModel(ILogger<IndexModel> logger,
            IExternalServicecs externalServicecs,
             UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _logger = logger;
            _externalServicecs = externalServicecs;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [BindProperty]
        public SearchOrBookModel Input { get; set; } = new SearchOrBookModel();


        public List<RoomViewModel> Listing { get; set; } = new List<RoomViewModel>();

        public void OnGet()
        {
            var hotel = _externalServicecs.GetHotel();
            Input.TotalRooms = hotel.Rooms.Sum(i => i.RoomCount);
            Input.RoomsSelected = "1";
        }

        public async Task<IActionResult> OnPostCheckAvailablity()
        {

            if (ModelState.IsValid)
            {
                var result = Input;
                var startDate = DateTime.Parse(Input.CheckIn);
                var endDate = DateTime.Parse(Input.CheckOutDate);
                Listing = _externalServicecs.GetRoomAvailablity(startDate, endDate);

                var user = await _userManager.GetUserAsync(User);
                if (user != null)
                {

                    Input.Email = user.Email;
                    Input.Name = user.FirstName + " " + user.LastName;
                    Input.Phone = user.PhoneNumber;

                }
                else
                {
                    Input.Email = String.Empty;
                    Input.Name = String.Empty;
                    Input.Phone = String.Empty;
                }
            }

            return Page();


        }

        public async Task<IActionResult> OnPostBookMyRooms()
        {

            if (ModelState.IsValid)
            {
                var result = Input;
                var startDate = DateTime.Parse(Input.CheckIn);
                var endDate = DateTime.Parse(Input.CheckOutDate);
                //Listing = _externalServicecs.boo(startDate, endDate);


            }

            return Page();

        }



        public class SearchOrBookModel
        {


            [BindProperty]
            [Required]
            [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]

            [Display(Name = "CheckIn")]

            public string CheckIn { get; set; }

            [BindProperty]
            [Required]
            [DateGreaterThan(DateFrom = "CheckIn", ErrorMessage = "CheckIn date should be lesser than CheckOut date ")]
            [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
            [Display(Name = "CheckOut")]
            public string CheckOutDate { get; set; }

            [BindProperty]
            [Display(Name = "Number Of Rooms")]
             
            [Required] 
            public string Name { get; set; } = "Name";
            [BindProperty]
            [Required]

            public string Email { get; set; } = "Email";
            [BindProperty]
            [Required]
            public string Phone { get; set; } = "Phone";


            [BindProperty]
            [HiddenInput(DisplayValue = false)]
            public int TotalRooms { get; set; }
            [BindProperty]
            public string RoomsSelected { get; set; } = "1";


            [BindProperty]
            public int RoomCount { get; set; } = 1;

        }
    }
}
