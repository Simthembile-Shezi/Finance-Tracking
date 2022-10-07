namespace Finance_Tracking.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Bursar Funds")]
    public partial class Bursar_Fund
    {
        public Bursar_Fund(string application_ID, string update_Fund_Request, string funding_Status, decimal? approved_Funds)
        {
            Application_ID = application_ID;
            Update_Fund_Request = update_Fund_Request;
            Funding_Status = funding_Status;
            Approved_Funds = approved_Funds;
        }
        public Bursar_Fund()
        {

        }

        [Key]
        [Display(Name = "Application ID")]
        [StringLength(50)]
        public string Application_ID { get; set; }

        [Display (Name = "Fund Request Status")]
        [StringLength(50)]
        public string Update_Fund_Request { get; set; }

        [Display(Name = "Funding Status")]
        [StringLength(50)]
        public string Funding_Status { get; set; }

        [Display(Name = "Approved Funds")]
        [Column(TypeName = "money")]
        public decimal? Approved_Funds { get; set; }

        public virtual Application Application { get; set; }
    }
}
