using KC.HMS.Core.Domain;
using KC.HMS.Core.ViewModel;

namespace KC.HMS.Core.Utility
{
    public static class UtilMapper
    {
        public static TVPBooking Map(this BookRoomViewModel model)
        {

            return new TVPBooking
            {
                CheckIn = model.CheckIn.Date.AddHours(11).AddSeconds(1),
                CheckOut = model.CheckOut.Date.AddHours(11).AddSeconds(-1),
                CustomerName = model.CustomerName,
                CustomerPhone = model.CustomerPhone,
                CustomerEmail = model.CustomerEmail,
                ApplicationUserId = model.UserId,
                ReferenceId = model.ReferenceId,
                RoomNumber = model.RoomNumber,
                 
            };
        }

        public static List<TVPBooking> Map(this List<BookRoomViewModel> datas)
        {
            var refernceId = Guid.NewGuid();
            return (from model in datas
                    select new TVPBooking
                    {
                        CheckIn = model.CheckIn.Date.AddHours(11).AddSeconds(1),
                        CheckOut = model.CheckOut.Date.AddHours(11),
                        CustomerName = model.CustomerName,
                        CustomerPhone = model.CustomerPhone,
                        CustomerEmail = model.CustomerEmail,
                        ApplicationUserId = model.UserId,
                        BookingStatus = (int)model.BookingStatus,
                        BookingId = model.BookingId,
                        ReferenceId = refernceId,
                        RoomNumber = model.RoomNumber,
                        OptForAdditionalBed = model.OptForAdditionalBed
                    }).ToList();
        }

        public static List<BookRoomViewModel> Map(this List<TVPBooking> datas)
        {

            return (from model in datas
                    select new BookRoomViewModel
                    {
                        CheckIn = model.CheckIn.Date.AddHours(11).AddSeconds(1),
                        CheckOut = model.CheckOut.Date.AddHours(11),
                        CustomerPhone = model.CustomerPhone,
                        CustomerEmail = model.CustomerEmail,
                        UserId = model.ApplicationUserId,
                        ReferenceId = model.ReferenceId,
                        BookingStatus = (BookingStatusKind)model.BookingStatus,
                        BookingId = model.BookingId,
                        CustomerName = model.CustomerName,
                        OptForAdditionalBed = model.OptForAdditionalBed
                    }).ToList();
        }

        public static BookRoomViewModel Map(this BookingDto model)
        {

            return new BookRoomViewModel
            {
                CheckIn = model.CheckIn.Date,
                CheckOut = model.CheckOut.Date,
                CustomerPhone = model.CustomerPhone,
                CustomerEmail = model.CustomerEmail,
                UserId = model.ApplicationUserId,
                ReferenceId = model.ReferenceId,
                BookingStatus = (BookingStatusKind)model.BookingStatus,
                BookingId = model.BookingId
            };
        }
        public static DateTime GetCheckIn(this DateTime model)
        {

            return model.Date.AddHours(11).AddSeconds(1);
        }

        public static DateTime GetCheckOut(this DateTime model)
        {
            return model.Date.AddHours(11).AddSeconds(-1);
        }

    }
}
