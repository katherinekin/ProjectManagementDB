﻿using DataLibrary.BusinessLogic;
using ProjManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

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

        public ActionResult Failed(LoginModel old)
        {
            if (old.Password == null)
            {
                ViewBag.ErrorMessage = "Wrong password";
            }
            else
            {
                ViewBag.ErrorMessage = "Wrong username";
                old.Username = null;
            }

            ModelState.Clear();

            AuthVM model = new AuthVM
            {
                RecoverForm = new LoginModelForgot(),
                LoginForm = old
            };
            ViewData["State"] = "initial";
            return View("Login", model);
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
                    FormsAuthentication.SetAuthCookie(item.LEmployee_ID.ToString(), false);
                    /*FormsIdentity identity = (FormsIdentity)HttpContext.User.Identity;
                    string[] roles = {"user"};
                    HttpContext.User = new System.Security.Principal.GenericPrincipal(identity, roles);
                    var x = User.Identity.Name;*/
                    int EmployeeID = item.LEmployee_ID;
                    
                    foreach (int managerID in managerList)
                    {
                        if (EmployeeID == managerID || EmployeeID == 1)
                        {
                            return RedirectToAction("Index", "Manager");
                        }
                    }
                    return RedirectToAction("Index", "User", new { id = EmployeeID });
                }
                if(model.Username == item.Username && model.Password != item.Password)
                {
                    model.Password = null;
                    break;
                }
            }         
            
            return RedirectToAction("Failed", model);
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

        public ActionResult Logout()
        {
            if (Request.IsAuthenticated)
            {
                FormsAuthentication.SignOut();
                Session.Abandon();
                Response.Redirect("~/Login/Logout", true);
                return View();
            }
            else
            {
                return View();
            }
        }
    }
}