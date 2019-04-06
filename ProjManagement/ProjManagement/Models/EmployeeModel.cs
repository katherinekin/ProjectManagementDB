using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjManagement.Models
{
    public class EmployeeModel
    {
        
        [Display(Name = "Employee ID")]
        [Range(0, 9999999999, ErrorMessage = "Please input a valid Employee ID.")]
        public int EmployeeID { get; set; } = 0;

        [Required(ErrorMessage = "You must enter a value.")]
        [Display(Name = "First Name")]
        public string FName { get; set; }

        [Required(ErrorMessage = "You must enter a value.")]
        [Display(Name = "Last Name")]
        public string LName { get; set; }
        
        //[Required(ErrorMessage = "You must enter a value.")]
        [DataType(DataType.Date)]
        [Display(Name = "Date of Birth")]
        public string DateOfBirth { get; set; } = "";

        [Required(ErrorMessage = "You must enter a value.")]
        [Display(Name = "Social Security")]
        public int Ssn { get; set; }
    }
}