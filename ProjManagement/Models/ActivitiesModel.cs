using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjManagement.Models
{
    public class ActivitiesModel
    {
        public int AEmployee_ID { get; set; }
        public int AProject_ID { get; set; }
        public string Description { get; set; }
        public double Weekly_Hours { get; set; }
        public string Week_Date { get; set; }

        public ActivitiesModel()
        {
            AEmployee_ID = 0;
            AProject_ID = 0;
            Description = "";
            Weekly_Hours = 0;
            Week_Date = "";
        }
    }
}