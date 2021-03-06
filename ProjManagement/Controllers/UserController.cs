﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataLibrary.BusinessLogic;
using ProjManagement.Models;

namespace ProjManagement.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        //Copied from Project Controller, allows private detail access with less features via this controller
        public ProjectModel pToModel(List<DataLibrary.Models.ProjectModel> data)
        {
            List<ProjectModel> foundProject = new List<ProjectModel>();
            foreach (var row in data)
            {
                foundProject.Add(new ProjectModel
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
            return foundProject[0];
        }

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
            EmployeeModel found = mapToModel(data);
            ViewEmployeeModel viewFound = new ViewEmployeeModel
            {
                employee = found
            };
            return View(viewFound);
        }
        //needs testing
        public ActionResult UserProfile(int id)
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

        public ActionResult UserProjects(int id)
        {
            try
            {
                var data = ProjectProcessor.FindProjectsByEmployee(id);
                List<ViewProjectModel> projects = new List<ViewProjectModel>();
            foreach (var row in data)
            {
                projects.Add(
                    new ViewProjectModel
                    {
                        project = new ProjectModel
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
                        }
                });
            }
            return View(projects);
            }
            catch
            {
                return RedirectToAction("Index", new { id = id });
            }
        }

        public ActionResult UserProjectDetails(int id)
        {
            ViewData["id"] = int.Parse(User.Identity.Name);
            var data = ProjectProcessor.FindProject(id);
            if (data.Count == 0)
            {
                return HttpNotFound();
            }
            ProjectModel found = pToModel(data);
            ViewProjectModel viewFound = new ViewProjectModel
            {
                project = found
            };
            return View(viewFound);
        }

        public ActionResult UserHours(int projectid, int employeeid)
        {
            ViewData["pid"] = projectid;
            ViewData["eid"] = employeeid;
            var data = EmployeeProcessor.loadHoursForEmployee(projectid, employeeid);
            List<Models.ActivitiesModel> hours = new List<Models.ActivitiesModel>();
            foreach (var row in data)
            {
                hours.Add(new Models.ActivitiesModel
                {
                    AEmployee_ID = row.AEmployee_ID,
                    AProject_ID = row.AProject_ID,
                    Description = row.Description,
                    Weekly_Hours = row.Weekly_Hours,
                    Week_Date = row.Week_Date
                });
            }
            return View(hours);
        }

        public ActionResult UserLogMenu(int id)
        {
            var data = ProjectProcessor.FindProjectsByEmployee(id);
            List<string> Name = new List<string>();
            List<int> PID = new List<int>();

            foreach (var row in data)
            {
                Name.Add(row.Pname);
                PID.Add(row.Project_ID);
            }

            var dic = PID.Zip(Name, (k, v) => new { k, v })
              .ToDictionary(x => x.k, x => x.v);

            ViewBag.dic = dic;


            var model = new Models.ActivitiesModel
            {
                AEmployee_ID = id,
                AProject_ID = 0,
                Description = null,
                Weekly_Hours = 0,
                Week_Date = null
            };

            return View(model);
        }

        public ActionResult LogHoursUser(int pid, int eid)
        {
            var model = new Models.ActivitiesModel
            {
                AEmployee_ID = eid,
                AProject_ID = pid,
                Description = null,
                Weekly_Hours = 0,
                Week_Date = null
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult LogHoursUserSuccess(ActivitiesModel data)
        {
            if (EmployeeProcessor.NewHours(data.AEmployee_ID, data.AProject_ID, data.Description, data.Weekly_Hours, data.Week_Date) > 0)
            {
                return View(data);
            }
            else
            {
                return RedirectToAction("LogHoursUserFail", data);
            }
        }

        public ActionResult LogHoursUserFail(ActivitiesModel data)
        {
            return View(data);
        }

        public ActionResult UserProjectCoworkers(int id)
        {
            int eid = int.Parse(User.Identity.Name);
            ViewData["id"] = eid;
            try
            {
                var data = EmployeeProcessor.FindEmployeesByProject(id);
                List<EmployeeModel> employees = new List<EmployeeModel>();
                foreach (var row in data)
                {
                    if (row.Employee_ID == eid)
                    {
                        continue;
                    }
                    employees.Add(new EmployeeModel
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
                        SuperName = EmployeeProcessor.getManagerName(row.Super_Ssn),
                        ProjectID = id
                    });
                }
                if (employees.Count == 0)
                {
                    employees.Add(new EmployeeModel
                    {
                        ProjectID = id
                    });
                }
                return View(employees);
            }
            catch
            {
                return RedirectToAction("UserProjectDetails", new { id = id });
            }
        }

        public ActionResult UserEdit(int id)
        {
            var data = EmployeeProcessor.FindEmployee(id);
            if (data.Count == 0)
            {
                return HttpNotFound();
            }
            EmployeeModel found = mapToModel(data);
            ViewEmployeeModel viewFound = new ViewEmployeeModel
            {
                employee = found,
                SelectedDep = found.EDname,
                SelectedManager = found.SuperSsn.ToString(),
                SelectedProf = found.Profession.ToString(),
                SelectedStatus = found.Estatus.ToString(),
                SelectedType = found.Type.ToString()
            };
            if (found.DateOfBirth.ToShortDateString() != "")
            {
                viewFound.DOB = found.DateOfBirth;
            }
            if (found.StartDate.ToShortDateString() != "")
            {
                viewFound.SD = found.StartDate;
            }
            if (User.IsInRole("Admin"))
            {
                var managers = LoginProcessor.LoadManagerList();
                viewFound.ManagerSelectList = managers.Select(x => new SelectListItem()
                {
                    Text = x.Fname + " " + x.Lname,
                    Value = x.Ssn.ToString()
                });
            }
            return View(viewFound);
        }

        [HttpPost]
        public ActionResult UserEdit(int id, ViewEmployeeModel model)
        {
            var data = EmployeeProcessor.FindEmployee(id);
            EmployeeModel oldModel = mapToModel(data);
            HashSet<KeyValuePair<string, string>> oldModelHashSet = oldModel.setToPairs();
            //returns a HashSet of the old model only if has not been set

            if (!ModelState.IsValid)
            {
                return View(model);
            }
            // Set status, type, profession, department variables
            try
            {
                model.employee.Estatus = Int32.Parse(model.SelectedStatus);
            }
            catch
            {
                model.employee.Estatus = oldModel.Estatus;
            }
            try
            {
                model.employee.Type = Int32.Parse(model.SelectedType);
            }
            catch
            {
                model.employee.Type = oldModel.Estatus;
            }
            try
            {
                model.employee.Profession = Int32.Parse(model.SelectedProf);
            }
            catch
            {
                model.employee.Profession = oldModel.Profession;
            }
            try
            {
                model.employee.EDname = model.SelectedDep.ToString();
            }
            catch
            {
                model.employee.EDname = oldModel.EDname;
            }
            try
            {
                model.employee.DateOfBirth = model.DOB;
            }
            catch
            {
                model.employee.DateOfBirth = oldModel.DateOfBirth;
            }
            try
            {
                model.employee.StartDate = model.SD;
            }
            catch
            {
                model.employee.StartDate = oldModel.StartDate;
            }
            try
            {
                model.employee.SuperSsn = int.Parse(model.SelectedManager);
            }
            catch
            {
                model.employee.SuperSsn = oldModel.SuperSsn;
            }

            HashSet<KeyValuePair<string, string>> newModelHashSet = model.employee.setToPairs();

            newModelHashSet.ExceptWith(oldModelHashSet);
            foreach (var pair in newModelHashSet)
            {
                EmployeeProcessor.EditEmployee(pair, model.employee.EmployeeID);
            }
            return RedirectToAction("Index", new { id = id });
        }
    }
}