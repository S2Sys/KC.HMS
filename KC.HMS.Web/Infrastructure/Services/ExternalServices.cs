using KC.HMS.Core.Domain;
using KC.HMS.Core.Utility;
using KC.HMS.Core.ViewModel;
using KC.HMS.Services.ViewModels;
using KC.HMS.Web.Infrastructure.Contracts;
using Newtonsoft.Json;
using System.Net;

namespace KC.HMS.Web.Infrastructure.Services
{
    public class ExternalServices : IExternalServices
    {

        private readonly ILogger<ExternalServices> _logger;
        private HttpContext _context;

        private readonly IConfiguration _configuration;
        private readonly string baseAPI = string.Empty;

        public ExternalServices(ILogger<ExternalServices> logger,
               IConfiguration configuration
              )
        {
            _logger = logger;
            _configuration = configuration;
            baseAPI = configuration.GetValue<string>("ExternalAPI");
        }

        private WebHeaderCollection GetHeader(string apiToken)
        {
            var header = new WebHeaderCollection();

            header.Add("Authorization", "Bearer " + apiToken);

            return header;
        }
        public HotelViewModel GetHotel()
        {
            var result = UtilRestClient.Get<HotelViewModel>(endPointUrl: baseAPI + APIUrl.HotelInfo);
            return result;

        }

        public List<RoomViewModel> GetRoomAvailablity(DateTime checkin, DateTime checkout, string apiToken)
        {


            var result = UtilRestClient.Get<List<RoomViewModel>>(endPointUrl: baseAPI + APIUrl.RoomAvailablity
                + $"?checkin={checkin.ToString("yyyy-MM-dd HH:mm:ss")}&checkout={checkout.ToString("yyyy-MM-dd HH:mm:ss")}",
                headers: GetHeader(apiToken));
            return result;
        }

        public List<TVPBooking> UpsertBooking(List<BookRoomViewModel> bookRooms, string apiToken)
        {
            var data = JsonConvert.SerializeObject(bookRooms);

            var result = UtilRestClient.PostAsync<List<TVPBooking>>(endPointUrl: baseAPI + APIUrl.BookRoom,
                postData: data, headers: GetHeader(apiToken));
            return result;
        }


        public List<BookRoomViewModel> GetMyBooking(string userId, BookingStatusKind bookingStatus, string apiToken)
        {

            var result = UtilRestClient.GetAsync<List<BookRoomViewModel>>(endPointUrl: baseAPI + APIUrl.MyBooking
                + $"?userId={userId}&bookingStatus={(int)bookingStatus}", headers: GetHeader(apiToken));
            return result;
        }

        public RoomViewModel UpsertRoom(RoomViewModel dto, string apiToken)
        {
            var data = JsonConvert.SerializeObject(dto);

            var result = UtilRestClient.PostAsync<RoomViewModel>(endPointUrl: baseAPI + APIUrl.UpsertRooms,
                postData: data, headers: GetHeader(apiToken));
            return result;
        }

        public List<RoomViewModel> GetRoomsListing(string apiToken)
        {
            var result = UtilRestClient.GetAsync<List<RoomViewModel>>(endPointUrl: baseAPI + APIUrl.RoomsListing,
                headers: GetHeader(apiToken));
            return result;
        }
        public RoomViewModel GetRoom(string name, int id, string apiToken)
        {
            var result = UtilRestClient.GetAsync<RoomViewModel>(endPointUrl: baseAPI + APIUrl.RoomsForEdit
                 + $"?roomName={name}&roomId={id}",
                headers: GetHeader(apiToken));
            return result;
        }
        public AuthenticationModel Login(TokenRequestModel model)
        {
            var data = JsonConvert.SerializeObject(model);
            var result = UtilRestClient.PostAsync<AuthenticationModel>(endPointUrl: baseAPI + APIUrl.Login,
                postData: data);
            return result;
        }

        public LookupViewModel GetLookups(LookupFormKind form, int id)
        {
            var result = UtilRestClient.GetAsync<LookupViewModel>(endPointUrl: baseAPI + APIUrl.Lookups);
            return result;
        }
        public List<DashboardCalenderViewModel> GetDashboardData(string apiToken)
        {
            var result = UtilRestClient.GetAsync<List<DashboardCalenderViewModel>>(endPointUrl: baseAPI + APIUrl.DashabordData,
                headers: GetHeader(apiToken));
            return result;
        }
        public bool GetCancelBooking(Guid bookingId, string userId, string apiToken)
        {
            var result = UtilRestClient.GetAsync<bool>(endPointUrl: baseAPI + APIUrl.CancelBooking
                 + $"?bookingId={bookingId}&userId={userId}",
                headers: GetHeader(apiToken));
            return result;
        }
    }
}
