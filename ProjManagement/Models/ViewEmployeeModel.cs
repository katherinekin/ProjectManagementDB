﻿using DataLibrary.BusinessLogic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjManagement.Models
{
    public class ViewEmployeeModel
    {
        public EmployeeModel employee { get; set; }
        
        public IEnumerable<SelectListItem> DepSelectList { get; set; }

        public IEnumerable<SelectListItem> ProfSelectList { get; set; }

        public IEnumerable<SelectListItem> TypeSelectList { get; set; }

        public IEnumerable<SelectListItem> StatusSelectList { get; set; }

        public IEnumerable<SelectListItem> ManagerSelectList { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date of Birth")]
        public DateTime DOB { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Start Date")]
        public DateTime SD { get; set; }

        [Display(Name = "Department")]
        public string SelectedDep { get; set; }

        [Display(Name = "Profession")]
        public string SelectedProf { get; set; }

        [Display(Name = "Part or Full Time")]
        public string SelectedType { get; set; }

        [Display(Name = "Status")]
        public string SelectedStatus { get; set; }

        [Display(Name = "Manager")]
        public string SelectedManager { get; set; }

        public int EmployeeID { get; set; }
        public int ProjectID { get; set; }
        public string Pname { get; set; }
        public ViewEmployeeModel()
        {
            employee = new EmployeeModel();
            // Default values            
            SelectedDep = "";
            SelectedProf = "";
            SelectedType = "";
            SelectedStatus = "";
            ProjectID = employee.ProjectID;
            EmployeeID = employee.EmployeeID;            
            Pname = "";
            var departments = EmployeeProcessor.LoadDepartmentNames();
            DepSelectList = departments.Select(x => new SelectListItem()
            {
                Text = x.Dname,
                Value = x.Dname
            });
            
            var professions = EmployeeProcessor.LoadProfessions();
            ProfSelectList = professions.Select(x => new SelectListItem()
            {
                Text = x.EPDescription,
                Value = x.EPCode.ToString()
            });
            var types = EmployeeProcessor.LoadTypes();
            TypeSelectList = types.Select(x => new SelectListItem()
            {
                Text = x.ETDescription,
                Value = x.ETCode.ToString()
            });

            var statuses = EmployeeProcessor.LoadStatuses();
            StatusSelectList = statuses.Select(x => new SelectListItem()
            {
                Text = x.ESDescription,
                Value = x.ESCode.ToString()
            });
        }
    }
}