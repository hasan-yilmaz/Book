using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Book.UI.Models
{
    public class CategoryCRUDModel
    {
        public int CategoryId { get; set; }

        public int AppUserId { get; set; }

        public string Name { get; set; }
      
    }
}