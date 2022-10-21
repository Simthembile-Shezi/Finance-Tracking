using Finance_Tracking.Models;
using System;
using System.IO;
using System.Web;
using System.Web.Mvc;
using System.Web.SessionState;
using static Data_Library.Business_Logic.Academic_RecordProcessor;
using static Data_Library.Business_Logic.Emails;
using static Data_Library.Business_Logic.Enrolled_AtProcessor;
using static Data_Library.Business_Logic.Finacial_RecordProcessor;
using static Data_Library.Business_Logic.InstitutionEmpProcessor;
using static Data_Library.Business_Logic.InstitutionProcessor;

namespace Finance_Tracking.Controllers
{

    [SessionState(SessionStateBehavior.Default)]
    public class InstitutionController : Controller
    {
        #region Set up Institution Profile add employees and make changes when needed
        // GET: Institution
        public ActionResult Index()
        {
            try
            {
                Institution_Employee employee = GetInstitution_Employee(Session["InstitutionEmployee"].ToString());
                Session["Organization_Name"] = employee.Organization_Name;
                return View(employee);
            }
            catch
            {
                return RedirectToAction("Login", "Home");
            }
        }

        // GET: Institution/Details
        public ActionResult InstitutionDetails()
        {
            Institution login = GetInstitutionByName(Session["Organization_Name"].ToString());
            return View(login);
        }

        // GET: Institution/Create
        public ActionResult RegisterInstitution()
        {
            return View();
        }

        // POST: Institution/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RegisterInstitution(Institution model)
        {
            model.Institution_Physical_Address = model.Street_Name + ";" + model.Sub_Town + ";" + model.City + ";" + model.Province + ";" + model.Zip_Code;
            model.Institution_Postal_Address = model.Postal_box + ";" + model.Town + ";" + model.City_Post + ";" + model.Province_Post + ";" + model.Postal_Code;
            if (model.Institution_Postal_Address == null)
                model.Institution_Physical_Address = model.Street_Name + ";" + model.Sub_Town + ";" + model.City + ";" + model.Province + ";" + model.Zip_Code;
            else
                model.Institution_Postal_Address = model.Postal_box + ";" + model.Town + ";" + model.City_Post + ";" + model.Province_Post + ";" + model.Postal_Code;

            Session["Institution"] = model;
            Session["Organization_Name"] = model.Institution_Name;
            return RedirectToAction("AdminInstitutionEmployee");
        }

