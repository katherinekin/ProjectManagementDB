using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjManagement.Controllers
{
    public class ManagerController : Controller
    {
        // GET: Manager
        public ActionResult Index()
        {            
            return View();
        }
        public ActionResult Employee()
        {
            return View();
        }
        public ActionResult Project()
        {
            return View();
        }
    }
}
