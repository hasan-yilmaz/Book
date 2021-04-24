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
    public class CategoryController : Controller
    {
        BookEntities db;
        CategoryOperation categoryOperation;

        public CategoryController()
        {
            db = new BookEntities();
            categoryOperation = new CategoryOperation(db);
        }

        public ActionResult Index()
        {
            AppUser appUser = (AppUser)Session["LoggedUser"];

            if (appUser != null)
            {
                List<Category> categoryList = categoryOperation.GetAllCategory(appUser.AppUserId);

                List<CategoryListViewModel> categoryListViewModelList = new List<CategoryListViewModel>();

                foreach (Category cat in categoryList)
                {
                    categoryListViewModelList.Add(new CategoryListViewModel()
                    {
                        CategoryId = cat.CategoryId,
                        Name=cat.Name,
                        IsActive=cat.IsActive
                    });
                }
                return View(categoryListViewModelList);
            }
            else
            {
                return RedirectToAction("Login","Home");
            }
        }

       
    }
}