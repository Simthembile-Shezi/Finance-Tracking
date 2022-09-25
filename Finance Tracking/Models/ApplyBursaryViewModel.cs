using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Finance_Tracking.Models
{
    public class ApplyBursaryViewModel
    {
        public List<Bursary> Bursaries = new List<Bursary>();
        public List<Application> Applications = new List<Application>();
        public Bursary Bursary { get; set; }
        public Application Application { get; set; }

    }
}