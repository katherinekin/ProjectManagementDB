using DataLibrary.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DataLibrary.Models
{
    
    public class LoginModel
    {
        public int EmployeeID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
