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
        public static int CreateEmployee(string fname, string lname, string dob, int ssn, string address,
            int type, string gender, double salary, string start, string edname, int profession)
        {
            EmployeeModel data = new EmployeeModel
            {
                Fname = fname,
                Lname = lname,
                Date_Of_Birth = dob,
                Ssn = ssn,
                Address = address,
                Type = type,
                Gender = gender, 
                Salary = salary,
                Start_Date = start,
                Estatus = 0,
                EDname = edname,
                Profession = profession
                
            };
            string sql = @"insert into PM.Employee (Employee_ID, Fname, Lname, Date_Of_Birth, Ssn, Address,
                            Type, Gender, Salary, Start_Date, Estatus, EDname, Profession, Super_ssn) 
                            values (0, @Fname, @Lname, @Date_Of_Birth, @Ssn, @Address, @Type, @Gender, @Salary, 
                            @Start_Date, 1, @EDname, @Profession, 377093932);";
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
        //------------------------------
        public static List<EmployeeModel> FindEmployeesNotInProject(int projectid)
        {
            ProjectModel data = new ProjectModel
            {
                Project_ID = projectid
            };
            string sql = @"select * from pm.employee where Employee_ID not in 
                            (select distinct Employee_ID from PM.Project, PM.PROJECT_EMPLOYEES, PM.EMPLOYEE
                            where EProject_ID = @Project_ID and PEmployee_ID = Employee_ID);";

            return SqlDataAccess.LoadData<EmployeeModel>(sql, data);
        }
        public static List<DepartmentModel> LoadDepartmentNames()
        {
            string sql = "select Dname from PM.Department;";
            return SqlDataAccess.LoadData<DepartmentModel>(sql);
        }
        public static List<EProfessionModel> LoadProfessions()
        {
            string sql = "select * from PM.Employee_Profession;";
            return SqlDataAccess.LoadData<EProfessionModel>(sql);
        }
        public static List<ETypeModel> LoadTypes()
        {
            string sql = "select * from PM.Employee_Type;";
            return SqlDataAccess.LoadData<ETypeModel>(sql);
        }
        public static List<EStatusModel> LoadStatuses()
        {
            string sql = "select * from PM.Employee_Status;";
            return SqlDataAccess.LoadData<EStatusModel>(sql);
        }
        public static List<ActivitiesModel> loadHoursForEmployee(int projectid, int employeeid)
        {
            ActivitiesModel data = new ActivitiesModel
            {
                AProject_ID = projectid,
                AEmployee_ID = employeeid
            };
            string sql = @"select * from pm.activities where AProject_ID = @AProject_ID and AEmployee_ID = @AEmployee_ID;";

            return SqlDataAccess.LoadData<ActivitiesModel>(sql, data);
        }

        public static int NewHours(int a1, int a2, string a3, double a4, string a5)
        {
            ActivitiesModel data = new ActivitiesModel
            {
                AEmployee_ID = a1,
                AProject_ID = a2,
                Description = a3,
                Weekly_Hours = a4,
                Week_Date = a5
            };
            string sql = @"Insert Into PM.ACTIVITIES VALUES (@AEmployee_ID, @AProject_ID, @Description, @Weekly_Hours, @Week_Date);";

            return SqlDataAccess.SaveData<ActivitiesModel>(sql, data);
        }
    }
}
