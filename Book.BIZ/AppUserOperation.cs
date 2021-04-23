using Book.DATA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book.BIZ
{

    public class AppUserOperation
    {
        BookEntities db;

        public AppUserOperation(BookEntities _db)
        {
            db = _db;
        }


        public List<AppUser> GetAllAppUser(int appUserId)
        {
            List<AppUser> appUserList = db.AppUser.Where(s => s.AppUserId == appUserId).ToList();
            return appUserList;
        }

        public void Insert(AppUser appUser)
        {
            db.AppUser.Add(appUser);
            db.SaveChanges();
        }

        public AppUser GetById(int id)
        {
            AppUser appUser = db.AppUser.Where(s => s.AppUserId == id && s.IsActive).SingleOrDefault();
            return appUser;
        }

        public void Update(AppUser appUser)
        {
            db.Entry(appUser).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
        }
    }
}
