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
    public class MyBookController : Controller
    {
        BookEntities db;
        MyBookOperation myBookOperation;
        CategoryOperation categoryOperation;

        public MyBookController()
        {
            db = new BookEntities();
            myBookOperation = new MyBookOperation(db);
            categoryOperation = new CategoryOperation(db);
        }


        public ActionResult Index()
        {
            AppUser appUser = (AppUser)Session["LoggedUser"];

            if (appUser != null)
            {
                List<MyBook> myBookList = myBookOperation.GetAllBook(appUser.AppUserId);

                List<MyBookListViewModel> myBookListViewModelList = new List<MyBookListViewModel>();

                foreach (MyBook book in myBookList)
                {
                    myBookListViewModelList.Add(new MyBookListViewModel()
                    {
                        BookId = book.BookId,
                        Name = book.Name,
                        Writer = book.Writer,
                        CategoryId = book.CategoryId,
                        ImagePath = book.Image,
                        IsActive = book.IsActive
                    });
                }
                return View(myBookListViewModelList);
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }

        [HttpGet]
        public ActionResult Insert(int id=0)
        {
            AppUser appUser = (AppUser)Session["LoggedUser"];

            if (appUser != null)
            {
                MyBookCRUDModel myBookCRUDModel = new MyBookCRUDModel();

                List<Category> categoryList = categoryOperation.GetAllCategory(appUser.AppUserId);
                myBookCRUDModel.CategoryList = new SelectList(categoryList, "CategoryId", "Name");

                return View(myBookCRUDModel);
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }

        [HttpPost]
        public ActionResult Insert(MyBookCRUDModel model)
        {
            AppUser appUser = (AppUser)Session["LoggedUser"];

            if (appUser != null)
            {

                #region Upload

                string Imagee = string.Empty;

                if (model.ImageFile != null)
                {
                    Imagee = model.ImageFile.FileName;
                    model.ImageFile.SaveAs(Server.MapPath("~/Content/Picture/") + model.ImageFile.FileName);
                }

                #endregion
                MyBook myBook = new MyBook()
                {
                    
                    BookId = model.BookId,
                    AppUserId=appUser.AppUserId,
                    Name=model.Name,
                    Writer=model.Writer,
                    CategoryId=model.CategoryId,
                    Image=model.ImagePath,
                    IsActive=true
                };
                myBookOperation.Insert(myBook);

                return RedirectToAction("Index", "MyBook");
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }


    }
}