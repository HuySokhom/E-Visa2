using eVisa.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eVisa.Controllers
{
    public class ContactInformationController : Controller
    {
        private eVisaContext db = new eVisaContext();
        //
        // GET: /ContactInformation/Get
        public ActionResult Get()
        {
            var userId = Session["userId"];
            if (userId == null)
            {
                userId = Session.SessionID;
            }
            var user = (
                from u in db.ContactInformation where (u.UserId == userId && u.Profile == "Primary")
                select new
                {
                    Id = u.id,
                    SurName = u.SurName,
                    GivenName = u.GivenName,
                    Country = u.Country,
                    PhoneNo = u.PhoneNo,
                    PrimaryEmail = u.PrimaryEmail,
                    SecondaryEmail = u.SecondaryEmail,
                    HeardFrom = u.HeardFrom
                }
            ).Single();

            return new JsonResult() { Data = user, JsonRequestBehavior = JsonRequestBehavior.AllowGet };  
        }

        // PUT: /ContactInformation/SaveContact
        [HttpPost]
        public ActionResult SaveContact(ContactInformation model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var userId = Session["userId"];
                    if (userId == null)
                    {
                        userId = Session.SessionID;
                    }
                    var user = (from u in db.ContactInformation where (u.UserId == userId && u.Profile == "Primary") select u).Single();
                    if (user != null)
                    {
                        user.SurName = model.SurName;
                        user.GivenName = model.GivenName;
                        user.PrimaryEmail = model.PrimaryEmail;
                        user.SecondaryEmail = model.SecondaryEmail;
                        user.Country = model.Country;
                        user.PhoneNo = model.PhoneNo;
                        user.UpdatedDate = DateTime.Now;
                        user.HeardFrom = model.HeardFrom;

                        db.Entry(user).State = EntityState.Modified;
                        db.SaveChanges();
                        return Json(new { success = true }, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            catch (Exception e)
            {

                var a = e.Message;
            }

            return View();
        }

	}
}