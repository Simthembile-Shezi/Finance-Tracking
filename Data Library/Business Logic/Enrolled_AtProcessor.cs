using Data_Library.Data_Access;
using Data_Library.ModelsDB;
using System.Collections.Generic;

namespace Data_Library.Business_Logic
{
    public static class Enrolled_AtProcessor
    {
        public static int CreateEnrolledDetails(string StudentNum, string StudentId, string InsName, string Quali, string email, string StudyAddress)
        {
            Enrolled_AtDB data = new Enrolled_AtDB
            {
                Student_Number = StudentNum,
                Student_Identity_Number = StudentId,
                Institution_Name = InsName,
                Qualification = Quali,
                Student_Email = email,
                Study_Residential_Address = StudyAddress
            };

            string sql = @" insert into dbo.[Enrolled At] (Student_Number, Student_Identity_Number, Institution_Name, Qualification, Student_Email, Study_Residential_Address)
                            values (@Student_Number, @Student_Identity_Number, @Institution_Name, @Qualification, @Student_Email, @Study_Residential_Address);";

            return SqlDataAccess.SaveData(sql, data);
        }

        public static int UpdateEnrolledDetails(string StudentNum, string StudentId, string InsName, string Quali, string email, string StudyAddress)
        {
            Enrolled_AtDB data = new Enrolled_AtDB();
            data.Student_Number = StudentNum;

            if (StudentId != null)
                data.Student_Identity_Number = StudentId;
            if (InsName != null)
                data.Institution_Name = InsName;
            if (Quali != null)
                data.Qualification = Quali;
            if (email != null)
                data.Student_Email = email;
            if (StudyAddress != null)
                data.Study_Residential_Address = StudyAddress;

            string sql = @"update dbo.[Enrolled At] 
                               set Student_Identity_Number = @Student_Identity_Number,
                                   Institution_Name = @Institution_Name,
                                   Qualification = @Qualification,
                                   Student_Email = @Student_Email,
                                   Study_Residential_Address = @Study_Residential_Address
                               where Student_Number = @Student_Number;";

            return SqlDataAccess.SaveData(sql, data);
        }

        public static int DeleteEnrolledDetails(string studentID)
        {
            Enrolled_AtDB data = new Enrolled_AtDB();
            data.Student_Number = studentID;
            string sql = @"delete from dbo.[Enrolled At]
                           where Student_Number = @Student_Number;";

            return SqlDataAccess.SaveData(sql, data);
        }

        public static Enrolled_AtDB GetEnrolledDetails(string studentNum)
        {
            string sql = @"select Student_Number, Student_Identity_Number, Institution_Name, Qualification, Student_Email, Study_Residential_Address
                           from dbo.[Enrolled At]
                           where Student_Number = '" + studentNum + "';";

            return SqlDataAccess.SingleData<Enrolled_AtDB>(sql);
        }

        public static List<Enrolled_AtDB> GetEnrolledDetailsList(string InsName)
        {
            string sql = @"select Student_Number, Student_Identity_Number, Institution_Name, Qualification, Student_Email, Study_Residential_Address
                           from dbo.[Enrolled At]
                           where Institution_Name = '" + InsName + "';";

            return SqlDataAccess.LoadData<Enrolled_AtDB>(sql);
        }

        public static List<Enrolled_AtDB> GetStudentEnrolledList(string studentID)
        {
            string sql = @"select Student_Number, Student_Identity_Number, Institution_Name, Qualification, Student_Email, Study_Residential_Address
                           from dbo.[Enrolled At]
                           where Student_Identity_Number = '" + studentID + "';";

            return SqlDataAccess.LoadData<Enrolled_AtDB>(sql);
        }

        public static List<Enrolled_AtDB> LoadEnrolledDetails()
        {
            string sql = @"select Student_Number, Student_Identity_Number, Institution_Name, Qualification, Student_Email, Study_Residential_Address
                           from dbo.[Enrolled At];";

            return SqlDataAccess.LoadData<Enrolled_AtDB>(sql);
        }
    }
}
