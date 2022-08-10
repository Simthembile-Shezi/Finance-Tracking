using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Finance_Tracking.Controllers
{
    public class FunderController : Controller
    {
        // GET: Funder
        public ActionResult Index()
        {
            return View();
        }

        // GET: Funder/Details/5
        public ActionResult FunderDetails(int id)
        {
            return View();
        }

        // GET: Funder/Create
        public ActionResult Register()
        {
            return View();
        }

        // POST: Funder/Create
        [HttpPost]
        public ActionResult Register(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Funder/Edit/5
        public ActionResult EditProfile(int id)
        {
            return View();
        }

        // POST: Funder/Edit/5
        [HttpPost]
        public ActionResult EditProfile(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Funder/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Funder/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
