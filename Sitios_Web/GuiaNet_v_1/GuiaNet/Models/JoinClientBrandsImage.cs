using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GuiaNet.Models
{
    public class JoinClientBrandsImage
    {
        public ClientBrands ClientBrands { get; set; }
        public Brands Brands { get; set; }
        public ClientBrandTypes ClientBrandTypes { get; set; }
    }
}