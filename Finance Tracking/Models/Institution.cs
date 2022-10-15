namespace Finance_Tracking.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Institution")]
    public partial class Institution
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Institution()
        {
            Enrolled_Ats = new List<Enrolled_At>();
            Institution_Employees = new List<Institution_Employee>();
        }
        public Institution(string institution_Name, string institution_Telephone_Number, string institution_Email_Address, string institution_Physical_Address, string institution_Postal_Address)
        {
            Institution_Name = institution_Name;
            Institution_Telephone_Number = institution_Telephone_Number;
            Institution_Email_Address = institution_Email_Address;
            Institution_Physical_Address = institution_Physical_Address;
            Institution_Postal_Address = institution_Postal_Address;
            Enrolled_Ats = new List<Enrolled_At>();
            Institution_Employees = new List<Institution_Employee>();
        }

        [Key]
        [Display(Name = "Institution Name")]
        [Required(ErrorMessage = "You need to enter the name of the Institution.")]
        public string Institution_Name { get; set; }

        [Display(Name = "Street Name")]
        [Required(ErrorMessage = "You need to enter the street name.")]
        public string Street_Name { get; set; }

        [Display(Name = "Sub Town")]
        [Required(ErrorMessage = "You need to enter the sub town name.")]
        public string Sub_Town { get; set; }

        [Display(Name = "City")]
        [Required(ErrorMessage = "You need to enter the city name.")]
        public string City { get; set; }

        [Display(Name = "Province")]
        [Required(ErrorMessage = "You need to enter province name.")]
        public string Province { get; set; }

        [Display(Name = "Zip Code")]
        [Required(ErrorMessage = "You need to enter zip code.")]
        public string Zip_Code { get; set; }

        [Display(Name = "Postal Box")]
        [Required(ErrorMessage = "You need to enter the street name.")]
        public string Postal_box { get; set; }

        [Display(Name = "Town")]
        [Required(ErrorMessage = "You need to enter the sub town name.")]
        public string Town { get; set; }

        [Display(Name = "City")]
        [Required(ErrorMessage = "You need to enter the city name.")]
        public string City_Post { get; set; }

        [Display(Name = "Province")]
        [Required(ErrorMessage = "You need to enter province name.")]
        public string Province_Post { get; set; }

        [Display(Name = "Postal Code")]
        [Required(ErrorMessage = "You need to enter zip code.")]
        public string Postal_Code { get; set; }

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

        [Required]
        public string Institution_Physical_Address { get; set; }

        [Required]
        public string Institution_Postal_Address { get; set; }

        public Enrolled_At Enrolled_At { get; set; }
        public virtual List<Enrolled_At> Enrolled_Ats { get; set; }

        public Institution_Employee Institution_Employee { get; set; }
        public virtual List<Institution_Employee> Institution_Employees { get; set; }
    }
}
