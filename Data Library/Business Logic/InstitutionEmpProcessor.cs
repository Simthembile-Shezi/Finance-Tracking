using Data_Library.Data_Access;
using Data_Library.ModelsDB;
using System.Collections.Generic;

namespace Data_Library.Business_Logic
{
    public static class InstitutionEmpProcessor
    {
        public static int AddInstitutionAdminEmp(string ID,string Emp_FName, string Emp_LName, string Emp_Telephone_Number, string Emp_Email,
            string Organization_Name, string Password, string Admin_Code)
        {
            InstitutionEmployeeDB data = new InstitutionEmployeeDB
            {
                Emp_UserID = ID,
                Emp_FName = Emp_FName,
                Emp_LName = Emp_LName,
                Emp_Telephone_Number = Emp_Telephone_Number,
                Emp_Email = Emp_Email,
                Organization_Name = Organization_Name,
                Password = Password,
                Admin_Code = Admin_Code
            };

            string sql = @" insert into [dbo].[Institution Employee] (Emp_UserID, Emp_FName, Emp_LName, Emp_Telephone_Number, Emp_Email, Organization_Name, Password, Admin_Code)
                            values (@Emp_UserID, @Emp_FName, @Emp_LName, @Emp_Telephone_Number, @Emp_Email, @Organization_Name, @Password, @Admin_Code);";

            return SqlDataAccess.SaveData(sql, data);
        }
        public static int AddInstitutionEmp(string ID,string Emp_FName, string Emp_LName, string Emp_Telephone_Number, string Emp_Email, string Organization_Name, string Password)
        {
            InstitutionEmployeeDB data = new InstitutionEmployeeDB
            {
                Emp_UserID = ID,
                Emp_FName = Emp_FName,
                Emp_LName = Emp_LName,
                Emp_Telephone_Number = Emp_Telephone_Number,
                Emp_Email = Emp_Email,
                Organization_Name = Organization_Name,
                Password = Password
            };

            string sql = @" insert into [dbo].[Institution Employee] (Emp_UserID ,Emp_FName, Emp_LName, Emp_Telephone_Number, Emp_Email, Organization_Name, Password)
                            values (@Emp_UserID, @Emp_FName, @Emp_LName, @Emp_Telephone_Number, @Emp_Email, @Organization_Name, @Password);";

            return SqlDataAccess.SaveData(sql, data);
        }
        public static int updateInstitutionEmpPassword(string email, string password)
        {
            InstitutionEmployeeDB data = new InstitutionEmployeeDB();
            data.Emp_Email = email;
            if (password != null)
                data.Password = password;

            string sql = @"update [dbo].[Institution Employee] 
                               set Password = @Password
                               where Emp_Email = @Emp_Email;";

            return SqlDataAccess.SaveData(sql, data);
        }

        public static int UpdateInstitutionEmp(string Emp_Email)
        {
            InstitutionEmployeeDB data = new InstitutionEmployeeDB();
            data.Emp_Email = Emp_Email;


            string sql = @"update dbo.[Bursar Funds] 
                                   set Update_Fund_Request = @Update_Fund_Request,
                                       Funding_Status = @Funding_Status,
                                       Approved_Funds = @Approved_Funds
                                   where Application_ID = @Application_ID;";

            return SqlDataAccess.SaveData(sql, data);
        }

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
            string sql = @"select Emp_UserID, Emp_FName, Emp_LName, Emp_Telephone_Number, Emp_Email, Organization_Name, Password, Admin_Code
                           from [dbo].[Institution Employee]
                           where Emp_Email = '" + Emp_Email + "';";

            return SqlDataAccess.SingleData<InstitutionEmployeeDB>(sql);
        }
        public static InstitutionEmployeeDB GetInstitutionEmpID(string Emp_UserID)
        {
            string sql = @"select Emp_UserID, Emp_FName, Emp_LName, Emp_Telephone_Number, Emp_Email, Organization_Name, Password, Admin_Code
                           from [dbo].[Institution Employee]
                           where Emp_UserID = '" + Emp_UserID + "';";

            return SqlDataAccess.SingleData<InstitutionEmployeeDB>(sql);
        }
        public static List<InstitutionEmployeeDB> GetInstitutionEmployees(string name)
        {
            string sql = @"select Emp_UserID, Emp_FName, Emp_LName, Emp_Telephone_Number, Emp_Email, Organization_Name, Password, Admin_Code
                           from [dbo].[Institution Employee]
                           where Organization_Name = '" + name + "';";

            return SqlDataAccess.LoadData<InstitutionEmployeeDB>(sql);
        }

        public static List<InstitutionEmployeeDB> LoadEmployees()
        {
            string sql = @"select Emp_UserID, Emp_FName, Emp_LName, Emp_Telephone_Number, Emp_Email, Organization_Name, Password, Admin_Code
                           from [dbo].[Institution Employee];";

            return SqlDataAccess.LoadData<InstitutionEmployeeDB>(sql);
        }
    }
}
