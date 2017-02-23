using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class Union_Brands
    {
        public Countries countrs { get; set; }
        public Editions editins { get; set; }
        public CompanyEditions compeditins { get; set; }
        public Companies comp { get; set; }
        public CompanyBrands compbrands { get; set; }
        public Brands brans { get; set; }
        public Indexes indexes { get; set; }
       
    }
}