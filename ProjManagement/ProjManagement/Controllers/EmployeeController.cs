using DataLibrary.BusinessLogic;
using ProjManagement.Models;
using System.Collections.Generic;
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
            EmployeeProcessor.CreateEmployee(model.FName, model.LName, model.DateOfBirth, model.Ssn,
                model.Type, model.StartDate, model.EDname, model.Profession);

            //var data = EmployeeProcessor.LoadEmployees();

            //Add a way to get the details page for the new employee here, or success page
            return RedirectToAction("SuccessEmployee", new { Model = model });
        }
        public ActionResult SuccessEmployee(EmployeeModel model)
        {
            return View(model);
        }
        // Map EmployeeModel from DataLibrary to EmployeeModel from ProjManagement if found, for Edit, Details, Delete
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
                    StartDate = row.Start_Date,
                    Estatus = row.Estatus,
                    EDname = row.EDname,
                    Profession = row.Profession,
                    SuperSsn = row.Super_ssn
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
            return View(found);
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
            return View(found);
        }
        
        // POST: Employee/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, EmployeeModel model)
        {
            var data = EmployeeProcessor.FindEmployee(id);
            EmployeeModel oldModel = mapToModel(data);
            HashSet<KeyValuePair<string, string>> oldModelHashSet = oldModel.setToPairs();  
            //returns a HashSet of the old model only if has not been set
                
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            HashSet<KeyValuePair<string,string>> newModelHashSet = model.setToPairs();
            newModelHashSet.ExceptWith(oldModelHashSet);
            foreach (var pair in newModelHashSet)
            {
                EmployeeProcessor.EditEmployee(pair, model.EmployeeID);
            }
            return RedirectToAction("Details", new { id = model.EmployeeID });   
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
    }
}
