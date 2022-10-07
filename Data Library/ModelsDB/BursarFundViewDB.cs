using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Library.ModelsDB
{
    public class BursarFundViewDB
    {
        public string Application_ID { get; set; }

        //Student details

        public string Student_FName { get; set; }

        public string Student_LName { get; set; }

        public string Student_Identity_Number { get; set; }

        public string Gender { get; set; }
        public string Student_Cellphone_Number { get; set; }

        public string Student_Email { get; set; }

        public string Update_Fund_Request { get; set; }

        public string Funding_Status { get; set; }

        public decimal? Approved_Funds { get; set; }

    }
}
