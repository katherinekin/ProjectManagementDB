using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.Models
{
    public class ProjectModel
    {
        public int Project_ID { get; set; }
        public string Pname { get; set; }
        public string PDname { get; set; }
        public string Client { get; set; }
        public string Pdescription { get; set; }
        public string Deliverables { get; set; }
        public DateTime Open_Date { get; set; }
        public DateTime Close_Date { get; set; }
        public DateTime Completion_Date { get; set; }
        public int Pstatus { get; set; }
        public string Collaborators { get; set; }

        public ProjectModel()
        {
            Project_ID = 0;
            Pname = "";
            PDname = "";
            Client = "";
            Pdescription = "";
            Deliverables = "";            
            Pstatus = 0;
            Collaborators = "";
        }       
    }
}