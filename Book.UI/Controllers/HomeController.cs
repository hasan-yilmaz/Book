using Book.BIZ;
using Book.DATA;
using Book.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Book.UI.Controllers
{
    public class HomeController : Controller
    {
        BookEntities db;
        AppUserOperation appUserOperation;

        public HomeController()
        {
            db = new BookEntities();
            appUserOperation = new AppUserOperation(db);
        }
        
        public ActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public ActionResult Insert()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Insert(AppUserCRUDModel model)
        {
            bool IsHaveToMail = db.AppUser.Where(s => s.Email.Equals(model.Email) && s.Password.Equals(model.Password) && s.IsActive).Any();
            if (IsHaveToMail)
            {
                ViewBag.result = "Bu mail daha önce kullanılmış. Lütfen yeni bir mail adresi deneyiniz...";
                return View();
            }
            AppUser appUser = new AppUser()
            {
                AppUserId = model.AppUserId,
                FullName = model.FullName,
                Email = model.Email,
                Password = model.Password,
                IsActive = true
            };

            appUserOperation.Insert(appUser);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginCRUDModel model)
        {
            AppUser appUser = db.AppUser.Where(s => s.Email.Equals(model.Email) && s.Password.Equals(model.Password) && s.IsActive).SingleOrDefault();

            if (appUser != null)
            {
                Session.Add("LoggedUser", appUser);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.result = "Kullanıcı adı veya şifre hatalı lütfen tekrar deneyiniz...";
                return View();
            }

        }

        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }
    }
}