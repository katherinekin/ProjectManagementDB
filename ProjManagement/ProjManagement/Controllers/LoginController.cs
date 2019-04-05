using DataLibrary.BusinessLogic;
using ProjManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjManagement.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Login()
        {
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
                if (model.Username == item.Username && model.Password == item.Password)
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
    }
}