using DataLibrary.BusinessLogic;
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

        [Display(Name = "Department")]
        public string SelectedDep { get; set; }

        [Display(Name = "Profession")]
        public string SelectedProf { get; set; }

        public ViewEmployeeModel()
        {
            SelectedDep = "";
            SelectedProf = "";
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
            
        }
    }
}