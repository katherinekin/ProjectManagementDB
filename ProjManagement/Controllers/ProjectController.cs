using ProjManagement.Models;
using DataLibrary.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjManagement.Controllers
{
    public class ProjectController : Controller
    {
        // GET: Project
        public ActionResult Index()
        {
            ViewBag.Message = "All projects";
            var data = ProjectProcessor.LoadProjects();
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
                    Pstatus = row.Pstatus
                });
            }
            return View(projects);
        }
        // bd TO FIND project
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
                    Completion_Date = row.Close_Date,
                    Collaborators = row.Close_Date,
                    Pstatus = row.Pstatus
                });
            }
            return foundProject[0];
        }

        // GET: Project/Details/5
        public ActionResult Details(int id)
        {
            var data = ProjectProcessor.FindProject(id);
            if (data.Count==0)
            {
                return HttpNotFound();
            }
            ProjectModel found = pToModel(data);
            return View(found);
        }

        // GET: Project/Create
        public ActionResult CreateProject()
        {
            return View();
        }

        // POST: Project/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateProject(ProjectModel collection)
        {
            ViewBag.Message = "Create a new project profile.";
            if (!ModelState.IsValid)
            {
                return View(collection);
            }
            ProjectProcessor.CreateProject(collection.ProjectID,
                  collection.PName, collection.PDName, collection.Client, collection.PDescription, collection.Deliverables, collection.Open_Date, collection.Close_Date,
                  collection.Completion_Date, collection.Collaborators, collection.Pstatus);
            var data = ProjectProcessor.LoadProjects();
            
            //Add a way to get the details page for the new employee here, or success page
            return RedirectToAction("SuccessProject", new { Model = collection });

        }
        public ActionResult SuccessProject(ProjectModel model)
        {
            return View(model);
        }

        // GET: Project/Edit/5--------------------------------------------------------------------------

        public ActionResult Edit(int id)
        {
            var data = ProjectProcessor.FindProject(id);
            if (data.Count == 0)
            {
                return HttpNotFound();
            }
            ProjectModel found = pToModel(data);
            return View(found);
        }

        // POST: project/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, ProjectModel model)
        {
            var data = ProjectProcessor.FindProject(id);
            ProjectModel oldModel = pToModel(data);

            HashSet<KeyValuePair<string, string>> oldModelHashSet = oldModel.PsetToPairs();

            //returns a HashSet of the old model only if has not been set

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            HashSet<KeyValuePair<string, string>> newModelHashSet = model.PsetToPairs();
            newModelHashSet.ExceptWith(oldModelHashSet);
            foreach (var pair in newModelHashSet)
            {
                ProjectProcessor.EditProject(pair, model.ProjectID);
            }
            return RedirectToAction("Details", new { id = model.ProjectID });
        }
        //--------------------------------------------------------------Edit not yet

        // GET: Project/Delete
        public ActionResult Delete(int id)
        {
            var data1= ProjectProcessor.FindProject(id);
            if (data1.Count == 0)
            {
                return HttpNotFound();
            }
            ProjectModel found = pToModel(data1);
            return View(found); 
        }

        // POST: Project/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, ProjectModel model)
        {
            try
            {
                ProjectProcessor.DeleteProject(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View(model);
            }
        }
        public ActionResult ViewEmployees(int id)
        {
            try
            {
                var data = EmployeeProcessor.FindEmployeesByProject(id);
                List<EmployeeModel> employees = new List<EmployeeModel>();
                foreach (var row in data)
                {
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
                return RedirectToAction("Details", new { id = id });
            }

        }
    }
}
