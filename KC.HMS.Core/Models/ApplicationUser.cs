
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace KC.HMS.Core.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required(ErrorMessage = "First Name is required.")]

        [StringLength(50)]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last Name is required.")]
        [StringLength(50)]
        public string LastName { get; set; }
        public int UsernameChangeLimit { get; set; } = 10;
        public byte[]? ProfilePicture { get; set; }

        
        public List<RefreshToken> RefreshTokens { get; set; }
    }
}
