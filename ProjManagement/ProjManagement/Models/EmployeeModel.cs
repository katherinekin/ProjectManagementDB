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
        [Display(Name = "Department")]
        public string EDname { get; set; } 
        
        public int Profession { get; set; }
        [Display(Name = "Manager")]
        public int SuperSsn { get; set; } //call in edit function, not create

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
            StartDate = "";
            Estatus = 0;
            EDname = "";
            Profession = 0;
            SuperSsn = 0;
        }

        public HashSet<KeyValuePair<string, string>> setToPairs()
        {
            HashSet<KeyValuePair<string, string>> hashSetPairs = new HashSet<KeyValuePair<string, string>>();
            KeyValuePair<string, string> somePair = new KeyValuePair<string, string>();
            //set attributes of model to keyvaluepairs, stringify the ints
            somePair = new KeyValuePair<string, string>("Employee_ID", EmployeeID.ToString()); hashSetPairs.Add(somePair);
            somePair = new KeyValuePair<string, string>("Fname", FName); hashSetPairs.Add(somePair);
            somePair = new KeyValuePair<string, string>("Lname", LName); hashSetPairs.Add(somePair);
            somePair = new KeyValuePair<string, string>("Date_Of_Birth", DateOfBirth); hashSetPairs.Add(somePair);
            /*
            Ssn = 0;
            Address = "";
            Type = 0;
            Gender = "";
            Start_Date = "";
            Estatus = 0;
            EDname = "";
            Profession = 0;
            Super_ssn = 0;
            */
            return hashSetPairs;
        }
    }
}