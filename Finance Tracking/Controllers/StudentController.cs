using Finance_Tracking.Models;
using System;
using System.IO;
using System.Web;
using System.Web.Mvc;
using System.Web.SessionState;
using System.Net.Mime;
using static Data_Library.Business_Logic.StudentProcessor;
using static Data_Library.Business_Logic.BursaryProcessor;
using static Data_Library.Business_Logic.ApplicationProcessor;
using static Data_Library.Business_Logic.Finacial_RecordProcessor;
using static Data_Library.Business_Logic.Bursar_FundProcessor;
using static Data_Library.Business_Logic.Enrolled_AtProcessor;

namespace Finance_Tracking.Controllers
{
    [SessionState(SessionStateBehavior.Default)]
    public class StudentController : Controller
    {
        #region Student profile
        // GET: Student 
        public ActionResult Index()
        {
            Student student = (Student)Session["Student"];
            return View(student);
        }

        // GET: Student/Details/5
        public ActionResult StudentDetails()
        {
            return View();
        }

        // GET: Student/Create
        public ActionResult RegisterStudentInfo()
        {
            return View();
        }

        // POST: Student/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RegisterStudentInfo(Student model)
        {
            try
            {
                Session["StudentInfo"] = model;

                return RedirectToAction("RegisterStudentContacts");
            }
            catch
            {
                return View();
            }

        }
        // GET: Student/Create
        public ActionResult RegisterStudentContacts()
        {
            return View();
        }

        // POST: Student/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RegisterStudentContacts(Student model)
        {
            try
            {
                Session["StudentContacts"] = model;
                return RedirectToAction("UploadStudentDocs");
            }
            catch
            {
                return View();
            }
        }
        // GET: Student/Create
        public ActionResult UploadStudentDocs()
        {
            return View();
        }

        // POST: Student/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UploadStudentDocs(Student model)
        {
            #region PDF
            //String Residential_FileExt = Path.GetExtension(Residential_Doc.FileName).ToUpper();
            //String ID_FileExt = Path.GetExtension(ID_Doc.FileName).ToUpper();
            //if (Residential_FileExt == ".PDF" && ID_FileExt == ".PDF")
            //{
            //}
            //else
            //{
            //    ViewBag.FileStatus = "Invalid file format.";
            //    return View();
            //}
            #endregion
            try
            {
                HttpPostedFileBase Residential_Doc = model.Residential_Document;
                HttpPostedFileBase ID_Doc = model.Identity_Document;

                Student studentInfo = (Student)Session["StudentInfo"];
                Student studentContacts = (Student)Session["StudentContacts"];

                if (Residential_Doc != null)
                {
                    //Convert HttpPostedFileBase to Byte[]
                    Stream str = Residential_Doc.InputStream;
                    BinaryReader Br = new BinaryReader(str);
                    Byte[] Residential_Doc_FileDet = Br.ReadBytes((Int32)str.Length);

                    //Store the file content in Byte[] format on the model
                    model.Upload_Residential_Document = Residential_Doc_FileDet;
                }
                if (ID_Doc != null)
                {
                    //Convert HttpPostedFileBase to Byte[]
                    Stream stream = ID_Doc.InputStream;
                    BinaryReader Binary = new BinaryReader(stream);
                    Byte[] ID_Doc_FileDet = Binary.ReadBytes((Int32)stream.Length);

                    //Store the file content in Byte[] format on the model
                    model.Upload_Identity_Document = ID_Doc_FileDet;
                }

                model.Student_Identity_Number = studentInfo.Student_Identity_Number;
                model.Student_FName = studentInfo.Student_FName;
                model.Student_LName = studentInfo.Student_LName;
                model.Student_Nationality = studentInfo.Student_Nationality;
                model.Race = studentInfo.Race;
                model.Title = studentInfo.Title;
                model.Gender = studentInfo.Gender;
                model.Date_Of_Birth = studentInfo.Date_Of_Birth;
                model.Marital_Status = studentInfo.Marital_Status;
                model.Student_Email = studentContacts.Student_Email;
                model.ConfirmStudent_Email = studentContacts.ConfirmStudent_Email;
                model.Student_Cellphone_Number = studentContacts.Student_Cellphone_Number;
                model.Password = studentContacts.Password;
                model.ConfirmPassword = studentContacts.ConfirmPassword;
                model.Street_Name = studentContacts.Street_Name;
                model.Sub_Town = studentContacts.Sub_Town;
                model.City = studentContacts.City;
                model.Province = studentContacts.Province;
                model.Zip_Code = studentContacts.Zip_Code;
                model.Student_Residential_Address = model.Street_Name + ";\n" + model.Sub_Town + ";\n" + model.City + ";\n" + model.Province + ";\n" + model.Zip_Code;

                Session["Student"] = model;
                return RedirectToAction("SaveProfile");
            }
            catch
            {
                return View();
            }
        }
        public ActionResult SaveProfile()
        {
            Student model = (Student)Session["Student"];
            CreateStudent(model.Student_Identity_Number,
                    model.Student_FName,
                    model.Student_LName,
                    model.Student_Nationality,
                    model.Race,
                    model.Title,
                    model.Gender,
                    model.Date_Of_Birth,
                    model.Marital_Status,
                    model.Student_Email,
                    model.Student_Cellphone_Number,
                    model.Student_Residential_Address,
                    model.Upload_Identity_Document,
                    model.Upload_Residential_Document,
                    model.Password);

            return View("Index", model);
        }

