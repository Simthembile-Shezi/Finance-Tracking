using Finance_Tracking.Models;
using System;
using System.IO;
using System.Web;
using System.Web.Mvc;
using System.Web.SessionState;
using static Data_Library.Business_Logic.Academic_RecordProcessor;
using static Data_Library.Business_Logic.Finacial_RecordProcessor;
using static Data_Library.Business_Logic.InstitutionEmpProcessor;
using static Data_Library.Business_Logic.InstitutionProcessor;
using static Data_Library.Business_Logic.StudentProcessor;

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
                Institution_Employee employee = (Institution_Employee)Session["InstitutionEmployee"];
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
            Institution_Employee employee = (Institution_Employee)Session["InstitutionEmployee"];
            var institution = GetInstitution(employee.Organization_Name);
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
            try
            {
                Session["Institution"] = model;
                string Physical_Address = model.Street_Name + ";" + model.Sub_Town + ";" + model.City + ";" + model.Province + ";" + model.Zip_Code;
                string Postal_Address = model.Postal_box + ";" + model.Town + ";" + model.City_Post + ";" + model.Province_Post + ";" + model.Postal_Code;

                //Insert the institution on the database
                CreateInstitution(model.Institution_Name, Physical_Address, Postal_Address, model.Institution_Telephone_Number, model.Institution_Email_Address);
                return RedirectToAction("AdminInstitutionEmployee");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult AdminInstitutionEmployee()
        {
            Institution model = (Institution)Session["Institution"];
            Institution_Employee employee = new Institution_Employee();
            employee.Organization_Name = model.Institution_Name;
            employee.Institution = model;
            return View(employee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AdminInstitutionEmployee(Institution_Employee employee)
        {
            Institution funder = (Institution)Session["Institution"];
            //try
            {
                //if (ModelState.IsValid)
                {
                    //Insert the Employee on the database
                    AddInstitutionEmp(employee.Emp_FName, employee.Emp_LName, employee.Emp_Telephone_Number, employee.Emp_Email,
                        employee.Organization_Name, employee.Password, employee.Admin_Code);
                }

                return RedirectToAction("Login", "Home");
            }
            //catch
            //{
            //    return View();
            //}
        }
        // GET: Institution/Edit/5
        public ActionResult MaintainInstitution()
        {
            Institution_Employee employee = (Institution_Employee)Session["InstitutionEmployee"];
            return View(employee.Institution);
        }

        // POST: Institution/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MaintainInstitution(Institution model)
        {
            //try
            {
                string Physical_Address = model.Street_Name + ";" + model.Sub_Town + ";" + model.City + ";" + model.Province + ";" + model.Zip_Code;
                string Postal_Address = model.Postal_box + ";" + model.Town + ";" + model.City_Post + ";" + model.Province_Post + ";" + model.Postal_Code;
                UpdateIns(model.Institution_Name, Physical_Address, Postal_Address, model.Institution_Telephone_Number, model.Institution_Email_Address);
                return View("InstitutionDetails", model);
            }
            //catch
            //{
            //    return View();
            //}
        }

        // GET: Institution/DeleteInstitution
        public ActionResult DeleteInstitution()
        {
            Institution_Employee employee = (Institution_Employee)Session["InstitutionEmployee"];
            return View(employee.Institution);
        }

        // POST: Institution/DeleteInstitution
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteInstitution(Institution model)
        {
            //try
            {
                DeleteIns(model.Institution_Name);
                return RedirectToAction("Login", "Home");
            }
            //catch
            //{
            //    return View();
            //}
        }
        #endregion

        #region View all funded students, update finacial and academic records 
        // GET: Institution/Delete/5
        public ActionResult ViewFundedStudents() // create a join tables - model name: FundedStudents
        {
            Institution_Employee employee = (Institution_Employee)Session["InstitutionEmployee"];
            EnrolledStudentsViewModel students = new EnrolledStudentsViewModel();            
            var list = fundedStudents(employee.Organization_Name);
            foreach (var item in list)
            {
                FundedStudents funded = new FundedStudents(item.Student_Number, item.Student_Identity_Number, item.Student_Email, item.Institution_Name, item.Application_Status);
                students.Students.Add(funded);
            }
            Session["students"] = students;
            return View(students);
        }
        // GET: Institution/Delete/5
        public ActionResult ViewStudent(string id) //return a student from
        {
            EnrolledStudentsViewModel students = (EnrolledStudentsViewModel)Session["students"];
            foreach (var item in students.Students)
            {
                if (item.Student_Number.Equals(id))
                {
                    Session["enrolled"] = item;
                    return View(item);
                }
            }
            return View();
        }
        // GET: Institution/Delete/5
        public ActionResult UploadFinacialStatement(string id)
        {
            FundedStudents student = (FundedStudents)Session["enrolled"];
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
            FundedStudents student = (FundedStudents)Session["enrolled"];
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
            FundedStudents student = (FundedStudents)Session["enrolled"];
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
        public ActionResult VerifyAdmin()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult VerifyAdmin(Institution_Employee model)
        {
            Institution_Employee employee = (Institution_Employee)Session["InstitutionEmployee"];
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

        public ActionResult AddEmployee()
        {
            Institution_Employee employee = (Institution_Employee)Session["InstitutionEmployee"];
            Institution_Employee model = new Institution_Employee();
            model.Institution = employee.Institution;
            model.Organization_Name = employee.Organization_Name;
            return View(model);
        }

        // POST: Funder/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddEmployee(Institution_Employee employee)
        {
            AddInstitutionEmp(employee.Emp_FName, employee.Emp_LName, employee.Emp_Telephone_Number, employee.Emp_Email,
                        employee.Organization_Name, employee.Password, employee.Admin_Code);

            return View("InstitutionDetails");
        }


        #endregion
    }
}
