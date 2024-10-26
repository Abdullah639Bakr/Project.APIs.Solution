using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Core.Dtos.Auth
{
    public class LoginDto
    {
        [Required(ErrorMessage = "Email Is Requred")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password Is Requred")]
        public string Password { get; set; }
    }
}
