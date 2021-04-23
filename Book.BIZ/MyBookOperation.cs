using Book.DATA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book.BIZ
{
    public class MyBookOperation
    {
        BookEntities db;

        public MyBookOperation(BookEntities _db)
        {
            db = _db;
        }


        public List<MyBook> GetAllBook(int appUserId)
        {
            List<MyBook> myBookList = db.MyBook.Where(s => s.BookId == appUserId).ToList();
            return myBookList;
        }

        public void Insert(MyBook myBook)
        {
            db.MyBook.Add(myBook);
            db.SaveChanges();
        }

        public MyBook GetById(int id)
        {
            MyBook myBook = db.MyBook.Where(s => s.BookId == id).SingleOrDefault();
            return myBook;
        }

        public void Update(MyBook myBook)
        {
            db.Entry(myBook).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
        }
    }
}
