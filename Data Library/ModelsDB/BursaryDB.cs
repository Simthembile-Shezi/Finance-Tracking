using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Library.ModelsDB
{
    public class BursaryDB
    {
        public string Bursary_Code { get; set; }
        public string Bursary_Name { get; set; }
        public DateTime Start_Date { get; set; }
        public string Funder_Name { get; set; }
        public DateTime? End_Date { get; set; }
        public decimal? Bursary_Amount { get; set; }
        public string Number_Available { get; set; }
        public string Description { get; set; }
        public string Funding_Year { get; set; }
    }
}
