using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Used to map pairs of column names to values
namespace DataLibrary.Models
{
    public class ColumnModel
    {
        public int Employee_ID { get; set; }
        public string ColumnName { get; set; }
        public string StringValue { get; set; }
        public int IntValue { get; set; }
    }
}
