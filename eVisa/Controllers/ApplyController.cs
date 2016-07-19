using eVisa.Function;
using eVisa.Models;
using eVisa.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eVisa.Controllers
{
    public class ApplyController : Controller
    {
        private eVisaContext db = new eVisaContext();
        private BaseFunction fun = new BaseFunction();
        static string language = "";
        //
        // GET: /Apply/GetContact
        public ActionResult GetContact()
        {
            var userId = Session.SessionID;
            var query = from a in db.ContactInformation
                        where a.UserId == userId
                        select new
                        {
                            a.SurName,
                            a.GivenName,
                            a.UserId,
                            a.Country,
                            a.HeardFrom,
                            a.PhoneNo,
                            a.PrimaryEmail,
                            a.SecondaryEmail,
                            a.id
                        };
            return Json(new { success = true, Data = query }, JsonRequestBehavior.AllowGet);
        }
        // POST: /Apply/SaveContact
        [HttpPost]
        public ActionResult SaveContact(ContactInformation data)
        {
            try
            {
                 if (ModelState.IsValid){
                    ContactInformation c = new ContactInformation();
                    
                    if (data.id > 0)
                    {
                        c = db.ContactInformation.Find(data.id);
                        c.SurName = data.SurName;
                        c.GivenName = data.GivenName;
                        c.Country = data.Country;
                        c.UpdatedDate = DateTime.Now;
                        c.HeardFrom = data.HeardFrom;
                        c.PhoneNo = data.PhoneNo;
                        c.PrimaryEmail = data.PrimaryEmail;
                        c.SecondaryEmail = data.SecondaryEmail;

                        db.Entry(c).State = EntityState.Modified;
                        db.SaveChanges();
                        return Json(new { success = true, message = "Update Complete." }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        c.SurName = data.SurName;
                        c.GivenName = data.GivenName;
                        //c.ReferenceNo = fun.GenerateReferenceNo();
                        c.Country = data.Country;
                        c.UpdatedDate = DateTime.Now;
                        c.HeardFrom = data.HeardFrom;
                        c.PhoneNo = data.PhoneNo;
                        c.PrimaryEmail = data.PrimaryEmail;
                        c.Status = 1;
                        c.SecondaryEmail = data.SecondaryEmail;
                        c.UserId = Session.SessionID;
                        c.CreatedDate = DateTime.Now;

                        db.ContactInformation.Add(c);
                        db.SaveChanges();
                        return Json(new { success = true, message = "Save Complete." }, JsonRequestBehavior.AllowGet);
                    }

                 }
                 return Json(new { success = false, message = "Error" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(
                    new
                    {
                        success = false,
                        message = ex.Message
                    },
                    JsonRequestBehavior.AllowGet
                );
            }
        }

        public ActionResult Index()
        {
            // Redirect to Review if user Login
            if (Session["userId"] != null)
            {
                return RedirectToAction("", "Application");
            }
            // Check if user guest have already apply form Info
            var uSession = Session.SessionID;
            int uSessionCount = db.Application.Count(u => u.Userid == uSession);
            if (uSessionCount > 0)
            {
                return RedirectToAction("", "Application");
            }
            // End check if user guest has to Apply


            if (Session["language"] == null)
            {
                language = "en";
            }
            else
            {
                language = Session["language"].ToString();
            }
            ViewBag.Message = "Your application description page.";
            ViewBag.MenuList = fun.getItemMenu(db, db.Menus.Where(e => e.Language_Code == language.Trim() && e.IsActive == 1 && e.TypeMenu == 1)
                  .Select(e => new MenuView()
                  {
                      Name = e.Name,
                      SubMenu = e.SubMenu,
                      Link_Code = e.Link_Code
                  }));
            ViewBag.LanguageList = db.Languages.Where(e => e.IsActive == 1 && e.Code != language)
                    .Select(e => new LanguageView()
                    {
                        Name = e.Name,
                        Url = e.Url,
                        Id = e.Id,
                        Code = e.Code
                    }).ToList();
            return View();
        }

	}
}