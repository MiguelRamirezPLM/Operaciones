using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GuiaNet.Models
{
    public class JoinBrandImagesSizes
    {
        public ClientBrands ClientBrands { get; set; }
        public Brands Brands { get; set; }
        public BrandImageSizes BrandImageSizes { get; set; }
        public ImagesSizes ImagesSizes { get; set; }
    }
}