using ProjManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjManagement.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Login()
        {
            ViewBag.Message = "Register an account.";

            return View();
        }

        //Validation of correct Login information
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(EmployeeModel model)
        {
            if (ModelState.IsValid)
            {
                //Redirects to another page
                return RedirectToAction("Index");
            }

            return View();
        }

    }
}