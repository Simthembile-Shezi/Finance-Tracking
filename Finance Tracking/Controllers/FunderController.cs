using Finance_Tracking.Models;
using System;
using System.IO;
using System.Web;
using System.Web.Mvc;
using static Data_Library.Business_Logic.ApplicationProcessor;
using static Data_Library.Business_Logic.Bursar_FundProcessor;
using static Data_Library.Business_Logic.BursaryProcessor;
using static Data_Library.Business_Logic.FunderEmpProcessor;
using static Data_Library.Business_Logic.FunderProcessor;
using static Data_Library.Business_Logic.Emails;

namespace Finance_Tracking.Controllers
{
    public class FunderController : Controller
    {
        #region Set up a funder profile, add employees and make changes when needed
        // GET: Funder
        public ActionResult Index()
        {
            try
            {
                Funder_Employee employee = (Funder_Employee)Session["FunderEmployee"];
                Session["Organization_Name"] = employee.Organization_Name;
                return View(employee);
            }
            catch
            {
                return RedirectToAction("Login", "Home");
            }
        }

        // GET: Funder/Details
        public ActionResult FunderDetails()
        {
            var funder = GetFunder(Session["Organization_Name"].ToString());
            Funder login = new Funder(funder.Funder_Name, funder.Funder_Tax_Number, funder.Funder_Email, funder.Funder_Telephone_Number, funder.Funder_Physical_Address, funder.Funder_Postal_Address);

            //Separating the Physical address
            string[] address = funder.Funder_Physical_Address.Split(';');
            login.Street_Name = address[0];
            login.Sub_Town = address[1];
            login.City = address[2];
            login.Province = address[3];
            login.Zip_Code = address[4];

            //Separating the Postal Address
            address = funder.Funder_Postal_Address.Split(';');
            login.Postal_box = address[0];
            login.Town = address[1];
            login.City_Post = address[2];
            login.Province_Post = address[3];
            login.Postal_Code = address[4];

            Session["Funder"] = login;
            return View(login);
        }

        // GET: Funder/Create
        public ActionResult RegisterFunder()
        {
            return View();
        }

        // POST: Funder/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RegisterFunder(Funder model)
        {
            Session["Funder"] = model;
            try
            {
                //if (ModelState.IsValid)
                {
                    string Physical_Address = model.Street_Name + ";" + model.Sub_Town + ";" + model.City + ";" + model.Province + ";" + model.Zip_Code;
                    string Postal_Address = model.Postal_box + ";" + model.Town + ";" + model.City_Post + ";" + model.Province_Post + ";" + model.Postal_Code;

                    //Insert the institution on the database
                    CreateFunder(model.Funder_Name, model.Funder_Tax_Number, model.Funder_Email, model.Funder_Telephone_Number, Physical_Address, Postal_Address);
                    return RedirectToAction("AdminFunderEmployee");
                }
                //else
                //    return View();

            }
            catch
            {
                ViewBag.FunderAlreadyExist = "Funder Already Exist";
                return View(model);
            }
        }
        // GET: Funder/Create/Employee
        public ActionResult AdminFunderEmployee()
        {
            Funder_Employee employee = new Funder_Employee();
            employee.Organization_Name = Session["Organization_Name"].ToString();
            return View(employee);
        }
        // POST: Funder/Create/Employee
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AdminFunderEmployee(Funder_Employee employee)
        {
            try
            {
                //Insert the Employee on the database
                AddFunderEmp(employee.Emp_FName, employee.Emp_LName, employee.Emp_Telephone_Number, employee.Emp_Email, employee.Organization_Name, employee.Password, employee.Admin_Code);
                return RedirectToAction("Login", "Home");
            }
            catch
            {
                ViewBag.AdminAlreadyExist = "Admin email is already registered with the system";
                return View(employee);
            }

        }
        private bool VerifyAccount(string email, string name)
        {
            string sub = "Account Verification";
            string body = $"Dear {name}\n\nPlease enter this code {RandomCode()} to verify your account";
            return (SendEmail(email, name, sub, body));
        }
        private int RandomCode()
        {
            Random random = new Random();
            int code = random.Next(1000, 9999);
            Session["Code"] = code;
            return code;
        }
        // GET: Funder/MaintainFunder
        public ActionResult MaintainFunder()
        {
            Funder funder = (Funder)Session["Funder"];
            return View(funder);
        }