        // GET: Student/Edit docs/5
        public ActionResult MaintainStudentDocs()
        {
            Student student = (Student)Session["Student"];
            return View(student);
        }

        // POST: Student/Edit docs/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MaintainStudentDocs(Student model)
        {
            try
            {
                HttpPostedFileBase Residential_Doc = model.Residential_Document;
                HttpPostedFileBase ID_Doc = model.Identity_Document;

                if (Residential_Doc != null)
                {
                    //Convert HttpPostedFileBase to Byte[]
                    Stream str = Residential_Doc.InputStream;
                    BinaryReader Br = new BinaryReader(str);
                    Byte[] Residential_Doc_FileDet = Br.ReadBytes((Int32)str.Length);

                    //Store the file content in Byte[] format on the model
                    model.Upload_Residential_Document = Residential_Doc_FileDet;
                }
                if (ID_Doc != null)
                {
                    //Convert HttpPostedFileBase to Byte[]
                    Stream stream = ID_Doc.InputStream;
                    BinaryReader Binary = new BinaryReader(stream);
                    Byte[] ID_Doc_FileDet = Binary.ReadBytes((Int32)stream.Length);

                    //Store the file content in Byte[] format on the model
                    model.Upload_Identity_Document = ID_Doc_FileDet;
                }
                UploadDocs(model.Student_Identity_Number, model.Upload_Identity_Document, model.Upload_Residential_Document);



                return RedirectToAction("Index");
            }
            catch
            {
                return View(model);
            }
        }

        //GET: Student/Edit/5
        public ActionResult MaintainStudent()
        {
            Student student = (Student)Session["Student"];
            return View(student);
        }

        // POST: Student/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MaintainStudent(Student model)
        {
            try
            {
                string Residential_Address = model.Street_Name + ";" + model.Sub_Town + ";" + model.City + ";" + model.Province + ";" + model.Zip_Code;
                UpdateStudent(model.Student_Identity_Number, model.Student_Email, model.Student_Cellphone_Number, Residential_Address);
                return RedirectToAction("Index");
            }
            catch
            {
                return View(model);
            }
        }

        // GET: Student/Delete/5
        public ActionResult DeleteStudent()
        {
            Student student = (Student)Session["Student"];
            return View(student);
        }

        // POST: Student/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        //Late change
        public ActionResult DeleteStudent(Student model)
        {
            try
            {
                deleteStudent(model.Student_Identity_Number);

                return RedirectToAction("Login", "Home");
            }
            catch
            {
                return View(model);
            }
        }
        #endregion

        #region Enrollment
        public ActionResult InstitutionInfo()
        {
            Student student = (Student)Session["Student"];
            var uni = GetStudentEnrolledList(student.Student_Identity_Number);
            Student model = new Student();
            foreach(var item in uni)
            {
                Enrolled_At enrolled = new Enrolled_At(item.Student_Number, item.Student_Identity_Number, item.Institution_Name, item.Qualification, item.Student_Email, item.Study_Residential_Address);
                model.Enrolled_Ats.Add(enrolled);
            }
            return View(model);
        }
        public ActionResult ViewInstitutionInfo(string id)
        {
            var item = GetEnrolledDetails(id);
            Enrolled_At enrolled = new Enrolled_At(item.Student_Number, item.Student_Identity_Number, item.Institution_Name, item.Qualification, item.Student_Email, item.Study_Residential_Address);
            
            string[] address = enrolled.Study_Residential_Address.Split(';');

            enrolled.Street_Name = address[0];
            enrolled.City = address[1];
            enrolled.Sub_Town = address[2];
            enrolled.Province = address[3];
            enrolled.Zip_Code = address[4];

            Session["Enrolled"] = enrolled;
            return View(enrolled);
        }
        public ActionResult RegisterInstitutionInfo()
        {
            Enrolled_At enrolled = new Enrolled_At();
            return View(enrolled);
        }

