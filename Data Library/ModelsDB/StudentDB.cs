using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Data_Library.ModelsDB
{
    public class StudentDB
    {        
        public string Student_Identity_Number { get; set; }
        public string Student_FName { get; set; }
        public string Student_LName { get; set; }
        public string Student_Nationality { get; set; }
        public string Race { get; set; }
        public string Title { get; set; }
        public string Gender { get; set; }
        public System.DateTime Date_Of_Birth { get; set; }
        public string Marital_Status { get; set; }
        public string Student_Email { get; set; }
        public string Student_Cellphone_Number { get; set; }
        public string Student_Residential_Address { get; set; }
        public byte[] Upload_Identity_Document { get; set; }
        public byte[] Upload_Residential_Document { get; set; }
        public string Password { get; set; }
    }
}
