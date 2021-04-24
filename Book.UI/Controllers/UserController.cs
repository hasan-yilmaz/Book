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
    public class UserController : Controller
    {
        BookEntities db;
        AppUserOperation appUserOperation;

        public UserController()
        {
            db = new BookEntities();
            appUserOperation = new AppUserOperation(db);
        }

        public ActionResult Index()
        {
            AppUser appUser = (AppUser)Session["LoggedUser"];
            if (appUser != null)
            {
                List<AppUser> appUserList = appUserOperation.GetAllAppUser(appUser.AppUserId);

                List<AppUserListVİewModel> appUserListVİewModelList = new List<AppUserListVİewModel>();

                foreach (AppUser app in appUserList)
                {
                    appUserListVİewModelList.Add(new AppUserListVİewModel()
                    {
                        AppUserId = app.AppUserId,
                        FullName = app.FullName,
                        Email = app.Email,
                        Password = app.Password,
                        IsActive = app.IsActive
                    });
                }
                return View(appUserListVİewModelList);
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
           
        }
    }
}