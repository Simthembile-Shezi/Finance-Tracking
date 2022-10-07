using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Finance_Tracking.Models
{
    public class BursarFundView
    {
        public BursarFundView()
        { }

        public BursarFundView(string FName, string LName, string studentID, string gender, string Cellphone, string email, string application_ID, string update_Fund_Request, string funding_Status, decimal? approved_Funds)
        {
            //Student details
            Student_FName = FName;
            Student_LName = LName;
            Student_Identity_Number = studentID;
            Gender = gender;
            Student_Cellphone_Number = Cellphone;
            Student_Email = email;

            Application_ID = application_ID;
            Update_Fund_Request = update_Fund_Request;
            Funding_Status = funding_Status;
            Approved_Funds = approved_Funds;
        }

        public string Application_ID { get; set; }

        //Student details

        [Display(Name = "First Name")]
        public string Student_FName { get; set; }

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


        [Display(Name = "Fund Request Status")]
        [StringLength(50)]
        public string Update_Fund_Request { get; set; }

        [Display(Name = "Funding Status")]
        [StringLength(50)]
        public string Funding_Status { get; set; }

        [Display(Name = "Approved Funds")]
        [Column(TypeName = "money")]
        public decimal? Approved_Funds { get; set; }
    }
}