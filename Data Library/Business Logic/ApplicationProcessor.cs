using Data_Library.Data_Access;
using Data_Library.ModelsDB;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Library.Business_Logic
{
    public static class ApplicationProcessor
    {
        public static int CreateApplication(string appID, string studentId, string burCode, string FY, string status) //, System.DateTime? FY, byte[] uploadAgreement, byte[] uploadSignedAgreement
        {
            ApplicationDB data = new ApplicationDB
            {
                Application_ID = appID,
                Student_Identity_Number = studentId,
                Bursary_Code = burCode,
                Funding_Year = FY,
                Application_Status = status,
                //Upload_Agreement = uploadAgreement,
                //Upload_Signed_Agreement = uploadSignedAgreement
            };

            string sql = @" insert into dbo.Application (Application_ID, Student_Identity_Number, Bursary_Code, Funding_Year, Application_Status) 
                            values (@Application_ID, @Student_Identity_Number, @Bursary_Code, @Funding_Year, @Application_Status);";

            //, Funding_Year, Application_Status, Upload_Agreement, Upload_Signed_Agreement
            //, @Funding_Year, @Application_Status, @Upload_Agreement, @Upload_Signed_Agreement
            return SqlDataAccess.SaveData(sql, data);
        }

        public static int updateApplicationStatus(string appID, string appStatus)
        {
            ApplicationDB data = new ApplicationDB();
            data.Application_ID = appID;

            if (appStatus != null)
                data.Application_Status = appStatus;

            string sql = @"update dbo.Application
                               set Application_Status = @Application_Status
                               where Application_ID = @Application_ID;";

            return SqlDataAccess.SaveData(sql, data);
        }

        public static int uploadSignedAgreement(string appID, byte[] uploadSignedAgreement)
        {
            ApplicationDB data = new ApplicationDB();
            data.Application_ID = appID;

            if (uploadSignedAgreement != null)
                data.Upload_Signed_Agreement = uploadSignedAgreement;

            string sql = @"update dbo.Application
                               set Upload_Signed_Agreement = @Upload_Signed_Agreement
                               where Application_ID = @Application_ID;";

            return SqlDataAccess.SaveData(sql, data);
        }

        public static int updateApplicationDocs(string appID, byte[] uploadAgreement)
        {
            ApplicationDB data = new ApplicationDB();
            data.Application_ID = appID;

            if (uploadAgreement != null)
                data.Upload_Agreement = uploadAgreement;

            string sql = @"update dbo.Application
                               set Application_Status = @Application_Status
                               where Application_ID = @Application_ID;";

            return SqlDataAccess.SaveData(sql, data);
        }

        public static int DeleteApplication(string appID)
        {
            ApplicationDB data = new ApplicationDB();
            data.Application_ID = appID;
            string sql = @"delete from dbo.Application
                           where Application_ID = @Application_ID;";

            return SqlDataAccess.SaveData(sql, data);
        }

        public static ApplicationDB GetApplication(string code)
        {
            foreach (var item in LoadApplications())
            {
                if (item.Application_ID.Equals(code))
                    return item;
            }
            return null;
        }

        public static List<ApplicationDB> GetApplications(string code)
        {
            var list = new List<ApplicationDB>();
            foreach (var item in LoadApplications())
            {
                if (item.Bursary_Code.Equals(code))
                    list.Add(item);
            }

            return list;
        }

        public static List<ApplicationDB> GetStudentApplications(string id)
        {
            var list = new List<ApplicationDB>();
            foreach (var item in LoadApplications())
            {
                if (item.Student_Identity_Number.Equals(id))
                    list.Add(item);
            }

            return list;
        }

        public static List<ApplicationDB> LoadApplications()
        {
            string sql = @"select Application_ID, Student_Identity_Number, Bursary_Code, Funding_Year, Application_Status, Upload_Agreement, Upload_Signed_Agreement
                           from dbo.Application;";

            return SqlDataAccess.LoadData<ApplicationDB>(sql);
        }
    }
}
