using DataLibrary.BusinessLogic;
using ProjManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjManagement.Controllers
{
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
        // GET: Budget/CreateBudget
        public ActionResult CreateBudget(int id)
        {
            // Get open and complete dates from project
            var dateRange = ProjectProcessor.getProjectDates(id);
            // Generate list of all months between open and complete dates, throws exception if the dates already in db
            
            DateTime newDate = dateRange[0];
            List<string> bdates = new List<string> { newDate.ToShortDateString() };
            while(DateTime.Compare(newDate, dateRange[1]) < 1)
            {
                newDate = newDate.AddMonths(1);
                bdates.Add(newDate.ToShortDateString());
            }
            ViewBudgetModel model = new ViewBudgetModel();
            model.BProjectID = id;
            model.BDateSelectList = bdates.Select(x => new SelectListItem()
            {
                Text = x,
                Value = x
            });
            return View(model);
        }
        // POST: Project/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateBudget(int id, ViewBudgetModel model)
        {
            ViewBag.Message = "Create a new budget for project.";
            model.BProjectID = id;
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            DateTime bdate = DateTime.Parse(model.SelectedBDate);
            try
            {
                BudgetProcessor.CreateBudget(model.BProjectID,
                    model.budget.Estimated_Income, model.budget.Estimated_Expense, bdate);
                return RedirectToAction("ViewBudget", new { id = model.BProjectID });
            }
            catch
            {                
                return View(model);
            }    
        }
        // Helper funciton mapToModel
        public BudgetModel mapToModel(List<DataLibrary.Models.BudgetModel> data)
        {
            List<BudgetModel> foundProject = new List<BudgetModel>();
            foreach (var row in data)
            {
                foundProject.Add(new BudgetModel
                {
                    BProject_ID = row.BProject_ID,
                    Estimated_Expense = row.Estimated_Expense,
                    Estimated_Income = row.Estimated_Income,
                    Estimated_Profit = row.Estimated_Profit,
                    BDate = row.BDate
                });
            }
            return foundProject[0];
        }
        // Delete function-------------------------------------
        public ActionResult Delete(int id, DateTime bdate)
        {
            var data = BudgetProcessor.FindBudget(id, bdate);
            if (data.Count == 0)
            {
                return HttpNotFound();
            }
            BudgetModel found = mapToModel(data);
            return View(found);
        }

        // POST: Project/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, BudgetModel model)
        {
            try
            {
                model.BProject_ID = id;
                BudgetProcessor.DeleteBudget(id, model.BDate);
                return RedirectToAction("ViewBudget", new { id = model.BProject_ID });
            }
            catch
            {
                return View(model);
            }
        }
    }
}