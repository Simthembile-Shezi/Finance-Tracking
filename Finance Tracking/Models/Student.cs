namespace Finance_Tracking.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Web;

    [Table("Student")]
    public partial class Student
    {
        public Student()
        {
            Applications = new List<Application>();
            Enrolled_Ats = new List<Enrolled_At>();
        }
        public Student(string student_Identity_Number, string student_FName, string student_LName, string student_Nationality, string race, string title, string gender, DateTime date_Of_Birth, string marital_Status, string student_Email, string student_Cellphone_Number, string student_Residential_Address, byte[] upload_Identity_Document, byte[] upload_Residential_Document)
        {
            Student_Identity_Number = student_Identity_Number;
            Student_FName = student_FName;
            Student_LName = student_LName;
            Student_Nationality = student_Nationality;
            Race = race;
            Title = title;
            Gender = gender;
            Date_Of_Birth = date_Of_Birth;
            Marital_Status = marital_Status;
            Student_Email = student_Email;
            Student_Cellphone_Number = student_Cellphone_Number;
            Student_Residential_Address = student_Residential_Address;
            Upload_Identity_Document = upload_Identity_Document;
            Upload_Residential_Document = upload_Residential_Document;
            Applications = new List<Application>();
            Enrolled_Ats = new List<Enrolled_At>();
        }

        [Required(ErrorMessage = "You need to enter the code sent via email.")]
        public string Code { get; set; }

        [Key]
        [Display(Name = "Identity Number")]
        [Required(ErrorMessage = "You need to enter your identity number.")]
        public string Student_Identity_Number { get; set; }

        [Display(Name = "First Name")]
        [Required(ErrorMessage = "You need to enter your first name.")]
        public string Student_FName { get; set; }

        [Display(Name = "Surname")]
        [Required(ErrorMessage = "You need to enter your surname.")]
        public string Student_LName { get; set; }

        [Display(Name = "Nationality")]
        [Required(ErrorMessage = "You need to enter your nationality.")]
        public string Student_Nationality { get; set; }

        [Display(Name = "Ethnic Group")]
        [Required(ErrorMessage = "You need to select your race.")]
        public string Race { get; set; }

        [Display(Name = "Title")]
        [Required(ErrorMessage = "You need to select your title.")]
        public string Title { get; set; }

        [Display(Name = "Gender")]
        [Required(ErrorMessage = "You need to select your gender.")]
        public string Gender { get; set; }

        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date)]

        [Required(ErrorMessage = "You need to enter your date of birth.")]
        public DateTime Date_Of_Birth { get; set; }

        [Display(Name = "Marital Status")]
        [Required(ErrorMessage = "You need to enter select marital status.")]
        public string Marital_Status { get; set; }

        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "You need to enter your email address.")]
        public string Student_Email { get; set; }

        [Display(Name = "Confirm Email")]
        [Compare("Student_Email", ErrorMessage = "Email and confirm email must match.")]
        public string ConfirmStudent_Email { get; set; }

        [Display(Name = "Cellphone Number")]
        [Required(ErrorMessage = "You need to enter your cellphone number.")]
        public string Student_Cellphone_Number { get; set; }

        [Display(Name = "Address 1")]
        [Required(ErrorMessage = "You need to enter the street name.")]
        public string Street_Name { get; set; }

        [Display(Name = "Address 2")]
        [Required(ErrorMessage = "You need to enter the suburb name.")]
        public string Sub_Town { get; set; }

        [Display(Name = "Address 3")]
        [Required(ErrorMessage = "You need to enter the city name.")]
        public string City { get; set; }

        [Display(Name = "Province")]
        [Required(ErrorMessage = "You need to enter province name.")]
        public string Province { get; set; }

        [Display(Name = "Zip Code")]
        [Required(ErrorMessage = "You need to enter zip code.")]
        public string Zip_Code { get; set; }

        [Display(Name = "Home Residential Address")]
        public string Student_Residential_Address { get; set; }

        [DataType(DataType.Upload)]
        [Display(Name = "Upload Identity Document")]
        public HttpPostedFileBase Identity_Document { get; set; }

        public byte[] Upload_Identity_Document { get; set; }

        [DataType(DataType.Upload)]
        [Display(Name = "Upload Resdential Document")]
        public HttpPostedFileBase Residential_Document { get; set; }
        public byte[] Upload_Residential_Document { get; set; }


        [Display(Name = "Password")]
        [Required(ErrorMessage = "You need to create a password.")]
        [DataType(DataType.Password)]
        [StringLength(20, MinimumLength = 8, ErrorMessage = "You need to a provide a long enoungh password.")]
        public string Password { get; set; }

        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password and confirm password must match.")]
        public string ConfirmPassword { get; set; }

        public Application Application { get; set; }
        public List<Application> Applications { get; set; }
        public Enrolled_At Enrolled_At { get; set; }
        public List<Enrolled_At> Enrolled_Ats { get; set; }
    }
}
