using System;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace Finance_Tracking.Models
{
    public class ApplicationView
    {
        public ApplicationView()
        {

        }

        public ApplicationView(string appID, string status, string FName, string LName, string studentID, string gender, string Cellphone, string email, string studentNum, string Ins_Name, string Quali, string AY, decimal marks, byte[] transcript, string bursary_Code)
        {
            Application_ID = appID;
            Application_Status = status;

            //Student details
            Student_FName = FName;
            Student_LName = LName;
            Student_Identity_Number = studentID;
            Gender = gender;
            Student_Cellphone_Number = Cellphone;
            Student_Email = email;

            //Institution details
            Student_Number = studentNum;
            Institution_Name = Ins_Name;
            Qualification = Quali;
            Academic_Year = AY;
            Avarage_Marks = marks;
            Upload_Transcript = transcript;

            //Bursary
            Bursary_Code = bursary_Code;
        }

        public string Application_ID { get; set; }

        //Student details

        [Display(Name = "First Name")]
        public string Student_FName { get; set; }

        [Display(Name = "Application Status")]
        public string Application_Status { get; set; }

        [Display(Name = "Surname")]
        public string Student_LName { get; set; }
        [Display(Name = "Identity Number")]
        public string Student_Identity_Number { get; set; }

        [Display(Name = "Gender")]
        public string Gender { get; set; }

        [Display(Name = "Cellphone Number")]
        public string Student_Cellphone_Number { get; set; }

        [Display(Name = "Email")]
        public string Student_Email { get; set; }

        [Display(Name = "Street Name")]
        public string Street_Name { get; set; }

        [Display(Name = "Sub Town")]
        public string Sub_Town { get; set; }

        [Display(Name = "City")]
        public string City { get; set; }

        [Display(Name = "Province")]
        public string Province { get; set; }

        [Display(Name = "Zip Code")]
        public string Zip_Code { get; set; }

        //Institution details


        [Display(Name = "Student Number")]
        public string Student_Number { get; set; }

        [Display(Name = "Institution Name")]
        public string Institution_Name { get; set; }

        [Display(Name = "Qualification")]
        public string Qualification { get; set; }

        [Display(Name = "Academic Year")]
        public string Academic_Year { get; set; }

        [Display(Name = "Avarage Marks")]
        public decimal Avarage_Marks { get; set; }

        [Display(Name = "Upload Transcript")]
        public byte[] Upload_Transcript { get; set; }

        public HttpPostedFileBase transcript { get; set; }

        public string Bursary_Code { get; set; }

        public virtual Bursary Bursary { get; set; }
        //public string Funding_Year { get; }
        //public string Application_Status { get; }
        //public byte[] Upload_Agreement { get; }
        //public byte[] Upload_Signed_Agreement { get; }
    }
}