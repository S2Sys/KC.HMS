using System.ComponentModel.DataAnnotations;

namespace KC.HMS.WebAPI.Models
{
    public class AddRoleModel
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Role { get; set; }
    }
}
