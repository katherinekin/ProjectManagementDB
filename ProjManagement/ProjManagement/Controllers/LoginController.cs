using DataLibrary.BusinessLogic;
using ProjManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

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
            ViewData["State"] = "initial";
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
                    return RedirectToAction("Index", "Employee");
                }
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(LoginModelForgot model, string forgot)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Login");
            }

            ModelState.Clear();

            AuthVM old = new AuthVM
            {
                RecoverForm = model,
                LoginForm = new LoginModel()
            };

            List<DataLibrary.Models.LoginModel> loginList = LoginProcessor.LoadLogins();
            foreach (DataLibrary.Models.LoginModel item in loginList)
            {
                if ((model.EmployeeID == item.LEmployee_ID) && ((model.NewPassword == item.Password && forgot == "user") || (model.NewUsername == item.Username && forgot == "pass")))
                {
                    int x = LoginProcessor.EditLogin(model.EmployeeID, model.NewUsername, model.NewPassword);
                    return View();
                }
                else if (model.EmployeeID == item.LEmployee_ID && forgot == "user" && model.NewPassword != item.Password)
                {
                    ViewBag.ErrorMessage = "Wrong password";
                    old.RecoverForm.NewPassword = old.RecoverForm.ConfirmPass = null;
                    ViewData["State"] = forgot;
                    return View("Login", old);
                }
                else if (model.EmployeeID == item.LEmployee_ID && forgot == "pass" && model.NewUsername != item.Username)
                {
                    ViewBag.ErrorMessage = "Wrong username";
                    old.RecoverForm.NewUsername = old.RecoverForm.ConfirmUsername = null;
                    ViewData["State"] = forgot;
                    return View("Login", old);
                }
            }

            ViewBag.ErrorMessage = "ID not found";
            old.RecoverForm.EmployeeID = 0;
            ViewData["State"] = forgot;
            return View("Login", old);
        }

            //test page
            public ActionResult Success()
        {
            ViewBag.Message = "SUCCESS.";

            return View();
        }
    }
}