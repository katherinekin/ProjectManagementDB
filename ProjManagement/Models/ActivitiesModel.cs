using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ProjManagement.Models
{
    public class ActivitiesModel
    {
        [Required(ErrorMessage = "You must enter a user")]
        [Display(Name = "Employee ID")]
        public int AEmployee_ID { get; set; }

        [Required(ErrorMessage = "You must enter a project")]
        [Display(Name = "Project ID")]
        public int AProject_ID { get; set; }

        public string Description { get; set; }
        
        [Required(ErrorMessage = "You must enter hours")]
        [Display(Name = "Weekly Hours")]
        public double Weekly_Hours { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [Required(ErrorMessage = "You must enter a week")]
        [Display(Name = "Week Date")]
        public string Week_Date { get; set; }

        public ActivitiesModel()
        {
            AEmployee_ID = 0;
            AProject_ID = 0;
            Description = "";
            Weekly_Hours = 0;
            Week_Date = "";
        }
    }
}