        // POST: Funder/MaintainFunder
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MaintainFunder(Funder model)
        {
            try
            {
                string Physical_Address = model.Street_Name + ";" + model.Sub_Town + ";" + model.City + ";" + model.Province + ";" + model.Zip_Code;
                string Postal_Address = model.Postal_box + ";" + model.Town + ";" + model.City_Post + ";" + model.Province_Post + ";" + model.Postal_Code;
                UpdateFunder(model.Funder_Name, model.Funder_Email, model.Funder_Telephone_Number, Physical_Address, Postal_Address);

                return RedirectToAction("FunderDetails");
            }
            catch
            {
                return View(model);
            }
        }

        // GET: Funder/DeleteFunder
        public ActionResult DeleteFunder()
        {
            Funder funder = (Funder)Session["Funder"];
            return View(funder);
        }

        // POST: Funder/DeleteFunder
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteFunder(Funder funder)
        {
            try
            {
                deleteFunder(funder.Funder_Name);
                return RedirectToAction("Login", "Home");
            }
            catch
            {
                return View(funder);
            }
        }
        #endregion

        #region View all applications, select one and review if needed
        // GET: Funder/ViewApplications
        public ActionResult ViewApplications(string id)
        {
            Bursary bursary = new Bursary();
            bursary.Bursary_Code = id;              //Used on the HttpPost
            var list = GetApplications(id);

            foreach (var app in list)
            {
                Application application = new Application(app.Application_ID, app.Student_Identity_Number, app.Bursary_Code, app.Funding_Year, app.Application_Status, app.Upload_Agreement, app.Upload_Signed_Agreement);
                bursary.Applications.Add(application);
            }
            return View(bursary);
        }
        // POST: Funder/ViewApplications
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ViewApplications(Bursary model) //used for searching application using ID number
        {
            var list = GetApplications(model.Bursary_Code);
            Bursary result = new Bursary();
            foreach (var item in list)
            {
                if ((item.Student_Identity_Number).Equals(model.Application.Student_Identity_Number))
                {
                    Application application = new Application(item.Application_ID, item.Student_Identity_Number, item.Bursary_Code, item.Funding_Year, item.Application_Status, item.Upload_Agreement, item.Upload_Signed_Agreement);
                    result.Applications.Add(application);
                    return View(result);
                }
            }
            return View(model);
        }
        
