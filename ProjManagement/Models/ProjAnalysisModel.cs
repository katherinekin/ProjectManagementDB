using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjManagement.Models
{
    public class ProjAnalysisModel
    {
        public BudgetModel budget { get; set; }
        [Display(Name = "Total Employees")]
        public int EmployeeCount{get;set;}
        [Display(Name = "Project Name")]
        public string Pname { get; set; }
        [Display(Name = "Project ID")]
        public int ProjectID { get; set; }        
    }
}