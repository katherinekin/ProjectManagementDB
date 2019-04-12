using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjManagement.Models
{
    public class ProjectModel
    {
        
        [Display(Name = "Project ID")]
        [Range(0, 999999999, ErrorMessage = "Please input a valid Project ID")]
        public int ProjectID { get; set; }
        [Required(ErrorMessage = "You must enter a value")]
        [Display(Name = "Project Name")]
        public string PName { get; set; }
        
        public string PDName { get; set; }
        public string Client { get; set; }
        public string PDescription { get; set; }
        public string Deliverables { get; set; }
        public string Open_Date { get; set; }
        public string Close_Date { get; set; }
        public string Completion_Date { get; set; }
        public int Pstatus { get; set; }
        public string Collaborators { get; set; }

    }
}