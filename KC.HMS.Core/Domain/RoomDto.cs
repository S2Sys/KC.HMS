using KC.HMS.Core.Settings;
using System.ComponentModel.DataAnnotations;


namespace KC.HMS.Core.Domain
{
    /// <summary>
    /// Represents a Rooms entity.
    /// NOTE: This class is used to keep Rooms information.
    /// </summary>

    public partial class RoomDto  
    {
        #region Constructor

        #endregion Constructor

        #region Properties
        [Key]
        [Display(Name = "RoomID")]
        public int RoomID { get; set; }

        [Display(Name = "Hotel ID"),
            Required(ErrorMessage = VALIDATION_MESSGAE.REQUIRED)]
        public int HotelID { get; set; }

        [Display(Name = "Room Number"), Required(ErrorMessage = VALIDATION_MESSGAE.REQUIRED),
            StringLength(100, ErrorMessage = VALIDATION_MESSGAE.MAXLENGTH)]
        public string RoomNumber { get; set; }

        [Display(Name = "Room Type Id"), Required(ErrorMessage = VALIDATION_MESSGAE.REQUIRED)]
        public int RoomTypeId { get; set; }

        [Display(Name = "BathRooms"), Required(ErrorMessage = VALIDATION_MESSGAE.REQUIRED)]
        public int BathRooms { get; set; }

        [Display(Name = "SquareFeet"), StringLength(50, ErrorMessage = VALIDATION_MESSGAE.MAXLENGTH)]
        public string SquareFeet { get; set; }

        [Display(Name = "BasicCost")]
        public decimal? BasicCost { get; set; }

        [Display(Name = "AdditonalBeds"), Required(ErrorMessage = VALIDATION_MESSGAE.REQUIRED)]
        public int AdditonalBeds { get; set; }

        [Display(Name = "AdditonalBedCost")]
        public decimal? AdditonalBedCost { get; set; }
        #endregion Properties

    }
}
