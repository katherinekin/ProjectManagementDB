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
        //This method creates an employee setting not null values, and returns number of records affected
        public static int CreateEmployee(string fname, string lname, string dob, int ssn,
            int type, string start, string edname, int profession)
        {
            EmployeeModel data = new EmployeeModel
            {
                Fname = fname,
                Lname = lname,
                Date_Of_Birth = dob,
                Ssn = ssn,
                //Address = "", employee fills this out
                Type = type,
                //Gender = "", employee fills this out
                Start_Date = start,
                //Estatus = 0, always 1
                EDname = edname,
                Profession = profession
                //Super_ssn = superssn, always 32145678
        };
            string sql = @"insert into PM.Employee (Employee_ID, Fname, Lname, Date_Of_Birth, Ssn, 
                            Type, Start_Date, Estatus, EDname, Profession, Super_ssn) 
                            values (0, @Fname, @Lname, @Date_Of_Birth, @Ssn, @Type, @Start_Date, 1, @EDname, @Profession, 32145678);";
            return SqlDataAccess.SaveData(sql, data);
        }
        /*
        public static int EditEmployee(KeyValuePair<string,string> pair, int id)
        {
            string columnName = pair.Key;
            string newValue = pair.Value;
            string sql = "";
            if (newValue contains only numbers)
            {
                int newValueNum = newValue.toInteger();
                string sql = @"update pm.employee
                    set @columnName = @newValueNum where Employee_ID = @Employee_ID;";
            }
            else
            {
                string sql = @"update pm.employee
                    set @columnName = @newValue where Employee_ID = @Employee_ID;";
            }
            
            return SqlDataAccess.SaveData(sql, data);
        }
        */
        public static int DeleteEmployee(int employeeid)
        {
            EmployeeModel data = new EmployeeModel
            {
                Employee_ID = employeeid
            };
            string sql = @"delete from PM.Employee where Employee_ID = @Employee_ID;";
            return SqlDataAccess.SaveData(sql, data);
        }
        // Loads all of the employees back into DataLibrary.Models.EmployeeModel
        public static List<EmployeeModel> LoadEmployees()
        {
            //string sql = @"select Employee_ID, Fname, Lname, Date_Of_Birth, Ssn from PM.Employee;";
            string sql = "select * from PM.Employee;";
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
