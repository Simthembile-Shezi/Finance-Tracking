using Data_Library.Data_Access;
using Data_Library.ModelsDB;
using System.Collections.Generic;

namespace Data_Library.Business_Logic
{
    public static class Bursar_FundProcessor
    {
        public static int CreateBursarFund(string appID, string status, decimal? approvedFunds)

        {
            Bursar_FundDB data = new Bursar_FundDB
            {
                Application_ID = appID,
                Funding_Status = status,
                Approved_Funds = approvedFunds
            };

            string sql = @" insert into dbo.[Bursar Funds] (Application_ID, Funding_Status, Approved_Funds)
                            values (@Application_ID , @Funding_Status, @Approved_Funds);";

            return SqlDataAccess.SaveData(sql, data);
        }

        public static int updateFundStatus(string appID, string status)
        {
            Bursar_FundDB data = new Bursar_FundDB();
            data.Application_ID = appID;

            if (status != null)
                data.Funding_Status = status;

            string sql = @"update dbo.[Bursar Funds] 
                               set Funding_Status = @Funding_Status
                               where Application_ID = @Application_ID;";

            return SqlDataAccess.SaveData(sql, data);
        }

        public static int updateFundsRequested(string appID, string updateRequest)
        {
            Bursar_FundDB data = new Bursar_FundDB();
            data.Application_ID = appID;

            if (updateRequest != null)
                data.Update_Fund_Request = updateRequest;

            string sql = @"update dbo.[Bursar Funds] 
                               set Update_Fund_Request = @Update_Fund_Request
                               where Application_ID = @Application_ID;";

            return SqlDataAccess.SaveData(sql, data);
        }

        public static int updateFundsApproved(string appID, decimal? approvedFunds)
        {
            Bursar_FundDB data = new Bursar_FundDB();
            data.Application_ID = appID;
                        
            if (approvedFunds != null)
                data.Approved_Funds = approvedFunds;

            string sql = @"update dbo.[Bursar Funds] 
                               set Approved_Funds = @Approved_Funds
                               where Application_ID = @Application_ID;";

            return SqlDataAccess.SaveData(sql, data);
        }

        public static int DeleteBursarFund(string code)
        {
            Bursar_FundDB data = new Bursar_FundDB();
            data.Application_ID = code;
            string sql = @"delete from dbo.[Bursar Funds]
                           where Application_ID = @Application_ID;";

            return SqlDataAccess.SaveData(sql, data);
        }

        public static Bursar_FundDB GetBursar(string appID)
        {
            string sql = @"select Application_ID, Update_Fund_Request, Funding_Status, Approved_Funds
                           from dbo.[Bursar Funds]
                           where Application_ID = '" + appID + "'; ";

            return SqlDataAccess.SingleData<Bursar_FundDB>(sql);
        }

        public static List<Bursar_FundDB> GetBursarList(string burCode) //gets the list of students from a certain bursary
        {
            string sql = @"select B.Application_ID, B.Update_Fund_Request, B.Funding_Status, B.Approved_Funds
                           from dbo.[Bursar Funds] AS B JOIN dbo.[Application] AS A ON B.Application_ID = A.Application_ID
                           where A.Bursary_Code = '" + burCode + "'; ";

            return SqlDataAccess.LoadData<Bursar_FundDB>(sql);
        }

        public static List<BursarFundViewDB> GetAllBursarsList(string name)
        {
            string sql = @"select B.Application_ID, B.Update_Fund_Request, B.Funding_Status, B.Approved_Funds, S.Student_FName, S.Student_LName, S.Student_Identity_Number, S.Gender, S.Student_Cellphone_Number, S.Student_Email
                           from dbo.[Bursar Funds] AS B JOIN dbo.[Application] AS A ON B.Application_ID = A.Application_ID
                           JOIN dbo.[Student] AS S ON A.Student_Identity_Number=S.Student_Identity_Number 
                           JOIN dbo.[Bursary] AS Bur ON Bur.Bursary_Code=A.Bursary_Code
                           JOIN dbo.Funder AS F ON Bur.Funder_Name=F.Funder_Name
                           where F.Funder_Name = '" + name + "';";

            return SqlDataAccess.LoadData<BursarFundViewDB>(sql);
        }
        public static List<BursarFundViewDB> GetOneBursarsList(string appID)
        {
            string sql = @"select B.Application_ID, B.Update_Fund_Request, B.Funding_Status, B.Approved_Funds, S.Student_FName, S.Student_LName, S.Student_Identity_Number, S.Gender, S.Student_Cellphone_Number, S.Student_Email
                           from dbo.[Bursar Funds] AS B JOIN dbo.[Application] AS A ON B.Application_ID = A.Application_ID
                           JOIN dbo.[Student] AS S ON A.Student_Identity_Number=S.Student_Identity_Number 
                           where A.Application_ID = '" + appID + "'; ";

            return SqlDataAccess.LoadData<BursarFundViewDB>(sql);
        }
        public static List<BursarFundViewDB> BursarFundViews(string burCode)
        {
            string sql = @"select B.Application_ID, B.Update_Fund_Request, B.Funding_Status, B.Approved_Funds, S.Student_FName, S.Student_LName, S.Student_Identity_Number, S.Gender, S.Student_Cellphone_Number, S.Student_Email
                           from dbo.[Bursar Funds] AS B JOIN dbo.[Application] AS A ON B.Application_ID = A.Application_ID
                           JOIN dbo.[Student] AS S ON A.Student_Identity_Number=S.Student_Identity_Number 
                           where A.Bursary_Code = '" + burCode + "'; ";

            return SqlDataAccess.LoadData<BursarFundViewDB>(sql);
        }


        public static List<Bursar_FundDB> LoadBursars()
        {
            string sql = @"select Application_ID, Update_Fund_Request, Funding_Status, Approved_Funds
                           from dbo.[Bursar Funds];";

            return SqlDataAccess.LoadData<Bursar_FundDB>(sql);
        }
    }
}
