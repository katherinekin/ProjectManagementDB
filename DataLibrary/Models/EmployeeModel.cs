﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// The model for the business logic, different from front end
// Entries here should follow the database schema for PM.Employee
namespace DataLibrary.Models
{
    public class EmployeeModel
    {
        public int Employee_ID { get; set; }
        public string Fname { get; set; }
        public string Lname { get; set; }
        public DateTime Date_Of_Birth { get; set; }
        public int Ssn { get; set; }
        
        public string Address { get; set; }
        public int Type { get; set; }
        public string Gender { get; set; }
        public double Salary { get; set; }

        public DateTime Start_Date { get; set; }
        public int Estatus { get; set; }
        public string EDname { get; set; }
        public int Profession { get; set; }
        public int Super_Ssn { get; set; }
        public int ProjectID { get; set; }

        // Default values, in case some fields are null
        public EmployeeModel()
        {
            Employee_ID = 0;
            Fname = "";
            Lname = "";
            //Date_Of_Birth = "";
            Ssn = 0;
            Address = "";
            Type = 0;
            Gender = "";
            Salary = 0;
            //Start_Date = "";
            Estatus = 0;
            EDname = "";
            Profession = 0;
            Super_Ssn = 0;       
        }
    }
}
