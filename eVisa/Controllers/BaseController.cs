using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eVisa.Controllers
{
    public class BaseController : Controller
    {
        //
        // GET: /Base/
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult LanguageEven(string language,string url,string name) 
        {
            Session["language"] = language;
            Session["image"] = url;
            Session["langname"] = name;

            return Json("success", JsonRequestBehavior.AllowGet);
        }
	}
}