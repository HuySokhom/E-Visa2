using eVisa.Function;
using eVisa.Models;
using eVisa.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.SqlServer;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eVisa.Controllers
{
    public class LanguageController : Controller
    {
        private eVisaContext db = new eVisaContext();
        //
        // GET: /Language/
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(LanguageView viewModel) {

            Language img = new Language();

            if (ModelState.IsValid)
            {

                //Save image
                var uploadImageFolder = "/Images/Language/";
                var fullUploadImageFolder = Server.MapPath(uploadImageFolder);

                if (db.Languages.Where(m => m.IsActive == 1).Any(m => m.Code == viewModel.Code))
                {
                    ModelState.AddModelError("Exist Photo ", "Wrong Formate");

                    return View();
                };

                if (!Directory.Exists(fullUploadImageFolder))
                {
                    Directory.CreateDirectory(fullUploadImageFolder);
                }

                var imageExtension = Path.GetExtension(viewModel.Photo.FileName);
                if (string.IsNullOrEmpty(imageExtension))
                {
                    ModelState.AddModelError("Photo", "Wrong Formate");
                    return View();
                }


                var uploadImagePath = Path.Combine(uploadImageFolder, viewModel.Name + imageExtension);
                viewModel.Photo.SaveAs(Server.MapPath(uploadImagePath));

                viewModel.Url = uploadImagePath;
                img.Name = viewModel.Name;
                img.Code = viewModel.Code;
                img.Url = uploadImagePath;
                img.IsActive = 1;
                img.CreatedBy = User.Identity.Name;
                img.CreatedDate = DateTime.Now;
                img.UpdatedDate = DateTime.Now;
                img.UpdateBy = User.Identity.Name;
            }
            else
            {
                ModelState.AddModelError("Photo", "Wrong Formate");
                return RedirectToAction("Create", "Language");
            }

            //var view = db.Images
            //    .Where(e => e.IsActive == 1)
            //    .Select(e => new ImageView
            //    {
            //        Id = e.Id,
            //        Name = e.Name,
            //        Url = e.Url,
            //        Description = e.Description
            //    });

            db.Languages.Add(img);
            db.SaveChanges();
            return View();
        }

        public JsonResult getLanguageList(string search)
        {
          
                var viewModel = db.Languages.Where(e => e.IsActive == 1 && e.Name.StartsWith(search))
                    .Select(e => new LanguageView()
                    {
                        Id = e.Id,
                        Name = e.Name,
                        Code = e.Code,
                        Url = e.Url
                    })
                  .AsEnumerable();

                return Json(viewModel, JsonRequestBehavior.AllowGet);
            
        }
        [HttpGet]
        public ActionResult Edit(int? id) {
            BaseFunction fun = new BaseFunction();
           
            if (!id.HasValue) {
                return View();
            }
            var e = db.Languages.Find(id);
            var entity = new LanguageView()
            {
                Id = e.Id,
                Name = e.Name,
                Code = e.Code,
                Url = e.Url
            };

            
            return Json(entity, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Edit(LanguageView viewModel) {

            string url = "";

                var entity = db.Languages.Find(viewModel.Id);

                var uploadImageFolder = "/Images/Language/";
                var fullUploadImageFolder = Server.MapPath(uploadImageFolder);

                if (viewModel.Photo == null)
                {
                    url = entity.Url;
                }
                else {

                    /* Delete Old Image */
                    var oldImagePath = Server.MapPath(entity.Url);
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }

                    /* new Image */
                    var imageExtension = Path.GetExtension(viewModel.Photo.FileName);
                    if (string.IsNullOrEmpty(imageExtension))
                    {
                        ModelState.AddModelError("Photo", "Wrong Formate");
                        return View();
                    }

                    var uploadImagePath = Path.Combine(uploadImageFolder, viewModel.Name + imageExtension);
                    viewModel.Photo.SaveAs(Server.MapPath(uploadImagePath));
                    url = uploadImagePath;
                }

                

                entity.Code = viewModel.Code;
                entity.Index = viewModel.Index;
                entity.Url = url;
                entity.UpdatedDate = DateTime.Now;
                entity.UpdateBy = User.Identity.Name;
                entity.Name = viewModel.Name;

                db.Entry(entity).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Create", "Language");

           
        }

        public JsonResult AjaxDelete(int? id)
        {
            if (!id.HasValue)
            {
                return Json(new { result = false, message = string.Format("Invalid", "Paper") });
            }

            var entity = db.Languages.Find(id);

            if (entity == null)
                return Json(new { result = false, message = string.Format("Delete Fail", "Paper") });

            var oldImagePath = Server.MapPath(entity.Url);
            if (System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);
            }
            //entity.UpdateBy = User.Identity.Name;
            
            //entity.IsActive = 0;
            //db.Entry(entity).State = EntityState.Modified;

            db.Languages.Remove(entity);
            var sta = db.SaveChanges();
            //Json(new { result = false, message = string.Format("Fail to delete", "Paper") })

            return Json(new { result = true, message = string.Format("Delete Success", "Paper") });
        }
	}
}