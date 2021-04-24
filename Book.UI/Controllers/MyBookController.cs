using Book.BIZ;
using Book.DATA;
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

            return View();
        }
    }
}