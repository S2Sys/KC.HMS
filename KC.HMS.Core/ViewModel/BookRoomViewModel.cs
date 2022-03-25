using System.ComponentModel.DataAnnotations;

namespace KC.HMS.Core.ViewModel
{

    public class BookRoomViewModel
    {
        [Required]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "CheckIn")]
        public DateTime CheckIn { get; set; }
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "CheckIn")]

        public DateTime CheckOut { get; set; }


        [Display(Name = "Room Number")]

        public string RoomNumber { get; set; }
       
        public double Paid { get; set; }
        [Display(Name = "Cost")]
        public double TotalCost { get; set; }
        [Display(Name = "Modified")]
        public DateTime Created { get; set; }

        [Display(Name = "Type")]
        public string RoomType { get; set; }
        [Display(Name = "Name")]
        public string CustomerName { get; set; }
        [Display(Name = "Email")]
        public string CustomerEmail { get; set; }
        [Display(Name = "Phone")]
        public string CustomerPhone { get; set; }
        [Display(Name = "Address")]
        public string CustomerAddress { get; set; }
        [Display(Name = "City")]
        public string CustomerCity { get; set; }
        public string UserId { get; set; }

        public Guid ReferenceId { get; set; }
        public Guid BookingId { get; set; }
        [Display(Name = "Status")]
        public BookingStatusKind BookingStatus { get; set; }

        [Display(Name = "Opt For Additional Bed")]
        public bool OptForAdditionalBed { get; set; }

    }

}
