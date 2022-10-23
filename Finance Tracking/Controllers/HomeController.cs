using Data_Library.ModelsDB;
using Finance_Tracking.Models;
using System;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.SessionState;
using static Data_Library.Business_Logic.FunderEmpProcessor;
using static Data_Library.Business_Logic.FunderProcessor;
using static Data_Library.Business_Logic.InstitutionEmpProcessor;
using static Data_Library.Business_Logic.InstitutionProcessor;
using static Data_Library.Business_Logic.StudentProcessor;
using static Data_Library.Business_Logic.Emails;

namespace Finance_Tracking.Controllers
{
    [SessionState(SessionStateBehavior.Default)]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            Session["Verified"] = "false";
            return View();
        }

        public ActionResult SignUp()
        {
            Session["Verified"] = "false";
            return View();
        }

        public ActionResult Login()
        {
            Session["Verified"] = "false";
            LoginViewModel modelview = new LoginViewModel();
            return View(modelview);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel modelview)
        {
            try
            {
                Session["user"] = modelview.UserType;
                if (modelview.UserType == "STUDENT_USER")
                {
                    //Get the student from the database 
                    StudentDB student = GetStudent(modelview.Username);

                    //Check if student is in the database and whether the password matches
                    if (student != null && student.Password.Equals(modelview.Password))
                    {
                        Student loginStudent = new Student(student.Student_Identity_Number, student.Student_FName, student.Student_LName,
                            student.Student_Nationality, student.Race, student.Title, student.Gender, student.Date_Of_Birth, student.Marital_Status,
                            student.Student_Email, student.Student_Cellphone_Number, student.Student_Residential_Address, student.Upload_Identity_Document, student.Upload_Residential_Document);

                        string[] address = (student.Student_Residential_Address).Split(';');

                        loginStudent.Street_Name = address[0];
                        loginStudent.City = address[1];
                        loginStudent.Sub_Town = address[2];
                        loginStudent.Province = address[3];
                        loginStudent.Zip_Code = address[4];

                        Session["Student"] = student.Student_Identity_Number;
                        FormsAuthentication.SetAuthCookie(modelview.Username, false);
                        return RedirectToAction("Index", "Student");
                    }
                    else
                    {
                        ViewBag.LoginFailed = "ID number and/or Password is incorrect, please try again or reset password";
                        return View(modelview);
                    }
                }
                else if (modelview.UserType == "INSTITUTION_USER")
                {
                    //Get the employee frm the database
                    InstitutionEmployeeDB institutionEmp = GetInstitutionEmp(modelview.Username);

                    //Get the institution from the database and check whether the password of the employee matches
                    if (institutionEmp != null && institutionEmp.Password.Equals(modelview.Password))
                    {
                        InstitutionDB inst = GetInstitution(institutionEmp.Organization_Name);

                        //Map institution to login
                        Institution institution = new Institution(inst.Institution_Name, inst.Institution_Telephone_Number, inst.Institution_Email_Address,
                            inst.Institution_Physical_Address, inst.Institution_Postal_Address);

                        //Separating the Physical address
                        string[] address = inst.Institution_Physical_Address.Split(';');
                        institution.Street_Name = address[0];
                        institution.Sub_Town = address[1];
                        institution.City = address[2];
                        institution.Province = address[3];
                        institution.Zip_Code = address[4];

                        //Separating the Postal Address
                        address = inst.Institution_Postal_Address.Split(';');
                        institution.Postal_box = address[0];
                        institution.Town = address[1];
                        institution.City_Post = address[2];
                        institution.Province_Post = address[3];
                        institution.Postal_Code = address[4];

                        //Map employee to login
                        Institution_Employee loginEmp = new Institution_Employee(institutionEmp.Emp_UserID,institutionEmp.Emp_FName, institutionEmp.Emp_LName, institutionEmp.Emp_Telephone_Number,
                            institutionEmp.Emp_Email, institutionEmp.Organization_Name, institutionEmp.Password, institutionEmp.Admin_Code);

                        Session["InstitutionEmployee"] = loginEmp.Emp_UserID;
                        FormsAuthentication.SetAuthCookie(modelview.Username, false);
                        return RedirectToAction("Index", "Institution");
                    }
                    else
                    {
                        ViewBag.LoginFailed = "Email and/or Password is incorrect, please try again or reset password";
                        return View(modelview);
                    }
                }
                else if (modelview.UserType == "FUNDER_USER")
                {
                    //Get the employee frm the database
                    FunderEmployeeDB funderEmp = GetFunderEmp(modelview.Username);

                    //Get the institution from the database and check whether the password of the employee matches
                    if (funderEmp != null && funderEmp.Password.Equals(modelview.Password))
                    {
                        FunderDB funder = GetFunder(funderEmp.Organization_Name);

                        //Map funder to login funder by initializing a new Funder
                        Funder loginFunder = new Funder(funder.Funder_Name, funder.Funder_Tax_Number, funder.Funder_Email,
                            funder.Funder_Telephone_Number, funder.Funder_Physical_Address, funder.Funder_Postal_Address);

                        //Separating the Physical address
                        string[] address = funder.Funder_Physical_Address.Split(';');
                        loginFunder.Street_Name = address[0];
                        loginFunder.Sub_Town = address[1];
                        loginFunder.City = address[2];
                        loginFunder.Province = address[3];
                        loginFunder.Zip_Code = address[4];

                        //Separating the Postal Address
                        address = funder.Funder_Postal_Address.Split(';');
                        loginFunder.Postal_box = address[0];
                        loginFunder.Town = address[1];
                        loginFunder.City_Post = address[2];
                        loginFunder.Province_Post = address[3];
                        loginFunder.Postal_Code = address[4];

                        //Map employee to login
                        Funder_Employee loginEmp = new Funder_Employee(funderEmp.Emp_UserID,funderEmp.Emp_FName, funderEmp.Emp_LName, funderEmp.Emp_Telephone_Number,
                            funderEmp.Emp_Email, funderEmp.Organization_Name, funderEmp.Password, funderEmp.Admin_Code);

                        Session["FunderEmployee"] = loginEmp.Emp_UserID;
                        FormsAuthentication.SetAuthCookie(modelview.Username, false);
                        return RedirectToAction("Index", "Funder");
                    }
                    else
                    {
                        ViewBag.LoginFailed = "Email and/or Password is incorrect, please try again or reset password";
                        return View(modelview);
                    }
                }
                else
                {
                    ViewBag.LoginFailed = "Please select the user type and try again";
                    return View(modelview);
                }
            }
            catch (Exception)
            {
                ViewBag.LoginFailed = "User account does not exist, please register a profile";
                return View(modelview);
            }
        }
        public ActionResult LogOut()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOut(LoginViewModel modelview)
        {
            FormsAuthentication.SignOut();
            Session["user"] = null;
            return RedirectToAction("Login", "Home");
        }

        public ActionResult ForgotPassword()
        {
            ViewBag.NotUser = null;
            Session["Verified"] = "false";
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [System.Obsolete]
        public ActionResult ForgotPassword(SendEmailViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (model.UserType == "STUDENT_USER")
                    {
                        var user = GetStudentEmail(model.ToEmail);
                        if (user == null)
                        {
                            ViewBag.NotUser = "Email address does not exist";
                            return View(model);
                        }
                        if (SendForgotPassword(user.Student_Email, user.Student_FName + "," + user.Student_LName) == false)
                        {
                            ViewBag.Error = "Email was not sent, please try again";
                            return View(model);
                        }
                        Session["Student"] = user;
                    }
                    else if (model.UserType == "INSTITUTION_USER")
                    {
                        var user = GetInstitutionEmp(model.ToEmail);
                        if (user == null)
                        {
                            ViewBag.NotUser = "Email address does not exist";
                            return View(model);
                        }
                        if (SendForgotPassword(user.Emp_Email, user.Emp_FName + "," + user.Emp_LName) == false)
                        {
                            ViewBag.Error = "Email was not sent, please try again";
                            return View(model);
                        }
                        Session["InstitutionEmployee"] = user;
                    }
                    else if (model.UserType == "FUNDER_USER")
                    {
                        var user = GetFunderEmp(model.ToEmail);
                        if (user == null)
                        {
                            ViewBag.NotUser = "Email address does not exist";
                            return View(model);
                        }
                        if(SendForgotPassword(user.Emp_Email, user.Emp_FName + "," + user.Emp_LName) == false)
                        {
                            ViewBag.Error = "Email was not sent, please try again";
                            return View(model);
                        }
                        Session["FunderEmployee"] = user;
                    }
                    Session["SendEmail"] = model;

                    return RedirectToAction("ChangePassword");
                }
                else
                {
                    ViewBag.Error = "Try again later";
                    return View(model);
                }
            }
            catch
            {
                ViewBag.Error = "Try again later";
                return View(model);
            }
        }
        private bool SendForgotPassword(string email, string name)
        {
            string sub = "Forgot password";
            string body = "Enter this code " + RandomCode() + ",to reset your password";
            return (SendEmail(email, name, sub, body));
        }
        private int RandomCode()
        {
            Random random = new Random();
            int code = random.Next(1000, 9999);
            Session["Code"] = code;
            return code;
        }
        public ActionResult ChangePassword()
        {
            ViewBag.Error = null;
            Session["Verified"] = "false";
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(ChangePasswordViewModel change)
        {
            SendEmailViewModel model = (SendEmailViewModel)Session["SendEmail"];
            if (change.Code.Equals(Session["Code"].ToString()))
            {
                if (model.UserType == "STUDENT_USER")
                {
                    StudentDB student = (StudentDB)Session["Student"];
                    updateStudentPassword(student.Student_Identity_Number, change.Password);
                }
                else if (model.UserType == "INSTITUTION_USER")
                {
                    InstitutionEmployeeDB institutionEmployee = (InstitutionEmployeeDB)Session["InstitutionEmployee"];
                    updateInstitutionEmpPassword(institutionEmployee.Emp_Email, change.Password);
                }
                else if (model.UserType == "FUNDER_USER")
                {
                    FunderEmployeeDB funderEmployee = (FunderEmployeeDB)Session["FunderEmployee"];
                    updateFunderEmpPassword(funderEmployee.Emp_Email, change.Password);
                }
                return RedirectToAction("Login");
            }
            else
            {
                ViewBag.Error = "Try again later";
                return View();
            }
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }
        public ActionResult Error()
        {
            return View();
        }
    }
}