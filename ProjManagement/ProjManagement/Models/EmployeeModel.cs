﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjManagement.Models
{
    public class EmployeeModel
    {
        [Display(Name = "Employee ID")]
        [Range(0, 9999999999, ErrorMessage ="Please input a valid Employee ID.")]
        public int EmployeeID { get; set; }

        [Required(ErrorMessage = "You must enter a value.")]
        [Display(Name = "First Name")]
        public string FName { get; set; }

        [Required(ErrorMessage = "You must enter a value.")]
        [Display(Name = "Last Name")]
        public string LName { get; set; }

        [Required(ErrorMessage = "You must enter a value.")]
        [DataType(DataType.Date)]
        [Display(Name = "Date of Birth")]
        public string DateOfBirth { get; set; }

        [Required(ErrorMessage = "You must enter a value.")]
        [DataType(DataType.Password)]
        [Display(Name = "Social Security")]
        [StringLength(9, MinimumLength = 1, ErrorMessage = "Incorrect format.")]
        public int Ssn { get; set; }

        [Required(ErrorMessage ="You must have a password.")]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "You need to provide a long enough password.")]
        public string Password { get; set; }

        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Does not match entered password.")]
        public string ConfirmPassword { get; set; }
    }
}