
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
    public class PageController : Controller
    {
        private eVisaContext db = new eVisaContext();
        //
        // GET: /Page/
        [HttpGet]
        public ActionResult Index()
        {
            BaseFunction bfn = new BaseFunction();

            var model = new PageView
            {
                LanguageList = bfn.getLanguage(1),
                MenuList = bfn.getPageMenu(),
                TitleIndexList = bfn.getIndexTitle()

            };

            return View(model);
        
        }

        [HttpGet]
        [ValidateInput(false)]
        public ActionResult Create() {
          
            eVisaContext db = new eVisaContext();
            BaseFunction bfn = new BaseFunction();
            var model = new PageView
            {
                LanguageList = bfn.getLanguage(2),
                MenuList = bfn.getPageMenu(),
                TitleIndexList = bfn.getIndexTitle()
                
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create(PageView viewModel)
        {
            if (ModelState.IsValid) {
                Page p = new Page();

                    p.Title = viewModel.Title;
                    p.Name = viewModel.Name;
                    p.Content = viewModel.Content;
                    p.UpdatedDate = DateTime.Now;
                    p.CreatedDate = DateTime.Now;
                    p.CreatedBy = User.Identity.Name;
                    p.UpdatedBy = User.Identity.Name;
                    p.Link_Code = int.Parse(viewModel.Name);
                    p.Description = viewModel.Description;
                    p.Language_Code = viewModel.Language_Code;
                    p.IsActive = 1;

                db.Pages.Add(p);
                db.SaveChanges();

                return RedirectToAction("Index", "Page");
            }

            BaseFunction bfn = new BaseFunction();
            var model = new PageView
            {
                LanguageList = bfn.getLanguage(2),
                MenuList = bfn.getPageMenu(),
                TitleIndexList = bfn.getIndexTitle()
               
            };

            return View(model);
        }

        [HttpGet]
        public ActionResult Edit(int? id) {

            BaseFunction bfn = new BaseFunction();
            if (!id.HasValue)
            {
                ModelState.AddModelError("res", "error");
                return View();
            }
            
            var model = db.Pages.Find(id);
          
                  var modelview = new PageView
                  {
                      Id = model.Id,
                      Name = model.Name,
                      Title = model.Title,
                      Link_Code = model.Link_Code,
                      Content = model.Content.Replace(System.Environment.NewLine,""),
                      Language_Code = model.Language_Code,
                      Description = model.Description,
                      LanguageList = bfn.getLanguage(1),
                      MenuList = bfn.getPageMenu(),
                      TitleIndexList = bfn.getIndexTitle()

                  };

            
            return View(modelview);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(PageView viewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                  
                    var entity = db.Pages.Find(viewModel.Id);
                    

                    if (entity != null) {
                            
                            entity.Content = viewModel.Content;
                            entity.UpdatedBy = User.Identity.Name;
                            entity.UpdatedDate = DateTime.Now;
                            entity.Title = viewModel.Title;
                            entity.Description = viewModel.Description;
                            entity.Name = viewModel.Name;
                            entity.Link_Code = int.Parse(viewModel.Name);
                            entity.Language_Code = viewModel.Language_Code;
                            db.Entry(entity).State = EntityState.Modified;
                            db.SaveChanges();
                    }


                    return RedirectToAction("View", "Page", new {@Id = entity.Id });
                }
            }
            catch (Exception e) {

                var a = e.Message;
            }

            return View();
        }

        [HttpGet]
        public ActionResult View(int? id)
        {

            BaseFunction bfn = new BaseFunction();
            if (!id.HasValue)
            {
                return View();
            }

            var model = db.Pages.Find(id);

            var modelview = new PageView
            {
                Id = model.Id,
                Name = model.Name,
                Title = model.Title,
                Link_Code = model.Link_Code,
                Content = model.Content.Replace(System.Environment.NewLine, ""),
                Language_Code = model.Language_Code,
                Description = model.Description,
                LanguageList = bfn.getLanguage(1),
                MenuList = bfn.getPageMenu(),
                TitleIndexList = bfn.getIndexTitle(),
                Links = bfn.getLinked(model.Link_Code)
            };


            return View(modelview);
        }

 


        [HttpPost]
        public JsonResult getListPage(string code)
        {

                var viewModel = db.Pages.Where(e => e.Language_Code == code & e.IsActive == 1 )
                    .Select(e => new PageView()
                    {
                        Id = e.Id,
                        Name = e.Name,
                        Link_Code = e.Link_Code,
                        Language_Code = e.Language_Code,
                        Title = e.Title,
                        Content = e.Content
                    })
                  .AsEnumerable();
                return Json(viewModel, JsonRequestBehavior.AllowGet);
            

        }

        public JsonResult AjaxDelete(int? id)
        {
            if (!id.HasValue)
            {
                return Json(new { result = false, message = string.Format("Invalid", "Paper") });
            }

            var entity = db.Pages.Find(id);

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