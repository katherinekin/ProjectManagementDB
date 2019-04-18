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
        public int BProject_ID {get;set;}
        [Display(Name = "Income")]
        public double Estimated_Income { get; set; }
        [Display(Name = "Expense")]
        public double Estimated_Expense { get; set; }
        [Display(Name = "Profit")]
        public double Estimated_Profit { get; set; }        
    }
}