        public ActionResult ViewApplication(string id)
        {
            #region
            //public ActionResult ViewApplication(Bursary model)
            //{
            //    var list = viewApplications(model.ApplicationView.Application_ID);
            //    foreach (var item in list)
            //    {
            //        if (item.Application_ID.Equals(model.ApplicationView.Application_ID))
            //        {
            //            ApplicationView application = new ApplicationView(item.Application_ID, item.Application_Status, item.Student_FName, item.Student_LName, item.Student_Identity_Number, item.Gender, item.Student_Cellphone_Number,
            //                item.Student_Email, item.Student_Number, item.Institution_Name, item.Qualification, item.Academic_Year, item.Avarage_Marks, item.Upload_Transcript, item.Bursary_Code);
            //            //bursary.ApplicationViews.Add(application);
            //            return View(application);
            //        }
            //    }
            //    return RedirectToAction("Error", "Home");
            //}
            #endregion
            Bursary bursary = new Bursary();
            bursary.Application.Application_ID = id;
            ApplicationView application = new ApplicationView();

            var item = viewApplications(id);
            {
                    application = new ApplicationView(item.Application_ID, item.Application_Status, item.Student_FName, item.Student_LName, item.Student_Identity_Number, item.Gender, item.Student_Cellphone_Number,
                        item.Student_Email, item.Student_Number, item.Institution_Name, item.Qualification, item.Academic_Year, item.Avarage_Marks, item.Upload_Transcript, item.Bursary_Code);
                    bursary.ApplicationViews.Add(application);
            }
            return View(application);
        }
        public ActionResult ViewAllApplications()      //using funder name
        {
            string id = Session["Organization_Name"].ToString();
            Bursary bursary = new Bursary();
            bursary.Funder_Name = id;              //Used on the HttpPost
            var list = GetAllApplications(id);

            foreach (var app in list)
            {
                Application application = new Application(app.Application_ID, app.Student_Identity_Number, app.Bursary_Code, app.Funding_Year, app.Application_Status, app.Upload_Agreement, app.Upload_Signed_Agreement);
                bursary.Applications.Add(application);
            }
            return View(bursary);
        }
        // POST: Funder/ViewApplications
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ViewAllApplications(Bursary model) //used for searching application using ID number
        {
            var list = GetAllApplications(model.Funder_Name);
            Bursary result = new Bursary();
            foreach (var item in list)
            {
                if ((item.Student_Identity_Number).Equals(model.Application.Student_Identity_Number))
                {
                    Application application = new Application(item.Application_ID, item.Student_Identity_Number, item.Bursary_Code, item.Funding_Year, item.Application_Status, item.Upload_Agreement, item.Upload_Signed_Agreement);
                    result.Applications.Add(application);
                }
            }
            /* I did this bcz the search was only returning the 1st ID of the list
             * and disregarded the rest ID's in the search
             */
            return View(result);
            //return View(model);
        }
        // GET: Funder/UpdateApplicationStatus/Application_ID
        public ActionResult UpdateApplicationStatus(string id)
        {
            var item = GetApplication(id);
            Application application = new Application(item.Application_ID, item.Student_Identity_Number, item.Bursary_Code, item.Funding_Year, item.Application_Status, item.Upload_Agreement, item.Upload_Signed_Agreement);
            if (application.Application_Status.Equals("Approved"))
            {
                ViewBag.WrongChoice = "Please be aware that application status for this application is final";
            }
            else
            {
                ViewBag.WrongChoice = null;
            }
            return View(application);
        }

        // POST: Funder/UpdateApplicationStatus/Application_ID
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateApplicationStatus(Application model)
        {
            try
            {
                if (model.Application_Status.Equals("Select"))
                {
                    ViewBag.WrongChoice = "Please select the appropriate choice";
                    return View(model);
                }
                else
                {
                    updateApplicationStatus(model.Application_ID, model.Application_Status);

                    //Removed the fund request and approved amount on the CreateBursarFund
                    if (model.Application_Status.Equals("Approved"))
                    {
                        try
                        {
                            CreateBursarFund(model.Application_ID, "Funded", 0);
                        }
                        catch
                        {
                            ViewBag.WrongChoice = "This application has been approved and already made a bursar, please check the bursars table";
                            return View(model);
                        }
                    }
                }
                return RedirectToAction("ViewApplication", new { id = model.Application_ID });
            }
            catch
            {
                ViewBag.WrongChoice = null;
                return View(model);
            }
        }
        // GET: Funder/UploadBursaryAgreement/Application_ID
        public ActionResult UploadBursaryAgreement(string id)
        {
            var item = GetApplication(id);
            Application application = new Application(item.Application_ID, item.Student_Identity_Number, item.Bursary_Code, item.Funding_Year, item.Application_Status, item.Upload_Agreement, item.Upload_Signed_Agreement);
            ViewBag.UploadStatus = null;
            return View(application);
        }

        // POST: Funder/UploadBursaryAgreement/Application_ID
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UploadBursaryAgreement(Application model)
        {
            try
            {
                if (model.Bursary_Agreement != null)
                {
                    HttpPostedFileBase Agreement = model.Bursary_Agreement;
                    String FileExt = Path.GetExtension(Agreement.FileName).ToUpper();
                    if (FileExt.Equals(".PDF"))
                    {
                        //Convert HttpPostedFileBase to Byte[]
                        Stream str = Agreement.InputStream;
                        BinaryReader Br = new BinaryReader(str);
                        Byte[] Agreement_Doc_FileDet = Br.ReadBytes((Int32)str.Length);

                        //Store the file content in Byte[] format on the model
                        model.Upload_Agreement = Agreement_Doc_FileDet;
                        updateApplicationDocs(model.Application_ID, model.Upload_Agreement);

                        //updateApplicationDocs must also update the status to Agreement Sent
                        updateApplicationStatus(model.Application_ID, "Agreement Sent");
                        return RedirectToAction("ViewApplication", new { id = model.Application_ID });
                    }
                }
                ViewBag.UploadStatus = "Please try selecting the file again";
                return View(model);
            }
            catch
            {
                ViewBag.UploadStatus = "Upload failed, please try again or contact your organization Finance Tracking System Admin";
                return View(model);
            }
        }

