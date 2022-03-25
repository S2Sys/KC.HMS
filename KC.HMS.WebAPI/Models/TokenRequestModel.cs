﻿using System.ComponentModel.DataAnnotations;

namespace KC.HMS.WebAPI.Models
{
    public class TokenRequestModel
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }

}
