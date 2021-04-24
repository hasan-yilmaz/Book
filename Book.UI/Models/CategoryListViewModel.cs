using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Book.UI.Models
{
    public class CategoryListViewModel
    {
        public int CategoryId { get; set; }

        public string Name { get; set; }

        public bool IsActive { get; set; }
    }
}