        [HttpGet]
        public FileResult DownloadSignedAgreement(string id)    //No need for a view
        {
            var item = GetApplication(id);

            if (item.Upload_Signed_Agreement == null)
            {
                return null;
            }
            return File(item.Upload_Signed_Agreement, "application/pdf", item.Application_ID + ".pdf");
        }
        #endregion

        #region View all the bursaries by a specific funder and make update if needed
        // GET: Funder/ViewBursaries
        [HttpGet]
        public ActionResult ViewBursaries()
        {
            Funder fund = new Funder();
            fund.Funder_Name = Session["Organization_Name"].ToString();
            var list = GetBursaries(fund.Funder_Name);

            //Binding the list of bursaries with the new funder, prevents repeation of the add method
            foreach (var bursary in list)
            {
                Bursary model = new Bursary(bursary.Bursary_Code, bursary.Bursary_Name, bursary.Start_Date, bursary.Funder_Name, bursary.End_Date, bursary.Bursary_Amount, bursary.Number_Available, bursary.Description, bursary.Funding_Year);
                fund.Bursaries.Add(model);
            }
            return View(fund);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ViewBursaries(Funder model)
        {
            Funder fund = new Funder();
            fund.Funder_Name = model.Funder_Name;
            var list = GetSearchBursaries(fund.Funder_Name, model.Bursary.Funding_Year);

            foreach (var bursary in list)
            {
                Bursary result = new Bursary(bursary.Bursary_Code, bursary.Bursary_Name, bursary.Start_Date, bursary.Funder_Name, bursary.End_Date, bursary.Bursary_Amount, bursary.Number_Available, bursary.Description, bursary.Funding_Year);
                fund.Bursaries.Add(result);
            }
            return View(fund);
        }

        // GET: Funder/ViewBursary/Bursary_Code
        [HttpGet]
        public ActionResult ViewBursary(string id)
        {
            var bursary = GetBursaryCode(id);
            Bursary result = new Bursary(bursary.Bursary_Code, bursary.Bursary_Name, bursary.Start_Date, bursary.Funder_Name, bursary.End_Date, bursary.Bursary_Amount, bursary.Number_Available, bursary.Description, bursary.Funding_Year);
            return View(result);
        }

        // GET: Funder/AddBursary
        public ActionResult AddBursary()
        {
            Bursary bursary = new Bursary();
            bursary.Funder_Name = Session["Organization_Name"].ToString();
            return View(bursary);
        }

        // POST: Funder/AddBursary
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddBursary(Bursary bursary)
        {
            try
            {
                CreateBursary(bursary.Bursary_Name + bursary.Funding_Year, bursary.Bursary_Name, bursary.Start_Date, bursary.Funder_Name, bursary.End_Date, bursary.Bursary_Amount, bursary.Number_Available, bursary.Description, bursary.Funding_Year);
                return RedirectToAction("ViewBursaries");
            }
            catch
            {
                ViewBag.BursaryCodeExist = "Bursary for " + bursary.Funding_Year + " already exist, enter a different year or change bursary name";
                return View(bursary);
            }
        }
        [HttpGet]
        // GET: Funder/ViewBursary/Bursary_Code
        public ActionResult MaintainBursary(string id)
        {
            var bursary = GetBursaryCode(id);
            Bursary result = new Bursary(bursary.Bursary_Code, bursary.Bursary_Name, bursary.Start_Date, bursary.Funder_Name, bursary.End_Date, bursary.Bursary_Amount, bursary.Number_Available, bursary.Description, bursary.Funding_Year);
            return View(result);
        }

        // POST: Funder/ViewBursary/Bursary_Code
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MaintainBursary(Bursary bursary)
        {
            try
            {
                UpdateBursary(bursary.Bursary_Code, bursary.Bursary_Name, bursary.Start_Date, bursary.Funder_Name, bursary.End_Date, bursary.Bursary_Amount, bursary.Number_Available, bursary.Description, bursary.Funding_Year);
                return RedirectToAction("ViewBursaries");
            }
            catch
            {
                ViewBag.UpdateFailed = "The update failed, please contact us for assistance";
                return View(bursary);
            }
        }
        // GET: Funder/ViewBursary/5
        public ActionResult DeleteBursary(string id)
        {
            var bursary = GetBursaryCode(id);
            Bursary result = new Bursary(bursary.Bursary_Code, bursary.Bursary_Name, bursary.Start_Date, bursary.Funder_Name, bursary.End_Date, bursary.Bursary_Amount, bursary.Number_Available, bursary.Description, bursary.Funding_Year);
            return View(result);
        }

        // POST: Funder/ViewBursary/5
        [HttpPost]
        public ActionResult DeleteBursary(Bursary bursary)
        {
            try
            {
                deleteBursary(bursary.Bursary_Code);

                return RedirectToAction("ViewBursaries");
            }
            catch
            {
                ViewBag.DeleteFailed = "The delete failed, please contact us for assistance";
                return View(bursary);
            }
        }
        #endregion

        #region View all students funded by the funder and updated if needed
        // GET: Funder/ViewBursaries
        public ActionResult ViewBursars(string id)
        {
            BursarsViewModel model = new BursarsViewModel();
            var list = BursarFundViews(id);
            foreach (var item in list)
            {
                BursarFundView bursar = new BursarFundView(item.Student_FName, item.Student_LName, item.Student_Identity_Number, item.Gender, item.Student_Cellphone_Number, item.Student_Email, item.Application_ID, item.Update_Fund_Request, item.Funding_Status, item.Approved_Funds);
                model.Bursars.Add(bursar);
            }
            return View(model);
        }
        // GET: Funder/ViewBursaries
        public ActionResult ViewAllBursars()       //Using funder name
        {
            BursarsViewModel model = new BursarsViewModel();
            var list = GetAllBursarsList(Session["Organization_Name"].ToString());
            foreach (var item in list)
            {
                BursarFundView bursar = new BursarFundView(item.Student_FName, item.Student_LName, item.Student_Identity_Number, item.Gender, item.Student_Cellphone_Number, item.Student_Email, item.Application_ID, item.Update_Fund_Request, item.Funding_Status, item.Approved_Funds);
                model.Bursars.Add(bursar);
            }
            return View(model);
        }
        [HttpGet]
        // GET: Funder/ViewBursary/5
        public ActionResult ViewBursar(string id)
        {
            var list = GetOneBursarsList(id);
            foreach (var item in list)
            {
                if ((item.Application_ID).Equals(id.ToString()))
                {
                    BursarFundView bursar = new BursarFundView(item.Student_FName, item.Student_LName, item.Student_Identity_Number, item.Gender, item.Student_Cellphone_Number, item.Student_Email, item.Application_ID, item.Update_Fund_Request, item.Funding_Status, item.Approved_Funds);
                    return View(bursar);
                }
            }
            return View();
        }
        //// GET: Funder/UpdateFundingStatus/5
        //public ActionResult UpdateFundingStatus(string id)
        //{
        //    var item = GetBursar(id);
        //    Bursar_Fund model = new Bursar_Fund(item.Application_ID,item.Update_Fund_Request,item.Funding_Status,item.Approved_Funds);

        //    return View(model);
        //}

        //// POST: Funder/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult UpdateFundingStatus(Bursar_Fund model)
        //{
        //    //try
        //    {
        //        updateFundStatus(model.Application_ID, model.Funding_Status);

        //        return RedirectToAction("ViewBursar", new { id = model.Application_ID });
        //    }
        //    //catch
        //    //{
        //    //    return View();
        //    //}
        //}
        // GET: Funder/Delete/5
        public ActionResult UpdateFundRequest(string id)
        {
            var item = GetBursar(id);
            Bursar_Fund model = new Bursar_Fund(item.Application_ID, item.Update_Fund_Request, item.Funding_Status, item.Approved_Funds);

            return View(model);
        }

        // POST: Funder/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateFundRequest(Bursar_Fund model)
        {
            try
            {
                updateFundsRequested(model.Application_ID, model.Update_Fund_Request);
                return RedirectToAction("ViewBursar", new { id = model.Application_ID });
            }
            catch
            {
                ViewBag.UpdateFailed = "The update failed, please contact us for assistance";
                return View(model);
            }
        }
        // GET: Funder/Delete/5
        public ActionResult MaintainFunds(string id)
        {
            var item = GetBursar(id);
            Bursar_Fund model = new Bursar_Fund(item.Application_ID, item.Update_Fund_Request, item.Funding_Status, item.Approved_Funds);

            return View(model);
        }

        // POST: Funder/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MaintainFunds(Bursar_Fund model)
        {
            try
            {
                updateFundsApproved(model.Application_ID, model.Approved_Funds);
                return RedirectToAction("ViewBursar", new { id = model.Application_ID });
            }
            catch
            {
                ViewBag.UpdateFailed = "The update failed, please contact us for assistance";
                return View(model);
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
        public ActionResult VerifyAdmin(Funder_Employee model)
        {
            Funder_Employee employee = (Funder_Employee)Session["FunderEmployee"];
            if (employee.Admin_Code == null)
            {
                ViewBag.Admin = "Admin code not found, please contact your organization admin";
                return View(model);
            }
            if ((employee.Admin_Code).Equals(model.Admin_Code))
                return RedirectToAction("FunderDetails");
            else
            {
                ViewBag.Admin = "Admin code is invalid, please check the code and try again";
                return View(model);
            }
        }
        public ActionResult ViewEmployees()
        {
            Funder funder = new Funder();
            var list = GetFundersEmployees(Session["Organization_Name"].ToString());
            foreach(var item in list)
            {
                Funder_Employee result = new Funder_Employee(item.Emp_FName, item.Emp_LName, item.Emp_Telephone_Number, item.Emp_Email, item.Organization_Name, item.Password, item.Admin_Code);
                funder.Funder_Employees.Add(result);
            }
            return View(funder);
        }
        public ActionResult EmployeeDetails(string id)
        {
            var item = GetFunderEmp(id);
            Funder_Employee employee = new Funder_Employee(item.Emp_FName, item.Emp_LName, item.Emp_Telephone_Number, item.Emp_Email, item.Organization_Name, item.Password, item.Admin_Code);
            return View(employee);
        }
        public ActionResult MaintainEmployee(string id)
        {
            var item = GetFunderEmp(id);
            Funder_Employee funder = new Funder_Employee(item.Emp_FName, item.Emp_LName, item.Emp_Telephone_Number, item.Emp_Email, item.Organization_Name, item.Password, item.Admin_Code);
            return View(funder);
        }

        // POST: Funder/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MaintainEmployee(Funder_Employee employee)
        {
            int result = UpdateFunderEmp(employee.Emp_Email);       //Not working
            if (result == 0)
            {
                return View(employee);
            }
            return RedirectToAction("EmployeeDetails", new {id = employee.Emp_Email});
        }
        public ActionResult DeleteEmployee(string id)
        {
            var item = GetFunderEmp(id);
            Funder_Employee funder = new Funder_Employee(item.Emp_FName, item.Emp_LName, item.Emp_Telephone_Number, item.Emp_Email, item.Organization_Name, item.Password, item.Admin_Code);
            return View(funder);
        }

        // POST: Funder/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteEmployee(Funder_Employee employee)
        {
            int result = DeleteFunderEmp(employee.Emp_Email);
            if(result == 0)
            {
                return View(employee);
            }
            return RedirectToAction("ViewEmployees");
        }
        public ActionResult AddEmployee()
        {
            Funder_Employee model = new Funder_Employee();
            model.Organization_Name = Session["Organization_Name"].ToString();
            return View(model);
        }

        // POST: Funder/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddEmployee(Funder_Employee employee)
        {
            int result = AddFunderEmp(employee.Emp_FName, employee.Emp_LName, employee.Emp_Telephone_Number, employee.Emp_Email, employee.Organization_Name, employee.Password);
            if(result == 0)
            {
                ViewBag.NotAdded = "Employee profile was not succefully created";
                return View(employee);
            }
            return RedirectToAction("FunderDetails");
        }
        #endregion
    }
}
