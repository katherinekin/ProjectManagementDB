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
    public class BudgetController : Controller
    {
        // GET: Budget
        public ActionResult ViewBudget(int id)
        {
            List<BudgetModel> model = new List<BudgetModel>();
            var budgets = BudgetProcessor.FindBudgetsForProject(id);
            //get list of budgets related to project, sorted by date
            foreach (var row in budgets)
            {
                model.Add(new BudgetModel
                {
                    BProject_ID = row.BProject_ID,
                    BDate = row.BDate,
                    Estimated_Income = row.Estimated_Income,
                    Estimated_Expense = row.Estimated_Expense,
                    Estimated_Profit = row.Estimated_Profit
                });
            }
            return View(model);
        }
    }
}