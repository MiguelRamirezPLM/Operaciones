using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class Union_companies_cb
    {
        public Companies Companies { get; set; }
        public CompanyBrands CompanyBrands { get; set; }
        public Brands Brands { get; set; }
        public CompanyPhones CompanyPhones { get; set; }
        public CompanyTypes CompanyTypes { get; set; }
        public Cities Cities { get; set; }
    }
}