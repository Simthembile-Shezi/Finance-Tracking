using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Library.ModelsDB
{
    public class Finacial_RecordDB
    {
        public string Student_Number { get; set; }
        public string Academic_Year { get; set; }
        public decimal? Balance_Amount { get; set; }
        public byte[] Upload_Statement { get; set; }
        public string Funding_Status { get; set; }
        public string Request_Funds { get; set; }
        public virtual Enrolled_AtDB Enrolled_At { get; set; }
    }
}
