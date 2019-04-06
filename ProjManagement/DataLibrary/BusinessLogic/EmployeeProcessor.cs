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
        public static int CreateEmployee(string fname, string lname, string dob, int ssn)
        {
            EmployeeModel data = new EmployeeModel
            {
                Fname = fname,
                Lname = lname,
                Date_Of_Birth = dob,
                Ssn = ssn
            };
            string sql = @"insert into PM.Employee (Employee_ID, Fname, Lname, Date_Of_Birth, Ssn) 
                            values (0, @Fname, @Lname, cast (@DateOfBirth to datetime), @Ssn);";
            return SqlDataAccess.SaveData(sql, data);
        }
        
        public static int DeleteEmployee(int employeeid, string fname, string lname)
        {
            EmployeeModel data = new EmployeeModel
            {
                Employee_ID = employeeid,
                Fname = fname,
                Lname = lname
            };
        string sql = @"delete from PM.Employee 
                        where Employee_ID = @EmployeeID and Fname = @Fname and Lname = @Lname;";
            return SqlDataAccess.SaveData(sql, data);
        }
        // Loads all of the employees back into DataLibrary.Models.EmployeeModel
        public static List<EmployeeModel> LoadEmployees()
        {
            string sql = @"select Employee_ID, Fname, Lname, Ssn from PM.Employee;";
            return SqlDataAccess.LoadData<EmployeeModel>(sql);
        }

        // Assume there is only one item found per primary key, but returns empty list if not found
        public static List<EmployeeModel> FindEmployee(int employeeid)
        {
            var list = LoadEmployees();
            List<EmployeeModel> found = new List<EmployeeModel>();
            foreach(EmployeeModel item in list)
            {
                if(item.Employee_ID == employeeid)
                {
                    found.Add(item);
                }
            }
            return found;
        }
    }
}
