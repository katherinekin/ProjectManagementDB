using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjManagement.Models
{
    public class ProjBudgetModel
    {
        [Display(Name = "Project ID")]
        public int ProjectID { get; set; }

        [Display(Name = "Project Name")]
        public string Pname { get; set; }
        
        [Display(Name = "Date")]
        public DateTime BudgetDate {get;set;}
        public BudgetModel budget { get; set; }
    }
}