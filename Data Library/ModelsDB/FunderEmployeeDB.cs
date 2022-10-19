using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Library.ModelsDB
{
    public class FunderEmployeeDB
    {
        public string Emp_UserID { get; set; }
        public string Emp_FName { get; set; }
        public string Emp_LName { get; set; }
        public string Emp_Telephone_Number { get; set; }
        public string Emp_Email { get; set; }
        public string Organization_Name { get; set; }
        public string Password { get; set; }
        public string Admin_Code { get; set; }
        public virtual FunderDB Funder { get; set; }
    }
}
