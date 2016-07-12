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
    public class FAQController : Controller
    {
        private eVisaContext db = new eVisaContext();
        //
        // GET: /FAQ/
        [HttpGet]
        public ActionResult Create()
        {

            BaseFunction bfn = new BaseFunction();
            var model = new FAQView
            {
                LanguageList = bfn.getLanguage(1)
               
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(FAQView viewModel)
        {

            BaseFunction bfn = new BaseFunction();
            var model = new FAQView
            {
                LanguageList = bfn.getLanguage(1)
               
            };
            if (ModelState.IsValid) { 
                var entity = new FAQ(){
                    Language_Code = viewModel.Language_Code,
                    question = viewModel.question,
                    answer = viewModel.answer,

                    UpdatedBy = User.Identity.Name,
                    UpdatedDate = DateTime.Now,
                    CreatedBy = User.Identity.Name,
                    CreatedDate = DateTime.Now,
                    IsActive = 1
                };

                db.FAQs.Add(entity);
                db.SaveChanges();

                return View(model);
            }

            return View(model);
        }

        [HttpGet]
        public ActionResult Edit(int? Id)
        {
            BaseFunction bfn = new BaseFunction();

            if (Id.HasValue)
            {
                var entity = db.FAQs.Find(Id);

                var model = new FAQView
                {
                    Id = entity.Id,
                    Language_Code = entity.Language_Code,
                    question = entity.question,
                    answer = entity.answer

                };

                return Json(model, JsonRequestBehavior.AllowGet);

            }
            //var models = new MenuView
            //{
            //    LanguageList = bfn.getLanguage(1),
            //    MenuList = bfn.getMenuList(1)
            //};

            return Json("error", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Edit(FAQView viewModel)
        {
            FAQ entity = new FAQ();

            if (ModelState.IsValid)
            {
                entity = db.FAQs.Find(viewModel.Id);

                entity.Language_Code = viewModel.Language_Code;
                entity.question = viewModel.question;
                entity.answer = viewModel.answer;
                entity.UpdatedBy = User.Identity.Name;
                entity.UpdatedDate = DateTime.Now;

                db.Entry(entity).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Create", "FAQ");
            }

            return RedirectToAction("Create", "FAQ");
        }

        public JsonResult getFAQ(string code) {
            
            if (code != null) {

                var entity = db.FAQs
                 .Where(e => e.IsActive == 1 && e.Language_Code == code)   
                 .Select(e => new FAQView() { 
                    Id = e.Id,
                    question = e.question,
                    answer = e.answer,
                    Language_Code = e.Language_Code
                
                }).ToList();

                return Json(entity, JsonRequestBehavior.AllowGet);
            }

            return Json(null, JsonRequestBehavior.AllowGet);
        }

        public JsonResult AjaxDelete(int? id)
        {
            if (!id.HasValue)
            {
                return Json(new { result = false, message = string.Format("Invalid", "Paper") });
            }

            var entity = db.FAQs.Find(id);

            if (entity == null)
                return Json(new { result = false, message = string.Format("Delete Fail", "Paper") });

            entity.UpdatedBy = User.Identity.Name;
            entity.UpdatedDate = DateTime.Now;
            entity.IsActive = 0;
            db.Entry(entity).State = EntityState.Modified;
            var sta = db.SaveChanges();

            //Json(new { result = false, message = string.Format("Fail to delete", "Paper") })

            return Json(new { result = true, message = string.Format("Delete Success", "Paper") });
        }
	}
}