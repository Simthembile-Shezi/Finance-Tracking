using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Finance_Tracking.Models
{
    public class SendEmailViewModel
    {
        [Display(Name = "Select user type")]
        public string UserType { get; set; }

        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "You need to enter your email address.")]
        [StringLength(50)]
        public string ToEmail { get; set; }
    }
}