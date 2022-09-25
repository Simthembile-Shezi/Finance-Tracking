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
            Funder_Employee employee = (Funder_Employee)Session["FunderEmployee"];
            var funder = GetFunder(employee.Organization_Name);
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
                return View(model);
            }
        }

        // GET: Funder/Create/Employee
        public ActionResult AdminFunderEmployee()
        {
            Funder funder = (Funder)Session["Funder"];
            Funder_Employee employee = new Funder_Employee();
            employee.Organization_Name = funder.Funder_Name;
            employee.Funder = funder;
            return View(employee);
        }

        // POST: Funder/Create/Employee
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AdminFunderEmployee(Funder_Employee employee)
        {
            Funder funder = (Funder)Session["Funder"];
            try
            {
                //Insert the Employee on the database
                //model state
                AddFunderEmp(employee.Emp_FName, employee.Emp_LName, employee.Emp_Telephone_Number, employee.Emp_Email,
                    employee.Organization_Name, employee.Password, employee.Admin_Code);

                return RedirectToAction("Login", "Home");
            }
            catch
            {
                return View(funder);
            }

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

                return View("FunderDetails", model);

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
            Funder fund = (Funder)Session["Funder"];
            Bursary bursary = new Bursary();
            bursary.Bursary_Code = id;

            foreach (var item in fund.Bursaries)
            {
                if ((item.Bursary_Code).Equals(bursary.Bursary_Code))
                {
                    var list = GetApplications(id.ToString());
                    foreach (var app in list)
                    {
                        Application application = new Application();
                        application.Application_ID = app.Application_ID;
                        application.Student_Identity_Number = app.Student_Identity_Number;
                        application.Bursary_Code = app.Bursary_Code;
                        application.Funding_Year = app.Funding_Year;
                        application.Application_Status = app.Application_Status;
                        application.Upload_Agreement = app.Upload_Agreement;
                        application.Upload_Signed_Agreement = app.Upload_Signed_Agreement;

                        bursary.Applications.Add(application);
                    }

                    Session["Applications"] = bursary;

                    return View(bursary);
                }
            }
            return View();
        }

        // GET: Funder/ViewApplication/5
        public ActionResult ViewApplication(string id)
        {
            Bursary bursary = (Bursary)Session["Applications"];
            if (bursary != null)
            {
                foreach (var application in bursary.Applications)
                {
                    if (application.Application_ID.Equals(id))
                    {
                        Session["Application"] = application;
                        return View(application);
                    }
                }
            }
            return RedirectToAction("ViewApplications", new { id = bursary.Bursary_Code });
        }
        // GET: Funder/UpdateFundingStatus/5
        public ActionResult UpdateApplicationStatus(string id)
        {
            Application application = (Application)Session["Application"];
            if (application == null)
            {
                return HttpNotFound();
            }

            return View(application);
        }

        // POST: Funder/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateApplicationStatus(Application model)
        {
            //try
            {
                updateApplicationStatus(model.Application_ID, model.Application_Status);
                return RedirectToAction("ViewApplication", new { id = model.Application_ID });
                ////Binding
                //Bursary bursary = (Bursary)Session["Applications"];
                //if (bursary != null)
                //{
                //    foreach (var application in bursary.Applications)
                //    {
                //        if (application.Application_ID.Equals(model.Application_ID))
                //        {
                //            application.Application_Status = model.Application_Status;
                //            return RedirectToAction("ViewApplication", new { id = model.Application_ID });
                //        }
                //    }
                //}
                //return RedirectToAction("UpdateApplicationStatus", new { id = model.Application_ID });
            }
            //catch
            //{
            //    return View();
            //}
        }
        // GET: Funder/Delete/5
        public ActionResult UploadBursaryAgreement(string id)
        {
            Application application = (Application)Session["Application"];
            if (application == null)
            {
                return HttpNotFound();
            }

            return View(application);
        }

        // POST: Funder/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UploadBursaryAgreement(Application application)
        {
            //try
            {
                HttpPostedFileBase Agreement = application.Bursary_Agreement;


                if (Agreement != null)
                {
                    //Convert HttpPostedFileBase to Byte[]
                    Stream str = Agreement.InputStream;
                    BinaryReader Br = new BinaryReader(str);
                    Byte[] Agreement_Doc_FileDet = Br.ReadBytes((Int32)str.Length);

                    //Store the file content in Byte[] format on the model
                    application.Upload_Agreement = Agreement_Doc_FileDet;
                    updateApplicationDocs(application.Application_ID, application.Upload_Agreement);
                }

                return RedirectToAction("ViewApplication", new { id = application.Application_ID });
            }
            //catch
            //{
            //    return View();
            //}
        }
        // GET: Funder/Delete/5
        public ActionResult ApproveSignedAgreement(string id)
        {
            Application application = (Application)Session["Application"];
            if (application == null)
            {
                return HttpNotFound();
            }

            return View(application);
        }

        // POST: Funder/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ApproveSignedAgreement(Application model)
        {
            //try
            {
                updateApplicationStatus(model.Application_ID, model.Application_Status);

                //Binding
                Bursary bursary = (Bursary)Session["Applications"];
                if (bursary != null)
                {
                    foreach (var application in bursary.Applications)
                    {
                        if (application.Application_ID.Equals(model.Application_ID))
                        {
                            application.Application_Status = model.Application_Status;
                            return RedirectToAction("ViewApplication", new { id = model.Application_ID });
                        }
                    }
                }
                return RedirectToAction("UpdateApplicationStatus", new { id = model.Application_ID });
            }
            //catch
            //{
            //    return View();
            //}
        }
        [HttpGet]
        [ValidateAntiForgeryToken]
        public FileResult DownLoadFile(string id)
        {
            Application application = (Application)Session["Application"];

            return File(application.Upload_Signed_Agreement, "application/pdf", application.Application_ID);

        }
        #endregion

        #region View all the bursaries by a specific funder and make update if needed
        // GET: Funder/ViewBursaries
        [HttpGet]
        public ActionResult ViewBursaries()
        {

            Funder_Employee funderEmp = (Funder_Employee)Session["FunderEmployee"];
            Funder fund = new Funder();
            fund.Funder_Name = funderEmp.Organization_Name;
            var list = GetBursaries(fund.Funder_Name);

            //Binding the list of bursaries with the new funder, prevents repeation of the add method
            foreach (var bursary in list)
            {
                Bursary model = new Bursary(bursary.Bursary_Code, bursary.Bursary_Name, bursary.Start_Date, bursary.Funder_Name, bursary.End_Date, bursary.Bursary_Amount, bursary.Number_Available, bursary.Description, bursary.Funding_Year);
                fund.Bursaries.Add(model);
            }
            Session["Funder"] = fund;
            return View(fund);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ViewBursaries(Funder model)
        {
            Funder funder = (Funder)Session["Funder"];
            Funder fund = new Funder();
            fund.Funder_Name = funder.Funder_Name;

            foreach (var bursary in funder.Bursaries)
            {
                if(bursary.Funding_Year != null && bursary.Funding_Year.Equals(model.Bursary.Funding_Year))
                    fund.Bursaries.Add(bursary);
            }
            return View(fund);
        }

        // GET: Funder/ViewBursary/Bursary_Code
        [HttpGet]
        public ActionResult ViewBursary(string id)
        {
            Funder fund = (Funder)Session["Funder"];
            foreach (Bursary bursary in fund.Bursaries)
            {
                if ((bursary.Bursary_Code).Equals(id.ToString()))
                {
                    return View(bursary);
                }
            }
            return View();
        }

        // GET: Funder/AddBursary
        public ActionResult AddBursary()
        {
            Funder fund = (Funder)Session["Funder"];
            Bursary bursary = new Bursary();
            bursary.Funder = fund;
            bursary.Funder_Name = fund.Funder_Name;
            return View(bursary);
        }

        // POST: Funder/AddBursary
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddBursary(Bursary bursary)
        {
            try
            {
                int result = CreateBursary(bursary.Bursary_Name + bursary.Funding_Year, bursary.Bursary_Name, bursary.Start_Date, bursary.Funder_Name, bursary.End_Date,
                                bursary.Bursary_Amount, bursary.Number_Available, bursary.Description, bursary.Funding_Year);

                return RedirectToAction("ViewBursaries");
            }
            catch
            {
                return View(bursary);
            }
        }
        [HttpGet]
        // GET: Funder/ViewBursary/Bursary_Code
        public ActionResult MaintainBursary(string id)
        {
            Funder fund = (Funder)Session["Funder"];
            foreach (Bursary bursary in fund.Bursaries)
            {
                if ((bursary.Bursary_Code).Equals(id.ToString()))
                    return View(bursary);
            }
            return View();
        }

        // POST: Funder/ViewBursary/Bursary_Code
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MaintainBursary(Bursary bursary)
        {
            try
            {
                int result = UpdateBursary(bursary.Bursary_Code, bursary.Bursary_Name, bursary.Start_Date, bursary.Funder_Name, bursary.End_Date,
                                bursary.Bursary_Amount, bursary.Number_Available, bursary.Description, bursary.Funding_Year);
                return RedirectToAction("ViewBursaries");
            }
            catch
            {
                return View(bursary);
            }
        }
        // GET: Funder/ViewBursary/5
        public ActionResult DeleteBursary(string id)
        {
            Funder fund = (Funder)Session["Funder"];
            foreach (Bursary bursary in fund.Bursaries)
            {
                if ((bursary.Bursary_Code).Equals(id))
                {
                    return View(bursary);
                }
            }
            return View();
        }

        // POST: Funder/ViewBursary/5
        [HttpPost]
        public ActionResult DeleteBursary(Bursary bursary)
        {
            try
            {
                int result = deleteBursary(bursary.Bursary_Code);

                return RedirectToAction("ViewBursaries");
            }
            catch
            {
                return View(bursary);
            }
        }
        #endregion

        #region View all students funded by the funder and updated if needed
        // GET: Funder/ViewBursaries
        public ActionResult ViewBursars(string id)
        {
            BursarsViewModel model = new BursarsViewModel();
            var list = LoadBursars();

            //binding
            foreach (var item in list)
            {
                Bursar_Fund funds = new Bursar_Fund();
                funds.Application_ID = item.Application_ID;
                funds.Update_Fund_Request = item.Update_Fund_Request;
                funds.Funding_Status = item.Funding_Status;
                funds.Approved_Funds = item.Approved_Funds;

                model.Bursars.Add(funds);
            }
            Session["Bursars"] = model;
            return View(model);
        }

        // GET: Funder/ViewBursary/5
        public ActionResult ViewBursar(string id)
        {
            BursarsViewModel model = (BursarsViewModel)Session["Bursars"];
            foreach (var item in model.Bursars)
            {
                if ((item.Application_ID).Equals(id.ToString()))
                {
                    Session["Bursar"] = item;
                    return View(item);
                }
            }
            return View();
        }
        // GET: Funder/UpdateFundingStatus/5
        public ActionResult UpdateFundingStatus(string id)
        {
            Bursar_Fund model = (Bursar_Fund)Session["Bursar"];

            return View(model);
        }

        // POST: Funder/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateFundingStatus(Bursar_Fund model)
        {
            //try
            {
                int result = updateFundStatus(model.Application_ID, model.Funding_Status);

                return RedirectToAction("ViewBursar", new { id = model.Application_ID });
            }
            //catch
            //{
            //    return View();
            //}
        }
        // GET: Funder/Delete/5
        public ActionResult UpdateFundRequest(string id)
        {
            Bursar_Fund model = (Bursar_Fund)Session["Bursar"];

            return View(model);
        }

        // POST: Funder/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateFundRequest(Bursar_Fund model)
        {
            //try
            {
                int result = updateFundsRequested(model.Application_ID, model.Update_Fund_Request);
                return RedirectToAction("ViewBursar", new { id = model.Application_ID });
            }
            //catch
            //{
            //    return View();
            //}
        }
        // GET: Funder/Delete/5
        public ActionResult MaintainFunds(int id)
        {
            Bursar_Fund model = (Bursar_Fund)Session["Bursar"];

            return View(model);
        }

        // POST: Funder/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MaintainFunds(Bursar_Fund model)
        {
            //try
            {
                int result = updateFundsApproved(model.Application_ID, model.Approved_Funds);

                return RedirectToAction("ViewBursar", new { id = model.Application_ID });
            }
            //catch
            //{
            //    return View();
            //}
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
                ViewBag.Admin = "Admin code not found, please contact your system admin";
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

        public ActionResult AddEmployee()
        {
            Funder_Employee employee = (Funder_Employee)Session["FunderEmployee"];
            Funder_Employee model = new Funder_Employee();
            model.Organization_Name = employee.Organization_Name;
            return View(model);
        }

        // POST: Funder/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddEmployee(Funder_Employee employee)
        {
            AddFunderEmp(employee.Emp_FName, employee.Emp_LName, employee.Emp_Telephone_Number, employee.Emp_Email, employee.Organization_Name, employee.Password, employee.Admin_Code);

            return RedirectToAction("FunderDetails");
        }
        #endregion
    }
}
