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
        // CRUD functions for budget
        public static int CreateBudget(int projectid, double income, double expense, DateTime bdate)
        {
            BudgetModel data = new BudgetModel
            {
                BProject_ID = projectid,
                Estimated_Income = income,
                Estimated_Expense = expense,
                Estimated_Profit = income - expense,
                BDate = bdate
            };
            string sql = @"insert into pm.budget (BProject_ID, Estimated_Income, Estimated_Expense, Estimated_Profit, BDate)
                            values (@BProject_ID, @Estimated_Income, @Estimated_Expense, @Estimated_Profit, @BDate);";
            return SqlDataAccess.SaveData(sql, data);
        }
        // Edit date, income, expense
        public static int EditBudget(KeyValuePair<string, string> pair, int projectid, DateTime bdate)
        {
            ColumnModel data;
            int setToNum = 0;
            string sql = "";
            // Tries to convert value as an integer
            if (Int32.TryParse(pair.Value, out setToNum))
            {
                data = new ColumnModel()
                {
                    Project_ID = projectid,
                    SomeDate = bdate,
                    IntValue = setToNum
                };
                sql = @"update pm.budget
                set " + pair.Key + " = @IntValue where BProject_ID = @Project_ID and Date = @SomeDate;";
                return SqlDataAccess.SaveData(sql, data);
            }
            data = new ColumnModel()
            {
                Project_ID = projectid,
                SomeDate = bdate,
                StringValue = pair.Value
            };
            sql = @"update pm.budget
            set " + pair.Key + " = @IntValue where BProject_ID = @Project_ID and Date = @SomeDate;"; ;

            return SqlDataAccess.SaveData(sql, data);
        }
        public static int DeleteBudget(int projectid, DateTime bdate)
        {
            BudgetModel data = new BudgetModel
            {
                BProject_ID = projectid,
                BDate = bdate
            };
            string sql = @"delete from PM.Budget where BProject_ID = @BProject_ID and BDate = @BDate;";
            return SqlDataAccess.SaveData(sql, data);
        }
        public static List<BudgetModel> FindBudget(int projectid, DateTime bdate)
        {
            var list = LoadBudgets();
            List<BudgetModel> found = new List<BudgetModel>();
            foreach (BudgetModel item in list)
            {
                if (item.BProject_ID == projectid && item.BDate == bdate)
                {
                    found.Add(item);
                }
            }
            return (found);
        }
    }
}