        // POST: Student/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RegisterInstitutionInfo(Enrolled_At model)
        {
            try
            {
                Student student = (Student)Session["Student"];
                model.Study_Residential_Address = model.Street_Name + ";" + model.Sub_Town + ";" + model.City + ";" + model.Province + ";" + model.Zip_Code;
                CreateEnrolledDetails(model.Student_Number, model.Student_Identity_Number, model.Institution_Name, model.Qualification, model.Student_Email, model.Study_Residential_Address);
                return RedirectToAction("InstitutionInfo");
            }
            catch
            {
                return View();
            }

        }
        public ActionResult EditInstitutionInfo(string id)
        {
            Enrolled_At enrolled = (Enrolled_At)Session["Enrolled"];
            return View(enrolled);
        }

        // POST: Student/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditInstitutionInfo(Enrolled_At model)
        {
            try
            {
                Student student = (Student)Session["Student"];
                model.Study_Residential_Address = model.Street_Name + ";" + model.Sub_Town + ";" + model.City + ";" + model.Province + ";" + model.Zip_Code;
                UpdateEnrolledDetails(model.Student_Number, model.Student_Identity_Number, model.Institution_Name,model.Qualification, model.Student_Email, model.Study_Residential_Address);
                return RedirectToAction("ViewInstitutionInfo", new { id = model.Student_Number });
            }
            catch
            {
                return View();
            }

        }
        public ActionResult DeleteInstitutionInfo(string id)
        {
            Enrolled_At enrolled = (Enrolled_At)Session["Enrolled"];
            return View(enrolled);
        }

        // POST: Student/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteInstitutionInfo(Enrolled_At model)
        {
            try
            {
                Student student = (Student)Session["Student"];

                DeleteEnrolledDetails(model.Student_Number);
                return RedirectToAction("InstitutionInfo");
            }
            catch
            {
                return View();
            }

        }
        
