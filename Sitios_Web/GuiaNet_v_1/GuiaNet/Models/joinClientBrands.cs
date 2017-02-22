using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GuiaNet.Models
{
    public class joinClientBrands
    {
        public ClientBrands ClientBrands { get; set; }
        public Brands Brands { get; set; }
        public Clients Clients { get; set; }
    }
}