using KC.HMS.Core.Abstracts;

namespace KC.HMS.Web.Infrastructure.Models
{
    public class SeedUser: ValidationEntity<SeedUser>, IEntity
    {

        public const string DefaultPassword = "Pa$$w0rd.";
        public string FirstName { get; set; } = "FName";
        public string LastName { get; set; } = "LName";

        public string Username { get; set; } = "";
        public string Email { get; set; } = "";
        public string Password { get; set; } = DefaultPassword;
        public Roles Role { get; set; } = Roles.User;

        public ApplicationUser ToDTO()
        {
            return new ApplicationUser { 
                FirstName= Username, 
                LastName= Username,
                UserName = Username +"@email.com", 
                Email = this.Email + "@email.com", 
                EmailConfirmed = true, 
                PhoneNumberConfirmed = true 
            };
        }

        public override ValidationResult Validate(SeedUser source)
        {
            throw new NotImplementedException();
        }
    }
}
