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
                    Pstatus = row.Pstatus
                });
            }
            return View(projects);
        }

        // GET: Project/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Project/Create
        public ActionResult CreateProject()
        {
            return View();
        }

        // POST: Project/Create
        [HttpPost]
        public ActionResult CreateProject(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Project/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Project/Edit/5
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

        // GET: Project/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Project/Delete/5
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
