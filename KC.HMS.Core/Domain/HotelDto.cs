
using KC.HMS.Core.Settings;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;
namespace KC.HMS.Core.Domain
{
    /// <summary>
    /// Represents a Hotels entity.
    /// NOTE: This class is used to keep Hotels information.
    /// </summary>
    /// 

    public partial class HotelDto 
    {
        #region Constructor

        #endregion Constructor

        #region Properties
        [Key]
        [Display(Name = "Hotel ID")]
        public int HotelID { get; set; }

        [Display(Name = "Name"),
            Required(ErrorMessage = VALIDATION_MESSGAE.REQUIRED),
            StringLength(100, ErrorMessage = VALIDATION_MESSGAE.MAXLENGTH)]
        public string Name { get; set; }

        [Display(Name = "Location"),
            Required(ErrorMessage = VALIDATION_MESSGAE.REQUIRED),
            StringLength(100, ErrorMessage = VALIDATION_MESSGAE.MAXLENGTH)]
        public string Location { get; set; }

        [Display(Name = "Description"),
            StringLength(8000, ErrorMessage = VALIDATION_MESSGAE.MAXLENGTH)]
        public string Description { get; set; }

        [Display(Name = "Have Swimming Pool")]
        public bool? HaveSwimmingPool { get; set; }

        [Display(Name = "Have Free WIFI")]
        public bool? HaveFreeWIFI { get; set; }

        [Display(Name = "Have Free Parking")]
        public bool? HaveFreeParking { get; set; }

        [Display(Name = "Have Room Service")]
        public bool? HaveRoomService { get; set; }

        [Display(Name = "Have Restaurant")]
        public bool? HaveRestaurant { get; set; }

        [Display(Name = "Have Bar")]
        public bool? HaveBar { get; set; }

        [Display(Name = "Have Free BreakFast")]
        public bool? HaveFreeBreakFast { get; set; }

        [Display(Name = "Have Air Conditioning")]
        public bool? HaveAirConditioning { get; set; }

        [Display(Name = "Have Power Backup")]
        public bool? HavePowerBackup { get; set; }

        [Display(Name = "Have Doctor OnCall")]
        public bool? HaveDoctorOnCall { get; set; }

        [Display(Name = "Bulk Booking POC"),
            StringLength(100, ErrorMessage = VALIDATION_MESSGAE.MAXLENGTH)]
        public string BulkBookingPOC { get; set; }

        [Display(Name = "POC Person"), 
            StringLength(50, ErrorMessage = VALIDATION_MESSGAE.MAXLENGTH)]
        public string POCPerson { get; set; }

        [Display(Name = "PO CPhone"), 
            StringLength(50, ErrorMessage = VALIDATION_MESSGAE.MAXLENGTH)]
        public string POCPhone { get; set; }

        public ModelStateDictionary Validate(ModelStateDictionary modelState)
        {
            return modelState;
        }
        #endregion Properties

    }


}
