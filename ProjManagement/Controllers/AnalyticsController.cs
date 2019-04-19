using DataLibrary.BusinessLogic;
using ProjManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjManagement.Controllers
{
    public class AnalyticsController : Controller
    {
        // GET: Analytics
        public ActionResult ProjectAnalysis()
        {
            
            // Assume for now every BProject_ID entry in LoadBudget is unique
            var data = BudgetProcessor.LoadBudgets();
            List<ProjAnalysisModel> list = new List<ProjAnalysisModel>();
            foreach (var row in data)
            {
                list.Add(new ProjAnalysisModel
                {
                    ProjectID = row.BProject_ID,
                    Pname = ProjectProcessor.FindProject(row.BProject_ID)[0].Pname,
                    EmployeeCount = ProjectProcessor.getTotalEmployees(row.BProject_ID),
                    budget = new BudgetModel
                    {
                        Estimated_Expense = row.Estimated_Expense,
                        Estimated_Income = row.Estimated_Income,
                        Estimated_Profit = row.Estimated_Profit
                    }                    
                });
            }
            return View(list);
        }
    }
}

