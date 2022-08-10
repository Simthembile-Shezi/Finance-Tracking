using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Finance_Tracking.Models
{
    public class InstitutionModel
    {
        //public int Institution_ID { get; set; }

        [Display(Name = "Institution Name")]
        [Required(ErrorMessage = "You need to enter the name of the Institution.")]
        public string Institution_Name { get; set; }

        [Display(Name = "Physical Address")]
        [Required(ErrorMessage = "You need to enter the institution's physical address.")]
        public string Institution_Physical_Address { get; set; }

        [Display(Name = "Postal Address")]
        [Required(ErrorMessage = "You need to enter the institution's postal address.")]
        public string Institution_Postal_Address { get; set; }

        [Display(Name = "Telephone Number")]
        [Required(ErrorMessage = "You need to enter the institution's telephone number.")]
        public string Institution_Telephone_Number { get; set; }

        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "You need to enter the institution's email address.")]
        public string Institution_Email_Address { get; set; }

        [Display(Name = "Confirm Email")]
        [DataType(DataType.EmailAddress)]
        [Compare("Institution_Email_Address", ErrorMessage = "Email and confirm email must match.")]
        public string ConfirmInstitution_Email { get; set; }
    }
}