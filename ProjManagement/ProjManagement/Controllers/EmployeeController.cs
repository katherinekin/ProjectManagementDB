using DataLibrary.BusinessLogic;
using ProjManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ProjManagement.Controllers
{
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
                    //DateOfBirth = row.Date_Of_Birth,
                    Ssn = row.Ssn
                });
            }
            return View(employees);
        }
        public ActionResult CreateEmployee()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateEmployee(EmployeeModel model)
        {
            ViewBag.Message = "Create a new employee profile.";
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            EmployeeProcessor.CreateEmployee(
                model.FName, model.LName, model.DateOfBirth, model.Ssn);
            
            var data = EmployeeProcessor.LoadEmployees();
            /*
            foreach(var row in data)
            {
                if (model.FName == row.Fname && model.LName==row.Lname && model.Ssn==row.Ssn)
                {
                    model.EmployeeID = row.Employee_ID;
                }
            }
            */
            return RedirectToAction("Index");
        }

        // GET: Employee/Details/5
        public ActionResult Details(int id)
        {
            var data = EmployeeProcessor.FindEmployee(id);
            if (data.Count==0)
            {
                return HttpNotFound();
            }
            List<EmployeeModel> foundEmployee = new List<EmployeeModel>();
            foreach (var row in data)
            {
                foundEmployee.Add(new EmployeeModel
                {
                    EmployeeID = row.Employee_ID,
                    FName = row.Fname,
                    LName = row.Lname,
                    DateOfBirth = row.Date_Of_Birth,
                    Ssn = row.Ssn
                });
            }
            
            return View(foundEmployee[0]);
        }

        // GET: Employee/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Employee/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Employee/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Employee/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
