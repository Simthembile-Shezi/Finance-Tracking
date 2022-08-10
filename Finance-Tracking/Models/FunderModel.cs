using System;
using System.ComponentModel.DataAnnotations;

namespace Finance_Tracking.Models
{
    public class FunderModel
    {
        [Display(Name = "Tax Number")]
        [Required(ErrorMessage = "You need to enter the tax number of the Funder.")]
        public string Funder_Tax_Number { get; set; }

        [Display(Name = "Bursary Name")]
        [Required(ErrorMessage = "You need to enter the name of the Bursary or N/A.")]
        public string Bursary_Name { get; set; }

        [Display(Name = "Funder Name")]
        [Required(ErrorMessage = "You need to enter the name of the Funder.")]
        public string Funder_Name { get; set; }

        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "You need to enter the email address of the Funder.")]
        public string Funder_Email { get; set; }

        [Display(Name = "Confirm Email")]
        [DataType(DataType.EmailAddress)]
        [Compare("Funder_Email", ErrorMessage = "Email and confirm email must match.")]
        public string ConfirmFunder_Email { get; set; }

        [Display(Name = "Telephone Number")]
        [Required(ErrorMessage = "You need to enter the telephone number of the Funder.")]
        public string Funder_Telephone_Number { get; set; }

        [Display(Name = "Physical Address")]
        [Required(ErrorMessage = "You need to enter the physical address of the Funder.")]
        public string Funder_Physical_Address { get; set; }
    }
}