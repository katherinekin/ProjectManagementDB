using DataLibrary.BusinessLogic;
using ProjManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjManagement.Controllers
{
    [Authorize(Roles = "Manager,Admin")]
    public class AnalyticsController : Controller
    {
        // GET: Analytics
        public ActionResult ProjectAnalysis()
        {
            var current = EmployeeProcessor.FindEmployee(int.Parse(User.Identity.Name)).First();
            ViewBag.ssn = current.Ssn;
            ViewBag.dep = current.EDname;

            // Assume for now every BProject_ID entry in LoadBudget is unique
            var data = ProjectProcessor.LoadProjects();
            List<ProjAnalysisModel> list = new List<ProjAnalysisModel>();
            foreach (var row in data)
            {
                if (User.IsInRole("Admin") || current.EDname == row.PDname)
                {
                    list.Add(new ProjAnalysisModel
                    {
                        ProjectID = row.Project_ID,
                        Pname = row.Pname,
                        EmployeeCount = ProjectProcessor.getTotalEmployees(row.Project_ID),
                        Departments = String.Join(", ", ProjectProcessor.getAllDepartments(row.Project_ID)
                                                                        .Where(s => !string.IsNullOrWhiteSpace(s)))
                    });
                }
            }
            return View(list);
        }
        public ActionResult ProjectBudget()
        {
            var current = EmployeeProcessor.FindEmployee(int.Parse(User.Identity.Name)).First();
            ViewBag.ssn = current.Ssn;
            ViewBag.dep = current.EDname;

            // Assume for now every BProject_ID entry in LoadBudget is unique
            var data = BudgetProcessor.LoadBudgets();
            List<ProjBudgetModel> list = new List<ProjBudgetModel>();
            foreach (var row in data)
            {
                if (User.IsInRole("Admin") || current.EDname == ProjectProcessor.FindProject(row.BProject_ID).First().PDname)
                {
                    list.Add(new ProjBudgetModel
                    {
                        ProjectID = row.BProject_ID,
                        Pname = ProjectProcessor.FindProject(row.BProject_ID)[0].Pname,
                        budget = new BudgetModel
                        {
                            Estimated_Expense = row.Estimated_Expense,
                            Estimated_Income = row.Estimated_Income,
                            Estimated_Profit = row.Estimated_Profit
                        },
                        BudgetDate = row.BDate
                    });
                }
            }
            return View(list);
        }
    }
}

