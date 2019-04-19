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

        [Display(Name = "Department Name")]
        public string PDName { get; set; }

        [Required(ErrorMessage = "You must enter a value")]
        [Display(Name = "Client")]
        public string Client { get; set; }

        [Required(ErrorMessage = "You must enter a value")]
        [Display(Name = "Description")]
        public string PDescription { get; set; }

        [Required(ErrorMessage = "You must enter a value")]
        [Display(Name = "Deliverables")]
        public string Deliverables { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Open Date")]
        public DateTime Open_Date { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Close Date")]
        public DateTime Close_Date { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Completion Date")]
        public DateTime Completion_Date { get; set; }

        [Display(Name = "Status")]
        public int Pstatus { get; set; }

        [Display(Name = "Collaborators")]
        public string Collaborators { get; set; }
        public int EmployeeID { get; set; }


        public ProjectModel()
        {
            ProjectID = 0;
            PName = "";
            PDName = "";
            Client = "";
            PDescription = "";
            Deliverables = "";
            Pstatus = 0;
            Collaborators = "";
            EmployeeID = 0;
        }

        public HashSet<KeyValuePair<string, string>> PsetToPairs()
        {
            HashSet<KeyValuePair<string, string>> hashSetPairs = new HashSet<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("Project_ID", this.ProjectID.ToString()),
                new KeyValuePair<string, string>("Pname", this.PName),
                new KeyValuePair<string, string>("PDname", this.PDName),
                new KeyValuePair<string, string>("Client", this.Client),
                new KeyValuePair<string, string>("Pdescription", this.PDescription),
                new KeyValuePair<string, string>("Deliverables", this.Deliverables),
                new KeyValuePair<string, string>("Open_Date", this.Open_Date.ToShortDateString()),
                new KeyValuePair<string, string>("Close_Date", this.Close_Date.ToShortDateString()),
                new KeyValuePair<string, string>("Completion_Date", this.Completion_Date.ToShortDateString()),
                new KeyValuePair<string, string>("Pstatus", this.Pstatus.ToString()),
                new KeyValuePair<string, string>("Collaborators", this.Collaborators),
            };
            return hashSetPairs;
        }
    }
}