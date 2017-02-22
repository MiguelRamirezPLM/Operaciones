using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GuiaNet.Models
{
    public class createcategories
    {
        Guia db = new Guia();
        Categories Categories = new Categories();

        public bool newcategories(string categoryname)
        {
            var cat = (from category in db.Categories
                       where category.Description == categoryname
                       select category).ToList();
            if (cat.LongCount() == 0)
            {
                Categories.Active = true;
                Categories.Description = categoryname.Trim();
                Categories.ParentId = null;

                db.Categories.Add(Categories);
                db.SaveChanges();
            }
            else
            {
                return false;
            }
            return true;
        }
    }
}