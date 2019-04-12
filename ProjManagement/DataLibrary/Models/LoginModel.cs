using DataLibrary.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DataLibrary.Models
{
    public class AuthVM
    {
        public LoginModel LoginForm { get; set; }
        public LoginModelForgot RecoverForm { get; set; }
    }
    public class LoginModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class LoginModelForgot
    {
        public int EmployeeID { get; set; }
        public string NewUsername { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmUsername { get; set; }
        public string ConfirmPass { get; set; }
    }
}
