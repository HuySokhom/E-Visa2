using eVisa.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace eVisa.Controllers
{
    //public class UsersController : Controller
    //{
    //    private eVisaDBContext db = new eVisaDBContext();

    //    // GET: /User/

    //    public ActionResult Index()
    //    {
    //        return View(db.userprofile.ToList());
    //    }

    //    //
    //    // GET: /User/Details/5

    //    public ActionResult Details(int id = 0)
    //    {

    //        UserProfile userprofile = db.userprofile.Find(id);
    //        if (userprofile == null)
    //        {
    //            return HttpNotFound();
    //        }
    //        return View(userprofile);
    //    }

    //    //
    //    // GET: /User/Create

    //    public ActionResult Create()
    //    {
    //        SelectList list = new SelectList(Roles.GetAllRoles());
    //        ViewBag.Roles = list;

    //        return View();
    //    }

    //    //
    //    // POST: /User/Create

    //    //[HttpPost]
    //    //[ValidateAntiForgeryToken]
    //    //public ActionResult Create(UserProfile userprofile, string RoleName)
    //    //{
    //    //    if (ModelState.IsValid)
    //    //    {
    //    //        db.userprofile.Add(userprofile);
    //    //        db.SaveChanges();
    //    //        SelectList list = new SelectList(Roles.GetAllRoles());
    //    //        ViewBag.Roles = list;

    //    //        if (Roles.IsUserInRole(userprofile.UserName, RoleName))
    //    //        {

    //    //            ViewBag.ResultMessage = "This user already has the role specified !";
    //    //        }
    //    //        else
    //    //        {
    //    //            Roles.AddUserToRole(userprofile.UserName, RoleName);
    //    //            ViewBag.ResultMessage = "Username added to the role succesfully !";
    //    //        }
    //    //        return RedirectToAction("Index");
    //    //    }

    //    //    return View(userprofile);
    //    //}

    //    //
    //    // GET: /User/Edit/5

    //    public ActionResult Edit(int id = 0)
    //    {
    //        UserProfile userprofile = db.userprofile.Find(id);
    //        SelectList list = new SelectList(Roles.GetAllRoles());
    //        ViewBag.Roles = list;
    //        if (userprofile == null)
    //        {
    //            return HttpNotFound();
    //        }
    //        return View(userprofile);
    //    }

    //    //
    //    // POST: /User/Edit/5

    //    //[HttpPost]
    //    //[ValidateAntiForgeryToken]
    //    //public ActionResult Edit(UserProfile userprofile, string RoleName)
    //    //{
    //    //    SelectList list = new SelectList(Roles.GetAllRoles());
    //    //    ViewBag.Roles = list;
    //    //    if (ModelState.IsValid)
    //    //    {
    //    //        db.Entry(userprofile).State = EntityState.Modified;
    //    //        db.SaveChanges();

    //    //        Roles.RemoveUserFromRole(userprofile.UserName,
    //    //            Roles.GetRolesForUser(userprofile.UserName)[0]);
    //    //        Roles.AddUserToRole(userprofile.UserName, RoleName);

    //    //        return RedirectToAction("Index");
    //    //    }
    //    //    return View(userprofile);
    //    //}

    //    //
    //    // GET: /User/Delete/5

    //    public ActionResult Delete(int id = 0)
    //    {
    //        UserProfile userprofile = db.userprofile.Find(id);
    //        if (userprofile == null)
    //        {
    //            return HttpNotFound();
    //        }
    //        return View(userprofile);
    //    }


    //    // POST: /User/Delete/5

    //    [HttpPost, ActionName("Delete")]
    //    [ValidateAntiForgeryToken]
    //    public ActionResult DeleteConfirmed(int id)
    //    {
    //        UserProfile userprofile = db.userprofile.Find(id);
    //        db.userprofile.Remove(userprofile);
    //        db.SaveChanges();
    //        return RedirectToAction("Index");
    //    }
    //    [HttpGet]
    //    public ActionResult Userlogs()
    //    {
    //        using (eVisaDBContext dc = new eVisaDBContext())
    //        {
    //            //ePassportContext db = new ePassportContext();
    //            //var usr = db.UserProfiles.ToList();
    //            //ViewBag.usr = new SelectList(usr, "UserName", "UserName");
    //            SelectList list = new SelectList(db.userprofile.ToList(), "UserName", "UserName");
    //            ViewBag.Users = list;
    //            return View(dc.userprofile.ToList());

    //        }
    //    }

      
    //    protected override void Dispose(bool disposing)
    //    {
    //        db.Dispose();
    //        base.Dispose(disposing);
    //    }
    //}
}