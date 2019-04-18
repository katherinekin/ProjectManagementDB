using DataLibrary.BusinessLogic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjManagement.Models
{
    public class ViewProjectModel
    {
        public ProjectModel project { get; set; }
        public IEnumerable<SelectListItem> PStatusSelectList { get; set; }

        public IEnumerable<SelectListItem> DepSelectList { get; set; }

        [StringLength(50, MinimumLength=1, ErrorMessage ="You must select a status.")]
        public string SelectedPStatus { get; set; }

        [Display(Name = "Department")]
        public string SelectedDep { get; set; }
        public ViewProjectModel()
        {
            project = new ProjectModel();
            var pstatuses = ProjectProcessor.LoadPStatuses();
            PStatusSelectList = pstatuses.Select(x => new SelectListItem()
            {
                Text = x.PSDescription,
                Value = x.PSCode.ToString()
            });
            var departments = EmployeeProcessor.LoadDepartmentNames();
            DepSelectList = departments.Select(x => new SelectListItem()
            {
                Text = x.Dname,
                Value = x.Dname
            });
        }
    }
}