        public ActionResult ViewFinacialStatement(string id)
        {
            Enrolled_At enrolled = (Enrolled_At)Session["Enrolled"];
            Finacial_Record record = new Finacial_Record();
            record.Student_Number = enrolled.Student_Number;
            return View(record);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ViewFinacialStatement(Finacial_Record model)
        {
            var item = GetStudentFinRec(model.Student_Number, model.Academic_Year);
            if(item == null)
            {
                return View(model);
            }
            Finacial_Record record = new Finacial_Record(item.Student_Number, item.Academic_Year, item.Balance_Amount, item.Upload_Statement, item.Funding_Status, item.Request_Funds);

            return View(record);
        }

        #endregion

        #region Apply for funding
        // GET: Student/ViewBursaries
        public ActionResult ViewBursaries()
        {
            ApplyBursaryViewModel model = new ApplyBursaryViewModel();
            var list = LoadBursaries();
            foreach(var item in list)
            {
                Bursary bursary = new Bursary(item.Bursary_Code,item.Bursary_Name, item.Start_Date, item.Funder_Name, item.End_Date, item.Bursary_Amount, item.Number_Available, item.Description, item.Funding_Year);
                
                model.Bursaries.Add(bursary);
                model.Bursary = bursary;
            }          
            Session["Apply"] = model;
            return View(model);
        }
        public ActionResult ApplyForFunds(string id)
        {
            ApplyBursaryViewModel model = (ApplyBursaryViewModel)Session["Apply"];
            foreach (var item in model.Bursaries)
            {
                if (item.Bursary_Code.Equals(id.ToString()))
                {
                    Bursary bursary = new Bursary(item.Bursary_Code, item.Bursary_Name, item.Start_Date, item.Funder_Name, item.End_Date, item.Bursary_Amount, item.Number_Available, item.Description, item.Funding_Year);
                    Session["Bursary"] = bursary;
                    return View(item);
                }
            }
            return RedirectToAction("ViewBursaries");
        }

        // POST: Student/Apply/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ApplyForFunds(/*Bursary model*/)
        {
            try
            {
                Bursary model = (Bursary)Session["Bursary"];
                if (model.Number_Available != 0)
                {
                    Student student = (Student)Session["Student"];
                    CreateApplication(model.Bursary_Code + student.Student_Identity_Number, student.Student_Identity_Number, model.Bursary_Code, model.Funding_Year, "Applied");
                    decimal num = model.Number_Available - 1;
                    UpdateBursaryNumberAvail(model.Bursary_Code, num);
                    return RedirectToAction("ViewBursaries");
                }
                else
                {
                    Bursary bursary = (Bursary)Session["Bursary"];
                    ViewBag.ApplicationExist = "No applications available at the moment";
                    return View(bursary);
                }
            }
            catch
            {
                Bursary bursary = (Bursary)Session["Bursary"];
                ViewBag.ApplicationExist = "You have already applied for this bursary";
                return View(bursary);
            }
        }
        #endregion

        #region Track funding status
        // GET: Student/TrackFunding
        public ActionResult TrackFunding() //Show all applications
        {
            Student student = (Student)Session["Student"];
            ApplyBursaryViewModel model = new ApplyBursaryViewModel();

           
            var list = GetStudentApplications(student.Student_Identity_Number);
            foreach(var item in list)
            {
                Application application = new Application();
                application.Application_ID = item.Application_ID;
                application.Student_Identity_Number = item.Student_Identity_Number;
                application.Bursary_Code = item.Bursary_Code;
                application.Application_Status = item.Application_Status;
                application.Upload_Agreement = item.Upload_Agreement;
                application.Upload_Signed_Agreement = item.Upload_Signed_Agreement;
                application.Funding_Year = item.Funding_Year;

                model.Applications.Add(application);
            }
            Session["Track"] = model;
            return View(model);
        }

        // GET: Student/ShowApplication/Application_ID
        public ActionResult ShowApplication(string id) // one application
        {
            ApplyBursaryViewModel model = (ApplyBursaryViewModel)Session["Track"];
            foreach (var item in model.Applications)
            {
                if (item.Application_ID.Equals(id.ToString()))
                {
                    Session["Application"] = item; 
                    return View(item);
                }                   
            }
            return View(model);
        }

        // GET: Student/ViewBursaryAgreement/Application_ID
        public ActionResult ViewBursaryAgreement(string id)
        {
            Application application = (Application)Session["Application"];
            if (application.Upload_Agreement == null)
            {
                ViewBag.DownloadFailed = "The funder has not uploaded the bursary agreement";
                return View(application);
            }
            ViewBag.DownloadFailed = "The funder has uploaded the bursary agreement, click the Download button to download the agreement";
            return View(application);
        }

        // GET: Student/DownloadBursaryAgreement/Application_ID
        public FileResult DownloadBursaryAgreement(string id)
        {
            Application application = (Application)Session["Application"];
            if(application.Upload_Agreement == null)
            {
                RedirectToAction("ViewBursaryAgreement");
                return File(new byte[10], MediaTypeNames.Application.Octet, application.Application_ID);
            }
            return File(application.Upload_Agreement, MediaTypeNames.Application.Octet , application.Application_ID);
        }

        // GET: Student/UploadSignedAgreement
        public ActionResult UploadSignedAgreement()
        {
            Application application = (Application)Session["Application"];
            return View(application);
        }

        // POST: Student/UploadSignedAgreement
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UploadSignedAgreement(Application application)
        {
            try
            {
                HttpPostedFileBase Agreement = application.Signed_Bursary_Agreement;


                if (Agreement != null)
                {
                    //Convert HttpPostedFileBase to Byte[]
                    Stream str = Agreement.InputStream;
                    BinaryReader Br = new BinaryReader(str);
                    Byte[] Agreement_Doc_FileDet = Br.ReadBytes((Int32)str.Length);

                    //Store the file content in Byte[] format on the model
                    application.Upload_Signed_Agreement = Agreement_Doc_FileDet;
                    uploadSignedAgreement(application.Application_ID, application.Upload_Signed_Agreement);
                }
                return RedirectToAction("ViewBursaryAgreement");
            }
            catch
            {
                return View();
            }

        }
        public ActionResult ViewFundRequest(string id)
        {
            Application application = (Application)Session["Application"];
            var item = GetBursar(id);
            if(item == null)
            {
                ViewBag.NotApproved = "Your application is still under review";
                return View("ShowApplication",application);
            }
            Bursar_Fund bursar = new Bursar_Fund(item.Application_ID, item.Update_Fund_Request, item.Funding_Status, item.Approved_Funds, application);
            return View(bursar);
        }
        #endregion
    }
}
