using Data_Library.Data_Access;
using Data_Library.ModelsDB;
using System.Collections.Generic;

namespace Data_Library.Business_Logic
{
    public static class Finacial_RecordProcessor
    {
        public static int CreateStudentFinRec(string StudentNum, string AY, decimal? amount, byte[] statement,
            string status, string funds)
        {
            Finacial_RecordDB data = new Finacial_RecordDB
            {
                Student_Number = StudentNum,
                Academic_Year = AY,
                Balance_Amount = amount,
                Upload_Statement = statement,
                Funding_Status = status,
                Request_Funds = funds
            };

            string sql = @" insert into dbo.[Finacial Records] (Student_Number, Academic_Year, Balance_Amount, Upload_Statement, Funding_Status, Request_Funds)
                            values (@Student_Number, @Academic_Year, @Balance_Amount, @Upload_Statement, @Funding_Status, @Request_Funds);";

            return SqlDataAccess.SaveData(sql, data);
        }

        public static int uploadFinacialStatement(string StudentNum, string AY, decimal? amount, byte[] statement) // upload the finacial statement of the student and update balance due 
        {

            Finacial_RecordDB data = new Finacial_RecordDB();
            data.Student_Number = StudentNum;
            data.Academic_Year = AY;
            if (statement != null)
                data.Upload_Statement = statement;
            if (amount != null)
                data.Balance_Amount = amount;

            string sql = @"update dbo.[Finacial Records] 
                               set Upload_Statement = @Upload_Statement,
                                   Balance_Amount = @Balance_Amount
                               where Student_Number = @Student_Number and
                                     Academic_Year = @Academic_Year;";

            return SqlDataAccess.SaveData(sql, data);
        }
        
        public static int updateStudentFundingStatus(string StudentNum, string AY, string status) //updating funding status from the institution point of view
        {

            Finacial_RecordDB data = new Finacial_RecordDB();
            data.Student_Number = StudentNum;
            data.Academic_Year = AY;
            if (status != null)
                data.Funding_Status = status;

            string sql = @"update dbo.[Finacial Records] 
                               set Funding_Status = @Funding_Status
                               where Student_Number = @Student_Number and
                                     Academic_Year = @Academic_Year;";

            return SqlDataAccess.SaveData(sql, data);
        }

        

        public static int requestFunds(string StudentNum, string AY, string funds) //request fund of the student
        {
            Finacial_RecordDB data = new Finacial_RecordDB();
            data.Student_Number = StudentNum;
            data.Academic_Year = AY;
            if (funds != null)
                data.Request_Funds = funds;

            string sql = @"update dbo.[Finacial Records] 
                               set Request_Funds = @Request_Funds
                               where Student_Number = @Student_Number and
                                     Academic_Year = @Academic_Year;";

            return SqlDataAccess.SaveData(sql, data);
        }

        public static int DeleteStudentFinRec(string stuNum, string AY)
        {
            Finacial_RecordDB data = new Finacial_RecordDB();
            data.Student_Number = stuNum;
            data.Academic_Year = AY;
            string sql = @"delete from dbo.[Finacial Records]
                           where Student_Number = @Student_Number and
                                 Academic_Year = @Academic_Year;";

            return SqlDataAccess.SaveData(sql, data);
        }

        public static Finacial_RecordDB GetStudentFinRec(string studentID, string AY)
        {
            string sql = @"select Student_Number, Academic_Year, Balance_Amount, Upload_Statement, Funding_Status, Request_Funds
                           from dbo.[Finacial Records]
                           where Student_Number = '" + studentID + "' and Academic_Year = '" + AY + "'; ";

            return SqlDataAccess.SingleData<Finacial_RecordDB>(sql);
        }

        //public static List<FunderEmployeeDB> GetFundersEmployees(string name)
        //{
        //    var list = new List<FunderEmployeeDB>();
        //    foreach (var item in LoadEmployees())
        //    {
        //        if (item.Organization_Name.Equals(name))
        //            list.Add(item);
        //    }

        //    return list;
        //}

        public static List<Finacial_RecordDB> LoadFinacialRecords()
        {
            string sql = @"select Student_Number, Academic_Year, Balance_Amount, Upload_Statement, Funding_Status, Request_Funds
                           from dbo.[Finacial Records];";

            return SqlDataAccess.LoadData<Finacial_RecordDB>(sql);
        }
    }
}
