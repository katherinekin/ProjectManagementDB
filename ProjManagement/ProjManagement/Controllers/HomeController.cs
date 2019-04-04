using DataLibrary.BusinessLogic;
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
        public ActionResult Login(LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                //Redirects to another page
                //return RedirectToAction("Success");
                return View(model);
            }
            List<DataLibrary.Models.LoginModel> loginList = LoginProcessor.LoadLogins();
            foreach (DataLibrary.Models.LoginModel item in loginList)
            {
               if (model.Username==item.Username && model.Password==item.Password)
                {
                    return RedirectToAction("Success");
                }
            }
            return View();
        }
        //test page
        public ActionResult Success()
        {
            ViewBag.Message = "SUCCESS.";

            return View();
        }

        //TEMPORARILY place here
        public ActionResult Employee()
        {
            ViewBag.Message = "Create an employee profile.";

            return View();
        }
    }
}