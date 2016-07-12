using eVisa.Function;
using eVisa.Models;
using eVisa.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eVisa.Controllers
{
    public class CreditsThanksController : Controller
    {
        private eVisaContext db = new eVisaContext();
        //
        // GET: /CreditsThanks/
        [HttpGet]
        public ActionResult Create()
        {
            BaseFunction bfn = new BaseFunction();

            ViewBag.Language = bfn.getLanguage(1);

            return View();
        }

        [HttpPost]
        public ActionResult Create(CreditsThanksView viewModel)
        {
            BaseFunction bfn = new BaseFunction();

            ViewBag.Language = bfn.getLanguage(1);

            if (ModelState.IsValid)
            {

                //Save image
                var uploadImageFolder = "/Images/People/";
                var fullUploadImageFolder = Server.MapPath(uploadImageFolder);

                if (db.CreditsThanks.Where(m => m.IsActive == 1).Any(m => m.Name == viewModel.Name))
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
                var entity = new CreditsThanks()
                {
                    Name = viewModel.Name,
                    Dates = DateTime.Now,
                    Url = uploadImagePath,
                    Content = viewModel.Content,
                    Language_Code = viewModel.Language_Code,
                    National = viewModel.National,

                    UpdatedBy = User.Identity.Name,
                    UpdatedDate = DateTime.Now,
                    CreatedBy = User.Identity.Name,
                    CreatedDate = DateTime.Now,
                    IsActive = 1

                };

                db.CreditsThanks.Add(entity);
                db.SaveChanges();

            }
            else
            {
                ModelState.AddModelError("Photo", "Wrong Formate");
                //return RedirectToAction("Index", "Language");
            }

            return View();
        }

        [HttpGet]
        public ActionResult Edit(int? Id)
        {
            BaseFunction bfn = new BaseFunction();
            List<CreditsThanksView> list = new List<CreditsThanksView>();

            if (Id.HasValue)
            {
                var viewModel = db.CreditsThanks.Find(Id);

                var model = new CreditsThanksView
                {
                    Id = viewModel.Id,
                    Name = viewModel.Name,
                    Dates = viewModel.Dates,
                    Url = viewModel.Url,
                    Content = viewModel.Content,
                    Language_Code = viewModel.Language_Code,
                    National = viewModel.National

                };

                list.Add(new CreditsThanksView()
                {
                    Id = model.Id,
                    Name = model.Name,
                    Dates = model.Dates,
                    Url = model.Url,
                    Content = model.Content,
                    Language_Code = model.Language_Code,
                    National = model.National

                });

                return Json(list.ToList(), JsonRequestBehavior.AllowGet);

            }
            var models = new MenuView
            {
                LanguageList = bfn.getLanguage(1),
                MenuList = bfn.getMenuList(1)
            };

            return Json("error", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Edit(CreditsThanksEditView viewModel)
        {
            CreditsThanks entity = new CreditsThanks();

            if (ModelState.IsValid)
            {
                string url = "";
                entity = db.CreditsThanks.Find(viewModel.Id);

                var uploadImageFolder = "/Images/People/";
                var fullUploadImageFolder = Server.MapPath(uploadImageFolder);

                if (viewModel.Photo == null)
                {
                    url = entity.Url;
                }
                else
                {

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
                        return RedirectToAction("Index", "CreditsThanks");
                    }

                    var uploadImagePath = Path.Combine(uploadImageFolder, viewModel.Name + imageExtension);
                    viewModel.Photo.SaveAs(Server.MapPath(uploadImagePath));
                    url = uploadImagePath;
                }

                entity.Url = url;
                entity.Name = viewModel.Name;
                entity.Content = viewModel.Content;
                entity.Language_Code = viewModel.Language_Code;
                entity.National = viewModel.National;
                entity.UpdatedBy = User.Identity.Name;
                entity.UpdatedDate = DateTime.Now;

                db.Entry(entity).State = EntityState.Modified;
                db.SaveChanges();

              
                return RedirectToAction("Create", "CreditsThanks");
            }

            return RedirectToAction("Create", "CreditsThanks");
        }

        public JsonResult getCreditsThanksList(string code)
        {

            if (code != "")
            {
                var viewModel = db.CreditsThanks
                    .Where(e => e.Language_Code == code && e.IsActive == 1)
                    .Select(e => new CreditsThanksView()
                    {
                        Id = e.Id,
                        Name = e.Name,
                        Dates = e.Dates,
                        Content = e.Content,
                        National = e.National,
                        Url = e.Url,
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

            var entity = db.CreditsThanks.Find(id);

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