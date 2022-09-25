namespace Finance_Tracking.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Institution Employee")]
    public partial class Institution_Employee
    {
        public Institution_Employee(string emp_FName, string emp_LName, string emp_Telephone_Number, string emp_Email, string organization_Name, string password, string admin_Code, Institution institution)
        {
            Emp_FName = emp_FName;
            Emp_LName = emp_LName;
            Emp_Telephone_Number = emp_Telephone_Number;
            Emp_Email = emp_Email;
            Organization_Name = organization_Name;
            Password = password;
            Admin_Code = admin_Code;
            Institution = institution;
        }
        public Institution_Employee()
        {

        }

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

        [Key]
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "You need to enter your email address.")]
        [StringLength(50)]
        public string Emp_Email { get; set; }

        [Display(Name = "Confirm Email")]
        [Compare("Emp_Email", ErrorMessage = "Email and confirm email must match.")]
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

        public virtual Institution Institution { get; set; }
    }
}
