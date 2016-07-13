using eVisa.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eVisa.Controllers
{
    public class PaymentController : Controller
    {
        private eVisaContext db = new eVisaContext();
        //
        // GET: /Payment/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SaveAppPayment(Application app)
        {
            Application application = new Application();
            //application = db.Application.Find(app.ReferenceNo);
            var appRef = db.Application.Where(a => a.ReferenceNo == app.ReferenceNo).ToList();
            foreach (var updateRef in appRef)
            {
                updateRef.Status = 1;
                updateRef.UpdatedDate = DateTime.Now;
                db.Entry(updateRef).State = EntityState.Modified;
                db.SaveChanges();
            }
            //application.UpdatedDate = DateTime.Now;
            //application.Status = 1;
            
            // update contact information
            ContactInformation contact = new ContactInformation();
            var user = Session["userId"];
            var contactInfo = db.ContactInformation.Where(u => u.UserId == user).FirstOrDefault();
            contactInfo.ReferenceNo = app.ReferenceNo;
            db.Entry(contactInfo).State = EntityState.Modified;
            db.SaveChanges();

            return Json(new { success = true, message = "Save Success." }, JsonRequestBehavior.AllowGet);
        }

	}
}