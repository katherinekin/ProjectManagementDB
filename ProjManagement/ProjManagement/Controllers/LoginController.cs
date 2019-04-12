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
            AuthVM model = new AuthVM
            {
                RecoverForm = new LoginModelForgot(),
                LoginForm = new LoginModel()
            };
            return View(model);
        }
        //Validation of correct Login information
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(LoginModelForgot model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Login");
            }
            if (model.ConfirmUsername == model.NewUsername && model.ConfirmPass == model.NewPassword)
            {
                int x = LoginProcessor.EditLogin(model.EmployeeID, model.ConfirmUsername, model.ConfirmPass);
                if (x != 0)
                {
                    return View();
                }
            }
            return RedirectToAction("Login");
        }

            //test page
            public ActionResult Success()
        {
            ViewBag.Message = "SUCCESS.";

            return View();
        }
    }
}