using System.ComponentModel.DataAnnotations;

namespace KC.HMS.Core.Validation
{
    public class DateRangeAttribute : ValidationAttribute
    {
        public string StartDate { get; set; }
        public string EndDate { get; set; }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {

            var memberProperty = validationContext.ObjectType.GetProperty(validationContext.MemberName);
            var memberDate = (DateTime)memberProperty.GetValue(validationContext.ObjectInstance, null);

            var startDate = DateTime.Parse(StartDate);
            var endDate = DateTime.Parse(EndDate);

            if (memberDate < startDate || memberDate > endDate)
                return new ValidationResult(ErrorMessage); // if fail
            else
                return ValidationResult.Success; // if success

        }

    }
}
