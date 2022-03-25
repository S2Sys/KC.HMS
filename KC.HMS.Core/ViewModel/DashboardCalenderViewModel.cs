namespace KC.HMS.Core.ViewModel
{
    public class DashboardCalenderViewModel
    {
       
        public int RoomID { get; set; }
        public int BathRooms { get; set; }
        public int SquareFeet { get; set; }
        public int Paid { get; set; }
        public int TotalCost { get; set; }
        public Guid BookingId { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public string CustomerPhone { get; set; }
        public string RoomType { get; set; }
        public string RoomNumber { get; set; }
        public string CustomerName { get; set; }
        public bool OptForAdditionalBed { get; set; }
    }
}
