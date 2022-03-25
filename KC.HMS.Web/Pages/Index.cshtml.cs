using KC.HMS.Core.ViewModel;
using KC.HMS.Web.Infrastructure.Contracts;

namespace KC.HMS.Web.Pages
{


    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IExternalServices _externalServicecs;
        public IndexModel(ILogger<IndexModel> logger,
            IExternalServices externalServicecs)
        {
            _logger = logger;
            _externalServicecs = externalServicecs;
        }



        public HotelViewModel Hotel { get; set; } = new HotelViewModel();

      
      
        public void OnGet()
        {
             Hotel = _externalServicecs.GetHotel();
            //SearchDtoModel.NumberOfRooms = Hotel.Rooms.Sum(i => i.RoomCount);
        }

        //public async Task OnPostCheckAvailablity([FromBody]  SearchDto searchDto)
        //{
        //    var result = SearchDtoModel;
        //    if (ModelState.IsValid)
        //    {
        //        result = SearchDtoModel;
                
        //    }
        //    else
        //    {

        //    }
        //    Hotel = _externalServicecs.GetHotel();
        //    SearchDtoModel.NumberOfRooms = Hotel.Rooms.Sum(i => i.RoomCount);
        //}

        
    }
}