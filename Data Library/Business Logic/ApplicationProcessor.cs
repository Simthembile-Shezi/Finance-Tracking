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

        public static ApplicationDB GetApplication(string appID)
        {
            string sql = @"select Application_ID, Student_Identity_Number, Bursary_Code, Funding_Year, Application_Status, Upload_Agreement, Upload_Signed_Agreement
                           from dbo.Application
                           where Application_ID = '" + appID + "';";

            return SqlDataAccess.SingleData<ApplicationDB>(sql);
        }

        public static List<ApplicationDB> GetApplications(string burCode)
        {
            string sql = @"select Application_ID, Student_Identity_Number, Bursary_Code, Funding_Year, Application_Status, Upload_Agreement, Upload_Signed_Agreement
                           from dbo.Application
                           where Bursary_Code = '" + burCode + "';";

            return SqlDataAccess.LoadData<ApplicationDB>(sql);
        }

        public static List<ApplicationDB> GetStudentApplications(string studentID)
        {
            string sql = @"select Application_ID, Student_Identity_Number, Bursary_Code, Funding_Year, Application_Status, Upload_Agreement, Upload_Signed_Agreement
                           from dbo.Application
                           where Student_Identity_Number = '" + studentID + "';";

            return SqlDataAccess.LoadData<ApplicationDB>(sql);
        }
        public static ApplicationViewDB viewApplications(string appID)
        {
            string sql = @"select B.Bursary_Code, A.Application_ID, A.Application_Status, S.Student_FName, S.Student_LName, A.Student_Identity_Number, S.Gender, S.Student_Cellphone_Number, S.Student_Email, S.Student_Residential_Address, 
                                  E.Student_Number, E.Institution_Name, E.Qualification, AR.Academic_Year, AR.Avarage_Marks, AR.Upload_Transcript
                           from (dbo.[Bursary] AS B JOIN dbo.[Application] AS A ON B.Bursary_Code=A.Bursary_Code) 
                                JOIN dbo.[Student] AS S ON A.Student_Identity_Number=S.Student_Identity_Number 
                                JOIN dbo.[Enrolled At] AS E ON S.Student_Identity_Number=E.Student_Identity_Number 
                                JOIN dbo.[Academic Records] AS AR ON E.Student_Number=AR.Student_Number                                
                           where A.Application_ID = '" + appID + "';";

            return SqlDataAccess.Joins<ApplicationViewDB>(sql);

            //return SqlDataAccess.LoadData<ApplicationViewDB>(sql);
        }
        public static List<ApplicationDB> GetAllApplications(string name)
        {
            string sql = @"select A.Application_ID, A.Student_Identity_Number, A.Bursary_Code, A.Funding_Year, A.Application_Status, A.Upload_Agreement, A.Upload_Signed_Agreement
                           from dbo.[Bursary] AS B JOIN dbo.[Application] AS A ON B.Bursary_Code=A.Bursary_Code
                           JOIN dbo.Funder AS F ON B.Funder_Name=F.Funder_Name
                           where F.Funder_Name = '" + name + "';";

            return SqlDataAccess.LoadData<ApplicationDB>(sql);
        }

        public static List<ApplicationDB> LoadApplications()
        {
            string sql = @"select Application_ID, Student_Identity_Number, Bursary_Code, Funding_Year, Application_Status, Upload_Agreement, Upload_Signed_Agreement
                           from dbo.Application;";

            return SqlDataAccess.LoadData<ApplicationDB>(sql);
        }
    }
}
