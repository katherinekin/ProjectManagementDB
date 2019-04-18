﻿using DataLibrary.BusinessLogic;
using ProjManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using System.Web.Security;

namespace ProjManagement.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {
        // GET: Employee
        public ActionResult Index()
        {
            ViewBag.Message = "All employees";
            var data = EmployeeProcessor.LoadEmployees();
            List<EmployeeModel> employees = new List<EmployeeModel>();
            foreach (var row in data)
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
            return View(employees);
        }
        public ActionResult CreateEmployee()
        {            
            return View(new ViewEmployeeModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateEmployee(ViewEmployeeModel model)
        {
            ViewBag.Message = "Create a new employee profile.";
            if (!ModelState.IsValid)
            {
                return View(model);
            }  
            EmployeeProcessor.CreateEmployee(model.employee.FName, model.employee.LName, model.employee.DateOfBirth, model.employee.Ssn, 
                model.employee.Address, Int32.Parse(model.SelectedType), model.employee.Gender, model.employee.Salary, model.employee.StartDate, 
                model.SelectedDep, Int32.Parse(model.SelectedProf));
                    
            return RedirectToAction("SuccessEmployee", new { Model = model });
        }
        public ActionResult SuccessEmployee(EmployeeModel model)
        {
            return View(model);
        }
        // Map EmployeeModel from DataLibrary to EmployeeModel from ProjManagement if found, for Edit, Details, Delete
        public static EmployeeModel mapToModel(List<DataLibrary.Models.EmployeeModel> data)
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
        // GET: Employee/Details/5
        public ActionResult Details(int id)
        {
            var data = EmployeeProcessor.FindEmployee(id);
            if (data.Count == 0)
            {
                return HttpNotFound();
            }
            EmployeeModel found = mapToModel(data);
            ViewEmployeeModel viewFound = new ViewEmployeeModel
            {
                employee = found
            };
            return View(viewFound);
        }

        // GET: Employee/Edit/5
        public ActionResult Edit(int id)
        {
            var data = EmployeeProcessor.FindEmployee(id);
            if (data.Count == 0)
            {
                return HttpNotFound();
            }
            EmployeeModel found = mapToModel(data);
            ViewEmployeeModel viewFound = new ViewEmployeeModel
            {
                employee = found
            };
            return View(viewFound);
        }

        // POST: Employee/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, ViewEmployeeModel model)
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
            
            

            HashSet<KeyValuePair<string, string>> newModelHashSet = model.employee.setToPairs();
            
            newModelHashSet.ExceptWith(oldModelHashSet);
            foreach (var pair in newModelHashSet)
            {
                EmployeeProcessor.EditEmployee(pair, model.employee.EmployeeID);
            }
            return RedirectToAction("Details", new { id = model.employee.EmployeeID });
        }

        // GET: Employee/Delete/5
        public ActionResult Delete(int id)
        {
            var data = EmployeeProcessor.FindEmployee(id);
            if (data.Count == 0)
            {
                return HttpNotFound();
            }
            EmployeeModel found = mapToModel(data);
            return View(found);
        }

        // POST: Employee/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, EmployeeModel model)
        {
            try
            {
                EmployeeProcessor.DeleteEmployee(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View(model);
            }
        }
        //displays list of projects the employee is involved in
        //return View(model);
        
        public ActionResult ViewProjects(int id)
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
                        Completion_Date = row.Completion_Date,
                        Collaborators = row.Collaborators,
                        Pstatus = row.Pstatus,
                        EmployeeID = id
                    });
                }
                if (projects.Count==0)
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
                return RedirectToAction("Details", new { id = id });
            }            
        }
        public ActionResult Hours(int projectid, int employeeid)
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



        public ActionResult LogHours(int pid, int eid)
        {
            var model = new Models.ActivitiesModel {
                AEmployee_ID = eid,
                AProject_ID = pid,
                Description = null,
                Weekly_Hours = 0,
                Week_Date = null };
            
            return View(model);
        }

        [HttpPost]
        public ActionResult Success(ActivitiesModel data)
        {
            EmployeeProcessor.NewHours(data.AEmployee_ID, data.AProject_ID, data.Description, data.Weekly_Hours, data.Week_Date);
            ViewData["Pname"] = 0;
            return View();
        }
    }
}