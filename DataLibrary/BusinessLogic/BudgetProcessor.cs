using DataLibrary.DataAccess;
using DataLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.BusinessLogic
{
    public class BudgetProcessor
    {

        public static List<BudgetModel> LoadBudgets()
        {
            string sql = "select * from pm.budget;";
            return SqlDataAccess.LoadData<BudgetModel>(sql);            
        }
        public static List<BudgetModel> FindBudgetsForProject(int projectid)
        {
            BudgetModel data = new BudgetModel
            {
                BProject_ID = projectid
            };
            string sql = @"select * from pm.budget where BProject_ID = @BProject_ID;";
            return SqlDataAccess.LoadData<BudgetModel>(sql, data);
        }

    }
}
