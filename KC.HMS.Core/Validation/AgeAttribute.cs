using System.ComponentModel.DataAnnotations;

namespace KC.HMS.Core.Validation
{

    public class AgeAttribute : ValidationAttribute
    {
        public int Age { get; set; } = 18;
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {

            var memberProperty = validationContext.ObjectType.GetProperty(validationContext.MemberName);
            var memberValue = (DateTime)memberProperty.GetValue(validationContext.ObjectInstance, null);


            if (memberValue.AddYears(18) < DateTime.Now)
                return new ValidationResult(ErrorMessage); // if fail
            else
                return ValidationResult.Success; // if success

        }

    }
}
