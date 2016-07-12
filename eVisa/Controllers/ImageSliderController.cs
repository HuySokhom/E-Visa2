using eVisa.Function;
using eVisa.Models;
using eVisa.Models.ImageSlide;
using eVisa.ViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;

namespace eVisa.Controllers
{
    public class ImageSliderController : Controller
    {
        private eVisaContext db = new eVisaContext();
        private BaseFunction fun = new BaseFunction();

        [HttpGet]
        public ActionResult Index(int? i) {
            string a = "";
            //var view = db.Images
            //    .Where(e => e.IsActive == 1)
            //    .Select(e => new ImageView
            //{
            //    Id = e.Id,
            //    Name = e.Name,
            //    Url = e.Url,
            //    Description = e.Description
            //});
           
            if (i.HasValue) {
                if (i.Value == 1) ViewBag.res = "Exist image name !";
                if (i.Value == 2) ViewBag.res = "Invalid image size !";
            }
           
            return View();
        }

        [HttpGet]
        public ActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Create(ImageViewModel model) {
            
            Image img = new Image();

            if (ModelState.IsValid)
            {

                //Save image
                var uploadImageFolder = "/Uploads/Images/";
                var fullUploadImageFolder = Server.MapPath(uploadImageFolder);

                if (db.Images.Where(m => m.IsActive == 1).Any(m => m.Name == model.Name))
                {
                    ModelState.AddModelError("Exist Photo ", "Wrong Formate");
                    ViewBag.res = "Image name exist !";
                    return RedirectToAction("Index", "ImageSlider", new  {i = 1 });
                };

                Stream stream = model.Photo.InputStream;
                System.Drawing.Image size = System.Drawing.Image.FromStream(stream);

                if (size.Height != 567 && size.Width != 1700) {
                    ModelState.AddModelError("res", "Invalid Image size !");
                    return RedirectToAction("Index", "ImageSlider", new {i = 2 });
                }



                if (!Directory.Exists(fullUploadImageFolder))
                {
                    Directory.CreateDirectory(fullUploadImageFolder);
                }

                var imageExtension = Path.GetExtension(model.Photo.FileName);
                
                if (string.IsNullOrEmpty(imageExtension))
                {
                    ModelState.AddModelError("Photo", "Wrong Formate");
                    return View(model);
                }


                var uploadImagePath = Path.Combine(uploadImageFolder, model.Name + imageExtension);
                model.Photo.SaveAs(Server.MapPath(uploadImagePath));
                
                    model.Url = uploadImagePath;
                    img.Name = model.Name;
                    img.Url = uploadImagePath;
                    img.Description = model.Description;
                    img.IsActive = 1;
                    img.CreatedBy = User.Identity.Name;
                    img.CreatedDate = DateTime.Now;
                    img.UpdatedDate = DateTime.Now;
                    img.UpdateBy = User.Identity.Name;

                db.Images.Add(img);
                db.SaveChanges();
            }
            else {
                ModelState.AddModelError("Photo", "Wrong Format");
                RedirectToAction("Index", "ImageSlider");
            }

             
            return RedirectToAction ("Index","ImageSlider");
        }

        [HttpGet]
        public JsonResult Edit(int? id) {

            if(id.HasValue){
            
                var entity = db.Images.Find(id);
                return Json(entity, JsonRequestBehavior.AllowGet);
            }

            return Json("error", JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public ActionResult Edit(ImageEditView viewModel) {

            if (ModelState.IsValid) {
                string url = "";

                var entity = db.Images.Find(viewModel.Id);

                var uploadImageFolder = "/Uploads/Images/";
                var fullUploadImageFolder = Server.MapPath(uploadImageFolder);

                if (viewModel.Photo == null)
                {
                    url = entity.Url;
                }
                else
                {
                    /* Check exist name */
                    if (db.Images.Where(m => m.IsActive == 1).Any(m => m.Name == viewModel.Name))
                    {
                        ModelState.AddModelError("Exist Photo ", "Wrong Formate");
                        ViewBag.res = "Image name exist !";
                        return RedirectToAction("Index", "ImageSlider", new { i = 1 });
                    };

                    /* Check image width/height */

                    Stream stream = viewModel.Photo.InputStream;
                    System.Drawing.Image size = System.Drawing.Image.FromStream(stream);

                    if (size.Height != 567 && size.Width != 1700)
                    {
                        ModelState.AddModelError("res", "Invalid Image size !");
                        return RedirectToAction("Index", "ImageSlider", new  {i = 2 });
                    }

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



               
                entity.Url = url;
                entity.UpdatedDate = DateTime.Now;
                entity.UpdateBy = User.Identity.Name;
                entity.Name = viewModel.Name;

                db.Entry(entity).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Create", "Language");

            }

            return RedirectToAction("Index", "ImageSlider");
        }

        [HttpPost]
        public JsonResult getList(string search) {
            BaseFunction fun = new BaseFunction();

            List<ImageView> list = new List<ImageView>();
            
            var viewModel = db.Images.Where(e => e.IsActive == 1 && e.Name.StartsWith(search))
                .Select(e => new ImageView()
                {
                    Id = e.Id,
                    Name = e.Name,
                    Url = e.Url,
                    Description = e.Description
                });
            foreach (var e in viewModel) {

                list.Add(new ImageView() {
                    Id = e.Id,
                    Name = e.Name,
                    Url = fun.ResolveServerUrl(e.Url,false),
                    Description = e.Description
                });
            }

            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public JsonResult AjaxDelete(int? id)
        {
            if (!id.HasValue)
            {
                return Json(new { result = false, message = string.Format("Invalid", "Paper") });
            }

            var entity = db.Images.Find(id);

            if (entity == null)
                return Json(new { result = false, message = string.Format("Delete Fail", "Paper") });

            var oldImagePath = Server.MapPath(entity.Url);
            if (System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);
            }
            entity.UpdateBy = User.Identity.Name;
            entity.IsActive = 0;
            db.Entry(entity).State = EntityState.Modified;
            var sta = db.SaveChanges();
            //Json(new { result = false, message = string.Format("Fail to delete", "Paper") })

            return Json(new { result = true, message = string.Format("Delete Success", "Paper") });
        }
	}

  
}