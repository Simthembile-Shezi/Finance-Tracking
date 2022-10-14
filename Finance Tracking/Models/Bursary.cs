namespace Finance_Tracking.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Bursary")]
    public partial class Bursary
    {
        public Bursary()
        {
            Application = new Application();
            Applications = new List<Application>();
            ApplicationViews = new List<ApplicationView>();
        }
        public Bursary(string bursary_Code, string bursary_Name, DateTime start_Date, string funder_Name, DateTime? end_Date, decimal? bursary_Amount, string number_Available, string description, string funding_Year)
        {
            Bursary_Code = bursary_Code;
            Bursary_Name = bursary_Name;
            Start_Date = start_Date;
            Funder_Name = funder_Name;
            End_Date = end_Date;
            Bursary_Amount = bursary_Amount;
            Number_Available = number_Available;
            Description = description;
            Funding_Year = funding_Year;
            Applications = new List<Application>();
        }

        [Key]
        [Display(Name = "Bursary Code")]
        [StringLength(50)]
        public string Bursary_Code { get; set; }

        [Required]
        [Display(Name = "Bursary Name")]
        [StringLength(50)]
        public string Bursary_Name { get; set; }

        [Display(Name = "Opening Date")]
        [DataType(DataType.Date)]
        public DateTime Start_Date { get; set; }

        [Required]
        [Display(Name = "Funder Name")]
        [StringLength(50)]
        public string Funder_Name { get; set; }

        [Display(Name = "Closing Date")]
        [DataType(DataType.Date)]
        public DateTime? End_Date { get; set; }

        [Display(Name = "Bursary Amount")]
        [Column(TypeName = "money")]
        public decimal? Bursary_Amount { get; set; }

        [Display(Name = "Available Bursaries")]
        [Column(TypeName = "numeric")]
        public string Number_Available { get; set; }

        [Required]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Required(ErrorMessage = "You need to enter the funding year.")]
        [Display(Name = "Funding Year")]
        public string Funding_Year { get; set; }

        public Application Application { get; set; }
        public List<Application> Applications { get; set; }

        public ApplicationView ApplicationView { get; set; }
        public List<ApplicationView> ApplicationViews { get; set; }

        public virtual Funder Funder { get; set; }
    }
}
