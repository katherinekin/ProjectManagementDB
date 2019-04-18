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
    }
}
