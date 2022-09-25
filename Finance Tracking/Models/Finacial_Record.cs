namespace Finance_Tracking.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web;

    [Table("Finacial Records")]
    public partial class Finacial_Record
    {
        public Finacial_Record()
        {

        }

        public Finacial_Record(string student_Number, string academic_Year, decimal? balance_Amount, byte[] upload_Statement, string funding_Status, string request_Funds)
        {
            Student_Number = student_Number;
            Academic_Year = academic_Year;
            Balance_Amount = balance_Amount;
            Upload_Statement = upload_Statement;
            Funding_Status = funding_Status;
            Request_Funds = request_Funds;
        }

        [Key]
        [Column(Order = 0)]
        [StringLength(50)]
        public string Student_Number { get; set; }

        [Key]
        [Required(ErrorMessage = "You need to enter the year e.g 2019.")]
        [Display (Name = "Academic Year")]
        public string Academic_Year { get; set; }

        [Display(Name = "Balance Amount")]
        [Column(TypeName = "money")]
        public decimal? Balance_Amount { get; set; }

        [DataType(DataType.Upload)]
        [Display(Name = "Upload Statement")]
        public HttpPostedFileBase Upload_Finacial_Statement { get; set; }
        public byte[] Upload_Statement { get; set; }

        [Display(Name = "Funding Status")]
        [StringLength(50)]
        public string Funding_Status { get; set; }

        [Display(Name = "Funds Request Status")]
        [StringLength(50)]
        public string Request_Funds { get; set; }

        public virtual Enrolled_At Enrolled_At { get; set; }
    }
}
