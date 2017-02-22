using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GuiaNet.Models
{
    public class joinproductsclientproducts
    {
        public Products Products { get; set; }
        public ClientProducts ClientProducts { get; set; }
        public BarCodes BarCodes { get; set; }
        public ClientProductBarCodes ClientProductBarCodes { get; set; }
    }
}