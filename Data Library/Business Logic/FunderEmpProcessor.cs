using Data_Library.Data_Access;
using Data_Library.ModelsDB;
using System.Collections.Generic;

namespace Data_Library.Business_Logic
{
    public static class FunderEmpProcessor
    {
        public static int AddFunderEmp(string Emp_FName, string Emp_LName, string Emp_Telephone_Number, string Emp_Email,
            string Organization_Name, string Password, string Admin_Code)
        {
            FunderEmployeeDB data = new FunderEmployeeDB
            {
                Emp_FName = Emp_FName,
                Emp_LName = Emp_LName,
                Emp_Telephone_Number = Emp_Telephone_Number,
                Emp_Email = Emp_Email,
                Organization_Name = Organization_Name,
                Password = Password,
                Admin_Code = Admin_Code
            };

            string sql = @" insert into dbo.[Funder Employee] (Emp_FName, Emp_LName, Emp_Telephone_Number, Emp_Email, Organization_Name, Password, Admin_Code)
                            values (@Emp_FName, @Emp_LName, @Emp_Telephone_Number, @Emp_Email, @Organization_Name, @Password, @Admin_Code);";

            return SqlDataAccess.SaveData(sql, data);
        }

        public static int updateFunderEmpPassword(string email, string password)
        {
            FunderEmployeeDB data = new FunderEmployeeDB();
            data.Emp_Email = email;
            if (password != null)
                  data.Password = password;

                string sql = @"update dbo.[Funder Employee] 
                               set Password = @Password
                               where Emp_Email = @Emp_Email;";

            return SqlDataAccess.SaveData(sql, data);
        }

        //public static int UpdateFunderEmp(string email)
        //{
        //    FunderEmployeeDB data = new FunderEmployeeDB();
        //    data.Emp_Email = email;

        //    if (updateRequest != null)
        //        data.Update_Fund_Request = updateRequest;
        //    if (status != null)
        //        data.Funding_Status = status;
        //    if (approvedFunds != null)
        //        data.Approved_Funds = approvedFunds;

        //    string sql = @"update dbo.[Funder Employee] 
        //                       set Update_Fund_Request = @Update_Fund_Request,
        //                           Funding_Status = @Funding_Status,
        //                           Approved_Funds = @Approved_Funds
        //                       where Application_ID = @Application_ID;";

        //    return SqlDataAccess.SaveData(sql, data);
        //}

        public static int DeleteFunderEmp(string email)
        {
            FunderEmployeeDB data = new FunderEmployeeDB();
            data.Emp_Email = email;
            string sql = @"delete from dbo.[Funder Employee]
                           where Emp_Email = @Emp_Email;";

            return SqlDataAccess.SaveData(sql, data);
        }

        public static FunderEmployeeDB GetFunderEmp(string Emp_Email)
        {
            string sql = @"select Emp_FName, Emp_LName, Emp_Telephone_Number, Emp_Email, Organization_Name, Password, Admin_Code
                           from dbo.[Funder Employee]
                           where Emp_Email = '" + Emp_Email + "';";
            return SqlDataAccess.SingleData<FunderEmployeeDB>(sql);
        }

        public static List<FunderEmployeeDB> GetFundersEmployees(string name)
        {
            string sql = @"select Emp_FName, Emp_LName, Emp_Telephone_Number, Emp_Email, Organization_Name, Password, Admin_Code
                           from dbo.[Funder Employee]
                           where Organization_Name ='" + name + "' ;";

            return SqlDataAccess.LoadData<FunderEmployeeDB>(sql);
        }

        public static List<FunderEmployeeDB> LoadEmployees()
        {
            string sql = @"select Emp_FName, Emp_LName, Emp_Telephone_Number, Emp_Email, Organization_Name, Password, Admin_Code
                           from dbo.[Funder Employee];";

            return SqlDataAccess.LoadData<FunderEmployeeDB>(sql);
        }
    }
}
