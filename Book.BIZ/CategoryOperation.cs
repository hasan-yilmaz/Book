using Book.DATA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book.BIZ
{
    public class CategoryOperation
    {
        BookEntities db;

        public CategoryOperation(BookEntities _db)
        {
            db = _db;
        }

        public void Insert(Category category)
        {
            db.Category.Add(category);
            db.SaveChanges();
        }

        public List<Category> GetAllCategory(int appUserId)
        {
            List<Category> categoryList = db.Category.Where(s => s.CategoryId == appUserId).ToList();
            return categoryList;
        }


        public Category GetById(int id)
        {
            Category category = db.Category.Where(s => s.CategoryId == id).SingleOrDefault();
            return category;
        }

        public void Update(Category category)
        {
            db.Entry(category).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
        }
    }
}
