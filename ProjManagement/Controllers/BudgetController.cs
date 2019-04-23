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
        // GET: Budget/CreateBudget
        public ActionResult CreateBudget(int id)
        {
            // Get open and complete dates from project
            var dateRange = ProjectProcessor.getProjectDates(id);
            var existingDates = BudgetProcessor.getBudgetDates(id);
            // Generate list of all months in dateRange, remove dates already in BudgetProcessor
            DateTime newDate = dateRange[0];
            DateTime existingDate;
            List<string> bdates = new List<string>();
            if (existingDates.Count != 0)
            {
                existingDate = existingDates.First();
                if (newDate.Year == existingDate.Year && newDate.Month == existingDate.Month)
                {
                    existingDates.Remove(existingDate);                   
                }                
            }
            else
            {
                bdates.Add(newDate.ToShortDateString());
            }

            while (DateTime.Compare(newDate, dateRange[1]) < 1)
            {
                newDate = newDate.AddMonths(1);
                if (existingDates.Count != 0)
                {
                    existingDate = existingDates.First();
                    if (newDate.Year == existingDate.Year && newDate.Month == existingDate.Month)
                    {
                        existingDates.Remove(existingDate);                        
                    }
                }                    
                else
                {
                    bdates.Add(newDate.ToShortDateString());
                }               
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
            List<BudgetModel> foundBudget = new List<BudgetModel>();
            foreach (var row in data)
            {
                foundBudget.Add(new BudgetModel
                {
                    BProject_ID = row.BProject_ID,
                    Estimated_Expense = row.Estimated_Expense,
                    Estimated_Income = row.Estimated_Income,
                    Estimated_Profit = row.Estimated_Profit,
                    BDate = row.BDate
                });
            }
            return foundBudget[0];
        }
        // Edit function---------------------------------------
        public ActionResult Edit(int id, DateTime bdate)
        {
            var data = BudgetProcessor.FindBudget(id, bdate);
            if (data.Count == 0)
            {
                return HttpNotFound();
            }
            BudgetModel found = mapToModel(data);
            ViewBudgetModel viewFound = new ViewBudgetModel
            {
                budget = found,
                BProjectID = found.BProject_ID,
                BDate = found.BDate
            };
            return View(viewFound);
        }
        // POST: project/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, ViewBudgetModel model)
        {
            var data = BudgetProcessor.FindBudget(id, model.BDate);
            BudgetModel oldModel = mapToModel(data);
            HashSet<KeyValuePair<string, string>> oldModelHashSet = oldModel.setToPairs();
            
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            try
            {
                model.budget.BDate = model.BDate;
            }
            catch
            {
                model.budget.BDate = oldModel.BDate;
            }
            model.budget.Estimated_Profit = model.budget.Estimated_Income - model.budget.Estimated_Expense;
            HashSet<KeyValuePair<string, string>> newModelHashSet = model.budget.setToPairs();
            newModelHashSet.ExceptWith(oldModelHashSet);
            foreach (var pair in newModelHashSet)
            {
                BudgetProcessor.EditBudget(pair, model.BProjectID, DateTime.Parse(model.SelectedBDate));
            }
            return RedirectToAction("ViewBudget", new { id = model.BProjectID });
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