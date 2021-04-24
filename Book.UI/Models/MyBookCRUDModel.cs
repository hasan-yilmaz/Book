using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Book.UI.Models
{
    public class MyBookCRUDModel
    {
        public int BookId { get; set; }

        public string Name { get; set; }

        public string Writer { get; set; }

        public int CategoryId { get; set; }
        public SelectList CategoryList { get; set; }

        public string ImagePath { get; set; }

        public HttpPostedFileBase ImageFile { get; set; }
    }
}