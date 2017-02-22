using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgroNet.Models
{
    public class JoinProducts
    {
        public plm_vwProductsByEdition plm_vwProductsByEdition { get; set; }
        public ProductCategories ProductCategories { get; set; }
        public Products Products { get; set; }
    }
}