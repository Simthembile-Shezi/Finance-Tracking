using System;
using System.ComponentModel.DataAnnotations;

namespace Finance_Tracking.Models
{
    public class FunderModel
    {
        [Display(Name = "Tax Number")]
        public int Funder_Tax_Number { get; set; }

        [Display(Name = "Bursary Name")]
        public string Bursary_Name { get; set; }

        [Display(Name = "Funder Name")]
        public string Funder_Name { get; set; }

        [Display(Name = "Email")]
        public string Funder_Email { get; set; }

        [Display(Name = "Telephone Number")]
        public string Funder_Telephone_Number { get; set; }

        [Display(Name = "Physical Address")]
        public string Funder_Physical_Address { get; set; }
    }
}