using KC.HMS.Core;
using KC.HMS.Core.Domain;
using KC.HMS.Core.Models;
using KC.HMS.Core.ViewModel;
 
namespace KC.HMS.Services.Contracts
{
    public interface IHotelRepository
    {
        #region Read
        Task<HotelViewModel> GetHotelInfoAsync(int id = 2);
        Task<IList<RoomViewModel>> GetAvailableRooms(DateTime checkin, DateTime checkout, int hotel = 2);

        Task<RoomViewModel> GetRoom(string name, int roomId);

        Task<List<RoomViewModel>> GetRooms();

        Task<List<BookRoomViewModel>> GetMyBooking(string userId, BookingStatusKind status);

        Task<LookupViewModel> GetLookupAsync(LookupFormKind form, int id = 2); 
        #endregion
         
        #region Update/Insert/Delete
        Task<List<DashboardCalenderViewModel>> GetDashboardAsync();

        Task<TVPBooking> UpsertBooking(TVPBooking bookRoom);

        Task<List<TVPBooking>> UpsertBulkBooking(List<TVPBooking> bookingDtos);

        Task<bool> CancelBookingAsync(Guid bookingId, string userId);

        Task<RoomViewModel> UpsertRoom(RoomViewModel dto); 
        #endregion
    }
}