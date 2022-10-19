namespace Finance_Tracking.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Enrolled At")]
    public partial class Enrolled_At
    {
        

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Enrolled_At()
        {
            Academic_Records = new List<Academic_Record>();
            Finacial_Records = new List<Finacial_Record>();
        }
        public Enrolled_At(string student_Number, string student_Identity_Number, string institution_Name, string qualification, string student_Email, string study_Residential_Address)
        {
            Student_Number = student_Number;
            Student_Identity_Number = student_Identity_Number;
            Institution_Name = institution_Name;
            Qualification = qualification;
            Student_Email = student_Email;
            Study_Residential_Address = study_Residential_Address;
            Academic_Records = new List<Academic_Record>();
            Finacial_Records = new List<Finacial_Record>();
        }

        [Key]
        [Display(Name ="Student Number")]
        [StringLength(50)]
        public string Student_Number { get; set; }

        [Required]
        [Display(Name = "Student ID Number")]
        [StringLength(50)]
        public string Student_Identity_Number { get; set; }

        [Required]
        [Display(Name = "Institution Name")]
        [StringLength(50)]
        public string Institution_Name { get; set; }

        [Required]
        [Display(Name = "Qualification")]
        [StringLength(50)]
        public string Qualification { get; set; }

        [Required]
        [Display(Name = "Email Address")]
        [StringLength(50)]
        public string Student_Email { get; set; }

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

        [Required]
        [Display(Name = "Study Residential Address")]
        [StringLength(50)]
        public string Study_Residential_Address { get; set; }
        public List<Academic_Record> Academic_Records { get; set; }

        public virtual Institution Institution { get; set; }

        public virtual Student Student { get; set; }

        public virtual Finacial_Record Finacial_Record { get; set; }

        public List<Finacial_Record> Finacial_Records { get; set; }
    }
}
