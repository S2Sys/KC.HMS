using KC.HMS.Core.Extentions;
using KC.HMS.Core.Validation;
using KC.HMS.Core.ViewModel;
using KC.HMS.Web.Infrastructure.Contracts;
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel.DataAnnotations;

namespace KC.HMS.Web.Pages
{

    [Authorize]
    public class SearchModel : BasePageModel
    {
        public SearchModel(ILogger<IndexModel> logger,
            IExternalServices externalServicecs,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager) : base(logger, externalServicecs, userManager, signInManager)
        {
        }


        [BindProperty]
        public SearchInputModel SearchInput { get; set; } = new SearchInputModel();
        [BindProperty]
        public BookModel BookInput { get; set; } = new BookModel();

        public List<RoomViewModel> Listing { get; set; } = null;

        public void OnGet()
        {
            string token = User.GetToken();
            var hotel = _externalServicecs.GetHotel();
            SearchInput.TotalRooms = hotel.Rooms.Sum(i => i.RoomCount);

        }

        public async Task<IActionResult> OnPostCheckAvailablity()
        {

            ModelState.MarkAllFieldsAsSkipped();
            if (!TryValidateModel(SearchInput, nameof(SearchInput)))
            {
                return Page();
            }
            //if (!IsValid(SearchInput))
            //{
            //    return Page();
            //}

            var result = SearchInput;
            var startDate = DateTime.Parse(SearchInput.CheckIn);
            var endDate = DateTime.Parse(SearchInput.CheckOutDate);
            string token = User.GetToken();

            Listing = _externalServicecs.GetRoomAvailablity(startDate, endDate, token);

            BookInput = new BookModel
            {
                CheckIn = SearchInput.CheckIn,
                CheckOutDate = SearchInput.CheckOutDate,
                RoomCount = SearchInput.RoomCount,
                RoomsSelected = "",
                UserId = "Guest"
            };
            //var user = await _userManager.GetUserAsync(User);
            if (ApplicationUser != null)
            {

                BookInput.Email = ApplicationUser.Email;
                BookInput.Name = ApplicationUser.FirstName + " " + ApplicationUser.LastName;
                BookInput.Phone = ApplicationUser.PhoneNumber;
                BookInput.UserId = ApplicationUser.Id;

            }

            return Page();
        }

        public async Task<IActionResult> OnPostBookMyRooms()
        {
            var referenceId = Guid.Empty;

            ModelState.MarkAllFieldsAsSkipped();
            if (!TryValidateModel(BookInput, nameof(BookInput)))
            {
                return Page();
            }
            //if (!IsValid(SearchInput))
            //{
            //    return Page();
            //}
            else
            {

                var bookRooms = new List<BookRoomViewModel>();
                char[] separator = { ';' };
                string[] rooms = BookInput.RoomsSelected.Split(separator, StringSplitOptions.RemoveEmptyEntries);


                var result = BookInput;
                var startDate = DateTime.Parse(BookInput.CheckIn);
                var endDate = DateTime.Parse(BookInput.CheckOutDate);
                foreach (var room in rooms)
                {
                    bookRooms.Add(new BookRoomViewModel
                    {
                        CheckIn = startDate,
                        CheckOut = endDate,
                        CustomerEmail = BookInput.Email,
                        CustomerPhone = BookInput.Phone,
                        RoomNumber = room,
                        UserId = BookInput.UserId,
                        CustomerName = BookInput.Name ,
                        OptForAdditionalBed = BookInput.OptForAdditionalBed

                    }
                    );
                }
                string token = User.GetToken();
                var booked = _externalServicecs.UpsertBooking(bookRooms, token);
                if (booked.Any())
                {
                    referenceId = booked.FirstOrDefault().ReferenceId;
                    StatusMessage = $"Thanks for your Booking with GS residency. " +
                        $"Your refrence number is {booked.FirstOrDefault().ReferenceId} ";
                }
            }
            return RedirectToPage("MyBooking", new { referenceId = referenceId, status = StatusMessage });
        }



        public class SearchInputModel
        {

            [Required]
            [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
            [Display(Name = "CheckIn")]
            public string CheckIn { get; set; }

            [Required]
            [DateGreaterThan(DateFrom = "CheckIn", ErrorMessage = "CheckIn date should be lesser than CheckOut date ")]
            [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
            [Display(Name = "CheckOut")]
            public string CheckOutDate { get; set; }


            [HiddenInput(DisplayValue = false)]
            public int TotalRooms { get; set; }
            [Display(Name = "Number Of Rooms")]
            public int RoomCount { get; set; } = 1;


        }

        public class BookModel
        {



            [Required]
            [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]

            [Display(Name = "CheckIn")]

            public string CheckIn { get; set; }


            [Required]
            [DateGreaterThan(DateFrom = "CheckIn", ErrorMessage = "CheckIn date should be lesser than CheckOut date ")]
            [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
            [Display(Name = "CheckOut")]
            public string CheckOutDate { get; set; }


            [Required]
            public string Name { get; set; }

            [Required]

            public string Email { get; set; }

            [Required]
            public string Phone { get; set; }

            public string UserId { get; set; }
            [Display(Name = "Room(s)")]
            public int RoomCount { get; set; } = 1;

            public string RoomsSelected { get; set; }
            [Display(Name = "Opt For Additional Bed")]
            public bool OptForAdditionalBed { get; set; }
        }
    }
}
