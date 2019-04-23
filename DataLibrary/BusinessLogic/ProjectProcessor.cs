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
        public static int CreateProject(int projectid, string PName1, string PDName1, string Client1, string PDescription1, 
            string Deliverables1, DateTime Open_Date1, DateTime Close_Date1, DateTime Completion_Date1, string Collaborators1, int Pstatus1)
        {
            ProjectModel data = new ProjectModel
            {
                Project_ID = projectid,
                Pname = PName1,
                PDname = PDName1,
                Client = Client1,
                Pdescription = PDescription1,
                Deliverables = Deliverables1,
                Open_Date = Open_Date1,
                Close_Date = Close_Date1,
                Completion_Date = Completion_Date1,
                Collaborators = Collaborators1,
                Pstatus = Pstatus1

            };
            string sql = @"insert into PM.Project (Project_ID, Pname, PDname, Client,
                            Pdescription, Deliverables, Open_Date, Close_Date, Completion_Date, Collaborators, Pstatus) 
                            values (@Project_ID, @Pname, @PDname, @Client, @Pdescription,  @Deliverables, @Open_Date,
                                    @Close_Date, @Completion_Date, @Collaborators, @Pstatus);";
            return SqlDataAccess.SaveData(sql, data);
        }

        // Loads all of the employees back into DataLibrary.Models.EmployeeModel
        public static List<ProjectModel> LoadProjects()
        {
            //string sql = @"select Employee_ID, Fname, Lname, Date_Of_Birth, Ssn from PM.Employee;";
            string sql = "select * from PM.Project;";
            return SqlDataAccess.LoadData<ProjectModel>(sql);
        }
        // Delete Project -----------------------
        public static int DeleteProject(int projectid)
        {
            ProjectModel data = new ProjectModel
            {
                Project_ID = projectid
            };
            string sql = @"delete from PM.Project where Project_ID = @Project_ID;";
            return SqlDataAccess.SaveData(sql, data);
        }
        //-----------------------------------------------------------------------------------------Edit Project
        public static int EditProject(KeyValuePair<string, string> pair, int id)
        {
            
            PColumnModel data;

            string sql = "";
            int PsetToNum = 0;

            // Tries to convert value as an integer
            if (Int32.TryParse(pair.Value, out PsetToNum))
            {
                data = new PColumnModel()
                {
                    Project_ID = id,
                    IntValue = PsetToNum
                };
                sql = @"update pm.project
                set " + pair.Key + " = @IntValue where Project_ID = @Project_ID;";
                return SqlDataAccess.SaveData(sql, data);
            }
            data = new PColumnModel()
            {
                Project_ID = id,
                StringValue = pair.Value
            };
            sql = @"update pm.project
            set " + pair.Key + " = @StringValue where Project_ID = @Project_ID;";

            return SqlDataAccess.SaveData(sql, data);
        }        
        // FInd project----------------------------------
        public static List<ProjectModel> FindProject(int projectid)
        {
            var list = LoadProjects();
            List<ProjectModel> found = new List<ProjectModel>();
            foreach(ProjectModel item in list)
            {
                if (item.Project_ID==projectid)
                {
                    found.Add(item);
                }
            }
            return (found);
        }
        public static string getProjectName(int projectid)
        {
            ProjectModel data = new ProjectModel
            {
                Project_ID = projectid
            };            
            string sql = "select Pname from pm.project where Project_ID = @Project_ID;";
            return SqlDataAccess.LoadData<string>(sql, data)[0];
        }
        public static List<ProjectModel> FindProjectsByEmployee(int employeeid)
        {
            EmployeeModel data = new EmployeeModel
            {
                Employee_ID = employeeid
            };
            string sql = @"select * from pm.project where Pname in 
                            (select distinct Pname from PM.Project, PM. PROJECT_EMPLOYEES, PM.EMPLOYEE
                            where EProject_ID = Project_ID and PEmployee_ID = @Employee_ID);";
            
            return SqlDataAccess.LoadData<ProjectModel>(sql, data);
        }
        // Add and Delete employees
        public static int AddEmployeeToProject(int employeeid, int projectid)
        {
            EmployeeModel data = new EmployeeModel
            {
                Employee_ID = employeeid,
                ProjectID = projectid
            };
            string sql = @"insert into pm.project_employees (EProject_ID, PEmployee_ID, Role_In_Project)
                            values (@ProjectID, @Employee_ID, 'default');";
            return SqlDataAccess.SaveData<EmployeeModel>(sql, data);
        }
        public static int DeleteEmployeeFromProject(int employeeid, int projectid)
        {
            EmployeeModel data = new EmployeeModel
            {
                Employee_ID = employeeid,
                ProjectID = projectid
            };
            string sql = @"delete from pm.project_employees where PEmployee_ID = @Employee_ID and EProject_ID = @ProjectID;";
            return SqlDataAccess.SaveData<EmployeeModel>(sql, data);
        }
        public static List<PStatusModel> LoadPStatuses()
        {
            string sql = "select * from PM.Project_Status;";
            return SqlDataAccess.LoadData<PStatusModel>(sql);
        }
        public static int getTotalEmployees(int projectid)
        {
            EmployeeModel data = new EmployeeModel
            {
                ProjectID = projectid
            };
            string sql = @"select distinct PEmployee_ID from pm.project_employees where eproject_id = @ProjectID;";
            return SqlDataAccess.LoadData<EmployeeModel>(sql, data).Count;
        }
        public static List<string> getAllDepartments(int projectid)
        {
            ProjectModel data = new ProjectModel
            {
                Project_ID = projectid
            };
            string sql = @"select distinct EDname from  PM.Employee, PM.Project_Employees
                    where EProject_ID = @Project_ID and Employee_ID = PEmployee_ID;";
            return SqlDataAccess.LoadData<string>(sql, data);
        }
        // Get open and complete dates for a given project
        public static List<DateTime> getProjectDates(int projectid)
        {
            ProjectModel data = new ProjectModel
            {
                Project_ID = projectid
            };
            List<DateTime> list = new List<DateTime>();
            string sql = @"select Open_Date from  PM.Project where Project_ID = @Project_ID;";
            list.Add(SqlDataAccess.LoadData<DateTime>(sql, data)[0]);
            sql = @"select Completion_Date from  PM.Project where Project_ID = @Project_ID;";
            list.Add(SqlDataAccess.LoadData<DateTime>(sql, data)[0]);
            return list;
        }
    }
}
