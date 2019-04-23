using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataLibrary.BusinessLogic;
using ProjManagement.Models;

namespace ProjManagement.Controllers
{
    [Authorize(Roles = "Manager,Admin")]
    public class ManagerController : Controller
    {
        //Imported special method from project controller
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

        //Imported special method from employee controller
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
            List<ViewProjectModel> projects = new List<ViewProjectModel>();
            foreach (var row in data)
            {
                if (dep == row.PDname)
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
            }
            return View(projects);
        }

        public ActionResult ManagerCreateEmployee()
        {
            var x = new ViewEmployeeModel();
            x.SelectedDep = EmployeeProcessor.FindEmployee(int.Parse(User.Identity.Name)).First().EDname;
            return View(x);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ManagerCreateEmployee(ViewEmployeeModel model)
        {
            ViewBag.Message = "Create a new employee profile.";
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            /*EmployeeProcessor.CreateEmployee(model.employee.EmployeeID, model.employee.FName, model.employee.LName, model.employee.DateOfBirth, model.employee.Ssn,
                model.employee.Address, Int32.Parse(model.SelectedType), model.employee.Gender, model.employee.Salary, model.employee.StartDate,
                model.SelectedDep, Int32.Parse(model.SelectedProf));*/

            return RedirectToAction("ManagerSuccessEmployee", new { Model = model });
        }
        public ActionResult ManagerSuccessEmployee(EmployeeModel model)
        {
            return View(model);
        }

        public ActionResult ManagerEmployeeDetails(int id)
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

        public ActionResult ManagerEmployeeEdit(int id)
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

        [HttpPost]
        public ActionResult ManagerEmployeeEdit(int id, ViewEmployeeModel model)
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
            return RedirectToAction("ManagerEmployeeDetails", new { id = model.employee.EmployeeID });
        }

        public ActionResult ManagerEmployeeDelete(int id)
        {
            var data = EmployeeProcessor.FindEmployee(id);
            if (data.Count == 0)
            {
                return HttpNotFound();
            }
            EmployeeModel found = mapToModel(data);
            return View(found);
        }

        [HttpPost]
        public ActionResult ManagerEmployeeDelete(int id, EmployeeModel model)
        {
            try
            {
                EmployeeProcessor.DeleteEmployee(id);
                return RedirectToAction("ManagerEmployees");
            }
            catch
            {
                return View(model);
            }
        }

        public ActionResult ManagerEmployeeProjects(int id)
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
                return RedirectToAction("Details", new { id = id });
            }
        }

        public ActionResult ManagerEmployeeProjectHours(int projectid, int employeeid)
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

        public ActionResult ManagerEmployeeProjectLogHours(int pid, int eid)
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
        public ActionResult ManagerEmployeeProjectLogHoursSuccess(ActivitiesModel data)
        {
            EmployeeProcessor.NewHours(data.AEmployee_ID, data.AProject_ID, data.Description, data.Weekly_Hours, data.Week_Date);
            ViewData["Pname"] = 0;
            return View(data);
        }

        public ActionResult ManagerCreateProject()
        {
            var x = new ViewProjectModel();
            x.SelectedDep = EmployeeProcessor.FindEmployee(int.Parse(User.Identity.Name)).First().EDname;
            return View(x);
        }

        // POST: Project/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ManagerCreateProject(ViewProjectModel model)
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

            return RedirectToAction("ManagerSuccessProject", new { Model = model });

        }
        public ActionResult ManagerSuccessProject(ProjectModel model)
        {
            return View(model);
        }

        public ActionResult ManagerProjectEdit(int id)
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

        [HttpPost]
        public ActionResult ManagerProjectEdit(int id, ViewProjectModel model)
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
            return RedirectToAction("ManagerProjectDetails", new { id = model.project.ProjectID });
        }

        public ActionResult ManagerProjectDelete(int id)
        {
            var data1 = ProjectProcessor.FindProject(id);
            if (data1.Count == 0)
            {
                return HttpNotFound();
            }
            ProjectModel found = pToModel(data1);
            return View(found);
        }

        [HttpPost]
        public ActionResult ManagerProjectDelete(int id, ProjectModel model)
        {
            try
            {
                ProjectProcessor.DeleteProject(id);
                return RedirectToAction("ManagerProjects");
            }
            catch
            {
                return View(model);
            }
        }

        public ActionResult ManagerProjectDetails(int id)
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

        public ActionResult ManagerProjectEmployees(int id)
        {
            try
            {
                var data = EmployeeProcessor.FindEmployeesByProject(id);
                List<ViewEmployeeModel> employees = new List<ViewEmployeeModel>();
                foreach (var row in data)
                {
                    employees.Add(new ViewEmployeeModel
                    {
                        employee = new EmployeeModel
                        {
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
                            ProjectID = id,
                            SuperSsn = row.Super_Ssn,
                            Ssn = row.Ssn
                        },
                        ProjectID = id
                    });
                }
                if (employees.Count == 0)
                {
                    employees.Add(new ViewEmployeeModel
                    {
                        ProjectID = id,
                        Pname = ProjectProcessor.getProjectName(id)
                    });
                }
                else
                {
                    employees.First().Pname = ProjectProcessor.getProjectName(id);
                }
                return View(employees);
            }
            catch
            {
                return RedirectToAction("ManagerProjectDetails", new { id = id });
            }
        }

        public ActionResult ManagerProjectRemoveEmployee(int employeeid, int projectid)
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
        public ActionResult ManagerProjectRemoveEmployee(int employeeid, ViewEmployeeModel model)
        {

            try
            {
                ProjectProcessor.DeleteEmployeeFromProject(employeeid, model.ProjectID);
                return RedirectToAction("ManagerProjectEmployees", new { id = model.ProjectID });
            }
            catch
            {
                return View(model);
            }
        }

        public ActionResult ManagerProjectAddEmployee(int projectid)
        {
            ViewProjectModel model = new ViewProjectModel();
            model.ProjectID = projectid;
            // Populate dropdown list
            var employees = EmployeeProcessor.FindEmployeesNotInProject(projectid);
            model.EmpSelectList = employees.Select(x => new SelectListItem()
            {
                Text = x.Fname + " " + x.Lname,
                Value = x.Employee_ID.ToString()
            });

            return View(model);
        }
        [HttpPost]
        public ActionResult ManagerProjectAddEmployee(int projectid, ViewProjectModel model)
        {
            try
            {
                ProjectProcessor.AddEmployeeToProject(Int32.Parse(model.SelectedEmp), projectid);
                return RedirectToAction("ManagerProjectEmployees", new { id = projectid });
            }
            catch
            {
                return View(model);
            }
        }
    }
}
