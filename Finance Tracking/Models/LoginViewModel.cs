using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Finance_Tracking.Models
{
    public class LoginViewModel
    {
        [Display(Name = "Select user type")]
        public string UserType { get; set; }

        [Display(Name = "Remeber username")]
        public bool Remember_username { get; set; }

        public string Username { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "You need to enter a password.")]
        [DataType(DataType.Password)]
        [StringLength(20, MinimumLength = 8, ErrorMessage = "You need to a provide a long enoungh password.")]
        public string Password { get; set; }
    }
}