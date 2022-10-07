using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Library.ModelsDB
{
    public class ApplicationViewDB
    {

        public string Application_ID { get; set; }
        public string Application_Status { get; set; }

        //Student details
        public string Student_FName { get; set; }
        public string Student_LName { get; set; }
        public string Student_Identity_Number { get; set; }
        public string Gender { get; set; }
        public string Student_Cellphone_Number { get; set; }
        public string Student_Email { get; set; }
        public string Student_Residential_Address { get; set; }

        //Institution details
        public string Student_Number { get; set; }
        public string Institution_Name { get; set; }
        public string Qualification { get; set; }
        public string Academic_Year { get; set; }
        public decimal Avarage_Marks { get; set; }
        public byte[] Upload_Transcript { get; set; }

        public string Bursary_Code { get; set; }
    }
}
