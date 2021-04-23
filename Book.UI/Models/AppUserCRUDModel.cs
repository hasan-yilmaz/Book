using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Book.UI.Models
{
    public class AppUserCRUDModel
    {
        public int AppUserId { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }
    }
}