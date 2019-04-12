using DataLibrary.DataAccess;
using DataLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.BusinessLogic
{
    public class ProjectProcessor
    {
        public static int CreateProject(string fname, string lname, string dob, int ssn)
        {
            ProjectModel data = new ProjectModel
            {
                
            };
            string sql = @"insert into PM.Project (Employee_ID, Fname, Lname, Date_Of_Birth, Ssn) 
                            values (0, @Fname, @Lname, @Date_Of_Birth, @Ssn);";
            return SqlDataAccess.SaveData(sql, data);
        }

        // Loads all of the employees back into DataLibrary.Models.EmployeeModel
        public static List<ProjectModel> LoadProjects()
        {
            //string sql = @"select Employee_ID, Fname, Lname, Date_Of_Birth, Ssn from PM.Employee;";
            string sql = "select * from PM.Project;";
            return SqlDataAccess.LoadData<ProjectModel>(sql);
        }
    }
}
