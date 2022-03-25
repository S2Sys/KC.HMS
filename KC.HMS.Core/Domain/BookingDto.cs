using System.ComponentModel.DataAnnotations;

namespace KC.HMS.Core.Domain
{



    public class BookingDto
    {
        [Key]
        public Guid BookingId { get; set; }
        [Required]
        public int RoomId { get; set; }
        [Required]
        public DateTime CheckIn { get; set; }
        [Required]
        public DateTime CheckOut { get; set; }
        public decimal TotalCost { get; set; }
        public decimal Paid { get; set; }
        public bool Completed { get; set; }
        public string ApplicationUserId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerPhone { get; set; }
        public string CustomerAddress { get; set; }
        public string CustomerCity { get; set; }
        public bool OptForAdditionalBed { get; set; }
        public int BookingStatus { get; set; }
        public Guid ReferenceId { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
        public string UpdatedBy { get; set; }
    }
}
