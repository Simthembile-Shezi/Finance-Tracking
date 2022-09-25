using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Library.ModelsDB
{
    public class Enrolled_AtDB
    {
        public string Student_Number { get; set; }
        public string Student_Identity_Number { get; set; }
        public string Institution_Name { get; set; }
        public string Qualification { get; set; }
        public string Student_Email { get; set; }
        public string Study_Residential_Address { get; set; }
        public virtual InstitutionDB Institution { get; set; }
        public virtual StudentDB Student { get; set; }
    }
}
