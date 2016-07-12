using eVisa.Function;
using eVisa.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eVisa.Controllers
{
    public class ChildrenController : Controller
    {
        private eVisaContext db = new eVisaContext();
        private BaseFunction fun = new BaseFunction();
        //
        // GET: /Children/
        public ActionResult Index() 
        {
            return View();
        }

        /// <summary>
        /// HTTP POST Children
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Save(Children model)
        {
            
            Children ch = new Children();
            // check if has id update 
            if (model.id > 0)
            {
                ch = db.Children.Find(model.id);
                ch.ChildDob = model.ChildDob;
                ch.ChildGivenName = model.ChildGivenName;
                ch.ChildSurName = model.ChildSurName;
                ch.ChildPhoto = model.ChildPhoto;
                ch.ChildSex = model.ChildSex;
                ch.UpdatedDate = DateTime.Now;
                db.Entry(ch).State = EntityState.Modified;
                db.SaveChanges();

                return Json(new { success = true, message = "update" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                ch.ChildDob = model.ChildDob;
                ch.ChildGivenName = model.ChildGivenName;
                ch.ChildSurName = model.ChildSurName;
                ch.ChildPhoto = model.ChildPhoto;
                ch.ChildSex = model.ChildSex;
                ch.UpdatedDate = DateTime.Now;
                ch.Status = 1;
                ch.CreatedDate = DateTime.Now;
                ch.Line1 = model.Line1;
                ch.ReferenceNo = model.ReferenceNo;
                ch.StatusDate = DateTime.Now;
                var count = db.Children.Count(c => c.ReferenceNo == model.ReferenceNo && c.Line1 == model.Line1);
                if (count < 3)
                {
                    ch.Line2 = count + 1;
                    db.Children.Add(ch);
                    db.SaveChanges();
                    return Json(new { success = true }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, message = "We accept only 3 child." }, JsonRequestBehavior.AllowGet);
                }
            }
        }


        /*******************
         *  Delete Child ***
         ******************/
        [HttpPost]
        public ActionResult Delete(Children model)
        {
            //*****************************************************************//
            //********* Verify data Children to delete to avoid ***************//
            //********* Hacker try to delete POST via url *********************//
            //*****************************************************************//
            int countAppChild = db.Children.Count(c => c.ReferenceNo == model.ReferenceNo && c.Line1 == model.Line1);
            if (countAppChild > 0)
            {
                Children children = db.Children.Find(model.id);
                db.Children.Remove(children);
                db.SaveChanges();
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false }, JsonRequestBehavior.AllowGet); ;
            }
        }
        
	}
}