using DataLibrary.DataAccess;
using DataLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Retrieves list of logins, checks if username exists, if username and pass matches, if manager or employee
namespace DataLibrary.BusinessLogic
{
    public static class LoginProcessor
    {
        // Edit an existing record in 
        public static int EditLogin(int employeeId, string username, string password)
        {
            LoginModelForgot data = new LoginModelForgot
            {
                EmployeeID = employeeId,
                NewUsername = username,
                NewPassword = password
            };
            string sql = @"update PM.Login 
                            set Username = @NewUsername, Password = @NewPassword
                            where LEmployee_Id = @EmployeeID;";
            return SqlDataAccess.SaveData(sql, data);
        }

        // Loads all of the employees back into DataLibrary.Models.EmployeeModel
        public static List<LoginModel> LoadLogins()
        {
            string sql = @"select *
                            from PM.Login;";
            return SqlDataAccess.LoadData<LoginModel>(sql);
        }
        
        // gets a list of manager ids from department
        public static List<int> LoadManagers()
        {
            string sql = @"select Manager_ID from pm.department";
            return SqlDataAccess.LoadData<int>(sql);
        }
    }
}
