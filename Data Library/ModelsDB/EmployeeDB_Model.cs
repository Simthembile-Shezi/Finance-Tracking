using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Library.ModelsDB
{
    public class EmployeeDB_Model
    {
        public string Emp_ID { get; set; }
        public string Emp_FName { get; set; }
        public string Emp_LName { get; set; }
        public string Emp_Telephone_Number { get; set; }
        public string Emp_Email { get; set; }
        public string Organization_Name { get; set; }
        public virtual FunderDB_Model Funder { get; set; }
        public virtual InstitutionDB_Model Institution { get; set; }


        public string Password { get; set; }
        public string Admin_Code { get; set; }

    }
}
