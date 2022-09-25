using Data_Library.Data_Access;
using Data_Library.ModelsDB;
using System.Collections.Generic;

namespace Data_Library.Business_Logic
{
    public static class Bursar_FundProcessor
    {
        public static int CreateBursarFund(string appID, string updateRequest, string status, decimal? approvedFunds)

        {
            Bursar_FundDB data = new Bursar_FundDB
            {
                Application_ID = appID,
                Update_Fund_Request = updateRequest,
                Funding_Status = status,
                Approved_Funds = approvedFunds
            };

            string sql = @" insert into dbo.[Bursar Funds] (Application_ID Update_Fund_Request, Funding_Status, Approved_Funds)
                            values (@Application_ID @Update_Fund_Request, @Funding_Status, @Approved_Funds);";

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

        public static Bursar_FundDB GetBursar(string code)
        {
            foreach (var item in LoadBursars())
            {
                if (item.Application_ID.Equals(code))
                {
                    return item;
                }
            }
            return null;
        }

        public static List<Bursar_FundDB> GetBursarList(string code) //gets the list of students from a certain bursary
        {
            var list = new List<Bursar_FundDB>();
            foreach (var item in LoadBursars())
            {
                if (item.Application.Bursary_Code.Equals(code))
                    list.Add(item);
            }

            return list;
        }


        public static List<Bursar_FundDB> LoadBursars()
        {
            string sql = @"select Application_ID, Update_Fund_Request, Funding_Status, Approved_Funds
                           from dbo.[Bursar Funds];";

            return SqlDataAccess.LoadData<Bursar_FundDB>(sql);
        }
    }
}
