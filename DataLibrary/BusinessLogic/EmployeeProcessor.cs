﻿using DataLibrary.DataAccess;
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
                
            };
            string sql = @"insert into PM.Employee (Employee_ID, Fname, Lname, Date_Of_Birth, Ssn, 
                            Type, Start_Date, Estatus, EDname, Profession, Super_ssn) 
                            values (0, @Fname, @Lname, @Date_Of_Birth, @Ssn, @Type, @Start_Date, 1, @EDname, @Profession, 377093932);";
            return SqlDataAccess.SaveData(sql, data);
        }
        // Loads all of the employees back into DataLibrary.Models.EmployeeModel
        public static List<EmployeeModel> LoadEmployees()
        {
            //string sql = @"select Employee_ID, Fname, Lname, Date_Of_Birth, Ssn from PM.Employee;";
            string sql = "select * from PM.Employee;";
            return SqlDataAccess.LoadData<EmployeeModel>(sql);
        }
        public static int EditEmployee(KeyValuePair<string,string> pair, int id)
        {
            ColumnModel data;
            int setToNum = 0;
            string sql = "";
            // Tries to convert value as an integer
            if (Int32.TryParse(pair.Value, out setToNum))
            {
                data = new ColumnModel()
                {
                    Employee_ID = id,
                    IntValue = setToNum
                };
                sql = @"update pm.employee
                set "+pair.Key+" = @IntValue where Employee_ID = @Employee_ID;";
                return SqlDataAccess.SaveData(sql, data);
            }
            data = new ColumnModel()
            {
                Employee_ID = id,
                StringValue = pair.Value
            };
            sql = @"update pm.employee
            set "+pair.Key+" = @StringValue where Employee_ID = @Employee_ID;";
              
            return SqlDataAccess.SaveData(sql, data);
        }
        
        public static int DeleteEmployee(int employeeid)
        {
            EmployeeModel data = new EmployeeModel
            {
                Employee_ID = employeeid
            };
            string sql = @"delete from PM.Employee where Employee_ID = @Employee_ID;";
            return SqlDataAccess.SaveData(sql, data);
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
        public static String getManagerName(int superssn)
        {            
            EmployeeModel data = new EmployeeModel
            {
                Super_Ssn = superssn
            };
            try
            {
                string sql = @"select Fname, Lname from PM.Employee where Ssn = @Super_Ssn;";
                var found = SqlDataAccess.LoadData<EmployeeModel>(sql, data);
                return found[0].Fname + " " + found[0].Lname;                
            }
            catch
            {
                return "No manager found";
            }
        }
        public static List<EmployeeModel> FindEmployeesByProject(int projectid)
        {
            ProjectModel data = new ProjectModel
            {
                Project_ID = projectid                
            };
            string sql = @"select * from pm.employee where Employee_ID in 
                            (select distinct Employee_ID from PM.Project, PM.PROJECT_EMPLOYEES, PM.EMPLOYEE
                            where EProject_ID = @Project_ID and PEmployee_ID = Employee_ID);";

            return SqlDataAccess.LoadData<EmployeeModel>(sql, data);
        }
    }
}