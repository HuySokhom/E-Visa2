using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using eVisa.Models;
using eVisa.Function;
using eVisa.ViewModel;

namespace eVisa.Controllers
{
    public class CheckStatusController : Controller
    {
        private eVisaContext db = new eVisaContext();
        private BaseFunction fun = new BaseFunction();
        static string language = "";
        //
        // GET: /CheckStatus/
        public ActionResult Index()
        {
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

        public ActionResult Status()
        {
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

        // Function Check Application Status 
        public ActionResult Check(ContactInformation con)
        {
            if(ModelState.IsValid){
            var check = db.ContactInformation.Where(c => c.ReferenceNo == con.ReferenceNo && c.PrimaryEmail == con.PrimaryEmail).Count();
                if(check == 1){
                    var query = from c in db.ContactInformation
                                where (c.ReferenceNo == con.ReferenceNo && c.PrimaryEmail == con.PrimaryEmail)
                                select new
                                {
                                    c.PrimaryEmail,
                                    c.CreatedDate,
                                    c.ReferenceNo,
                                    c.PaymentStatus,
                                    ApplicationDetail = from a in db.Application
                                        where (c.ReferenceNo == a.ReferenceNo && a.Status == 1)
                                        select new
                                        {
                                            a.SurName,
                                            a.GivenName,
                                            a.PassportNo,
                                            a.EntryDate,
                                            a.PointOfEntry,
                                            a.PaymentStatus
                                        }
                                };
                    //var data = db.Application.Where(a => a.ReferenceNo == con.ReferenceNo).ToList();
                    return Json(new { success = true, message = "Check Status.", data = query }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, message = "No ReferenceNo Or Primary Email." }, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(new { success = false, message = "Invalid" }, JsonRequestBehavior.AllowGet);
        }


        // functionality for search reference number 
        public ActionResult SearchRefernceNo(ContactInformation con)
        {
            if (ModelState.IsValid)
            {
                var check = db.ContactInformation.Where(c => c.PassportNo == con.PassportNo && c.PrimaryEmail == con.PrimaryEmail).Count();
                if (check > 0)
                {
                    var query = from c in db.ContactInformation
                                where (c.PassportNo == con.PassportNo && c.PrimaryEmail == con.PrimaryEmail)
                                select new
                                {
                                    c.PrimaryEmail,
                                    c.CreatedDate,
                                    c.ReferenceNo,
                                    c.PaymentStatus,
                                    c.PassportNo,
                                    ApplicationDetail = from a in db.Application
                                        where (c.ReferenceNo == a.ReferenceNo && a.Status == 1)
                                        select new
                                        {
                                            a.SurName,
                                            a.GivenName,
                                            a.PassportNo,
                                            a.EntryDate,
                                            a.PointOfEntry,
                                            a.PaymentStatus
                                        }
                                };
                    //var data = db.Application.Where(a => a.ReferenceNo == con.ReferenceNo).ToList();
                    return Json(new { success = true, message = "Check Status.", data = query }, JsonRequestBehavior.AllowGet);
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