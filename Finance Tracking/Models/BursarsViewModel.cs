using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Finance_Tracking.Models
{
    public class BursarsViewModel
    {
        public List<Bursar_Fund> Bursars = new List<Bursar_Fund>();
        public Bursary Bursary { get; set; }
        
        public Bursar_Fund Bursar_Fund { get; set; }
        public Application Application { get; set; }
    }
}