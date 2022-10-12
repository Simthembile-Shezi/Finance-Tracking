using Data_Library.Data_Access;
using Data_Library.ModelsDB;
using System.Collections.Generic;

namespace Data_Library.Business_Logic
{
    public static class BursaryProcessor
    {
        public static int CreateBursary(string burCode, string burName, System.DateTime startDate, string funderName,
            System.DateTime? endDate, decimal? burAmount, string numAvail, string desc, string FY)
        {
            BursaryDB data = new BursaryDB
            {
                Bursary_Code = burCode,
                Bursary_Name = burName,
                Start_Date = startDate,
                Funder_Name = funderName,
                End_Date = endDate,
                Bursary_Amount = burAmount,
                Number_Available = numAvail,
                Description = desc,
                Funding_Year = FY,
            };

            string sql = @" insert into dbo.[Bursary] (Bursary_Code, Bursary_Name, Start_Date, Funder_Name, End_Date, Bursary_Amount, Number_Available, Description, Funding_Year)
                            values (@Bursary_Code, @Bursary_Name, @Start_Date, @Funder_Name, @End_Date, @Bursary_Amount, @Number_Available, @Description, @Funding_Year);";

            return SqlDataAccess.SaveData(sql, data);
        }
        public static int UpdateBursaryNumberAvail(string burCode, string numAvail)
        {
            BursaryDB data = new BursaryDB();
            data.Bursary_Code = burCode;

            if (int.Parse(numAvail) > -1)
                data.Number_Available = numAvail;

            string sql = @"update dbo.[Bursary] 
                               set Number_Available = @Number_Available
                               where Bursary_Code = @Bursary_Code;";

            return SqlDataAccess.SaveData(sql, data);
        }

        public static int UpdateBursary(string burCode, string burName, System.DateTime startDate, string funderName,
            System.DateTime? endDate, decimal? burAmount, string numAvail, string desc, string FY)
        {
            BursaryDB data = new BursaryDB();
            data.Bursary_Code = burCode;

            if (burName != null)
                data.Bursary_Name = burName;
            if (startDate != null)
                data.Start_Date = startDate;
            if (funderName != null)
                data.Funder_Name = funderName;
            if (endDate != null)
                data.End_Date = endDate;
            if (burAmount != null)
                data.Bursary_Amount = burAmount;
            if (int.Parse(numAvail) != 0)
                data.Number_Available = numAvail;
            if (desc != null)
                data.Description = desc;
            if (FY != null)
                data.Funding_Year = FY;

            string sql = @"update dbo.[Bursary] 
                               set Bursary_Name = @Bursary_Name,
                                   Start_Date = @Start_Date,
                                   Funder_Name = @Funder_Name,
                                   End_Date = @End_Date,
                                   Bursary_Amount = @Bursary_Amount,
                                   Number_Available = @Number_Available,
                                   Description = @Description,
                                   Funding_Year = @Funding_Year
                               where Bursary_Code = @Bursary_Code;";

            return SqlDataAccess.SaveData(sql, data);
        }

        public static int deleteBursary(string code)
        {
            BursaryDB data = new BursaryDB();
            data.Bursary_Code = code;
            string sql = @"delete from dbo.[Bursary]
                           where Bursary_Code = @Bursary_Code;";

            return SqlDataAccess.SaveData(sql, data);
        }

        public static BursaryDB GetBursaryCode(string code)
        {
            string sql = @"select Bursary_Code, Bursary_Name, Start_Date, Funder_Name, End_Date, Bursary_Amount, Number_Available, Description, Funding_Year
                           from dbo.[Bursary]
                           where Bursary_Code = '" + code + "';";

            return SqlDataAccess.SingleData<BursaryDB>(sql);
        }

        public static List<BursaryDB> GetBursaries(string funderName)
        {
            string sql = @"select Bursary_Code, Bursary_Name, Start_Date, Funder_Name, End_Date, Bursary_Amount, Number_Available, Description, Funding_Year
                           from dbo.[Bursary]
                           where Funder_Name = '" + funderName + "';";

            return SqlDataAccess.LoadData<BursaryDB>(sql);
        }
        public static List<BursaryDB> GetSearchBursaries(string funderName, string FY)
        {
            string sql = @"select Bursary_Code, Bursary_Name, Start_Date, Funder_Name, End_Date, Bursary_Amount, Number_Available, Description, Funding_Year
                           from dbo.[Bursary]
                           where Funder_Name = '" + funderName + "' and Funding_Year = '" + FY + "';";

            return SqlDataAccess.LoadData<BursaryDB>(sql);
        }
        public static List<BursaryDB> LoadBursaries()
        {
            string sql = @"select Bursary_Code, Bursary_Name, Start_Date, Funder_Name, End_Date, Bursary_Amount, Number_Available, Description, Funding_Year
                           from dbo.[Bursary];";

            return SqlDataAccess.LoadData<BursaryDB>(sql);
        }
    }
}
