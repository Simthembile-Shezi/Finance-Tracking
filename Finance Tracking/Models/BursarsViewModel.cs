using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Finance_Tracking.Models
{
    public class BursarsViewModel
    {
        public List<BursarFundView> Bursars = new List<BursarFundView>();
        public Bursary Bursary { get; set; }
        
        public BursarFundView Bursar_Fund { get; set; }
        public Application Application { get; set; }
    }
}