using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using eVisa.Models;
using System.Data.Entity;
using System.Web.Security;
using eVisa.ViewModel;
using PagedList;
namespace eVisa.Controllers
{

    [Authorize]
    public class AccountController : Controller
    {
       
        public AccountController()
            : this(new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext())))
        {

        }
        private ApplicationDbContext db = new ApplicationDbContext();
        public AccountController(UserManager<ApplicationUser> userManager)
        {
            UserManager = userManager;
        }

        public UserManager<ApplicationUser> UserManager { get; private set; }
        

        public ActionResult Index() {
            
            return View(db.Users.ToList());
        }

        public JsonResult getUserList(string search)
        {

            List<RegisterViewModel> list = new List<RegisterViewModel>();
            
            var entity = db.Users
            .Where(e => e.IsActive == 1 && e.UserName.StartsWith(search))
            .Select(e => new RegisterViewModel()
            {
                Id = e.Id,
                UserName = (e.UserName == null ? "" : e.UserName),
                FirstName = (e.FirstName == null ? "" : e.FirstName),
                LastName = (e.LastName == null ? "" : e.LastName),
                Email = (e.Email == null ? "" : e.Email),
                CreatedBy = (e.CreatedBy == null ? "" : e.CreatedBy),
                CreatedDate = e.CreatedDate,
                Address = (e.Address == null ? "" : e.Address)

            });

            foreach (var e in entity) {
                list.Add(new RegisterViewModel() {
                    Id = e.Id,
                    UserName = (e.UserName == null ? "" : e.UserName),
                    FirstName = (e.FirstName == null ? "" : e.FirstName),
                    LastName = (e.LastName == null ? "" : e.LastName),
                    Email = (e.Email == null ? "" : e.Email),
                    CreatedBy = (e.CreatedBy == null ? "" : e.CreatedBy),
                    CreatedDate = e.CreatedDate,
                    Address = (e.Address == null ? "" : e.Address),
                    Role = (UserManager.GetRoles(e.Id).Any() ? UserManager.GetRoles(e.Id).First() : string.Empty)
                });
            }

          
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public JsonResult getRoleList()
        {
            var entity = db.Roles
                .Select(e => new RoleView()
                {
                    Id = e.Id,
                    Name=e.Name
                }).ToList();

            return Json(entity, JsonRequestBehavior.AllowGet);
        }
           
        [HttpGet]
        public ActionResult EditProfile(string id)
        {

            var entity = db.Users.Find(id);
            var model = new RegisterViewModel() { 
                Id= entity.Id,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                UserName = entity.UserName,
                Address = entity.Address,
                Email = entity.Email
                
            };

            return View(model);
        }
        
            
        [HttpPost]   
        public ActionResult EditProfile(RegisterViewModel viewModel)
        {
                var entity = db.Users.Find(viewModel.Id);

                entity.Address = viewModel.Address;
                entity.UserName = viewModel.UserName;
                entity.LastName = viewModel.LastName;
                entity.FirstName = viewModel.FirstName;
                entity.Email = viewModel.Email;
                entity.UpdatedBy = User.Identity.Name;

                db.Entry(entity).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index", "Admin");
         
        }

        [HttpGet]
        public ActionResult Edit(string id) {

            var entity = db.Users.Find(id);
            var model = new RegisterViewModel() { 
                Id= entity.Id,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                UserName = entity.UserName,
                Address = entity.Address,
                Email = entity.Email
                
            };

            return Json(model, JsonRequestBehavior.AllowGet);
        }
        
        [HttpPost]
        public ActionResult Edit(RegisterViewModel viewModel)
        {
                var entity = db.Users.Find(viewModel.Id);

                entity.Address = viewModel.Address;
                entity.UserName = viewModel.UserName;
                entity.LastName = viewModel.LastName;
                entity.FirstName = viewModel.FirstName;
                entity.Email = viewModel.Email;
                entity.UpdatedBy = User.Identity.Name;

                db.Entry(entity).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Register", "Account");
          
        }

        [HttpGet]
        public ActionResult View(string id)
        {

            var entity = db.Users.Find(id);
            var model = new RegisterViewModel()
            {
                Id = id ,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                UserName = entity.UserName,
                Address = entity.Address,
                Email = entity.Email

            };

            return View(model);
        }

        public JsonResult AjaxDelete(string id)
        {
            if (id == null)
            {
                return Json(new { result = false, message = string.Format("Invalid", "Paper") });
            }

            var entity = db.Users.Find(id);

            if (entity == null)
                return Json(new { result = false, message = string.Format("Delete Fail", "Paper") });

            db.Users.Remove(entity);
            db.Entry(entity).State = EntityState.Deleted;
            var sta = db.SaveChanges();
            //Json(new { result = false, message = string.Format("Fail to delete", "Paper") })

            return Json(new { result = true, message = string.Format("Delete Success", "User") });
        }

        [HttpGet]
        public ActionResult Role()
        {
            var Roles = db.Roles.ToList();
            return View(Roles);
        }

        [HttpPost]
        public ActionResult Role(string name){
           if(name.Length > 0){
            
               try
               {
                   var role = new IdentityRole()
                   {
                       Name = name
                   };
                   db.Roles.Add(role);
                   db.SaveChanges();
                   return RedirectToAction("Register", "Account");
               }
               catch (Exception e) {
                   var res = e.Message;
                   return View();
               }
              
               return RedirectToAction("Register", "Account");
           }
            return View();
        }

        [HttpGet]
        public ActionResult EditRole() {
            return View();
        }

        [HttpPost]
        public ActionResult EditRole(RoleView viewModel)
        {
            return View();
        }

        


        public ActionResult Users(string id)
        {
            var model = db.Users.Where(s => s.Id == id);
            var view = model.Select(e => new ManageUserViewModel
            {
                Id=e.Id,
                LastName=e.LastName,
                FirstName=e.FirstName,
                Email = e.Email,
                Address=e.Address,
                CreatedBy =e.CreatedBy,
                CreatedDate=e.CreatedDate,
                IsActive = e.IsActive
            });
            return View(view.ToList());
        }

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindAsync(model.UserName, model.Password);
                if (user != null)
                {
                    await SignInAsync(user, model.RememberMe);
                    //return RedirectToLocal(returnUrl);
                    return RedirectToAction("Index", "Admin");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid username or password.");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            ViewBag.RoleList = db.Roles.Select(e => new SelectListItem() { 
                Value=e.Id,
                Text=e.Name
            });

            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            { 
                var user = new ApplicationUser() { 
                    UserName = model.UserName,
                    LastName=model.LastName, 
                    Email=model.Email,
                    Address = model.Address,
                    CreatedBy = User.Identity.GetUserName()
                };

                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    
                   // await SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Register", "Account");
                }
                else
                {
                    AddErrors(result);
                    
                }
            }

            ViewBag.RoleList = db.Roles.Select(e => new SelectListItem()
            {
                Value = e.Id,
                Text = e.Name
            });

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [HttpGet]
        public ActionResult ConfigureRole(string id)
        {
            if (id != "") {
                var entity = db.Users.Find(id);
                entity.Id = id;
                var model = new UserView() { 
                    Id=id,
                    UserName=entity.UserName
                };
                
                return Json(model, JsonRequestBehavior.AllowGet);
            }


            return Json("error", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> ConfigureRole(string id , string role)
        {
            if (id != "" && role != "")
            {
                var entity = db.Roles.Find(role);

                foreach (var item in db.Roles)    
                {
                    if(UserManager.IsInRole(id,item.Name)){
                        /* Remove role from user */
                        await this.UserManager.RemoveFromRoleAsync(id, item.Name);
                    }
                }

                await this.UserManager.AddToRoleAsync(id,entity.Name);

                return RedirectToAction("Register", "Account");
            }

            return RedirectToAction("Register", "Account");
        }

        [HttpGet]
        public ActionResult Resetpassword(string id) {

            if (id != "" && id != null) {
                var entity = db.Users.Find(id);

                var model = new UserView() {
                    Id=id,
                    UserName=entity.UserName
                };

                return Json(model, JsonRequestBehavior.AllowGet);
            }

            return Json(null, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public async Task<ActionResult> Resetpassword(string resetuserId, string Password, string ConfirmPassword)
        {
            if (ModelState.IsValid)
            {
                IdentityResult result = await UserManager.RemovePasswordAsync(resetuserId);
                   
                if (result.Succeeded)
                {
                  IdentityResult res = await UserManager.AddPasswordAsync(resetuserId,Password);
                    ModelState.AddModelError("result", "Reset Password Success !");
                    return RedirectToAction("Register", "Account");
                }
                else
                {
                    AddErrors(result);
                    ModelState.AddModelError("result", "Reset Password Fail !");
                    return Json("error", JsonRequestBehavior.AllowGet);
                }
            }

            return Json("result", JsonRequestBehavior.AllowGet);
        }

        //
        // POST: /Account/Disassociate
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Disassociate(string loginProvider, string providerKey)
        {
            ManageMessageId? message = null;
            IdentityResult result = await UserManager.RemoveLoginAsync(User.Identity.GetUserId(), new UserLoginInfo(loginProvider, providerKey));
            if (result.Succeeded)
            {
                message = ManageMessageId.RemoveLoginSuccess;
            }
            else
            {
                message = ManageMessageId.Error;
            }
            return RedirectToAction("Manage", new { Message = message });
        }

        //
        // GET: /Account/Manage
        public ActionResult Manage(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.ChangePasswordSuccess ? "Your password has been changed."
                : message == ManageMessageId.SetPasswordSuccess ? "Your password has been set."
                : message == ManageMessageId.RemoveLoginSuccess ? "The external login was removed."
                : message == ManageMessageId.Error ? "An error has occurred."
                : "";
            ViewBag.HasLocalPassword = HasPassword();
            ViewBag.ReturnUrl = Url.Action("Manage");
            return View();
        }

        //
        // POST: /Account/Manage
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Manage(ManageViewModel model)
        {
            bool hasPassword = HasPassword();
            ViewBag.HasLocalPassword = hasPassword;
            ViewBag.ReturnUrl = Url.Action("Manage");
            if (hasPassword)
            {
                if (ModelState.IsValid)
                {
                    IdentityResult result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Manage", new { Message = ManageMessageId.ChangePasswordSuccess });
                    }
                    else
                    {
                        AddErrors(result);
                    }
                }
            }
            else
            {
                // User does not have a password so remove any validation errors caused by a missing OldPassword field
                ModelState state = ModelState["OldPassword"];
                if (state != null)
                {
                    state.Errors.Clear();
                }

                if (ModelState.IsValid)
                {
                    IdentityResult result = await UserManager.AddPasswordAsync(User.Identity.GetUserId(), model.NewPassword);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Manage", new { Message = ManageMessageId.SetPasswordSuccess });
                    }
                    else
                    {
                        AddErrors(result);
                    }
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // POST: /Account/ExternalLogin
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            // Request a redirect to the external login provider
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        }

        //
        // GET: /Account/ExternalLoginCallback
        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                return RedirectToAction("Login");
            }

            // Sign in the user with this external login provider if the user already has a login
            var user = await UserManager.FindAsync(loginInfo.Login);
            if (user != null)
            {
                await SignInAsync(user, isPersistent: false);
                return RedirectToLocal(returnUrl);
            }
            else
            {
                // If the user does not have an account, then prompt the user to create an account
                ViewBag.ReturnUrl = returnUrl;
                ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
                return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { UserName = loginInfo.DefaultUserName });
            }
        }

        //
        // POST: /Account/LinkLogin
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LinkLogin(string provider)
        {
            // Request a redirect to the external login provider to link a login for the current user
            return new ChallengeResult(provider, Url.Action("LinkLoginCallback", "Account"), User.Identity.GetUserId());
        }

        //
        // GET: /Account/LinkLoginCallback
        public async Task<ActionResult> LinkLoginCallback()
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync(XsrfKey, User.Identity.GetUserId());
            if (loginInfo == null)
            {
                return RedirectToAction("Manage", new { Message = ManageMessageId.Error });
            }
            var result = await UserManager.AddLoginAsync(User.Identity.GetUserId(), loginInfo.Login);
            if (result.Succeeded)
            {
                return RedirectToAction("Manage");
            }
            return RedirectToAction("Manage", new { Message = ManageMessageId.Error });
        }

        //
        // POST: /Account/ExternalLoginConfirmation
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Manage");
            }

            if (ModelState.IsValid)
            {
                // Get the information about the user from the external login provider
                var info = await AuthenticationManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    return View("ExternalLoginFailure");
                }
                var user = new ApplicationUser() { UserName = model.UserName };
                var result = await UserManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await UserManager.AddLoginAsync(user.Id, info.Login);
                    if (result.Succeeded)
                    {
                        await SignInAsync(user, isPersistent: false);
                        return RedirectToLocal(returnUrl);
                    }
                }
                AddErrors(result);
            }

            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }

        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/ExternalLoginFailure
        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }

        [ChildActionOnly]
        public ActionResult RemoveAccountList()
        {
            var linkedAccounts = UserManager.GetLogins(User.Identity.GetUserId());
            ViewBag.ShowRemoveButton = HasPassword() || linkedAccounts.Count > 1;
            return (ActionResult)PartialView("_RemoveAccountPartial", linkedAccounts);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && UserManager != null)
            {
                UserManager.Dispose();
                UserManager = null;
            }
            base.Dispose(disposing);
        }

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private async Task SignInAsync(ApplicationUser user, bool isPersistent)
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
            var identity = await UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            AuthenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = isPersistent }, identity);
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private bool HasPassword()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                return user.PasswordHash != null;
            }
            return false;
        }

        public enum ManageMessageId
        {
            ChangePasswordSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
            Error
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        private class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri) : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties() { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion
    }
}