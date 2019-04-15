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
            var loginList = LoginProcessor.LoadLogins();
            var managerList = LoginProcessor.LoadManagers();
            foreach (DataLibrary.Models.LoginModel item in loginList)
            {
                if (model.Username == item.Username && model.Password == item.Password)
                {
                    int EmployeeID = item.LEmployee_ID;
                    
                    foreach (int managerID in managerList)
                    {
                        if (EmployeeID == managerID)
                        {
                            return RedirectToAction("Index", "Manager");
                        }
                    }
                    return RedirectToAction("Index", "User", new { id = EmployeeID });
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