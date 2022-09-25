using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Finance_Tracking.Models
{
    public class LoginViewModel
    {
        public Student Student { get; set; }
        public Funder Funder { get; set; }
        public Institution Institution { get; set; }

        [Display(Name = "Select user type")]
        public string UserType { get; set; }

        [Display(Name = "Remeber username")]
        public bool Remember_username { get; set; }

        public string Username { get; set; }
        public string Password { get; set; }
    }
}