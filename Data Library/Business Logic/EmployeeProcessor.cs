using Data_Library.Data_Access;
using Data_Library.ModelsDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Library.Business_Logic
{
    public static class EmployeeProcessor
    {
        public static int AddEmployee(string Emp_ID, string Emp_FName,string Emp_LName, string Emp_Telephone_Number, string Emp_Email,
            string Organization_Name, string Password, string Admin_Code/*, FunderDB_Model Funder, InstitutionDB_Model Institution*/)
        {
            EmployeeDB_Model data = new EmployeeDB_Model
            {
                Emp_ID = Emp_ID,
                Emp_FName = Emp_FName,
                Emp_LName = Emp_LName,
                Emp_Telephone_Number = Emp_Telephone_Number,
                Emp_Email = Emp_Email,
                Organization_Name = Organization_Name,
                Password = Password,
                Admin_Code = Admin_Code,
                //Funder = Funder,
                //Institution = Institution
            };


            //set identity_insert Institution on;

            string sql = @" set identity_insert Employee on;
                            insert into dbo.Employee (Emp_ID, Emp_FName, Emp_LName, Emp_Telephone_Number, Emp_Email, Organization_Name, Password, Admin_Code)
                            values (@Emp_ID, @Emp_FName, @Emp_LName, @Emp_Telephone_Number, @Emp_Email, @Organization_Name, @Password, @Admin_Code);";

            return SqlDataAccess.SaveDataEmp(sql, data);
        }

        public static List<EmployeeDB_Model> LoadEmployees()
        {
            string sql = @"select Emp_ID, Emp_FName, Emp_LName, Emp_Telephone_Number, Emp_Email, Organization_Name, Password, Admin_Code
                           from dbo.Employee;";

            return SqlDataAccess.LoadData<EmployeeDB_Model>(sql);
        }
    }
}
