using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Library.ModelsDB
{
    public class ApplicationDB
    {
        public string Application_ID { get; set; }
        public string Student_Identity_Number { get; set; }
        public string Bursary_Code { get; set; }
        public string Funding_Year { get; set; }
        public string Application_Status { get; set; }
        public byte[] Upload_Agreement { get; set; }
        public byte[] Upload_Signed_Agreement { get; set; }
        public virtual Bursar_FundDB Bursar_Funds { get; set; }
        public virtual BursaryDB Bursary { get; set; }
        public virtual StudentDB Student { get; set; }
    }
}
