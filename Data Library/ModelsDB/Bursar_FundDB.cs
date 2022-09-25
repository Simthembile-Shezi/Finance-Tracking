using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Library.ModelsDB
{
    public class Bursar_FundDB
    {
        public string Application_ID { get; set; }
        public string Update_Fund_Request { get; set; }
        public string Funding_Status { get; set; }
        public decimal? Approved_Funds { get; set; }
        public virtual ApplicationDB Application { get; set; }
    }
}
