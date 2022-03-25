using KC.HMS.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

using KC.HMS.Core.Domain;
using KC.HMS.Core.ViewModel;
using Swashbuckle.AspNetCore.Annotations;
using KC.HMS.Core.Utility;
using Microsoft.AspNetCore.Authorization;

namespace KC.HMS.WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : Controller
    {

        private readonly IRoomRepository _roomRepository;
        private readonly IHotelRepository _hotelRepository;
        // private readonly IBookingRepository _bookingRepository;
        public BookingController(
            IRoomRepository roomRepository,
            IHotelRepository hotelRepository
            )
        {
            _roomRepository = roomRepository;
            _hotelRepository = hotelRepository;
        }


        [AllowAnonymous]
        [HttpGet("hotel")]
        [SwaggerResponse(200, Type = typeof(HotelViewModel))]

        public async Task<IActionResult> GetHotelAsync()
        {

            var result = await _hotelRepository.GetHotelInfoAsync(); ;
            return Ok(result);
        }


        [HttpGet("get-available-rooms")]
        [SwaggerResponse(200, Type = typeof(List<RoomViewModel>))]

        public async Task<IActionResult> GetAvailableRooms(DateTime checkin, DateTime checkout)
        {

            var result = await _hotelRepository.GetAvailableRooms(checkin.GetCheckIn(), checkout.GetCheckOut()); ;
            return Ok(result);
        }


        [HttpPost("book-single")]
        public async Task<IActionResult> PostBookRoomAsync(BookRoomViewModel bookRoom)
        {

            if (bookRoom != null)
            {
                var bookingTVP = bookRoom.Map();
                var inputRequest = new List<TVPBooking>();
                var referenceId = Guid.NewGuid();
                bookRoom.ReferenceId = referenceId;
                inputRequest.Add(bookingTVP);

                var result = await _hotelRepository.UpsertBulkBooking(inputRequest);

                return Ok(result);
            }
            else
            {
                return BadRequest("Model is null");
            }
        }
        [HttpPost("bulk-book")]
        public async Task<IActionResult> PostBulkBookRoomAsync(List<BookRoomViewModel> bookRoom)
        {

            if (bookRoom.Any())
            {

                var bookingTVP = bookRoom.Map();

                var result = await _hotelRepository.UpsertBulkBooking(bookingTVP);

                return Ok(result);
            }
            else
            {
                return BadRequest("Model is null");
            }
        }


        //[HttpPost("addorupdate-room")]

        //public async Task<IActionResult> PostRoomAsync(RoomDto dto)
        //{

        //    var result = await _hotelRepository.UpsertRoom(dto);
        //    return Ok(result);
        //}


        [HttpGet("get-rooms")]
        [SwaggerResponse(200, Type = typeof(List<RoomDto>))]

        public async Task<IActionResult> GetRoomsAsync()
        {

            var result = await _hotelRepository.GetRooms(); ;
            return Ok(result);
        }

        [HttpGet("get-my-booking")]
        [SwaggerResponse(200, Type = typeof(List<BookRoomViewModel>))]

        public async Task<IActionResult> GetMyBookingAsync(string userId, BookingStatusKind bookingStatus)
        {

            var result = await _hotelRepository.GetMyBooking(userId, bookingStatus); ;
            return Ok(result);
        }

        [HttpPost("upsert-rooms")]

        public async Task<IActionResult> UpsertRoomAsync(RoomViewModel viewModel)
        {
            var result = await _hotelRepository.UpsertRoom(viewModel);
            return Ok(result);
        }

        [HttpGet("get-rooms-listing")]
        [SwaggerResponse(200, Type = typeof(List<RoomViewModel>))]
        public async Task<IActionResult> GetRoomListingAsync()
        {
            var result = await _hotelRepository.GetRooms();
            return Ok(result);
        }
        [HttpGet("get-room")]
        [SwaggerResponse(200, Type = typeof(RoomViewModel))]

        public async Task<IActionResult> GetRoomAsync(string roomName, int roomId)
        {
            var result = await _hotelRepository.GetRoom(roomName, roomId);
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpGet("get-lookup-forms")]
        [SwaggerResponse(200, Type = typeof(LookupViewModel))]
        public async Task<IActionResult> GeLookupsAsync(LookupFormKind form, int id = 2)
        {
            var result = await _hotelRepository.GetLookupAsync(form, id);
            return Ok(result);
        }

        
        [HttpGet("cancel-booking")] 
        public async Task<IActionResult> CancelBookingAsync(Guid bookingId, string userId)
        {
            var result = await _hotelRepository.CancelBookingAsync(bookingId, userId);
            return Ok(result);
        }

         
        [HttpGet("get-dashboard")]
        [SwaggerResponse(200, Type = typeof(List<DashboardCalenderViewModel>))]
        public async Task<IActionResult> GetDashboardAsync()
        {
            var result = await _hotelRepository.GetDashboardAsync();
            return Ok(result);
        }
    }
}
