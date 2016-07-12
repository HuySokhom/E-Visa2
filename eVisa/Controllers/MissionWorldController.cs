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
    public class MissionWorldController : Controller
    {
        private eVisaContext db = new eVisaContext();
        //Index
        // GET: /MissionWorld/
        [HttpGet]
        public ActionResult Create()
        {
            BaseFunction bfn = new BaseFunction();

            var model = new MissionWorldView
            {
                LanguageList = bfn.getLanguage(1)

            };
            return View(model);
        }
        [HttpPost]
        public ActionResult Create(MissionWorldView viewModel)
        {

            BaseFunction bfn = new BaseFunction();

            var model = new MissionWorldView
            {
                LanguageList = bfn.getLanguage(1)

            };
            if (ModelState.IsValid)
            {
                var entity = new MissionWorld()
                {
                    Name = viewModel.Name,
                    Country = viewModel.Country,
                    Mission = viewModel.Mission,
                    Tel = viewModel.Tel,
                    Email = viewModel.Email,
                    Website = viewModel.Website,
                    Fax = viewModel.Fax,
                    Address = viewModel.Address,
                    Language_Code = viewModel.Language_Code,
                    Header = viewModel.Header,

                    CreatedBy = User.Identity.Name,
                    CreatedDate = DateTime.Now,
                    UpdatedBy = User.Identity.Name,
                    UpdatedDate = DateTime.Now,
                    IsActive = 1
                };

                db.MissionWorlds.Add(entity);
                db.SaveChanges();

                return RedirectToAction("Create", "MissionWorld");
            }
            return RedirectToAction("Create", "MissionWorld");
        }

        [HttpGet]
        public ActionResult Edit(int? Id)
        {
            BaseFunction bfn = new BaseFunction();

            if (Id.HasValue)
            {
                var entity = db.MissionWorlds.Find(Id);

                var model = new MissionWorldView
                {
                    Id = entity.Id,
                    Name =  entity.Name,
                    Country = entity.Country,
                    Mission = entity.Mission,
                    Tel = entity.Tel,
                    Email = entity.Email,
                    Website = entity.Website,
                    Fax = entity.Fax,
                    Address = entity.Address,
                    Language_Code = entity.Language_Code
                    
                };

                return Json(model, JsonRequestBehavior.AllowGet);

            }
            var models = new MenuView
            {
                LanguageList = bfn.getLanguage(1),
                MenuList = bfn.getMenuList(1)
            };

            return Json("error", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Edit(MissionWorldView viewModel)
        {
            MissionWorld entity = new MissionWorld();
           
            if (ModelState.IsValid)
            {
                entity = db.MissionWorlds.Find(viewModel.Id);

                entity.Name = viewModel.Name;
                entity.Country = viewModel.Country;
                entity.Mission = viewModel.Mission;
                entity.Tel = viewModel.Tel;
                entity.Email = viewModel.Email;
                entity.Website = viewModel.Website;
                entity.Fax = viewModel.Fax;
                entity.Address = viewModel.Address;
                entity.Language_Code = viewModel.Language_Code;
                entity.UpdatedBy = User.Identity.Name;
                entity.UpdatedDate = DateTime.Now;

                db.Entry(entity).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Create", "MissionWorld", new {lange=viewModel.Language_Code });
            }

            return RedirectToAction("Create", "MissionWorld");
        }

        public JsonResult getMissionList(string code)
        {
           
            if (code != "")
            {
                var viewModel = db.MissionWorlds
                    .Where(e => e.Language_Code == code && e.IsActive == 1)
                    .Select(e => new MissionWorldView()
                    {
                        Id = e.Id,
                        Name = e.Name,
                        Country = (e.Country == null ? "" : e.Country),
                        Mission = (e.Mission == null ? "" : e.Mission),
                        Address = (e.Address == null ? "" : e.Address),
                        
                        Tel = (e.Tel == null ? "" : e.Tel),
                        Email = (e.Email == null ? "" : e.Email),
                        Fax = (e.Fax == null ? "" : e.Fax),
                        Website = (e.Website == null ? "" : e.Website),
                        Language_Code = e.Language_Code
                    })
                  .AsEnumerable();

                return Json(viewModel, JsonRequestBehavior.AllowGet);
            }

            return Json(null, JsonRequestBehavior.AllowGet);

        }

        public JsonResult AjaxDelete(int? id)
        {
            if (!id.HasValue)
            {
                return Json(new { result = false, message = string.Format("Invalid", "Paper") });
            }

            var entity = db.MissionWorlds.Find(id);

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