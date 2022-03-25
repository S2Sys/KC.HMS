namespace KC.HMS.Web.Infrastructure.Services
{
    public static class APIUrl
    {
        public const string Login = "User/token";
        public const string HotelInfo = "Booking/hotel";
        public const string RoomAvailablity = "Booking/get-available-rooms";
        public const string BookRoom = "Booking/bulk-book";
        public const string MyBooking = "Booking/get-my-booking";
        public const string UpsertRooms = "Booking/upsert-rooms";
        public const string RoomsListing = "Booking/get-rooms-listing";
        public const string Lookups = "Booking/get-lookup-forms";
        public const string RoomsForEdit = "Booking/get-room";
        public const string DashabordData = "Booking/get-dashboard";
        public const string CancelBooking = "Booking/cancel-booking";
    }
}
