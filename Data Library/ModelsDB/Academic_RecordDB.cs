using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Library.ModelsDB
{
    public class Academic_RecordDB
    {
        public string Student_Number { get; set; }
        public DateTime Academic_Year { get; set; }
        public string Qualification { get; set; }
        public decimal? Avarage_Marks { get; set; }
        public byte[] Upload_Transcript { get; set; }
        public virtual Enrolled_AtDB Enrolled_At { get; set; }
    }
}
