

using KC.HMS.Core.Models;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace KC.HMS.Services.ViewModels
{
    public class ApplicationUserViewModel : IdentityUser
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public List<RefreshToken> RefreshTokens { get; set; }
    }
}
