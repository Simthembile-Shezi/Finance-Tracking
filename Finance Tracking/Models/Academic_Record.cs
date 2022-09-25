namespace Finance_Tracking.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web;

    [Table("Academic Records")]
    public partial class Academic_Record
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(50)]
        public string Student_Number { get; set; }

        [Key]
        [Display(Name = "Academic Year")]
        [Column(Order = 1, TypeName = "date")]
        public DateTime Academic_Year { get; set; }

        [Required]
        [Display(Name = "Qualification")]
        [StringLength(50)]
        public string Qualification { get; set; }

        [Display(Name = "Avarage Marks")]
        public decimal? Avarage_Marks { get; set; }

        [Display(Name = "Upload Transcript")]
        public byte[] Upload_Transcript { get; set; }

        public HttpPostedFileBase transcript { get; set; }

        public virtual Enrolled_At Enrolled_At { get; set; }
    }
}
