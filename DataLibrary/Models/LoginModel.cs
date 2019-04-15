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
        public int LEmployee_ID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        
    }
}
