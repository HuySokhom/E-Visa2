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
    public class NavMenuController : Controller
    {
        private eVisaContext db = new eVisaContext();
        private BaseFunction bfn = new BaseFunction();
        //
        // GET: /Menu/
        public ActionResult Index()
        {

            return View();
        }

        [HttpGet]
        public ActionResult Create()
        {

            
            var model = new MenuView
            {
                LanguageList = bfn.getLanguage(2),
                MenuList = bfn.getMenuList(2)
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult Create(MenuView viewModel)
        {
           
            if (ModelState.IsValid)
            {
                if (!db.Menus.Where(e => e.IsActive == 1).Any(e => e.Language_Code == viewModel.Language_Code && e.Link_Code == viewModel.Link_Code && e.TypeMenu == 2))
                {
                    var model = new Menu
                    {
                        Id = viewModel.Id,
                        Name = viewModel.Name,
                        Language_Code = viewModel.Language_Code,
                        Link_Code = viewModel.Link_Code,
                        CreatedBy = User.Identity.Name,
                        CreatedDate = DateTime.Now,
                        UpdatedBy = User.Identity.Name,
                        UpdatedDate = DateTime.Now,
                        SubMenu = viewModel.SubMenu,
                        TypeMenu = 2,
                        IsActive = 1
                    };

                    db.Menus.Add(model);
                    var sta = db.SaveChanges();
                }
                else
                {

                    ModelState.AddModelError("result", "Save Error !");
                }
            }

            var models = new MenuView
            {
                LanguageList = bfn.getLanguage(1),
                MenuList = bfn.getMenuList(2)
            };

            return View(models);
        }

        [HttpGet]
        public ActionResult Edit(int? Id)
        {
            BaseFunction bfn = new BaseFunction();

            if (Id.HasValue)
            {
                var entity = db.Menus.Find(Id);

                var model = new MenuView
                {
                    Id = entity.Id,
                    Name = entity.Name,
                    Language_Code = entity.Language_Code,
                    Link_Code = entity.Link_Code,
                    SubMenu = entity.SubMenu,
                    LanguageList = bfn.getLanguage(1),
                    MenuList = bfn.getMenuList(2),

                };

                return Json(model, JsonRequestBehavior.AllowGet);

            }
            return Json("error", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Edit(MenuView viewModel)
        {
            if (ModelState.IsValid)
            {
                var entity = db.Menus.Find(viewModel.Id);

                entity.Link_Code = viewModel.Link_Code;
                entity.Language_Code = viewModel.Language_Code;
                entity.Name = viewModel.Name;
                entity.UpdatedDate = DateTime.Now;
                entity.UpdatedBy = User.Identity.Name;
                entity.SubMenu = viewModel.SubMenu;
                db.Entry(entity).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Create", "NavMenu");
            }

            return RedirectToAction("Create", "NavMenu");
        }

        [HttpPost]
        public JsonResult getListMenu(int? id, string code,string search)
        {

            if (code != "" && id.HasValue)
            {
                var viewModel = db.Menus.Where(e => e.Link_Code == id && e.TypeMenu == 2 && e.Name.StartsWith(search))
                    .Select(e => new MenuView()
                    {
                        Id = e.Id,
                        Name = e.Name,
                        Link_Code = e.Link_Code,
                        Language_Code = e.Language_Code,
                        SubMenu = e.SubMenu,
                        TypeMenu = e.TypeMenu
                    })
                  .AsEnumerable();
                return Json(viewModel, JsonRequestBehavior.AllowGet);
            }

            return Json(null, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AjaxDelete(int? id)
        {
            if (!id.HasValue)
            {
                return Json(new { result = false, message = string.Format("Invalid", "Paper") });
            }

            var entity = db.Menus.Find(id);

            if (entity == null)
                return Json(new { result = false, message = string.Format("Delete Fail", "Paper") });

            entity.UpdatedBy = User.Identity.Name;
            entity.IsActive = 0;
            db.Entry(entity).State = EntityState.Modified;
            var sta = db.SaveChanges();
            //Json(new { result = false, message = string.Format("Fail to delete", "Paper") })

            return Json(new { result = true, message = string.Format("Delete Success", "Paper") });
        }
	}
}