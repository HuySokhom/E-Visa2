using eVisa.Function;
using eVisa.Models;
using eVisa.ViewModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System.Security.Claims;
using System.Web.Helpers;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Text;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Data.Entity;
using System.Net.Mime;
using System.Data.Entity.Core.Objects;
using System.Web.SessionState;

namespace eVisa.Controllers
{
    public class UserController : Controller
    {
        private eVisaContext db = new eVisaContext();
        private BaseFunction fun = new BaseFunction();

        static string language = "";
        //
        // GET: /User/
        public ActionResult Index()
        {
            var userId = Session["userId"];
            if ( userId == null)
            {
                return RedirectToAction("FQLogin");
            }
            if (Session["language"] == null)
            {
                language = "en";
            }
            else
            {
                language = Session["language"].ToString();
            }
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
        // GET: /User/Profile
        [HttpGet]
        public ActionResult Profile()
        {
            //check authenticated if user not login return false
            var userId = Session["userId"];
            if (userId == null)
            {
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
                //return RedirectToAction("FQLogin");
            }
            if (Session["language"] == null)
            {
                language = "en";
            }
            else
            {
                language = Session["language"].ToString();
            }
            ViewBag.Message = "User Account";
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

            var user = (from u in db.ContactInformation where (u.UserId == userId && u.Profile == "Primary") select u).FirstOrDefault();
            return new JsonResult() { Data = user, JsonRequestBehavior = JsonRequestBehavior.AllowGet };            
                
        }

        // PUT: /User/SaveProfile
        [HttpPost]
        public ActionResult Profile(ContactInformation model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var userId = Session["userId"];
                    var user = (from u in db.ContactInformation where (u.UserId == userId && u.Profile == "Primary") select u).FirstOrDefault();
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
                        user.Sex = model.Sex;
                        user.DOB = model.DOB;
                        user.Nationality = model.Nationality;
                        user.Photo = model.Photo;
                        user.PassportNo = model.PassportNo;
                        user.PassportExpiryDate = model.PassportExpiryDate;
                        user.PassportIssueDate = model.PassportIssueDate;
                        user.CountryIssue = model.CountryIssue;
                        user.ContactName = model.ContactName;
                        user.ResidentialAddress = model.ResidentialAddress;


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
        //
        // GET: /User/History
        public ActionResult History()
        {
            var userId = Session["userId"];
            if (userId == null)
            {
                return RedirectToAction("FQLogin");
            }
            if (Session["language"] == null)
            {
                language = "en";
            }
            else
            {
                language = Session["language"].ToString();
            }
            ViewBag.Message = "User Account";
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

        // PUT: /User/ChangePassword
        [HttpPost]
        public ActionResult ChangePassword(string oldPassword, string newPassword)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var userId = Session["userId"];
                    var hash = Crypto.Hash(oldPassword);
                    var user = (from u in db.ContactInformation where u.UserId == userId && u.Password == hash && u.Profile == "Primary" select u).FirstOrDefault();
                    if (user != null)
                    {
                        user.Password = Crypto.Hash(newPassword);
                        db.Entry(user).State = EntityState.Modified;
                        db.SaveChanges();
                        return Json(new { success = true }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { success = false }, JsonRequestBehavior.AllowGet);
                    }                    
                }
            }
            catch (Exception e)
            {
                var a = e.Message;
            }

            return View();
        }

