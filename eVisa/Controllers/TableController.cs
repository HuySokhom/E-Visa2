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
    public class TableController : Controller
    {
        private eVisaContext db = new eVisaContext();
        //
        // GET: /Table/
        public ActionResult Index()
        {
            BaseFunction bfn = new BaseFunction();
            
            ViewBag.menu = bfn.getMenu();
            

            return View();
        }

        [HttpGet]
        public ActionResult TouristVisa() {
            BaseFunction bfn = new BaseFunction();
            var model = new TouristVisaView
            {
                LanguageList = bfn.getLanguage(1)
            };

            return View(model);
        }

        [HttpGet]
        public ActionResult CreateTouristVisa()
        {
            BaseFunction bfn = new BaseFunction();
            var model = new TouristVisaView
            {
                LanguageList = bfn.getLanguage(1)
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult CreateTouristVisa(TouristVisaView viewModel)
        {
            BaseFunction bfn = new BaseFunction();
            var model = new TouristVisaView
            {
                LanguageList = bfn.getLanguage(1)
            };
            
            if (ModelState.IsValid) {

                var entity = new TouristVisa()
                {
                    Name = viewModel.Name,
                    Text = viewModel.Text,
                    IsActive = 1,
                    Header = viewModel.Header,
                    Language_Code = viewModel.Language_code
                };

                db.TouristVisas.Add(entity);
                db.SaveChanges();

                return RedirectToAction("TouristVisa", "Table");
            }

            return RedirectToAction("TouristVisa", "Table");
        }
        [HttpGet]
        public ActionResult EditTouristVisa(int? Id)
        {
           
            BaseFunction bfn = new BaseFunction();
            if (Id.HasValue) {

                var viewModel = db.TouristVisas.Find(Id);
                var model = new TouristVisaView
                {
                    Name = viewModel.Name,
                    Text = viewModel.Text,
                    Language_code = viewModel.Language_Code,
                    Header = viewModel.Header,
                    LanguageList = bfn.getLanguage(1)
                };

                return Json(viewModel, JsonRequestBehavior.AllowGet);

            }

            var models = new TouristVisaView
            {
                LanguageList = bfn.getLanguage(1)
            };

            return Json("error", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult EditTouristVisa(TouristVisaView viewModel)
        {
            BaseFunction bfn = new BaseFunction();
            var model = new TouristVisaView
            {
                LanguageList = bfn.getLanguage(1)
            };

            if (ModelState.IsValid) {
                var entity = db.TouristVisas.Find(viewModel.Id);
              
                entity.Name = viewModel.Name;
                entity.Text = viewModel.Text;
                entity.Language_Code = viewModel.Language_code;
                entity.Header = viewModel.Header;
                db.Entry(entity).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("NameofPort", "Table");

            }

            return RedirectToAction("NameofPort", "Table");
        }

        public JsonResult getTouristVisa(string code) {
            if (code != "")
            {
                var viewModel = db.TouristVisas
                    .Where(e => e.Language_Code == code && e.IsActive == 1)
                    .Select(e => new TouristVisaView()
                    {
                        Text = (e.Text == null ? "" : e.Text),
                        Name = (e.Name == null ? "" : e.Name),
                        Language_code = e.Language_Code,
                        Id = e.Id,
                        Header = e.Header
                    })
                  .AsEnumerable();

                return Json(viewModel, JsonRequestBehavior.AllowGet);
            }

            return Json(null, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Create()
        {
            BaseFunction bfn = new BaseFunction();
            ViewBag.menu = bfn.getMenu();
            ViewBag.language = bfn.getLanguage(1);

            return View();
        }

        [HttpPost]
        public ActionResult Create(TouristVisa viewModel)
        {
            if (ModelState.IsValid) {
                var entity = new TouristVisa()
                {
                  
                    Name = viewModel.Name,
                    Text = viewModel.Text
                   
                };

                db.TouristVisas.Add(entity);
                db.SaveChanges();

                return View();
            
            }
            return View();
        }

        [HttpGet]
        public ActionResult NameofPort() {
            BaseFunction bfn = new BaseFunction();
            var model = new NameofPortView
            {
                LanguageList = bfn.getLanguage(1)
            };

            return View(model);
          
        }

        [HttpGet]
        public ActionResult CreateNameofPort()
        {
            BaseFunction bfn = new BaseFunction();
            var model = new NameofPortView
            {
                LanguageList = bfn.getLanguage(1)
            };

            return View(model);

        }

        [HttpPost]
        public ActionResult CreateNameofPort(NameofPortView viewModel)
        {

            BaseFunction bfn = new BaseFunction();
            var model = new NameofPortView
            {
                LanguageList = bfn.getLanguage(1)
            };
            
            if (ModelState.IsValid)
            {

                var entity = new NameofPort()
                {
                    Name = viewModel.Name,
                    Border = viewModel.Border,
                    Entry = viewModel.Entry,
                    Exit = viewModel.Exit,
                    IsActive = 1,
                    Header = viewModel.Header,
                    Language_Code = viewModel.Language_Code
                };

                db.NameofPorts.Add(entity);
                db.SaveChanges();

                return RedirectToAction("NameofPort", "Table");
            }

            return RedirectToAction("NameofPort", "Table");
        }

        [HttpGet]
        public ActionResult EditNameofPort(int ? Id) {


            BaseFunction bfn = new BaseFunction();
            if (Id.HasValue)
            {

                var viewModel = db.NameofPorts.Find(Id);
                var model = new NameofPortView
                {
                    Name = viewModel.Name,
                    Border = viewModel.Border,
                    Entry = viewModel.Entry,
                    Exit = viewModel.Exit,
                    Language_Code = viewModel.Language_Code,
                    Header = viewModel.Header,
                    LanguageList = bfn.getLanguage(1)
                };

                return Json(viewModel, JsonRequestBehavior.AllowGet);

            }

            var models = new TouristVisaView
            {

                LanguageList = bfn.getLanguage(1)
            };

            return Json("error", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult EditNameofPort(NameofPortView viewModel)
        {

            BaseFunction bfn = new BaseFunction();
            var model = new TouristVisaView
            {
                LanguageList = bfn.getLanguage(1)
            };

            if (ModelState.IsValid)
            {
                var entity = db.NameofPorts.Find(viewModel.Id);
                

                entity.Name = viewModel.Name;
                entity.Border = viewModel.Border;
                entity.Entry = viewModel.Entry;
                entity.Exit = viewModel.Exit;

                entity.Language_Code = viewModel.Language_Code;
                entity.Header = viewModel.Header;
                db.Entry(entity).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("NameofPort", "Table");

            }

            return View(model);
        }

        public JsonResult getNameofPort(string code)
        {
            if (code != "")
            {
                var viewModel = db.NameofPorts
                    .Where(e => e.Language_Code == code && e.IsActive == 1)
                    .Select(e => new NameofPortView()
                    {
                        Id = e.Id,
                        Name = (e.Name == null ? "" : e.Name),
                        Border = (e.Border == null ? "" : e.Border),
                        Entry = (e.Entry == null ? "" : e.Entry),
                        Exit = (e.Exit == null ? "" : e.Exit),
                        Header = e.Header,
                        Language_Code = e.Language_Code
                    })
                  .AsEnumerable();

                return Json(viewModel, JsonRequestBehavior.AllowGet);
            }

            return Json(null, JsonRequestBehavior.AllowGet);
        }

        public JsonResult AjaxDeleteTouristVisa(int? id)
        {
            if (!id.HasValue)
            {
                return Json(new { result = false, message = string.Format("Invalid", "Paper") });
            }

            var entity = db.TouristVisas.Find(id);

            if (entity == null)
                return Json(new { result = false, message = string.Format("Delete Fail", "Paper") });

            entity.IsActive = 0;
            db.Entry(entity).State = EntityState.Modified;
            var sta = db.SaveChanges();
            //Json(new { result = false, message = string.Format("Fail to delete", "Paper") })

            return Json(new { result = true, message = string.Format("Delete Success", "Paper") });
        }
        public JsonResult AjaxDeleteNameofPort(int? id)
        {
            if (!id.HasValue)
            {
                return Json(new { result = false, message = string.Format("Invalid", "Paper") });
            }

            var entity = db.NameofPorts.Find(id);

            if (entity == null)
                return Json(new { result = false, message = string.Format("Delete Fail", "Paper") });

            entity.IsActive = 0;
            db.Entry(entity).State = EntityState.Modified;
            var sta = db.SaveChanges();
            //Json(new { result = false, message = string.Format("Fail to delete", "Paper") })

            return Json(new { result = true, message = string.Format("Delete Success", "Paper") });
        }


	}
}