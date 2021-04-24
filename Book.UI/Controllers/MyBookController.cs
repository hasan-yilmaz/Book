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

        public MyBookController()
        {
            db = new BookEntities();
            myBookOperation = new MyBookOperation(db);
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


    }
}