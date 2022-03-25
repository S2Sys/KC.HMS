using KC.HMS.Core.Validation;
using System.ComponentModel.DataAnnotations;

namespace KC.HMS.Core.ViewModel
{
    public class RoomListingViewModel
    {

    }

    public class RoomViewModel
    {
        [Key]
        public int RoomID { get; set; }

        [Required]
        public int HotelID { get; set; }
        [Required]
        public int RoomTypeId { get; set; }

        //[Remote("RoomExists", "User", AdditionalFields = "RoomID,HotelID")] 
        [RoomExists]
        [Required]
        [Display(Name = "Room Number")]
        public string RoomNumber { get; set; }
        [Display(Name = "Bath Rooms")] 
        public int BathRooms { get; set; } = 1;
        [Display(Name = "Sq Ft")]
        public int SquareFeet { get; set; } = 120;
        [Display(Name = "Cost")] 
        public decimal BasicCost { get; set; } = 500;
        [Display(Name = "Additonal Bed(s)")] 
        public int AdditonalBeds { get; set; } = 1;
        [Display(Name = "Additonal Bed Cost")]
        public decimal AdditonalBedCost { get; set; } = 150;
        [Display(Name = "Type")]
        public string RoomType { get; set; } = String.Empty;

        //public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        //{
        //    if (this.Birthday.Year < 1900)
        //        yield return new ValidationResult("Surely you are not THAT old?", new[] { "Birthday" });
        //    if (this.Birthday.Year > 2000)
        //        yield return new ValidationResult("Sorry, you're too young for this website!", new[] { "Birthday" });
        //    if (this.Birthday.Month == 12)
        //        yield return new ValidationResult("Sorry, we don't accept anyone born in December!", new[] { "Birthday" });
        //}

    }

}
