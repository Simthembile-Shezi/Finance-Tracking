using Data_Library.Data_Access;
using Data_Library.ModelsDB;
using System.Collections.Generic;

namespace Data_Library.Business_Logic
{
    public static class InstitutionEmpProcessor
    {
        public static int AddInstitutionEmp(string Emp_FName, string Emp_LName, string Emp_Telephone_Number, string Emp_Email,
            string Organization_Name, string Password, string Admin_Code)
        {
            InstitutionEmployeeDB data = new InstitutionEmployeeDB
            {
                Emp_FName = Emp_FName,
                Emp_LName = Emp_LName,
                Emp_Telephone_Number = Emp_Telephone_Number,
                Emp_Email = Emp_Email,
                Organization_Name = Organization_Name,
                Password = Password,
                Admin_Code = Admin_Code
            };

            string sql = @" insert into [dbo].[Institution Employee] (Emp_FName, Emp_LName, Emp_Telephone_Number, Emp_Email, Organization_Name, Password, Admin_Code)
                            values (@Emp_FName, @Emp_LName, @Emp_Telephone_Number, @Emp_Email, @Organization_Name, @Password, @Admin_Code);";

            return SqlDataAccess.SaveData(sql, data);
        }

        //public static int UpdateInstitutionEmp(string Emp_Email)
        //{
        //    InstitutionEmployeeDB data = new InstitutionEmployeeDB();
        //    data.Emp_Email = Emp_Email;

        //    if (updateRequest != null)
        //        data.Update_Fund_Request = updateRequest;
        //    if (status != null)
        //        data.Funding_Status = status;
        //    if (approvedFunds != null)
        //        data.Approved_Funds = approvedFunds;

        //    string sql = @"update dbo.[Bursar Funds] 
        //                           set Update_Fund_Request = @Update_Fund_Request,
        //                               Funding_Status = @Funding_Status,
        //                               Approved_Funds = @Approved_Funds
        //                           where Application_ID = @Application_ID;";

        //    return SqlDataAccess.SaveData(sql, data);
        //}

        public static int DeleteInstitutionEmp(string Emp_Email)
        {
            InstitutionEmployeeDB data = new InstitutionEmployeeDB();
            data.Emp_Email = Emp_Email;
            string sql = @"delete from dbo.[Institution Employee]
                           where Emp_Email = @Emp_Email;";
             
            return SqlDataAccess.SaveData(sql, data);
        }

        public static InstitutionEmployeeDB GetInstitutionEmp(string Emp_Email)
        {
            var list = LoadEmployees();
            foreach (var item in list)
            {
                if (item.Emp_Email.Equals(Emp_Email))
                {
                    return item;
                }
            }
            return null;
        }

        public static List<InstitutionEmployeeDB> GetInstitutionEmployees(string name)
        {
            var list = new List<InstitutionEmployeeDB>();
            foreach (var item in LoadEmployees())
            {
                if (item.Organization_Name.Equals(name))
                    list.Add(item);
            }

            return list;
        }

        public static List<InstitutionEmployeeDB> LoadEmployees()
        {
            string sql = @"select Emp_FName, Emp_LName, Emp_Telephone_Number, Emp_Email, Organization_Name, Password, Admin_Code
                           from [dbo].[Institution Employee];";

            return SqlDataAccess.LoadData<InstitutionEmployeeDB>(sql);
        }
    }
}
