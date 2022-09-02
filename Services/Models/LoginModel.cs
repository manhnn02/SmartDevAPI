using System;
using System.ComponentModel.DataAnnotations;

namespace Services.Models
{
    public class LoginModel
    {
        public LoginModel()
        {
        }

        [Required]
        [MaxLength(50)]
        public string UserName { get; set; }

        [Required]
        [MaxLength(250)]
        public string Password { get; set; }
    }
}

