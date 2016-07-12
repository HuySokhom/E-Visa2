using eVisa.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eVisa.Controllers
{
   
        public class AdminController : Controller {

            private ApplicationDbContext db = new ApplicationDbContext();

            //
            // GET: /Admin/
            public ActionResult Index()
            {

                if (!Request.IsAuthenticated)
                {
                    return RedirectToAction("Login", "Account");
                }
                //var Roles = db.Roles.ToList();
                return View();
            }
        }
       
	
}