using Data_Library.Data_Access;
using Data_Library.ModelsDB;
using System.Collections.Generic;

namespace Data_Library.Business_Logic
{
    public static class StudentProcessor
    {
        public static int CreateStudent(string studentID, string Student_FName, string Student_LName, string Student_Nationality, string Race, string Title,
                string Gender, System.DateTime Date_Of_Birth, string Marital_Status, string Student_Email, string Student_Cellphone_Number, string Student_Residential_Address,
                byte[] Upload_Identity_Document, byte[] Upload_Residential_Document, string Password)
        {
            StudentDB data = new StudentDB
            {
                Student_Identity_Number = studentID,
                Student_FName = Student_FName,
                Student_LName = Student_LName,
                Student_Nationality = Student_Nationality,
                Race = Race,
                Title = Title,
                Gender = Gender,
                Date_Of_Birth = Date_Of_Birth,
                Marital_Status = Marital_Status,
                Student_Email = Student_Email,
                Student_Cellphone_Number = Student_Cellphone_Number,
                Student_Residential_Address = Student_Residential_Address,
                Upload_Identity_Document = Upload_Identity_Document,
                Upload_Residential_Document = Upload_Residential_Document,
                Password = Password
            };

            string sql = @" insert into dbo.Student (Student_Identity_Number, Student_FName, Student_LName, Student_Nationality, Race, Title, Gender, Date_Of_Birth, Marital_Status, Student_Email, 
                            Student_Cellphone_Number, Student_Residential_Address, Upload_Identity_Document, Upload_Residential_Document, Password)
                            values (@Student_Identity_Number, @Student_FName, @Student_LName, @Student_Nationality, @Race, @Title, @Gender, @Date_Of_Birth, @Marital_Status, @Student_Email, 
                            @Student_Cellphone_Number, @Student_Residential_Address, @Upload_Identity_Document, @Upload_Residential_Document, @Password);";

            return SqlDataAccess.SaveData(sql, data);
        }

        public static int UpdateStudent(string studentID, string Student_Email, string Student_Cellphone_Number, string Student_Residential_Address)
        {
            StudentDB data = new StudentDB();
            data.Student_Identity_Number = studentID;

            if (Student_Email != null)
                data.Student_Email = Student_Email;
            if (Student_Cellphone_Number != null)
                data.Student_Cellphone_Number = Student_Cellphone_Number;
            if (Student_Residential_Address != null)
                data.Student_Residential_Address = Student_Residential_Address;

            string sql = @"update dbo.Student 
                               set Student_Email = @Student_Email,
                                   Student_Cellphone_Number = @Student_Cellphone_Number,
                                   Student_Residential_Address = @Student_Residential_Address
                               where Student_Identity_Number = @Student_Identity_Number;";

            return SqlDataAccess.SaveData(sql, data);
        }

        public static int updateStudentPassword(string studentID, string password)
        {
            StudentDB data = new StudentDB();
            data.Student_Identity_Number = studentID;
            if (password != null)
                data.Password = password;

            string sql = @"update dbo.Student 
                               set Password = @Password
                               where studentID = @studentID;";

            return SqlDataAccess.SaveData(sql, data);
        }

        public static int UploadRes_Doc(string studentID, byte[] resDoc)
        {
            StudentDB data = new StudentDB();
            data.Student_Identity_Number = studentID;
            data.Upload_Residential_Document = resDoc;

            string sql = @"update dbo.Student 
                               set Upload_Residential_Document = @Upload_Residential_Document
                               where Student_Identity_Number = @Student_Identity_Number;";

            return SqlDataAccess.SaveData(sql, data);
        }

        public static int UploadID_Doc(string studentID, byte[] idDoc)
        {
            StudentDB data = new StudentDB();
            data.Student_Identity_Number = studentID;
            data.Upload_Identity_Document = idDoc;

            string sql = @"update dbo.Student 
                               set Upload_Identity_Document = @Upload_Identity_Document
                               where Student_Identity_Number = @Student_Identity_Number;";

            return SqlDataAccess.SaveData(sql, data);
        }

        public static int deleteStudent(string studentID)
        {

            StudentDB data = new StudentDB();
            data.Student_Identity_Number = studentID;
            string sql = @"delete from dbo.Student
                           where Student_Identity_Number = @Student_Identity_Number;";

            return SqlDataAccess.SaveData(sql, data);
        }

        public static StudentDB GetStudent(string studentID)
        {
            string sql = @"select Student_Identity_Number, Student_FName, Student_LName, Student_Nationality, Race, Title, Gender, Date_Of_Birth, Marital_Status, Student_Email, 
                           Student_Cellphone_Number, Student_Residential_Address, Upload_Identity_Document, Upload_Residential_Document, Password
                           from dbo.Student
                           where Student_Identity_Number = '" + studentID + "';";
            return SqlDataAccess.SingleData<StudentDB>(sql);
        }

        public static StudentDB GetStudentEmail(string Student_Email)
        {
            string sql = @"select Student_Identity_Number, Student_FName, Student_LName, Student_Nationality, Race, Title, Gender, Date_Of_Birth, Marital_Status, Student_Email, 
                           Student_Cellphone_Number, Student_Residential_Address, Upload_Identity_Document, Upload_Residential_Document, Password
                           from dbo.Student
                           where Student_Email = '" + Student_Email + "';";
            return SqlDataAccess.SingleData<StudentDB>(sql);
        }

        public static List<StudentDB> LoadStudents()
        {
            string sql = @"select Student_Identity_Number, Student_FName, Student_LName, Student_Nationality, Race, Title, Gender, Date_Of_Birth, Marital_Status, Student_Email, 
                           Student_Cellphone_Number, Student_Residential_Address, Upload_Identity_Document, Upload_Residential_Document, Password
                           from dbo.Student;";

            return SqlDataAccess.LoadData<StudentDB>(sql);
        }
    }
}