        public ActionResult AdminInstitutionEmployee()
        {
            Institution_Employee employee = new Institution_Employee();
            employee.Organization_Name = Session["Organization_Name"].ToString();
            return View(employee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AdminInstitutionEmployee(Institution_Employee employee)
        {
            Institution model = (Institution)Session["Institution"];
            if (VerifyAccount(employee.Emp_Email, employee.Emp_FName + ", " + employee.Emp_LName, model))
            {
                Session["Verify"] = employee;
                return RedirectToAction("CreatePassword");
            }
            else
            {
                ViewBag.UploadStatus = "Email was not sent, please try again";
                return View(employee);
            }
        }

        [HttpGet]
        public ActionResult CreatePassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreatePassword(Institution institution)
        {
            try
            {
                Institution model = (Institution)Session["Institution"];
                Institution_Employee employee = (Institution_Employee)Session["Verify"];
                if (institution.Institution_Employee.Code.Equals(Session["Code"].ToString()))
                {
                    try
                    {
                        //Insert the institution on the database
                        CreateInstitution(model.Institution_Name, model.Institution_Physical_Address, model.Institution_Postal_Address, model.Institution_Telephone_Number, model.Institution_Email_Address);
                        //Insert the Employee on the database
                        employee.Emp_UserID = employee.Emp_Email.Replace('@', '0');
                        employee.Emp_UserID = employee.Emp_UserID.Replace('.', '0');
                        AddInstitutionAdminEmp(employee.Emp_UserID, employee.Emp_FName, employee.Emp_LName, employee.Emp_Telephone_Number, employee.Emp_Email, employee.Organization_Name, institution.Institution_Employee.Password, institution.Institution_Employee.Code);
                    }
                    catch
                    {
                        ViewBag.RegistrationError = "Admin email is already registered with the system or Funder Already Exist";
                        return View(institution);
                    }
                    return RedirectToAction("Login", "Home");
                }
                else
                {
                    ViewBag.RegistrationError = "Inncorrect code, please try again";
                    return View(institution);
                }
            }
            catch
            {
                ViewBag.RegistrationError = "You are already registered, if not please contact Finance.Tracking@outlook.com";
                return View(institution);
            }
        }

        private bool VerifyAccount(string email, string name, Institution model)
        {
            string sub = "Account Verification";
            string body = $"Dear {name}\n\nPlease enter this code {RandomCode()} to verify your account as an Institution Admin user. Also note that this will be your admin code, keep it safe and hidden.";
            return (SendEmail(email, name, sub, body));
        }
        private int RandomCode()
        {
            Random random = new Random();
            int code = random.Next(1000, 9999);
            Session["Code"] = code;
            return code;
        }

        #endregion

        #region View all funded students, update finacial and academic records 
        // GET: Institution/ViewFundedStudents
        public ActionResult ViewFundedStudents()
        {
            EnrolledStudentsViewModel students = new EnrolledStudentsViewModel();
            var list = fundedStudents(Session["Organization_Name"].ToString());
            foreach (var item in list)
            {
                FundedStudents funded = new FundedStudents(item.Student_Number, item.Student_Identity_Number, item.Student_Email, item.Institution_Name, item.Application_Status);
                students.Students.Add(funded);
            }
            Session["students"] = students;
            return View(students);
        }
        // GET: Institution/ViewStudent/123456789
        public ActionResult ViewStudent(string id) //return a student from
        {
            EnrolledStudentsViewModel students = (EnrolledStudentsViewModel)Session["students"];
            foreach (var item in students.Students)
            {
                if (item.Student_Number.Equals(id))
                {
                    Session["funded"] = item;
                    return View(item);
                }
            }
            return View();
        // GET: Institution/ViewAllStudents
        }
        public ActionResult ViewAllStudents()
        {
            Institution students = new Institution();
            var list = GetEnrolledDetailsList(Session["Organization_Name"].ToString());
            foreach (var item in list)
            {
                Enrolled_At enrolled = new Enrolled_At(item.Student_Number, item.Student_Identity_Number, item.Institution_Name, item.Qualification, item.Student_Email, item.Study_Residential_Address);
                students.Enrolled_Ats.Add(enrolled);
            }
            Session["AllStudents"] = students;
            return View(students);
        }// GET: Institution/ViewStudent/123456789
        public ActionResult ViewEnrolledStudent(string id) //return a student from
        {
            Institution students = (Institution)Session["AllStudents"];
            foreach (var item in students.Enrolled_Ats)
            {
                if (item.Student_Number.Equals(id))
                {
                    string[] address = (item.Study_Residential_Address).Split(';');
                    item.Street_Name = address[0];
                    item.City = address[1];
                    item.Sub_Town = address[2];
                    item.Province = address[3];
                    item.Zip_Code = address[4];

                    Session["enrolled"] = item;
                    return View(item);
                }
            }
            return View();
        }
        // GET: Institution/Delete/5
        public ActionResult UploadFinacialStatement(string id)
        {
            Enrolled_At student = (Enrolled_At)Session["enrolled"];
            Finacial_Record record = new Finacial_Record();
            record.Student_Number = student.Student_Number;
            return View(record);
        }

        // POST: Institution/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UploadFinacialStatement(Finacial_Record model)
        {
            try
            {
                HttpPostedFileBase loadStatement = model.Upload_Finacial_Statement;

                if (loadStatement != null)
                {
                    //Convert HttpPostedFileBase to Byte[]
                    Stream str = loadStatement.InputStream;
                    BinaryReader Br = new BinaryReader(str);
                    Byte[] loadStatement_Doc_FileDet = Br.ReadBytes((Int32)str.Length);

                    //Store the file content in Byte[] format on the model
                    model.Upload_Statement = loadStatement_Doc_FileDet;
                }
                //Set the academic year

                uploadFinacialStatement(model.Student_Number, model.Academic_Year, model.Balance_Amount, model.Upload_Statement);

                return RedirectToAction("ViewStudent", new { id = model.Student_Number });
            }
            catch
            {
                return View();
            }
        }
        // GET: Institution/Delete/5
        public ActionResult RequestStudentFunds(string id)
        {
            FundedStudents student = (FundedStudents)Session["funded"];
            Finacial_Record record = new Finacial_Record();
            record.Student_Number = student.Student_Number;
            return View(record);
        }

        // POST: Institution/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RequestStudentFunds(Finacial_Record model)
        {
            try
            {
                requestFunds(model.Student_Number, model.Academic_Year, model.Request_Funds);

                return RedirectToAction("UploadFinacialStatement", new { id = model.Student_Number });
            }
            catch
            {
                return View();
            }
        }
        // GET: Institution/Delete/5
        public ActionResult ProvideStudentResult(string id)
        {
            Enrolled_At student = (Enrolled_At)Session["enrolled"];
            Academic_Record record = new Academic_Record();
            record.Student_Number = student.Student_Number;
            return View(record);
        }

        // POST: Institution/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ProvideStudentResult(Academic_Record model)
        {
            try
            {
                HttpPostedFileBase transcript = model.transcript;

                if (transcript != null)
                {
                    //Convert HttpPostedFileBase to Byte[]
                    Stream str = transcript.InputStream;
                    BinaryReader Br = new BinaryReader(str);
                    Byte[] Transcript_Doc_FileDet = Br.ReadBytes((Int32)str.Length);

                    //Store the file content in Byte[] format on the model
                    model.Upload_Transcript = Transcript_Doc_FileDet;
                }
                provideStudentResult(model.Student_Number, model.Academic_Year, model.Qualification, model.Avarage_Marks, model.Upload_Transcript);

                return RedirectToAction("ViewStudent", new { id = model.Student_Number });
            }
            catch
            {
                return View();
            }
        }
        #endregion

        #region Admin
        // GET: Institution/Edit/5
        public ActionResult MaintainInstitution()
        {
            Institution model = GetInstitutionByName(Session["Organization_Name"].ToString());
            return View(model);
        }

        // POST: Institution/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MaintainInstitution(Institution model)
        {
            try
            {
                string Physical_Address = model.Street_Name + ";" + model.Sub_Town + ";" + model.City + ";" + model.Province + ";" + model.Zip_Code;
                string Postal_Address = model.Postal_box + ";" + model.Town + ";" + model.City_Post + ";" + model.Province_Post + ";" + model.Postal_Code;
                UpdateIns(model.Institution_Name, Physical_Address, Postal_Address, model.Institution_Telephone_Number, model.Institution_Email_Address);
                return RedirectToAction("InstitutionDetails");
            }
            catch
            {
                return View(model);
            }
        }

        // GET: Institution/DeleteInstitution
        public ActionResult DeleteInstitution()
        {
            Institution model = GetInstitutionByName(Session["Organization_Name"].ToString());
            return View(model);
        }

        // POST: Institution/DeleteInstitution
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteInstitution(Institution model)
        {
            try
            {
                DeleteIns(model.Institution_Name);
                return RedirectToAction("Login", "Home");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult VerifyAdmin()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult VerifyAdmin(Institution_Employee model)
        {
            Institution_Employee employee = GetInstitution_Employee(Session["InstitutionEmployee"].ToString());
            if (employee.Admin_Code == null)
            {
                ViewBag.Admin = "Admin code not found, please contact your system admin";
                return View(model);
            }
            if ((employee.Admin_Code).Equals(model.Admin_Code))
                return RedirectToAction("InstitutionDetails");
            else
            {
                ViewBag.Admin = "Admin code is invalid, please check the code and try again";
                return View(model);
            }
        }

        public ActionResult ViewEmployees()
        {
            Institution institution = new Institution();
            var list = GetInstitutionEmployees(Session["Organization_Name"].ToString());
            foreach (var item in list)
            {
                Institution_Employee result = new Institution_Employee(item.Emp_UserID, item.Emp_FName, item.Emp_LName, item.Emp_Telephone_Number, item.Emp_Email, item.Organization_Name, item.Password, item.Admin_Code);
                institution.Institution_Employees.Add(result);
            }
            return View(institution);
        }
        public ActionResult EmployeeDetails(string id)
        {
            Institution_Employee employee = GetInstitution_Employee(id);
            return View(employee);
        }

        public ActionResult DeleteEmployee(string id)
        {
            Institution_Employee employee = GetInstitution_Employee(id);
            return View(employee);
        }

        // POST: Funder/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteEmployee(Funder_Employee employee)
        {
            int result = DeleteInstitutionEmp(employee.Emp_Email);
            if (result == 0)
            {
                return View(employee);
            }
            return RedirectToAction("ViewEmployees");
        }

        public ActionResult AddEmployee()
        {
            Institution_Employee model = new Institution_Employee();
            model.Organization_Name = Session["Organization_Name"].ToString();
            return View(model);
        }

        // POST: Funder/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddEmployee(Institution_Employee employee)
        {
            employee.Emp_UserID = employee.Emp_Email.Replace('@', '0');
            employee.Emp_UserID = employee.Emp_UserID.Replace('.', '0');
            int result = AddInstitutionEmp(employee.Emp_UserID, employee.Emp_FName, employee.Emp_LName, employee.Emp_Telephone_Number, employee.Emp_Email, employee.Organization_Name, employee.Password);
            if (result == 0)
            {
                ViewBag.NotAdded = "Employee profile was not succefully created";
                return View(employee);
            }
            return RedirectToAction("InstitutionDetails");
        }
        #endregion

        private Institution GetInstitutionByName(string name)
        {
            var institution = GetInstitution(name);
            Institution login = new Institution(institution.Institution_Name, institution.Institution_Telephone_Number, institution.Institution_Email_Address, institution.Institution_Physical_Address, institution.Institution_Postal_Address);

            //Separating the Physical address
            string[] address = institution.Institution_Physical_Address.Split(';');
            login.Street_Name = address[0];
            login.Sub_Town = address[1];
            login.City = address[2];
            login.Province = address[3];
            login.Zip_Code = address[4];

            //Separating the Postal Address
            address = institution.Institution_Postal_Address.Split(';');
            login.Postal_box = address[0];
            login.Town = address[1];
            login.City_Post = address[2];
            login.Province_Post = address[3];
            login.Postal_Code = address[4];

            return login;
        }
        private Institution_Employee GetInstitution_Employee(string userID)
        {
            //Get the employee frm the database
            var institutionEmp = GetInstitutionEmpID(userID);

            //Map employee to login
            Institution_Employee loginEmp = new Institution_Employee(institutionEmp.Emp_UserID, institutionEmp.Emp_FName, institutionEmp.Emp_LName, institutionEmp.Emp_Telephone_Number,
                institutionEmp.Emp_Email, institutionEmp.Organization_Name, institutionEmp.Password, institutionEmp.Admin_Code);

            return loginEmp;
        }
    }
}
