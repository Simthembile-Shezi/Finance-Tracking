using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Finance_Tracking.Models
{
    public class EnrolledStudentsViewModel
    {
        //public List<Bursar_Fund> Students = new List<Bursar_Fund>();

        //public Bursar_Fund Bursar_Fund { get; set; }

        public List<FundedStudents> Students = new List<FundedStudents>();

        public FundedStudents FundedStudent { get; set; }

    }
}