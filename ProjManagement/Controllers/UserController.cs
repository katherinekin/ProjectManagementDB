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
    [OutputCache(NoStore = true, Duration = 0)]
    public class UserController : Controller
    {
        public EmployeeModel mapToModel(List<DataLibrary.Models.EmployeeModel> data)
        {
            List<EmployeeModel> foundEmployee = new List<EmployeeModel>();
            foreach (var row in data)
            {
                foundEmployee.Add(new EmployeeModel
                {
                    EmployeeID = row.Employee_ID,
                    FName = row.Fname,
                    LName = row.Lname,
                    DateOfBirth = row.Date_Of_Birth,
                    Ssn = row.Ssn,
                    Address = row.Address,
                    Type = row.Type,
                    Gender = row.Gender,
                    Salary = row.Salary,
                    StartDate = row.Start_Date,
                    Estatus = row.Estatus,
                    EDname = row.EDname,
                    Profession = row.Profession,
                    SuperSsn = row.Super_Ssn,
                    SuperName = EmployeeProcessor.getManagerName(row.Super_Ssn)
                });
            }
            return foundEmployee[0];
        }
        // GET: User
        public ActionResult Index(int id)
        {
            var data = EmployeeProcessor.FindEmployee(id);
            
            foreach (DataLibrary.Models.EmployeeModel single in data)
            {
                ViewData["name"] = single.Fname + " " + single.Lname;
                ViewData["id"] = int.Parse(User.Identity.Name);
                ViewData["role"] = "Employee";
            }

            if (data.Count == 0)
            {
                return HttpNotFound();
            }
            EmployeeModel found = mapToModel(data);
            return View(found);
        }
        //needs testing
        public ActionResult MyProfile(int id)
        {
            var data = EmployeeProcessor.FindEmployee(id);
            if (data.Count == 0)
            {
                return HttpNotFound();
            }
            EmployeeModel found = mapToModel(data);
            return View(found);
        }

        public ActionResult LogHours(int id)
        {
            var data = EmployeeProcessor.FindEmployee(id);
            if (data.Count == 0)
            {
                return HttpNotFound();
            }
            EmployeeModel found = mapToModel(data);
            return View(found);
        }

        public ActionResult MyProjects(int id)
        {
            try
            {
                var data = ProjectProcessor.FindProjectsByEmployee(id);
                List<ProjectModel> projects = new List<ProjectModel>();
                foreach (var row in data)
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
                        Completion_Date = row.Close_Date,
                        Collaborators = row.Close_Date,
                        Pstatus = row.Pstatus,
                        EmployeeID = id
                    });
                }
                if (projects.Count == 0)
                {
                    projects.Add(new ProjectModel
                    {
                        EmployeeID = id
                    });
                }
                return View(projects);
            }
            catch
            {
                return RedirectToAction("Index", new { id = id });
            }
        }
    }
}