using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgroNet.Models
{
    public class JSON
    {
        public ProductCategories ProductCategories { get; set; }
        public Products Products { get; set; }
        public Divisions Divisions { get; set; }
        public ProductPharmaForms ProductPharmaForms { get; set; }
        public PharmaForms PharmaForms { get; set; }
    }
}