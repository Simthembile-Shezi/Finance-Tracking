namespace Finance_Tracking.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Funder")]
    public partial class Funder
    {
        public Funder()
        {
            Bursaries = new List<Bursary>();
            Funder_Employee = new List<Funder_Employee>();
        }

        public Funder(string funder_Name, string funder_Tax_Number, string funder_Email, string funder_Telephone_Number, string funder_Physical_Address, string funder_Postal_Address)
        {
            Funder_Name = funder_Name;
            Funder_Tax_Number = funder_Tax_Number;
            Funder_Email = funder_Email;
            Funder_Telephone_Number = funder_Telephone_Number;
            Funder_Physical_Address = funder_Physical_Address;
            Funder_Postal_Address = funder_Postal_Address;
            Bursaries = new List<Bursary>();
            Funder_Employee = new List<Funder_Employee>();
        }

        [Key]
        [Display(Name = "Funder Name")]
        [Required(ErrorMessage = "You need to enter the name of the Funder.")]
        [StringLength(50)]
        public string Funder_Name { get; set; }

        [Display(Name = "Tax Number")]
        [Required(ErrorMessage = "You need to enter the tax number of the Funder.")]
        [StringLength(50)]
        public string Funder_Tax_Number { get; set; }

        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "You need to enter the email address of the Funder.")]
        [StringLength(100)]
        public string Funder_Email { get; set; }

        [Display(Name = "Confirm Email")]
        [DataType(DataType.EmailAddress)]
        [Compare("Funder_Email", ErrorMessage = "Email and confirm email must match.")]
        [StringLength(50)]
        public string ConfirmFunder_Email { get; set; }

        [Display(Name = "Telephone Number")]
        [Required(ErrorMessage = "You need to enter the telephone number of the Funder.")]
        [StringLength(50)]
        public string Funder_Telephone_Number { get; set; }

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

        [Required]
        public string Funder_Physical_Address { get; set; }

        [Required]
        public string Funder_Postal_Address { get; set; }

        public Bursary Bursary { get; set; }
        public List<Bursary> Bursaries = new List<Bursary>();

        public List<Funder_Employee> Funder_Employee { get; set; }
    }
}
