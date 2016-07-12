using eVisa.Function;
using eVisa.Models;
using eVisa.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eVisa.Controllers
{
    public class ContactController : Controller
    {
        private eVisaContext db = new eVisaContext();
        //
        // GET: /Contact/
        [HttpGet]
        public ActionResult Create()
        {
            BaseFunction bfn = new BaseFunction();

            var model = new ContactView
            {
                LanguageList = bfn.getLanguage(1)

            };

            return View(model);
        }

        [HttpPost]
        public ActionResult Create(ContactView viewModel)
        {
            BaseFunction bfn = new BaseFunction();

            var model = new ContactView
            {
                LanguageList = bfn.getLanguage(1)

            };

            if (ModelState.IsValid)
            {
                var entity = new Contact()
                {
                    GeneralEmail = viewModel.GeneralEmail,
                    GeneralFax = viewModel.GeneralFax,
                    GeneralTel = viewModel.GeneralTel,
                    PaymentTel = viewModel.PaymentTel,
                    Paymentemail = viewModel.Paymentemail,
                    PaymentFax = viewModel.PaymentFax,
                    guidelink = viewModel.guidelink,
                    Photoguide = viewModel.Photoguide,
                    Photohelp = viewModel.Photohelp,

                    CreatedBy = User.Identity.Name,
                    CreatedDate = DateTime.Now,
                    UpdatedBy = User.Identity.Name,
                    UpdatedDate = DateTime.Now,
                    IsActive = 1
                };

                db.Contacts.Add(entity);
                db.SaveChanges();

                return View(model);

            }

            return View(model);
        }


        public JsonResult getContact(string code)
        {
            if (code != null)
            {
                var entity = db.Contacts.Select(e => new ContactView()
                {
                    Id = e.Id,
                    GeneralTel = e.GeneralTel,
                    GeneralEmail = e.GeneralEmail,
                    GeneralFax = e.GeneralFax,
                    Paymentemail = e.Paymentemail,
                    PaymentFax = e.PaymentFax,
                    PaymentTel = e.PaymentTel,
                    Photoguide = e.Photoguide,
                    Photohelp = e.Photohelp
                }).ToList();

                return Json(entity, JsonRequestBehavior.AllowGet);
            }

            return Json(new { result = true, message = string.Format("Delete Success", "Paper") });

        }
	}
}