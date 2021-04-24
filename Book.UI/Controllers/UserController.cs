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

        [HttpGet]
        public ActionResult Update(int id = 0)
        {
            AppUser appUser = (AppUser)Session["LoggedUser"];

            if (appUser != null)
            {
                AppUser app = appUserOperation.GetById(id);

                if (app != null)
                {
                    AppUserCRUDModel appUserCRUDModel = new AppUserCRUDModel();

                    appUserCRUDModel.AppUserId = app.AppUserId;
                    appUserCRUDModel.FullName = app.FullName;
                    appUserCRUDModel.Email = app.Email;
                    appUserCRUDModel.Password = app.Password;

                    return View(appUserCRUDModel);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
           
        }

        [HttpPost]
        public ActionResult Update(AppUserCRUDModel model)
        {
            if (Session["LoggedUser"] != null)
            {
                AppUser appUser = appUserOperation.GetById(model.AppUserId);

                appUser.AppUserId = model.AppUserId;
                appUser.FullName = model.FullName;
                appUser.Email = model.Email;
                appUser.Password = model.Password;

                appUserOperation.Update(appUser);
            }
            return RedirectToAction("Index","Home");
        }

        public ActionResult ActiveOrPassive(int id = 0)
        {
            if (Session["LoggedUser"] != null)
            {
                AppUser appUser = db.AppUser.Where(s => s.AppUserId == id).SingleOrDefault();


                if (appUser != null) 
                {
                    appUser.IsActive = false;
                }
                else
                {
                    appUser.IsActive = true;
                }

                return RedirectToAction("Index", "Home");
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
           
        }
    }
}