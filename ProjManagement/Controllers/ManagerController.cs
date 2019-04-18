using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataLibrary.BusinessLogic;
using ProjManagement.Models;

namespace ProjManagement.Controllers
{
    [Authorize]
    public class ManagerController : Controller
    {
        // GET: Manager
        public ActionResult Index()
        {            
            return View();
        }
        // GET: Employee
        public ActionResult ManagerEmployees()
        {
            var ssn = EmployeeProcessor.FindEmployee(int.Parse(User.Identity.Name)).First().Ssn;
            ViewBag.Message = "My employees";
            var data = EmployeeProcessor.LoadEmployees();
            List<EmployeeModel> employees = new List<EmployeeModel>();
            foreach (var row in data)
            {
                if (row.Super_Ssn == ssn)
                {
                    employees.Add(new EmployeeModel
                    {
                        EmployeeID = row.Employee_ID,
                        FName = row.Fname,
                        LName = row.Lname,
                        DateOfBirth = row.Date_Of_Birth,
                        Ssn = row.Ssn
                    });
                }
            }
            return View(employees);
        }
        public ActionResult ManagerProjects()
        {
            var dep = EmployeeProcessor.FindEmployee(int.Parse(User.Identity.Name)).First().EDname;
            ViewBag.Message = "My projects";
            var data = ProjectProcessor.LoadProjects();
            List<ProjectModel> projects = new List<ProjectModel>();
            foreach (var row in data)
            {
                if (dep == row.PDname)
                {
                    projects.Add(new ProjectModel
                    {
                    ProjectID = row.Project_ID,
                    PName = row.Pname,
                    PDName = row.PDname,
                    Client = row.Client,
                    PDescription = row.Pdescription,
                    Deliverables = row.Deliverables,
                    Open_Date = row.Open_Date,
                    Close_Date = row.Close_Date,
                    Completion_Date = row.Completion_Date,
                    Collaborators = row.Collaborators,
                    Pstatus = row.Pstatus
                    });
                }
            }
            return View(projects);
            
        }
    }
}
