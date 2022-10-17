using Data_Library.Data_Access;
using Data_Library.ModelsDB;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Library.Business_Logic
{
    public static class Academic_RecordProcessor
    {
        public static int CreateAcademicRecord(string studentNum, string AY, string Quali, decimal? avarageMarks, byte[] transcript)
        {
            Academic_RecordDB data = new Academic_RecordDB
            {
                Student_Number = studentNum,
                Academic_Year = AY,
                Qualification = Quali,
                Avarage_Marks = avarageMarks,
                Upload_Transcript = transcript
            };

            string sql = @" insert into dbo.[Academic Records] (Student_Number, Academic_Year, Qualification, Avarage_Marks, Upload_Transcript)
                            values (@Student_Number, @Academic_Year, @Qualification, @Avarage_Marks, @Upload_Transcript);";

            return SqlDataAccess.SaveData(sql, data);
        }

        public static int CreateAcademicRecord(string studentNum, string AY, string Quali)
        {
            Academic_RecordDB data = new Academic_RecordDB
            {
                Student_Number = studentNum,
                Academic_Year = AY,
                Qualification = Quali
            };

            string sql = @" insert into dbo.[Academic Records] (Student_Number, Academic_Year, Qualification)
                            values (@Student_Number, @Academic_Year, @Qualification);";

            return SqlDataAccess.SaveData(sql, data);
        }

        public static int provideStudentResult(string studentNum, string AY, string Quali, decimal? avarageMarks, byte[] transcript)
       {
            Academic_RecordDB data = new Academic_RecordDB();
            data.Student_Number = studentNum;
            data.Academic_Year = AY;

            if (Quali != null)
                data.Qualification = Quali;
            if (avarageMarks != null)
                data.Avarage_Marks = avarageMarks;
            if (transcript != null)
                data.Upload_Transcript = transcript;

            string sql = @"update dbo.[Academic Records]
                                set Qualification =@Qualification,
                                    Avarage_Marks =@Avarage_Marks,
                                    Upload_Transcript =@Upload_Transcript
                                where Student_Number = @Student_Number and
                                      Academic_Year = @Academic_Year;";
            return SqlDataAccess.SaveData(sql, data);
       }

        public static int DeleteAcademicRecord(string studentNum, string AY)
        {
            Academic_RecordDB data = new Academic_RecordDB();
            data.Student_Number = studentNum;
            data.Academic_Year = AY;
            string sql = @"delete from dbo.[Academic Records]
                           where Student_Number = @Student_Number and
                                 Academic_Year = @Academic_Year;";

            return SqlDataAccess.SaveData(sql, data);
        }

        //public static Academic_RecordDB GetAcademicRecord(string studentNum)
        //{
        //    string sql = @"select Student_Number, Academic_Year, Qualification, Avarage_Marks, Upload_Transcript
        //                   from dbo.[Academic Records]
        //                   where Student_Number = @Student_Number and
        //                         Academic_Year = @Academic_Year;";
        //}

        public static List<Academic_RecordDB> LoadAcademicRecords()
        {
            string sql = @"select Student_Number, Academic_Year, Qualification, Avarage_Marks, Upload_Transcript
                           from dbo.[Academic Records];";

            return SqlDataAccess.LoadData<Academic_RecordDB>(sql);
        }
    }
}
