using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using eVisa.Models;

namespace eVisa.Controllers
{
    public class CheckStatusController : Controller
    {
        private eVisaContext db = new eVisaContext();
        //
        // GET: /CheckStatus/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Check(ContactInformation con)
        {
            if(ModelState.IsValid){
            var check = db.ContactInformation.Where(c => c.ReferenceNo == con.ReferenceNo && c.PrimaryEmail == con.PrimaryEmail).Count();
                if(check == 1){
                    return Json(new { success = true, message = "Check Status." }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, message = "No ReferenceNo Or Primary Email." }, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(new { success = false, message = "Invalid" }, JsonRequestBehavior.AllowGet);
        }

	}
}