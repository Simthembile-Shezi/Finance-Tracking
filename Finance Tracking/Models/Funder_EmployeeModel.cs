using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Data.Entity.Spatial;

namespace Finance_Tracking.Models
{
    [Table("Funder Employee")]
    public class Funder_EmployeeModel
    {
        [Required]
        [StringLength(50)]
        public string Emp_ID { get; set; }

        [Display(Name = "First Name")]
        [Required(ErrorMessage = "You need to enter your first name.")]
        [StringLength(50)]
        public string Emp_FName { get; set; }

        [Display(Name = "Surname")]
        [Required(ErrorMessage = "You need to enter your surname.")]
        [StringLength(50)]
        public string Emp_LName { get; set; }

        [Display(Name = "Telephone Number")]
        [Required(ErrorMessage = "You need to enter your telephone number.")]
        [StringLength(50)]
        public string Emp_Telephone_Number { get; set; }

        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "You need to enter your email address.")]
        [StringLength(50)]
        public string Emp_Email { get; set; }

        [Display(Name = "Confirm Email")]
        [Compare("Emp_Email", ErrorMessage = "Email and confirm email must match.")]
        [StringLength(50)]
        public string ConfirmEmp_Email { get; set; }


        [Display(Name = "Organization Name")]
        [Required(ErrorMessage = "You need to enter the organization name you work for.")]
        [StringLength(50)]
        public string Organization_Name { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "You need to create a password.")]
        [DataType(DataType.Password)]
        [StringLength(20, MinimumLength = 8, ErrorMessage = "You need to a provide a long enoungh password.")]
        public string Password { get; set; }

        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password and confirm password must match.")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Admin Code")]
        [Required(ErrorMessage = "You need to create an admin code.")]
        [StringLength(10)]
        public string Admin_Code { get; set; }

        public virtual FunderModel FunderModel { get; set; }
    }
}