﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
        [Display(Name = "SSN")]
        [Range(1, 999999999, ErrorMessage = "Please input a valid social security number.")]
        public int Ssn { get; set; }

        public string Address { get; set; }
        [Range(1,2, ErrorMessage = "Please input 1 for part time or 2 for full time.")]
        [Display(Name = "Part or Full Time")]
        public int Type { get; set; }
        public string Gender { get; set; }
        public double Salary { get; set; } //call in create function, not edit
        [Display(Name = "Start Date")]
        public string StartDate { get; set; }

        public int Estatus { get; set; } //call in manager edit function, not create

        //[Display(Name = "Department")]
        public string EDname { get; set; } 
                
        public int Profession { get; set; }
        [Display(Name = "Manager")]
        public int SuperSsn { get; set; } //call in edit function, not create
        public string SuperName { get; set; }
        public int ProjectID { get; set; }

        public EmployeeModel()
        {
            EmployeeID = 0;
            FName = "";
            LName = "";
            DateOfBirth = "";
            Ssn = 0;
            Address = "";
            Type = 0;
            Gender = "";
            Salary = 0;
            StartDate = "";
            Estatus = 0;
            EDname = "";
            Profession = 0;
            SuperSsn = 0;
            SuperName = "";
            ProjectID = 0;
        }

        public HashSet<KeyValuePair<string, string>> setToPairs()
        {
            HashSet<KeyValuePair<string, string>> hashSetPairs = new HashSet<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("Employee_ID", this.EmployeeID.ToString()),
                new KeyValuePair<string, string>("Fname", this.FName),
                new KeyValuePair<string, string>("Lname", this.LName),
                new KeyValuePair<string, string>("Date_Of_Birth", this.DateOfBirth),
                new KeyValuePair<string, string>("Ssn", this.Ssn.ToString()),
                new KeyValuePair<string, string>("Address", this.Address),
                new KeyValuePair<string, string>("Type", this.Type.ToString()),
                new KeyValuePair<string, string>("Date_Of_Birth", this.DateOfBirth),
                new KeyValuePair<string, string>("Gender", this.Gender),
                new KeyValuePair<string, string>("Salary", this.Salary.ToString()),
                new KeyValuePair<string, string>("Start_Date", this.StartDate),
                new KeyValuePair<string, string>("Estatus", this.Estatus.ToString()),
                new KeyValuePair<string, string>("EDname", this.EDname),
                new KeyValuePair<string, string>("Profession", this.Profession.ToString()),
                new KeyValuePair<string, string>("Super_ssn", this.SuperSsn.ToString())
            };   
            return hashSetPairs;
        }
    }
}