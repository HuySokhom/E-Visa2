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
    public class ServicesController : Controller
    {
        private eVisaContext db = new eVisaContext();
        private BaseFunction fun = new BaseFunction();
        private string language = "";
        //
        // GET: /Services/
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

            ViewBag.LanguageList = db.Languages.Where(e => e.IsActive == 1 && e.Code != language)
                    .Select(e => new LanguageView()
                    {
                        Name = e.Name,
                        Url = e.Url,
                        Id = e.Id,
                        Code = e.Code
                    }).ToList();

            ViewBag.MenuList = fun.getItemMenu(db,db.Menus.Where(e => e.Language_Code == language && e.IsActive == 1)
                  .Select(e => new MenuView()
                  {
                      Name = e.Name,
                      SubMenu = e.SubMenu,
                      Link_Code = e.Link_Code
                  }));
             
            return View();
        }

        public ActionResult Links() {

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

          
            var model = fun.getTourismContent(db,db.Tourisms.Where(e => e.Language_Code == language && e.IsActive == 1)
                           .Select(e => new TourismView() { 
                            Id = e.Id,
                            Language_Code = e.Language_Code,
                            Content = e.Content,
                            Url = e.Url,
                            Name = e.Name,
                            Tel = e.Tel,
                            Website = e.Website,
                            Fax = e.Fax,
                            Email = e.Email
                           }).AsEnumerable());

            return View(model);
        }

        public ActionResult Ministry() {

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

            var entity = fun.getMinistryContent(db,
                    db.Ministries.Where(e => e.Language_Code == language && e.IsActive == 1)
                                    .Select(e => new MinistryView()
                                    {
                                        Id = e.Id,
                                        Name = e.Name,
                                        Language_Code = e.Language_Code,
                                        Address = e.Address,
                                        Tel = e.Tel,
                                        Fax = e.Fax,
                                        Email = e.Email,
                                        Website = e.Website
                                    }).ToList());

             
            return View(entity);
        }

        public ActionResult Mission()
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

            var entity = fun.getMissionWorldsContent(db,
                db.MissionWorlds.Where(e => e.Language_Code == language && e.IsActive == 1)
                        .Select(e => new MissionWorldView()
                        {
                            Id = e.Id,
                            Language_Code = e.Language_Code,
                            Mission = e.Mission,
                            Name = e.Name,
                            Tel = e.Tel,
                            Website = e.Website,
                            Header = e.Header,
                            Email = e.Email,
                            Country = e.Country,
                            Address = e.Address,
                            Fax = e.Fax,

                        }));

            return View(entity);
        }

        public ActionResult ForeignMission() {
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

            var entity = fun.getForeignMissionContent(db,
                    db.ForeignMissions.Where(e => e.Language_Code == language && e.IsActive == 1)
                        .Select(e => new ForeignMissionView() { 
                                Id=e.Id,
                                Name=e.Name,
                                Tel=e.Tel,
                                Website=e.Website,
                                Language_Code=e.Language_Code,
                                Fax=e.Fax,
                                Email=e.Email,
                                Address=e.Address
                        }));
             
            return View(entity);
        }
	}
}