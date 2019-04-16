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
            string Deliverables1, string Open_Date1, string Close_Date1, string Completion_Date1, string Collaborators1, int Pstatus1)
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
        //--------------------------------------------------------Edit Project
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
    }
}
