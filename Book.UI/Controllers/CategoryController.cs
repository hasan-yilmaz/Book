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

            Category category = new Category();

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

        [HttpGet]
        public ActionResult Insert(int id=0)
        {
            return View();            
        }

        [HttpPost]
        public ActionResult Insert(CategoryCRUDModel model)
        {
            AppUser appUser = (AppUser)Session["LoggedUser"];

            if (appUser != null)
            {
                Category category = new Category()
                {
                    CategoryId = model.CategoryId,
                    Name = model.Name,
                    IsActive = true
                };
                categoryOperation.Insert(category);

                return RedirectToAction("Index", "Category");
            }
            else
            {
                return RedirectToAction("Login","Home");
            }
        }

        [HttpGet]
        public ActionResult Update(int id = 0)
        {
            AppUser appUser = (AppUser)Session["LoggedUser"];

            if (appUser != null)
            {
                Category category = categoryOperation.GetById(id);

                if (category != null)
                {
                    CategoryCRUDModel categoryCRUDModel = new CategoryCRUDModel();

                    categoryCRUDModel.CategoryId = category.CategoryId;
                    categoryCRUDModel.Name = category.Name;

                    return View(categoryCRUDModel);
                }
                else
                {
                    return RedirectToAction("Index", "Category");
                }
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }          
        }

        [HttpPost]
        public ActionResult Update(CategoryCRUDModel model)
        {
            if (Session["LoggedUser"] != null)
            {
                Category category = categoryOperation.GetById(model.CategoryId);

                category.CategoryId = model.CategoryId;
                category.Name = model.Name;

                categoryOperation.Update(category);
            }

            return RedirectToAction("Index", "Category");
        }

        
    }
}