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
    public class FeedbackController : Controller
    {
        private eVisaContext db = new eVisaContext();
        //
        // GET: /Feedback/
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(FeedbackView viewModel)
        {
            if (ModelState.IsValid) {
                var entity = new Feedback() { 
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

                return View();
            }

            return View();
        }


        [HttpGet]
        public ActionResult Edit(int? Id)
        {
            BaseFunction bfn = new BaseFunction();

            if (Id.HasValue)
            {
                var entity = db.Feedbacks.Find(Id);

                var model = new FeedbackView
                {
                    Id = entity.Id,
                    Name = entity.Name,
                    Email = entity.Email,
                    Content = entity.Content

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
        public ActionResult Edit(FeedbackView viewModel)
        {
            Feedback entity = new Feedback();

            if (ModelState.IsValid)
            {
                entity = db.Feedbacks.Find(viewModel.Id);

                entity.Name = viewModel.Name;
                entity.Email = viewModel.Email;
                entity.Content = viewModel.Content;
                entity.UpdatedBy = User.Identity.Name;
                entity.UpdatedDate = DateTime.Now;

                db.Entry(entity).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Create", "Feedback");
            }

            return RedirectToAction("Create", "Feedback");
        }

        [HttpPost]
        public JsonResult getFeedback(string search) {
            var entity = db.Feedbacks.Where(e => e.Name.StartsWith(search) && e.IsActive == 1)
                    .Select( e => new FeedbackView() { 
                
                Id=e.Id,
                Name = e.Name,
                Email = e.Email,
                Content = e.Content
            }).ToList();

            return Json(entity, JsonRequestBehavior.AllowGet);
        }

        public JsonResult AjaxDelete(int? id)
        {
            if (!id.HasValue)
            {
                return Json(new { result = false, message = string.Format("Invalid", "Paper") });
            }

            var entity = db.Feedbacks.Find(id);

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