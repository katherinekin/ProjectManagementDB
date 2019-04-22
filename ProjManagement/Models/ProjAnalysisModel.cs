using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjManagement.Models
{
    public class ProjAnalysisModel
    {
        [Display(Name = "Project ID")]
        public int ProjectID { get; set; }

        [Display(Name = "Project Name")]
        public string Pname { get; set; }

        [Display(Name = "Total Employees")]
        public int EmployeeCount { get; set; }

        [Display(Name = "Managers")]
        public List<string> Managers { get; set; }
        [Display(Name = "Departments Involved")]
        public string Departments { get; set; }
        public BudgetModel budget { get; set; }   
    }
}