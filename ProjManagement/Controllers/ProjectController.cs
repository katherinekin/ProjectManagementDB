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
                    Completion_Date = row.Completion_Date,
                    Collaborators = row.Collaborators,
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
            ViewProjectModel viewFound = new ViewProjectModel
            {
                project = found
            };
            return View(viewFound);
        }

        // GET: Project/Create
        public ActionResult CreateProject()
        {
            return View(new ViewProjectModel());
        }

        // POST: Project/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateProject(ViewProjectModel model)
        {
            int Pstatus = 2;
            ViewBag.Message = "Create a new project profile.";
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            try
            {
                Pstatus = Int32.Parse(model.SelectedPStatus);
            }
            catch
            {
                Pstatus = 2;
            }         
            ProjectProcessor.CreateProject(model.project.ProjectID,
                model.project.PName, model.SelectedDep, model.project.Client, model.project.PDescription, model.project.Deliverables,
                model.project.Open_Date, model.project.Close_Date, model.project.Completion_Date, model.project.Collaborators, Pstatus);
                       
            return RedirectToAction("SuccessProject", new { Model = model });

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
            ViewProjectModel viewFound = new ViewProjectModel
            {
                project = found
            };
            return View(viewFound);
        }

        // POST: project/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, ViewProjectModel model)
        {
            var data = ProjectProcessor.FindProject(id);
            ProjectModel oldModel = pToModel(data);
            HashSet<KeyValuePair<string, string>> oldModelHashSet = oldModel.PsetToPairs();
            //returns a HashSet of the old model only if has not been set

            if (!ModelState.IsValid)
            {
                return View(model);
            }
            try
            {
                model.project.Pstatus = Int32.Parse(model.SelectedPStatus);
            }
            catch
            {
                model.project.Pstatus = oldModel.Pstatus;
            }
            try
            {
                model.project.PDName = model.SelectedDep.ToString();
            }
            catch
            {
                model.project.PDName = oldModel.PDName;
            }
            HashSet<KeyValuePair<string, string>> newModelHashSet = model.project.PsetToPairs();
            newModelHashSet.ExceptWith(oldModelHashSet);
            foreach (var pair in newModelHashSet)
            {
                ProjectProcessor.EditProject(pair, model.project.ProjectID);
            }
            return RedirectToAction("Details", new { id = model.project.ProjectID });
        }
        
        // GET: Project/Delete--------------------------------------------------------------
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
        // GET: Project/ViewEmployees--------------------------------------------------------------
        public ActionResult ViewEmployees(int id)
        {
            try
            {
                var data = EmployeeProcessor.FindEmployeesByProject(id);
                List<ViewEmployeeModel> employees = new List<ViewEmployeeModel>();
                foreach (var row in data)
                {
                    employees.Add(new ViewEmployeeModel
                    {
                        employee = new EmployeeModel{
                            EmployeeID = row.Employee_ID,
                            FName = row.Fname,
                            LName = row.Lname,
                            DateOfBirth = row.Date_Of_Birth,
                            Type = row.Type,
                            Salary = row.Salary,
                            Estatus = row.Estatus,
                            EDname = row.EDname,
                            Profession = row.Profession,
                            SuperName = EmployeeProcessor.getManagerName(row.Super_Ssn),
                            ProjectID = id
                        }
                    });
                }
                if (employees.Count == 0)
                {
                    employees.Add(new ViewEmployeeModel
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
        // Remove an employee from a project---------------------------------------------------------
        public ActionResult RemoveEmployee(int employeeid, int projectid)
        {
            ViewEmployeeModel model = new ViewEmployeeModel
            {
                employee = new EmployeeModel
                {
                    EmployeeID = employeeid,
                    ProjectID = projectid
                }
            };
            return View(model);
        }
        [HttpPost]
        public ActionResult RemoveEmployee(int employeeid, ViewEmployeeModel model)
        {
            
            try
            {
                ProjectProcessor.DeleteEmployeeFromProject(employeeid, model.ProjectID);
                return RedirectToAction("ViewEmployees", new { id = model.ProjectID });
            }
            catch
            {
                return View(model);
            }
        }
        // Remove add an employee to a project---------------------------------------------------------
        public ActionResult AddEmployee(int projectid)
        {
            ViewProjectModel model = new ViewProjectModel();
            model.ProjectID = projectid;            
            // Populate dropdown list
            var employees = EmployeeProcessor.FindEmployeesNotInProject(projectid);
            model.EmpSelectList = employees.Select(x => new SelectListItem()
            {
                Text = x.Fname +" "+ x.Lname,
                Value = x.Employee_ID.ToString()
            });

            return View(model);                    
        }
        [HttpPost]
        public ActionResult AddEmployee(int projectid, ViewProjectModel model)
        {
            try
            {
                ProjectProcessor.AddEmployeeToProject(Int32.Parse(model.SelectedEmp), projectid);
                return RedirectToAction("ViewEmployees", new { id = projectid });
            }
            catch
            {
                return View(model);
            }
        }
    }
}
