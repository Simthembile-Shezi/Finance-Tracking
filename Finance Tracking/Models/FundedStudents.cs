using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Finance_Tracking.Models
{
    public class FundedStudents
    {
        public FundedStudents()
        {

        }

        public FundedStudents(string StudentNum, string StudentID, string Email, string InstName, string Status)
        {
            Student_Number = StudentNum;
            Student_Identity_Number = StudentID;
            Student_Email = Email;
            Institution_Name = InstName;
            Funding_Status = Status;
        }
        [Display(Name = "Student Number")]
        public string Student_Number { get; set; }

        [Display(Name = "Student ID Number")]
        public string Student_Identity_Number { get; set; }

        [Display(Name = "Institution Name")]
        public string Institution_Name { get; set; }

        [Display(Name = "Email Address")]
        public string Student_Email { get; set; }

        [Display(Name = "Funding Status")]
        public string Funding_Status { get; set; }

        //public virtual Bursar_Fund Bursar_Funds { get; set; }
        //public virtual Bursary Bursary { get; set; }
        //public virtual Enrolled_At Student { get; set; }
    }
}