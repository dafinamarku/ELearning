using E_learning.DataLayer;
using E_learning.DomainModels;
using E_learning.Extensions;
using E_learning.ServiceInterface;
using E_learning.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace E_learning.Controllers
{
    public class AccountController : Controller
    {

        IUserService userService;
        public AccountController(IUserService serv)
        {
          this.userService = serv;
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel model)
        {
          if (ModelState.IsValid)
          {
            var appDbContext = new ProjectDbContext();
            List<ApplicationUser> searchUsers = appDbContext.Users.Where(m => m.UserName == model.Username || m.Email == model.Email).ToList();
            if (searchUsers.Count > 0)
            {
              ModelState.AddModelError("Err", "Nje perdorues me te njetin Email OSE Username tashme ekziston ne sistem.");
              return View(model);
            }
            var userStore = new ApplicationUserStore(appDbContext);
            var userManager = new ApplicationUserManager(userStore);
            var passwordHash = Crypto.HashPassword(model.Password);
            ApplicationUser user = new ApplicationUser() { Email = model.Email, UserName = model.Username, PasswordHash = passwordHash, Emri=model.Emri, Mbiemri=model.Mbiemri };
            IdentityResult result = userManager.Create(user);

            if (result.Succeeded)
            {
              userManager.AddToRole(user.Id, "Student");
              return RedirectToAction("Login");
            }
          }
          return View(model);
        }

        public ActionResult Login()
        {
          return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel lvm)
        {
          if (ModelState.IsValid)
          {
            var appDbContext = new ProjectDbContext();
            var userStore = new ApplicationUserStore(appDbContext);
            var userManager = new ApplicationUserManager(userStore);
            var user = userManager.Find(lvm.Username, lvm.Password);
            if (user != null)
            {
              //login
              var authenticationManager = HttpContext.GetOwinContext().Authentication;
              var userIdentity = userManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
              authenticationManager.SignIn(new AuthenticationProperties(), userIdentity);
              return Redirect("/Home/Index");
            }
            else
            {
              ModelState.AddModelError("myerror", "Invalid username or password");
              return View(lvm);
            }
          }
          return View(lvm);

        }

        [Authorize]
        public ActionResult Logout()
        {
          var authenticationManager = HttpContext.GetOwinContext().Authentication;
          authenticationManager.SignOut();
          return RedirectToAction("Index", "Home");
        }

        [Authorize]
        public ActionResult UserProfile()
        {
          string uid = User.Identity.GetUserId();
          var model = userService.GetUserById(uid);
          return View(model);
        }

        [HttpPost]
        [Authorize]
        public ActionResult UpdatePicture(HttpPostedFileBase file)
        {
          string userId = User.Identity.GetUserId();
          string[] allowedFileTypes = { ".png", ".jpeg", ".jpg" };
          var extension = Path.GetExtension(file.FileName);
          ApplicationUser currentUser = userService.GetUserById(userId);
          if (file != null)
          {
            if (allowedFileTypes.Contains(extension))
            {
              //Update Image
              currentUser.Photo=file.FileName;
              var path = Path.Combine(Server.MapPath(MaterialPaths.ProfileImagesPath), file.FileName);
              userService.UpdateUser(currentUser);
              file.SaveAs(path);
              return RedirectToAction("UserProfile");
            }
          }
          ViewBag.Photo = "Duhet te zgjidhni nje foto.";
          return RedirectToAction("UserProfile");
        }

        [Authorize]
        public ActionResult ChangePassword()
        {
          string userId = User.Identity.GetUserId();
          ViewBag.UserId = userId;
          return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult ChangePassword(ChangePasswordVM obj)
        {
          if (ModelState.IsValid)
          {
            var appDbContext = new ProjectDbContext();
            var userStore = new ApplicationUserStore(appDbContext);
            var userManager = new ApplicationUserManager(userStore);
            string userId = User.Identity.GetUserId();
            var currentUser = userService.GetUserById(userId);
            var userIdentification = userManager.Find(currentUser.UserName, obj.OldPassword);
            if (userIdentification==null)
            {
              ModelState.AddModelError("OldPassword", "Passwordi i vjeter nuk eshte i sakte");
            }
            else
            {
              IdentityResult result=userManager.ChangePassword(userId, obj.OldPassword, obj.NewPassword );
              if (result.Succeeded)
                this.AddNotification("Passwordi u ndryshua.", NotificationType.SUCCESS);
              else
                this.AddNotification("Passwordi nuk u ndryshua.", NotificationType.ERROR);
              return RedirectToAction("UserProfile");
            }
          }
          return View();
        }
  }
}
