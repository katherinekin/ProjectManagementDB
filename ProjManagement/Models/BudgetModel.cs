using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjManagement.Models
{
    public class BudgetModel
    {
        [Display(Name = "Project ID")]
        public int BProject_ID { get; set; }
        [Display(Name = "Income")]
        public double Estimated_Income { get; set; }
        [Display(Name = "Expense")]
        public double Estimated_Expense { get; set; }
        [Display(Name = "Profit")]
        public double Estimated_Profit { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Date")]        
        public DateTime BDate { get; set; }
        public BudgetModel()
        {
            BProject_ID = 0;
            Estimated_Income = 0;
            Estimated_Expense = 0;
            Estimated_Profit = 0;
        }
        public HashSet<KeyValuePair<string, string>> setToPairs()
        {
            HashSet<KeyValuePair<string, string>> hashSetPairs = new HashSet<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("BProject_ID", this.BProject_ID.ToString()),
                new KeyValuePair<string, string>("Estimated_Income", this.Estimated_Income.ToString()),
                new KeyValuePair<string, string>("Estimated_Expense", this.Estimated_Expense.ToString()),
                new KeyValuePair<string, string>("Estimated_Profit", this.Estimated_Profit.ToString()),
                new KeyValuePair<string, string>("BDate", this.BDate.ToShortDateString()),
            };
            return hashSetPairs;
        }
    }
}