using KC.HMS.Core.Domain;
using KC.HMS.Core.ViewModel;
using KC.HMS.Services.ViewModels;

namespace KC.HMS.Web.Infrastructure.Contracts
{
    public interface IExternalServices
    {
        AuthenticationModel Login(TokenRequestModel model);
        HotelViewModel GetHotel();

        List<RoomViewModel> GetRoomAvailablity(DateTime checkin, DateTime checkout, string apiToken);

        List<TVPBooking> UpsertBooking(List<BookRoomViewModel> bookRooms, string apiToken);

        List<BookRoomViewModel> GetMyBooking(string userId, BookingStatusKind bookingStatus, string apiToken);

        RoomViewModel UpsertRoom(RoomViewModel dto, string apiToken);

        List<RoomViewModel> GetRoomsListing(string apiToken);

        RoomViewModel GetRoom(string name, int id, string apiToken);
        LookupViewModel GetLookups(LookupFormKind form, int id);

        List<DashboardCalenderViewModel> GetDashboardData(string apiToken);
        bool GetCancelBooking(Guid bookingId, string userId, string apiToken);
    }
}