        // POST: /User/SignUp 
        [HttpPost]        
        public ActionResult SignUp(ContactInformation data)
        {
            
            
            var userId = db.ContactInformation.FirstOrDefault( i => i.UserId.ToLower() == data.UserId.ToLower() );

            if (userId == null)
            {
                try
                {
                    ContactInformation c = new ContactInformation();
                    //c.SurName = data.SurName;
                    //c.GivenName = data.GivenName;
                    //c.ReferenceNo = fun.GenerateReferenceNo();
                    //c.Country = data.Country;
                    c.CreatedDate = DateTime.Now;
                    c.UpdatedDate = DateTime.Now;
                    //c.HeardFrom = data.HeardFrom;
                    //c.PhoneNo = data.PhoneNo;
                    //c.PrimaryEmail = data.PrimaryEmail;
                    c.Status = 1;
                    //c.SecondaryEmail = data.SecondaryEmail;
                    c.UserId = data.UserId;
                    c.Password = Crypto.Hash(data.Password);
                    c.Profile = "primary";
                    db.ContactInformation.Add(c);
                    db.SaveChanges();

                    // set session userId redirect to user profile
                    Session["userId"] = c.UserId;

                    //var ident = new ClaimsIdentity( new[] { 
                    //    // adding following 2 claim just for supporting default antiforgery provider
                    //    new Claim(ClaimTypes.NameIdentifier, c.UserId),
                    //    new Claim("http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/identityprovider", "ASP.NET Identity", "http://www.w3.org/2001/XMLSchema#string"),
                    //    new Claim(ClaimTypes.Name, c.UserId),

                    //    // optionally you could add roles if any
                    //    //new Claim(ClaimTypes.Role, "RoleName"),
                    //    //new Claim(ClaimTypes.Role, "AnotherRole"),
                    //},
                    //DefaultAuthenticationTypes.ApplicationCookie);
                    //HttpContext.GetOwinContext().Authentication.SignIn(new AuthenticationProperties { IsPersistent = false }, ident);
                    
                    /* start send email */

                    //MailMessage mail = new MailMessage();
                    //mail.Subject = "Your Subject";
                    ///* MailAddress("senderMailAddress"); */
                    //mail.From = new MailAddress("cinetemail@gmail.com");

                    ///* Add("ReceiverMailAddress")*/
                    //mail.To.Add("kom.huy@gmail.com");
                    //mail.Body = "Hello! your mail content goes here...";
                    //mail.IsBodyHtml = true;
                    //mail.BodyEncoding = Encoding.Default;


                    //SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                    //smtp.EnableSsl = false;
                    //smtp.UseDefaultCredentials = false;
                    ///* NetworkCredential("SenderMailAddress","SenderPassword") */
                    //NetworkCredential netCre = new NetworkCredential("cinetemail@gmail.com", "cinet123");
                    //smtp.Credentials = netCre;

                    //smtp.Send(mail);
                    return Json(new { success = true }, JsonRequestBehavior.AllowGet);

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
            else
            {
                return Json(
                    new
                    {
                        success = false,
                        message = "UserID already exists. Please enter a different UserID."
                    },
                    JsonRequestBehavior.AllowGet
                );
            }
            
        }

        // POST: LogOff
        [HttpPost]
        public ActionResult LogOff()
        {
            Session["userId"] = null;
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Success()
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

        [HttpPost]
        public ActionResult LoginAction(Users model)
        {
            var user = new UserManager();
            if (user.IsValid(model.user_id, model.password))
            {
                Session["userId"] = model.user_id;

                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            // invalid username or password
            return Json(new { success = false }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult FQLogin()
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

        public ActionResult FQSignup()
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

        /** 
         * Functionality for Image Upload with POST HTTP
         */
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult UploadImage()
        {
            string _imgname = string.Empty;
            if (System.Web.HttpContext.Current.Request.Files.AllKeys.Any())
            {
                var pic = System.Web.HttpContext.Current.Request.Files["MyImages"];
                if (pic.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(pic.FileName);
                    var _ext = Path.GetExtension(pic.FileName);

                    /* rename file */
                    _imgname = Guid.NewGuid().ToString();

                    var _comPath = Server.MapPath("/Uploads/Users/") + _imgname + _ext;
                    _imgname = _imgname + _ext;

                    ViewBag.Msg = _comPath;

                    /* check image extension if valid */
                    int i = checkValidExtension(_ext);
                    if ( i == 0 )
                    {
                        return Json(
                            new
                            {
                                success = false,
                                message = Convert.ToString("Invalid image type.")
                            }, 
                            JsonRequestBehavior.AllowGet);
                    }

                    /* validate if image size is bigger than 2MB */
                    decimal size = (pic.ContentLength / 1024) / 1024;
                    if( size > 2 ){
                        return Json(
                            new { 
                                success = false,
                                message = Convert.ToString("Invalid image size. image should be smaller than 2MB.")
                            }, 
                            JsonRequestBehavior.AllowGet
                        );
                    }

                    var path = _comPath;

                    // Saving Image in Original Mode
                    pic.SaveAs(path);

                    /*
                     * to resizing image uncommand
                     * 
                    MemoryStream ms = new MemoryStream();
                    WebImage img = new WebImage(_comPath);

                    if (img.Width > 200)
                    img.Resize(200, 200);
                    img.Save(_comPath);
                     
                     * 
                     */
                    // end resize 
                }
            }
            return Json(new { success = true, image = Convert.ToString(_imgname), message = "Upload Success." }, JsonRequestBehavior.AllowGet);
        }

        // functionality fro crop image upload 
        public System.Drawing.Image Base64ToImage(string base64String)   
        {
            var base64Content = base64String.Split(',')[1];
            //byte[] bImage = Convert.FromBase64String(base64Content);            
            //string convert = base64String.Substring(1, base64String.Length - 2).Replace(@"\/", "/");//.Replace("data:image/jpeg;base64,", String.Empty);
            byte[] imageBytes = Convert.FromBase64String(base64Content);
            MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);
            ms.Write(imageBytes, 0, imageBytes.Length);
            System.Drawing.Image image = System.Drawing.Image.FromStream(ms, true);
            return image;  
        }

        public ActionResult BaseToImage(string base64String)
        {
            var imgname = Guid.NewGuid().ToString() + ".jpg";
            Base64ToImage(base64String).Save(Server.MapPath("~/Uploads/Users/" + imgname));
            var image = "/Uploads/Users/" + imgname;
            return Json(new { success = true, image_name = image, message = "Upload Success." }, JsonRequestBehavior.AllowGet);

        }  


        public int checkValidExtension(string ext)
        {
            string[] AllowedExtensions = new string[] { ".gif", ".jpeg", ".jpg", ".png" };
            for (int i = 0; i < AllowedExtensions.Length; i++)
            {
                if (ext == AllowedExtensions[i])
                    return 1;
            }

            return 0;
        }

    }
}