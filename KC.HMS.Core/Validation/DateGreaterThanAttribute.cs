using KC.HMS.Core.Extentions;
using System.ComponentModel.DataAnnotations;

namespace KC.HMS.Core.Validation
{
    public class DateGreaterThanAttribute : ValidationAttribute
    {
        public string DateFrom { get; set; }


        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var startDateProperty = validationContext.ObjectType.GetProperty(DateFrom);
            var endDateProperty = validationContext.ObjectType.GetProperty(validationContext.MemberName);



            var startDateIn = (string)startDateProperty.GetValue(validationContext.ObjectInstance, null);
            var endDateIn = (string)endDateProperty.GetValue(validationContext.ObjectInstance, null);
            if (string.IsNullOrEmpty(startDateIn) && string.IsNullOrEmpty(endDateIn))
                return ValidationResult.Success;
            var startDate = DateTime.Parse(startDateIn);
            var endDate = DateTime.Parse(endDateIn);

            if (startDate > endDate)
                if (ErrorMessage.Length > 0)
                    return new ValidationResult(ErrorMessage); // if fail
                else
                {
                    var startDisplayName = startDateProperty.GetDisplayName();
                    var endDisplayName = startDateProperty.GetDisplayName();
                    return new ValidationResult(string.Format("{0} can be less than {1}", startDisplayName, endDisplayName));
                }

            else
                return ValidationResult.Success; // if success

        }

    }
}
