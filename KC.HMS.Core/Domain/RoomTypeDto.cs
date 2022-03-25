using KC.HMS.Core.Settings;
using System.ComponentModel.DataAnnotations;
namespace KC.HMS.Core.Domain
{
    /// <summary>
    /// Represents a RoomTypes entity.
    /// NOTE: This class is used to keep RoomTypes information.
    /// </summary>
    public partial class RoomTypeDto  
    {
        #region Constructor

        #endregion Constructor

        #region Properties
        [Key]
        [Display(Name = "RoomTypeID")]
        public int RoomTypeID { get; set; }

        [Display(Name = "HotelId")]
        public int? HotelId { get; set; }

        [Display(Name = "Title"),
            Required(ErrorMessage = VALIDATION_MESSGAE.REQUIRED),
            StringLength(100, ErrorMessage = VALIDATION_MESSGAE.MAXLENGTH)]
        public string Title { get; set; }

        [Display(Name = "InternalName"),
            Required(ErrorMessage = VALIDATION_MESSGAE.REQUIRED),
            StringLength(100, ErrorMessage = VALIDATION_MESSGAE.MAXLENGTH)]
        public string InternalName { get; set; }

        [Display(Name = "Allowed Persons"),
            Required(ErrorMessage = VALIDATION_MESSGAE.REQUIRED)]
        public int AllowedPersons { get; set; }

        [Display(Name = "Allowed Kids"),
            Required(ErrorMessage = VALIDATION_MESSGAE.REQUIRED)]
        public int AllowedKids { get; set; }

        [Display(Name = "Bath Rooms"),
            Required(ErrorMessage = VALIDATION_MESSGAE.REQUIRED)]
        public int BathRooms { get; set; }

        [Display(Name = "Square Feet"),
            StringLength(50, ErrorMessage = VALIDATION_MESSGAE.MAXLENGTH)]
        public string SquareFeet { get; set; }

        [Display(Name = "Basic Cost")]
        public decimal? BasicCost { get; set; }

        [Display(Name = "Additonal Beds"),
            Required(ErrorMessage = VALIDATION_MESSGAE.REQUIRED)]
        public int AdditonalBeds { get; set; }

        [Display(Name = "Additonal Bed Cost")]
        public decimal? AdditonalBedCost { get; set; }
        #endregion Properties

    }


}
