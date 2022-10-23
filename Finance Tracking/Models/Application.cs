namespace Finance_Tracking.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web;

    [Table("Application")]
    public partial class Application
    {
        public Application(string application_ID, string student_Identity_Number, string bursary_Code, string funding_Year, string application_Status, byte[] upload_Agreement, byte[] upload_Signed_Agreement)
        {
            Application_ID = application_ID;
            Student_Identity_Number = student_Identity_Number;
            Bursary_Code = bursary_Code;
            Funding_Year = funding_Year;
            Application_Status = application_Status;
            Upload_Agreement = upload_Agreement;
            Upload_Signed_Agreement = upload_Signed_Agreement;
            Bursar_Funds = new List<BursarFundView>();
        }
        public Application()
        {
            Bursar_Funds = new List<BursarFundView>();
        }

        [Key]
        [Display(Name = "Application ID")]
        [StringLength(50)]
        public string Application_ID { get; set; }

        [Required]
        [Display(Name = "Identity Number")]
        [StringLength(50)]
        public string Student_Identity_Number { get; set; }

        [Required]
        [Display(Name = "Bursary Code")]
        [StringLength(50)]
        public string Bursary_Code { get; set; }

        [Display(Name = "Funding Year")]
        public string Funding_Year { get; set; }

        [Display(Name = "Application Status")]
        [StringLength(50)]
        public string Application_Status { get; set; }

        [DataType(DataType.Upload)]
        [Display(Name = "Upload Bursary Agreement")]
        public HttpPostedFileBase Bursary_Agreement { get; set; }
        public byte[] Upload_Agreement { get; set; }

        [DataType(DataType.Upload)]
        [Display(Name = "Upload Signed Agreement")]
        public HttpPostedFileBase Signed_Bursary_Agreement { get; set; }
        public byte[] Upload_Signed_Agreement { get; set; }

        public virtual Bursar_Fund Bursar_Fund { get; set; }
        public virtual BursarFundView BursarFundView { get; set; }
        public List<BursarFundView> Bursar_Funds { get; set; }

        public virtual Bursary Bursary { get; set; }

        public virtual Student Student { get; set; }
    }
}
