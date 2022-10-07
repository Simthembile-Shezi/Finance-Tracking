using Data_Library.Data_Access;
using Data_Library.ModelsDB;
using System.Collections.Generic;

namespace Data_Library.Business_Logic
{
    public static class FunderProcessor
    {
        public static int CreateFunder(string Funder_Name, string Funder_Tax_Number, string Funder_Email, string Funder_Telephone_Number,
            string Funder_Physical_Address, string Funder_Postal_Address)
        {
            FunderDB data = new FunderDB
            {
                Funder_Name = Funder_Name,
                Funder_Tax_Number = Funder_Tax_Number,
                Funder_Email = Funder_Email,
                Funder_Telephone_Number = Funder_Telephone_Number,
                Funder_Physical_Address = Funder_Physical_Address,
                Funder_Postal_Address = Funder_Postal_Address
            };

            //set identity_insert Institution on;

            string sql = @" insert into dbo.Funder (Funder_Name, Funder_Tax_Number, Funder_Email, Funder_Telephone_Number, Funder_Physical_Address, Funder_Postal_Address)
                            values (@Funder_Name, @Funder_Tax_Number, @Funder_Email, @Funder_Telephone_Number, @Funder_Physical_Address, @Funder_Postal_Address);";

            return SqlDataAccess.SaveData(sql, data);
        }

        public static int UpdateFunder(string Funder_Name, string Funder_Email, string Funder_Telephone_Number, string Funder_Physical_Address, string Funder_Postal_Address)
        {
            FunderDB data = new FunderDB();
            data.Funder_Name = Funder_Name;

            if (Funder_Email != null)
                data.Funder_Email = Funder_Email;
            if (Funder_Telephone_Number != null)
                data.Funder_Telephone_Number = Funder_Telephone_Number;
            if (Funder_Physical_Address != null)
                data.Funder_Physical_Address = Funder_Physical_Address;
            if (Funder_Postal_Address != null)
                data.Funder_Postal_Address = Funder_Postal_Address;

            string sql = @"update dbo.Funder
                               set Funder_Email = @Funder_Email,
                                   Funder_Telephone_Number = @Funder_Telephone_Number,
                                   Funder_Physical_Address = @Funder_Physical_Address,                        
                                   Funder_Postal_Address = @Funder_Postal_Address
                               where Funder_Name = @Funder_Name;";

            return SqlDataAccess.SaveData(sql, data);
        }

        public static int deleteFunder(string funderName)
        {
            FunderDB data = new FunderDB();
            data.Funder_Name = funderName;
            string sql = @"delete from dbo.Funder
                           where Funder_Name = @Funder_Name;";

            return SqlDataAccess.SaveData(sql, data);
        }

        public static FunderDB GetFunder(string funderName)
        {
            string sql = @"select Funder_Name, Funder_Tax_Number, Funder_Email, Funder_Telephone_Number, Funder_Physical_Address, Funder_Postal_Address
                           from dbo.Funder
                           where Funder_Name = '" + funderName + "';";
            return SqlDataAccess.SingleData<FunderDB>(sql);
        }

        public static List<FunderDB> LoadFunders()
        {
            string sql = @"select Funder_Name, Funder_Tax_Number, Funder_Email, Funder_Telephone_Number, Funder_Physical_Address, Funder_Postal_Address
                           from dbo.Funder;";

            return SqlDataAccess.LoadData<FunderDB>(sql);
        }        
    }
}
