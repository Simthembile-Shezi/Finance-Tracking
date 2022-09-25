using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Finance_Tracking.Models
{
    public class EmployeeModel
    {
        public string Emp_ID { get; set; }

        [Display(Name = "First Name")]
        [Required(ErrorMessage = "You need to enter your first name.")]
        public string Emp_FName { get; set; }

        [Display(Name = "Surname")]
        [Required(ErrorMessage = "You need to enter your surname.")]
        public string Emp_LName { get; set; }

        [Display(Name = "Telephone Number")]
        [Required(ErrorMessage = "You need to enter your telephone number.")]
        public string Emp_Telephone_Number { get; set; }

        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "You need to enter your email address.")]
        public string Emp_Email { get; set; }

        [Display(Name = "Confirm Email")]
        [Compare("Emp_Email", ErrorMessage = "Email and confirm email must match.")]
        public string ConfirmEmp_Email { get; set; }


        [Display(Name = "Organization Name")]
        [Required(ErrorMessage = "You need to enter the organization name you work for.")]
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
        public string Admin_Code { get; set; }

    }
}