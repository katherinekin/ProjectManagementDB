using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjManagement.Models
{
    public class ViewBudgetModel
    {
        public BudgetModel budget { get; set; }
        public string SelectedBDate { get; set; }
        public int BProjectID { get; set; }
        public IEnumerable<SelectListItem> BDateSelectList { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime BDate { get; set; }
        public ViewBudgetModel()
        {
            budget = new BudgetModel();            
        }
    }
}