using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using eVisa.Models;
using eVisa.ViewModel;

namespace eVisa.Controllers
{
    public class CountryController : Controller
    {
        private eVisaContext db = new eVisaContext();
        
        // GET: /Country/
        [HttpGet]
        public ActionResult Index()
        {
            var results = db.Countries.ToList();

            return new JsonResult() { Data = results, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
	}
}