using DataLibrary.DataAccess;
using DataLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.BusinessLogic
{
    public static class EmployeeProcessor
    {
        // Not nulls are employeeID, fname, lname, and ssn, but I also think DOB is important
        //This method creates an employee setting not null values, and returns number of records affected
        public static int CreateEmployee(int employeeId, string fname, string lname, string dob, int ssn)
        {
            EmployeeModel data = new EmployeeModel
            {
                EmployeeID = employeeId,
                Fname = fname,
                Lname = lname,
                DateOfBirth = dob,
                Ssn = ssn
            };
            string sql = @"insert into PM.Employee (EmployeeID, Fname, Lname, Date_Of_Birth, Ssn) 
                            values (@EmployeeID, @Fname, @Lname, @DateOfBirth, @Ssn);";
            return SqlDataAccess.SaveData(sql, data);
        }
        // Loads all of the employees back into DataLibrary.Models.EmployeeModel
        public static List<EmployeeModel> LoadEmployees()
        {
            string sql = @"select EmployeeId, Fname, Lname, Date_Of_Birth, Ssn
                            from PM.Employee;";
            return SqlDataAccess.LoadData<EmployeeModel>(sql);
        }
    }
}
