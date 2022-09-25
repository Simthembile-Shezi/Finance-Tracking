using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Library.ModelsDB
{
    public class FundedStudentsDB
    {
        public string Student_Number { get; set; }
        public string Student_Identity_Number { get; set; }  
        public string Student_Email { get; set; }      
        public string Institution_Name { get; set; }
        public string Application_Status { get; set; }

        //public virtual Bursar_FundDB Bursar_Funds { get; set; }
        //public virtual BursaryDB Bursary { get; set; }
        //public virtual Enrolled_AtDB Student { get; set; }
    }
}
