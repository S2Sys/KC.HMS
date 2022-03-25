namespace KC.HMS.Core.Models
{

    public class SeedUser  
    {

        public const string DefaultPassword = "!QAZ1qaz";
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";

        public string Username { get; set; } = "";
        public string Email { get; set; } = "";
        public string Password { get; set; } = DefaultPassword;
        public RoleKind Role { get; set; } = RoleKind.Guest;

        public ApplicationUser ToDTO()
        {
            return new ApplicationUser
            {
                FirstName = Username,
                LastName = "Mr.",
                UserName = Username + "@test.com",
                Email = Username + "@test.com",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };
        }

        //public override ValidationResult Validate(SeedUser source)
        //{
        //    throw new NotImplementedException();
        //}


        //public override void Validate(SeedUser model)
        //{
        //    ValidationContext context = new ValidationContext(model, null, null);
        //    List<ValidationResult> validationResults = new List<ValidationResult>();
        //    bool valid = Validator.TryValidateObject(model, context, validationResults, true);

        //    if (!valid)
        //    {
        //        foreach (ValidationResult validationResult in
        //        validationResults)
        //        {
        //            Console.WriteLine("{0}",
        //            validationResult.ErrorMessage);
        //        }
        //    }
        //}
    }
}
