using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.Models
{
    public class BudgetModel
    {
        public int BProject_ID { get; set; }
        public double Estimated_Income { get; set; }
        public double Estimated_Expense { get; set; }
        public double Estimated_Profit { get; set; }
    }
}
