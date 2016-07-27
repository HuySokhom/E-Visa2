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
    public class ApplicationController : Controller
    {
        private eVisaContext db = new eVisaContext();
        private BaseFunction fun = new BaseFunction();

        static string language = "";


        public ActionResult Index()
        {
            //*********** Redirect to review if user login ********************//
            if (Session["userId"] != null)
            {
                return RedirectToAction("Review");
            }
            // *********End redirect user to review **********//

            //***** Check If User Guest Already Apply Redirect to Review  ********//
            var uSession = Session.SessionID;
            int uSessionCount = db.Application.Count(u => u.Userid == uSession && u.Status == 0);
            if (uSessionCount > 0)
            {
                return RedirectToAction("Review");
            }
            //******** End check if user guest has to Apply **************//

            // Check if user guest Not yet Apply Form Redirect to Apply //
            int countContact = db.ContactInformation.Count(u => u.UserId == uSession);
            if (countContact == 0)
            {
                return RedirectToAction("", "Apply");
            }
            //*********** End Condition **************//

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

        public ActionResult SearchReferenceNumber()
        {
            if (Session["language"] == null)
            {
                language = "en";
            }
            else
            {
                language = Session["language"].ToString();
            }
            ViewBag.Message = "Search Reference Number.";
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

        public ActionResult Review()
        {
            var user = Session["userId"];
            // Check if user guest have already apply form Info
            if (user == null)
            {
                user = Session.SessionID;
            }
            int uSessionCount = db.Application.Count(u => u.Userid == user && u.Status == 0);
            if (uSessionCount == 0)
            {
                var u = Session["userId"];
                if (u == null)
                {
                    return RedirectToAction("");
                }
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
            return View();
        }

        

        //
        // GET: /Application/Get
        public ActionResult Get()
        {
            var userId = Session["userId"];
            if (userId == null)
            {
                userId = Session.SessionID;
            }
            var query = from a in db.Application
                where (a.Userid == userId && a.Status == 0)
                select new
                {
                    a.ReferenceNo,
                    a.SurName,
                    a.GivenName,
                    a.Type,
                    a.ArrivalTime,
                    a.ArrivalVehicleNo,
                    a.DOB,
                    a.Nationality,
                    a.PassportCountry,
                    a.EntryDate,
                    a.PassportExpiryDate,
                    a.PassportIssueDate,
                    a.PassportNo,
                    a.Photo,
                    a.VisitPerson,
                    a.PointOfEntry,
                    a.PointOfExit,
                    a.ResidentialAddress,
                    a.CountryOfBirth,
                    a.Sex,
                    a.Userid,
                    a.Line,
                    a.VisitAddress,
                    a.TravelMode,
                    a.id,
                    Children = from c in db.Children
                        where ( c.ReferenceNo == a.ReferenceNo && c.Line1 == a.Line)
                        select new { 
                            c.ReferenceNo,
                            c.Line1,
                            c.Line2,
                            c.ChildDob,
                            c.id,
                            c.ChildGivenName,
                            c.ChildPhoto,
                            c.ChildSex,
                            c.ChildSurName
                        }
                };
            return Json(new { success = true, Data = query }, JsonRequestBehavior.AllowGet);
            
        }

        [HttpGet]
        // GET: /Application/Get
        public ActionResult History()
        {
            var userId = Session["userId"];
            var query = from c in db.ContactInformation
                        where (c.UserId == userId)
                        select new
                        {
                            c.ReferenceNo,
                            c.SurName,
                            c.GivenName,
                            c.DOB,
                            c.Nationality,
                            c.Country,
                            c.PrimaryEmail,
                            c.PaymentStatus
                        };
            return Json(new { success = true, data = query }, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public ActionResult Save(Application model)
        {
            var userId = Session["userId"];
            Application app = new Application();
            
            //********************************************************//
            //********************************************************//
            //******* CHECK IF ID > 0 LET UPDATE APPLICATION *********//
            //********************************************************//
            //********************************************************//
            if (model.id > 0)
            {
                if(userId == null){
                    userId = Session.SessionID;
                }
                app = db.Application.Find(model.id);
                app.SurName = model.SurName;
                app.GivenName = model.GivenName;
                app.CountryOfBirth = model.CountryOfBirth;
                app.Userid = userId.ToString();
                app.UpdatedDate = DateTime.Now;
                app.PassportNo = model.PassportNo;
                app.DOB = model.DOB;
                app.Type = model.Type;
                app.Sex = model.Sex;
                app.Nationality = model.Nationality;
                app.ResidentialAddress = model.ResidentialAddress;
                app.PointOfEntry = model.PointOfEntry;
                app.EntryDate = model.EntryDate;                
                app.TravelMode = model.TravelMode;
                app.Photo = model.Photo;
                app.Path = model.Photo;
                app.ArrivalTime = model.ArrivalTime;
                app.ArrivalVehicleNo = model.ArrivalVehicleNo;
                app.PassportCountry = model.PassportCountry;
                app.PassportExpiryDate = model.PassportExpiryDate;
                app.PassportIssueDate = model.PassportIssueDate;
                app.VisitAddress = model.VisitAddress;
                app.VisitPerson = model.VisitPerson;

                db.Entry(app).State = EntityState.Modified;
                db.SaveChanges();
                return Json(new { success = true, message = "update" }, JsonRequestBehavior.AllowGet);
            }
            //********************************************************//
            //********************************************************//
            //************** END OF UPDATE APPLICATION ***************//
            //********************************************************//
            //********************************************************//
            else
            {
                //********************************************************//
                //********************************************************//
                //*************** START SAVE APPLICATION *****************//
                //********************************************************//
                //********************************************************//
                app.SurName = model.SurName;
                app.GivenName = model.GivenName;
                app.CountryOfBirth = model.CountryOfBirth;
                app.CreateDate = DateTime.Now;
                app.UpdatedDate = DateTime.Now;
                app.Type = model.Type;
                app.PassportNo = model.PassportNo;
                app.DOB = model.DOB;
                app.Status = 0;
                app.StatusDate = DateTime.Now;
                app.Sex = model.Sex;
                app.Nationality = model.Nationality;
                app.ResidentialAddress = model.ResidentialAddress;
                app.PointOfEntry = model.PointOfEntry;
                app.EntryDate = model.EntryDate;
                app.EntryStatusDate = DateTime.Now;
                app.TravelMode = model.TravelMode;
                app.Photo = model.Photo;
                app.PaymentStatus = "fail";
                app.Path = model.Photo;
                app.ArrivalTime = model.ArrivalTime;
                app.ApplyDate = DateTime.Now;
                app.ArrivalVehicleNo = model.ArrivalVehicleNo;
                app.PassportCountry = model.PassportCountry;
                app.PassportExpiryDate = model.PassportExpiryDate;
                app.PassportIssueDate = model.PassportIssueDate;
                app.VisitAddress = model.VisitAddress;
                app.VisitPerson = model.VisitPerson;
                ////////////////////////////////////////////////////////////////////////////////
                /******************************************************************************/
                //********* Verify application limit 9 application with user login ***********//
                /******************************************************************************/
                ////////////////////////////////////////////////////////////////////////////////

                if (userId != null)
                {
                    var userSessionString = Convert.ToString(userId);
                    int count = db.Application.Count(a => a.Userid == userSessionString && a.Status == 0);
                    if (count < 9)
                    {
                        app.Userid = userId.ToString();
                        app.ReferenceNo = fun.GenerateReferenceNo(userSessionString);
                        app.Line = RandomNumber();
                        db.Application.Add(app);
                        db.SaveChanges();

                        // verify to avoid duplication ReferenceNo in application 
                        //var countDuplicateRef = db.Application.Count(cf => cf.Userid != userSessionString && cf.ReferenceNo == app.ReferenceNo);
                        //if(countDuplicateRef > 0){
                        // update ReferenceNo 
                        //var user = (from u in db.ContactInformation where u.UserId == userId select u).FirstOrDefault();                        
                        //user.ReferenceNo = app.ReferenceNo;
                        //db.Entry(user).State = EntityState.Modified;
                        //db.SaveChanges();
                        //}
                    }
                    // *************** return limitation app ******************** //
                    else
                    {
                        return Json(new { success = false }, JsonRequestBehavior.AllowGet);
                    }
                }
                ////////////////////////////////////////////////////////////////////////////////
                /******************************************************************************/
                //********* Verify application limit 9 application with guest User ***********//
                /******************************************************************************/
                ////////////////////////////////////////////////////////////////////////////////
                else
                {
                    userId = Session.SessionID;
                    // get session id from asp.net SessionID
                    string sessionId = Session.SessionID;
                    var count = db.Application.Count(a => a.Userid == sessionId && a.Status == 0);
                    if (count < 9)
                    {
                        app.Userid = userId.ToString();
                        app.Line = RandomNumber();
                        app.ReferenceNo = fun.GenerateReferenceNo(userId.ToString());

                        db.Application.Add(app);
                        db.SaveChanges();
                    }
                    // *************** return limitation app ******************** //
                    else
                    {
                        return Json(new { success = false }, JsonRequestBehavior.AllowGet);
                    }
                }
            }

            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
                
        }

        [HttpPost]
        public ActionResult Delete(int? id)
        {
            string uId = "";
            if( Session["userId"] != null ){
                uId = Session["userId"].ToString();
            }
            else
            {
                uId = Session.SessionID;
            }
            //**************************************************************//
            //********* Count app belong to user to delete to **************//
            //********* avoid hacker try to delete POST via url ************//
            //**************************************************************//
            int countAppUser = db.Application.Count(a => a.Userid == uId && a.id == id);
            if ( countAppUser > 0 )
            {
                Application app = db.Application.Find(id);
                // remove detail children 
                var childToRemove = (from c in db.Children where (c.Line1 == app.Line && c.ReferenceNo == app.ReferenceNo) select c).ToList();
                foreach (var i in childToRemove)
                {
                    Children child = db.Children.Find(i.id);
                    db.Children.Remove(child);
                    db.SaveChanges();
                }                
                db.Application.Remove(app);
                db.SaveChanges();
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return HttpNotFound();
            }
       }

        public static int RandomNumber()
        {
            Random generator = new Random();
            // creates a number between 1 and 6
            return generator.Next(10000, 99999);//Guid.NewGuid().GetHashCode();
        }

	}
}