using Data_Library.Data_Access;
using Data_Library.ModelsDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Library.Business_Logic
{
    public static class InstitutionProcessor
    {
        public static int CreateInstitution(string Ins_Name, string Ins_Physical_Address,
            string Ins_Postal_Address, string Ins_Telephone_Number, string Ins_Email_Address)
        {
            InstitutionDB data = new InstitutionDB
            {
                Institution_Name = Ins_Name,
                Institution_Physical_Address = Ins_Physical_Address,
                Institution_Postal_Address = Ins_Postal_Address,
                Institution_Telephone_Number = Ins_Telephone_Number,
                Institution_Email_Address = Ins_Email_Address
            };

            //set identity_insert Institution on;

            string sql = @" insert into dbo.Institution (Institution_Name, Institution_Physical_Address, Institution_Postal_Address, Institution_Telephone_Number, Institution_Email_Address)
                            values (@Institution_Name, @Institution_Physical_Address, @Institution_Postal_Address, @Institution_Telephone_Number, @Institution_Email_Address);";

            return SqlDataAccess.SaveData(sql, data);
        }

        public static int UpdateIns(string Ins_Name, string Ins_Physical_Address,
            string Ins_Postal_Address, string Ins_Telephone_Number, string Ins_Email_Address)
        {
            InstitutionDB data = new InstitutionDB();
            data.Institution_Name = Ins_Name;

            if (Ins_Physical_Address != null)
                data.Institution_Physical_Address = Ins_Physical_Address;
            if (Ins_Postal_Address != null)
                data.Institution_Postal_Address = Ins_Postal_Address;
            if (Ins_Telephone_Number != null)
                data.Institution_Telephone_Number = Ins_Telephone_Number;
            if (Ins_Email_Address != null)
                data.Institution_Email_Address = Ins_Email_Address;

            string sql = @"update dbo.Institution 
                           set Institution_Physical_Address = @Institution_Physical_Address, 
                               Institution_Postal_Address = @Institution_Postal_Address, 
                               Institution_Telephone_Number = @Institution_Telephone_Number, 
                               Institution_Email_Address = @Institution_Email_Address
                           where Institution_Name = @Institution_Name;";

            return SqlDataAccess.SaveData(sql, data);
        }

        public static int DeleteIns(string Ins_Name)
        {
            InstitutionDB data = new InstitutionDB();
            data.Institution_Name = Ins_Name;
            string sql = @"delete from dbo.Institution
                           where Institution_Name = @Institution_Name;";

            return SqlDataAccess.SaveData(sql, data);
        }

        public static InstitutionDB GetInstitution(string Ins_Name)
        {
            string sql = @"select Institution_Name, Institution_Physical_Address, Institution_Postal_Address, Institution_Telephone_Number, Institution_Email_Address
                           from dbo.Institution
                           where Institution_Name = '" + Ins_Name + "';";
            return SqlDataAccess.SingleData<InstitutionDB>(sql);
        }

        public static List<FundedStudentsDB> fundedStudents(string Ins_Name)
        {
            string sql = @"select E.Student_Number, E.Student_Identity_Number, E.Student_Email, E.Institution_Name, A.Application_Status
                           from (dbo.[Enrolled At] AS E JOIN [dbo].[Application] AS A ON E.Student_Identity_Number=A.Student_Identity_Number)
                           where A.Application_Status = 'Approved' and
                                 E.Institution_Name = '"+Ins_Name+"';";

            return SqlDataAccess.LoadData<FundedStudentsDB>(sql);
        }
        public static List<InstitutionDB> LoadInstitutions()
        {
            string sql = @"select Institution_Name, Institution_Physical_Address, Institution_Postal_Address, Institution_Telephone_Number, Institution_Email_Address
                           from dbo.Institution;";

            return SqlDataAccess.LoadData<InstitutionDB>(sql);
        }
    }
}
