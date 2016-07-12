using eVisa.Function;
using eVisa.Models;
using eVisa.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eVisa.Controllers
{
    public class InformationController : Controller
    {
        private eVisaContext db = new eVisaContext();
        private BaseFunction fun = new BaseFunction();
        private string language = "";
        //
        // GET: /Information/
        public ActionResult Credits()
        {
            if (Session["language"] == null)
            {
                language = "en";
            }
            else
            {
                language = Session["language"].ToString();
            }

            ViewBag.MenuList = fun.getItemMenu(db,db.Menus.Where(e => e.Language_Code == language && e.IsActive == 1 && e.TypeMenu == 1)
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

            var entity = db.CreditsThanks.Where(e => e.Language_Code == language && e.IsActive == 1)
                    .Select(e => new CreditsThanksView()
                    {
                        Id = e.Id,
                        Dates = e.Dates,
                        Language_Code = e.Language_Code,
                        Name = e.Name,
                        National = e.National,
                        Url = e.Url,
                        Content = e.Content

                    }).ToList();

            return View(entity);
        }

        public ActionResult Testimonial() {
            if (Session["language"] == null)
            {
                language = "en";
            }
            else
            {
                language = Session["language"].ToString();
            }

            ViewBag.MenuList = fun.getItemMenu(db,db.Menus.Where(e => e.Language_Code == language && e.IsActive == 1 && e.TypeMenu == 1)
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

            var entity = fun.getTestimonialContent(db,
                    db.Testimonials.Where(e => e.Language_Code == language && e.IsActive == 1)
                        .Select(e => new TestimonialView() { 
                            Id=e.Id,
                            Language_Code=e.Language_Code,
                            Name=e.Name,
                             Title=e.Title,
                             Url=e.Url,
                             Subject=e.Subject,
                             Tel=e.Tel,
                             Country=e.Country,
                             Dates=e.Dates,
                             Email=e.Email,
                             Content=e.Content
                        }));

            return View(entity);
        }

        public ActionResult NeedYourHelp() {
            if (Session["language"] == null)
            {
                language = "en";
            }
            else
            {
                language = Session["language"].ToString();
            }

            ViewBag.MenuList = fun.getItemMenu(db,db.Menus.Where(e => e.Language_Code == language && e.IsActive == 1 && e.TypeMenu == 1)
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

        public ActionResult TouchingStory() {
            if (Session["language"] == null)
            {
                language = "en";
            }
            else
            {
                language = Session["language"].ToString();
            }

            ViewBag.MenuList = fun.getItemMenu(db,db.Menus.Where(e => e.Language_Code == language && e.IsActive == 1 && e.TypeMenu == 1)
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

             var entity = fun.getTouchingStoryContent(db,
                        db.TouchingStorys.Where(e => e.Language_Code == language && e.IsActive == 1)
                            .Select(e => new TouchingStoryView() { 
                                Id=e.Id,
                                Email=e.Email,
                                Dates=e.Dates,
                                Country=e.Country,
                                Content=e.Content,
                                Name=e.Name,
                                Url=e.Url,
                                Title=e.Title,
                                Tel=e.Tel,
                                Language_Code=e.Language_Code,
                            }).ToList());

            return View(entity);
        }
        public ActionResult FAQ() {
            if (Session["language"] == null)
            {
                language = "en";
            }
            else
            {
                language = Session["language"].ToString();
            }

            ViewBag.MenuList = fun.getItemMenu(db,db.Menus.Where(e => e.Language_Code == language && e.IsActive == 1 && e.TypeMenu ==1)
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

            var entity = fun.getFAQContent(db,
                    db.FAQs.Where(e => e.Language_Code == language && e.IsActive == 1)
                        .Select(e => new FAQView() { 
                            Id=e.Id,
                            question = e.question,
                            answer = e.answer
                        }));

            return View(entity);
        }
        [HttpGet]
        public ActionResult TellUs() {
            if (Session["language"] == null)
            {
                language = "en";
            }
            else
            {
                language = Session["language"].ToString();
            }

            ViewBag.MenuList = fun.getItemMenu(db, db.Menus.Where(e => e.Language_Code == language && e.IsActive == 1 && e.TypeMenu == 1)
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

            
            
            //var entity = db.Pages.Where(e => e.Language_Code == language && e.IsActive == 1 && e.Link_Code == 2)
            //        .Select(e => new PageView()
            //        {
            //            Id = e.Id,
            //            Title = e.Title,
            //            Content = e.Content
            //        }).FirstOrDefault();
            

            return View();
        }

        [HttpPost]
        public ActionResult TellUs(FeedbackView viewModel)
        {
            ViewBag.MenuList = fun.getItemMenu(db, db.Menus.Where(e => e.Language_Code == language && e.IsActive == 1 && e.TypeMenu == 1)
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

            if (ModelState.IsValid)
            {

                var entity = new Feedback()
                {
                    Name = viewModel.Name,
                    Email = viewModel.Email,
                    Content = viewModel.Content,

                    IsActive = 1,
                    CreatedBy = User.Identity.Name,
                    CreatedDate = DateTime.Now,
                    UpdatedBy = User.Identity.Name,
                    UpdatedDate = DateTime.Now
                };

                db.Feedbacks.Add(entity);
                db.SaveChanges();


                ModelState.AddModelError("result", "Saved !");
                

                return View();
            }

            return View();
        }

        public ActionResult Public() {

            if (Session["language"] == null)
            {
                language = "en";
            }
            else
            {
                language = Session["language"].ToString();
            }

            ViewBag.MenuList = fun.getItemMenu(db,db.Menus.Where(e => e.Language_Code == language && e.IsActive == 1 && e.TypeMenu == 1)
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

            var entity = fun.getPageContent(db,db.Pages.Where(e => e.Language_Code == language && e.IsActive == 1 && e.Link_Code == 5)
                        .Select(e => new PageView()
                        {
                            Id = e.Id,
                            Title = e.Title,
                            Content = e.Content
                        }).FirstOrDefault(),5);

            return View(entity);
        }

        public ActionResult Guidelines() {

            if (Session["language"] == null)
            {
                language = "en";
            }
            else
            {
                language = Session["language"].ToString();
            }

            ViewBag.MenuList = fun.getItemMenu(db,db.Menus.Where(e => e.Language_Code == language && e.IsActive == 1 && e.TypeMenu == 1)
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

            var entity = db.Pages.Where(e => e.Language_Code == language && e.IsActive == 1 && e.Link_Code == 4)
                        .Select(e => new PageView() { 
                            Id=e.Id,
                            Title=e.Title,
                            Content=e.Content
                        }).FirstOrDefault();

            return View(entity);
        }

        public ActionResult Exemption()
        {

            if (Session["language"] == null)
            {
                language = "en";
            }
            else
            {
                language = Session["language"].ToString();
            }

            ViewBag.MenuList = fun.getItemMenu(db, db.Menus.Where(e => e.Language_Code == language && e.IsActive == 1 && e.TypeMenu == 1)
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

            var entity = fun.getPageContent(db, db.Pages.Where(e => e.Language_Code == language.Trim() && e.IsActive == 1 && e.Link_Code == 6)
                    .Select(e => new PageView()
                    {
                        Id = e.Id,
                        Title = e.Title,
                        Content = e.Content
                    }).FirstOrDefault(), 6);

            return View(entity);
        }

        public ActionResult HowToApply() {

            if (Session["language"] == null)
            {
                language = "en";
            }
            else
            {
                language = Session["language"].ToString();
            }

            ViewBag.MenuList = fun.getItemMenu(db, db.Menus.Where(e => e.Language_Code == language && e.IsActive == 1 && e.TypeMenu == 1)
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


        public ActionResult Map()
        {

            if (Session["language"] == null)
            {
                language = "en";
            }
            else
            {
                language = Session["language"].ToString();
            }

            ViewBag.MenuList = fun.getItemMenu(db, db.Menus.Where(e => e.Language_Code == language && e.IsActive == 1 && e.TypeMenu == 1)
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