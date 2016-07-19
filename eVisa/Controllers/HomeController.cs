using eVisa.Function;
using eVisa.Models;
using eVisa.ViewModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;


namespace eVisa.Controllers
{
    public class HomeController : Controller
    {
        private eVisaContext db = new eVisaContext();
        private BaseFunction fun = new BaseFunction();

        static string language = "";

        public ActionResult Index()
        {
            //if (Request.IsAuthenticated) {
            //    return RedirectToAction("Index", "Admin");
            //}

             if(Session["language"] == null){
                language = "en";
            }else{
                language = Session["language"].ToString();   
            }

            List<ImageView> list = new List<ImageView>();

            foreach (var item in db.Images.Where(e => e.IsActive == 1)
                .Select(e => new ImageView() { 
                Id=e.Id,
                Name=e.Name,
                Url=e.Url,
                Description=e.Description
                })
                .ToList())
            {
                item.Url = fun.ResolveServerUrl(item.Url, false);
                list.Add(item);
            }

            PageView pageView = fun.getPageContent(db,db.Pages.Where(e => e.Language_Code == language.Trim() && e.IsActive == 1 && e.Link_Code == 1)
                 .Select(e => new PageView
                 {
                     Content = e.Content,
                     Title = e.Title
                 }).FirstOrDefault(),1);

            PageView pageInfo = fun.getPageContent(db, db.Pages.Where(e => e.Language_Code == language.Trim() && e.IsActive == 1 && e.Link_Code == 7)
                .Select(e => new PageView
                {
                    Content = e.Content.Trim(),
                    Title = e.Title
                }).FirstOrDefault(), 7);

            PageView pageAnnouncement = fun.getPageContent(db, db.Pages.Where(e => e.Language_Code == language.Trim() && e.IsActive == 1 && e.Link_Code == 8)
                .Select(e => new PageView
                {
                    Content = e.Content.Trim(),
                    Title = e.Title
                }).FirstOrDefault(), 8);

            var entity = new HomeView()
            {
              
                ImageView = (IEnumerable<ImageView>)list,
                Pageview = pageView,
                PageInformation = pageInfo,
                TouristTable = db.TouristVisas.Where(e => e.Language_Code == language.Trim() && e.IsActive == 1)
                .Select(e => new TouristVisaView()
                {
                    Id = e.Id,
                    Language_code = e.Language_Code,
                    Name = e.Name,
                    Text = e.Text,
                    Header = e.Header
                }).ToList(),
                NameofPort = db.NameofPorts.Where(e => e.Language_Code == language.Trim() && e.IsActive == 1)
                .Select(e => new NameofPortView()
                {
                    Id = e.Id,
                    Header = e.Header,
                    Border = e.Border,
                    Entry = e.Entry,
                    Exit = e.Exit,
                    Name = e.Name,

                }),
                NavMenu = db.Menus.Where(e => e.Language_Code == language.Trim() && e.IsActive == 1 && e.TypeMenu == 2)
                .Select(e => new MenuView()
                {
                    Name = e.Name,
                    SubMenu = e.SubMenu,
                    Link_Code = e.Link_Code,
                    TypeMenu = e.TypeMenu
                }),
                PageAnnounment = pageAnnouncement
            };

            ViewBag.LanguageList = db.Languages.Where(e => e.IsActive == 1 && e.Code != language)
                .Select(e => new LanguageView()
                {
                    Name = e.Name,
                    Url = e.Url,
                    Id = e.Id,
                    Code = e.Code
                }).ToList();

            ViewBag.MenuList = fun.getItemMenu(db, db.Menus.Where(e => e.Language_Code == language.Trim() && e.IsActive == 1 && e.TypeMenu == 1)
                .Select(e => new MenuView()
                {
                    Name = e.Name,
                    SubMenu = e.SubMenu,
                    Link_Code = e.Link_Code,
                    TypeMenu = e.TypeMenu
                }));
             
            return View(entity);
        }

        public ActionResult About()
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


            var entity = fun.getPageContent(db, db.Pages.Where(e => e.Language_Code == language.Trim() && e.IsActive == 1 && e.Link_Code == 2)
                    .Select(e => new PageView()
                    {
                        Id = e.Id,
                        Title = e.Title,
                        Content = e.Content
                    }).FirstOrDefault(),2);
            
            return View(entity);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            if(Session["language"] == null){
                language = "en";
            }else{
                language = Session["language"].ToString();   
            }

            ViewBag.MenuList = fun.getItemMenu(db,db.Menus.Where(e => e.Language_Code == language.Trim() && e.IsActive == 1 && e.TypeMenu == 1)
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

            var entity = fun.getPageContent(db, db.Pages.Where(e => e.Language_Code == language.Trim() && e.IsActive == 1 && e.Link_Code == 3)
                    .Select(e => new PageView(){
                        Id=e.Id,
                        Title=e.Title,
                        Content = e.Content
                    }).FirstOrDefault(),3);

            return View(entity);
        }

        
    }
}