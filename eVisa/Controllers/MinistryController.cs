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
    public class MinistryController : Controller
    {
        private eVisaContext db = new eVisaContext();

        //
        // GET: /Ministry/
        [HttpGet]
        public ActionResult Create()
        {
            BaseFunction bfn = new BaseFunction();

            var model = new MinistryView
            {
                LanguageList = bfn.getLanguage(1)

            };

            return View(model);
        }

        [HttpPost]
        public ActionResult Create(MinistryView viewModel) {

            BaseFunction bfn = new BaseFunction();

            //var model = new MinistryView
            //{
            //    LanguageList = bfn.getLanguage(1)

            //};

            if (ModelState.IsValid) {
                var entity = new Ministry()
                {
                    Name = viewModel.Name,
                    Tel = viewModel.Tel,
                    Fax = viewModel.Fax,
                    Language_Code = viewModel.Language_Code,
                    Email = viewModel.Email,
                    Address = viewModel.Address,
                    Website = viewModel.Website,
                    CreatedBy = User.Identity.Name,
                    CreatedDate = DateTime.Now,
                    UpdatedBy = User.Identity.Name,
                    UpdatedDate = DateTime.Now,
                    IsActive = 1
                };

                db.Ministries.Add(entity);
                db.SaveChanges();

                return View();
            }
            
            ModelState.AddModelError("", "Save not completed!");

            return View();
        }

        [HttpGet]
        public ActionResult Edit(int? Id)
        {
            BaseFunction bfn = new BaseFunction();

            if (Id.HasValue)
            {
                var entity = db.Ministries.Find(Id);

                var viewModel = new MinistryView
                {
                    Id= entity.Id,
                    Name = entity.Name,
                    Tel = entity.Tel,
                    Fax = entity.Fax,
                    Language_Code = entity.Language_Code,
                    Email = entity.Email,
                    Address = entity.Address,
                    Website = entity.Website
                };

                return Json(viewModel, JsonRequestBehavior.AllowGet);

            }
            var models = new MenuView
            {
                LanguageList = bfn.getLanguage(1),
                MenuList = bfn.getMenuList(1)
            };

            return Json("error", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Edit(MinistryView viewModel)
        {
            Ministry entity = new Ministry();

            if (ModelState.IsValid)
            {
                entity = db.Ministries.Find(viewModel.Id);

                 entity.Name = viewModel.Name;
                 entity.Tel = viewModel.Tel;
                 entity.Fax = viewModel.Fax;
                 entity.Language_Code = viewModel.Language_Code;
                 entity.Email = viewModel.Email;
                 entity.Address = viewModel.Address;
                 entity.Website = viewModel.Website;
                
                db.Entry(entity).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Create", "Ministry", new { lange = viewModel.Language_Code });
            }

            return RedirectToAction("Create", "Ministry");
        }


        public JsonResult getMinistryList(string code) {

            if (code != "")
            {
                var viewModel = db.Ministries
                    .Where(e => e.Language_Code == code && e.IsActive == 1)
                    .Select(e => new MinistryView()
                    {
                        Id = e.Id,
                        Name = e.Name,
                        Address = e.Address,
                        Tel = e.Tel,
                        Email = e.Email,
                        Fax = e.Fax,
                        Website = e.Website,
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

            var entity = db.Ministries.Find(id);

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