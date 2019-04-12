using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjManagement.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "You must enter a username")]
        [Display(Name = "Username")]
        public string Username { get; set; }


        [Required(ErrorMessage = "You must enter a password")]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "Password is too long.")]
        public string Password { get; set; }
    }
}