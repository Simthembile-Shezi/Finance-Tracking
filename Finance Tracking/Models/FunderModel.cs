using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace Finance_Tracking.Models
{
    [Table("Funder")]

    public class FunderModel
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]

        //public string Funder { get; set; }
        public FunderModel()
        {
            Funder_EmployeeModel = new HashSet<Funder_EmployeeModel>();
        }

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

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Funder_EmployeeModel> Funder_EmployeeModel { get; set; }
    }
}