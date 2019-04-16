using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjManagement.Models
{
    public class AuthVM
    {
        public LoginModel LoginForm { get; set; }
        public LoginModelForgot RecoverForm { get; set; }
    }

    public class LoginModel
    {
        public int LEmployee_ID { get; set; }

        [Required(ErrorMessage = "You must enter a username")]
        [Display(Name = "Username")]
        public string Username { get; set; }

        [Required(ErrorMessage = "You must enter a password")]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "Password is too long.")]
        public string Password { get; set; }

    }


    public class LoginModelForgot
    {
        [Required(ErrorMessage = "Please enter an ID")]
        [Display(Name = "Employee ID")]
        public int EmployeeID { get; set; }

        [Required(ErrorMessage = "You must enter a username")]
        [Display(Name = "Enter Username")]
        public string NewUsername { get; set; }

        [Required(ErrorMessage = "You must enter a password")]
        [Display(Name = "Enter Password")]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "Password is too long.")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "You must enter a username")]
        [Compare("NewUsername", ErrorMessage = "Usernames do not match")]
        [Display(Name = "Confirm username")]
        public string ConfirmUsername { get; set; }

        [Required(ErrorMessage = "You must enter a password")]
        [Compare("NewPassword", ErrorMessage = "Passwords do not match")]
        [Display(Name = "Confirm password")]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "Password is too long.")]
        public string ConfirmPass { get; set; }
    }
}
