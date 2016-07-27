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
    public class PaymentController : Controller
    {
        private eVisaContext db = new eVisaContext();
        private BaseFunction fun = new BaseFunction();
        static string language = "";
        
        // GET: /Payment/
        public ActionResult Index()
        {
            var user = Session["userId"];
            // Check if user guest have already apply form Info
            if (user == null)
            {
                user = Session.SessionID;
            }
            int uSessionCount = db.Application.Count(u => u.Userid == user);
            if (uSessionCount == 0)
            {
                return RedirectToAction("", "Application");
            }

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


            var entities = new ContactInformation();
            if (user == null)
            {
                user = Session.SessionID;
                entities = (from u in db.ContactInformation where u.UserId == user select u).OrderByDescending(p => p.id).First();
            }
            entities = (from u in db.ContactInformation where u.UserId == user select u).OrderByDescending(p => p.id).First();
            return View(entities);
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
            // Check if user guest have already apply form Info
            if (user == null)
            {
                user = Session.SessionID;
            }

            /**********************************************************
             ** check record if have ReferenceNo insert new contact ***
             *********** info copy from current contact ***************
             *********************************************************/
            var contactInfo = db.ContactInformation.Where(u => u.UserId == user && u.ReferenceNo != "").FirstOrDefault();
            if (contactInfo.ReferenceNo == null)
            {
                contactInfo.ReferenceNo = app.ReferenceNo;
                contactInfo.PaymentStatus = "Failed";
                db.Entry(contactInfo).State = EntityState.Modified;
                db.SaveChanges();
            }
            else
            {
                contact.SurName = contactInfo.SurName;
                contact.GivenName = contactInfo.GivenName;
                contact.ReferenceNo = app.ReferenceNo;
                contact.Country = contactInfo.Country;
                contact.UpdatedDate = DateTime.Now;
                contact.HeardFrom = contactInfo.HeardFrom;
                contact.PhoneNo = contactInfo.PhoneNo;
                contact.PrimaryEmail = contactInfo.PrimaryEmail;
                contact.Status = 1;
                contact.SecondaryEmail = contactInfo.SecondaryEmail;
                contact.UserId = user.ToString();
                contact.CreatedDate = DateTime.Now;
                contact.PaymentStatus = "Failed";
                contact.PhoneNo = contactInfo.PhoneNo;
                contact.Sex = contactInfo.Sex;
                contact.DOB = contactInfo.DOB;
                contact.Nationality = contactInfo.Nationality;
                contact.Photo = contactInfo.Photo;
                contact.PassportNo = contactInfo.PassportNo;
                contact.PassportExpiryDate = contactInfo.PassportExpiryDate;
                contact.PassportIssueDate = contactInfo.PassportIssueDate;
                contact.CountryIssue = contactInfo.CountryIssue;
                contact.ContactName = contactInfo.ContactName;
                contact.ResidentialAddress = contactInfo.ResidentialAddress;

                db.ContactInformation.Add(contact);
                db.SaveChanges();
            }

            return Json(new { success = true, message = "Save Success." }, JsonRequestBehavior.AllowGet);
        }


	}
}