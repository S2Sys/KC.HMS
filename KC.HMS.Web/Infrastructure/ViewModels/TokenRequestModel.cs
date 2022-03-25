using System.ComponentModel.DataAnnotations;

namespace KC.HMS.Web.Infrastructure.ViewModels
{
    public class TokenRequestModel
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
