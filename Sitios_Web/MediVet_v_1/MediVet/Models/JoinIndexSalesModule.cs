using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MediVet.Models
{
    public class JoinIndexSalesModule
    {
        public Products Products { get; set; }
        public ProductPharmaForms ProductPharmaForms { get; set; }
        public PharmaceuticalForms PharmaceuticalForms { get; set; }
        public ProductCategories ProductCategories { get; set; }
        public Categories Categories { get; set; }
    }
}