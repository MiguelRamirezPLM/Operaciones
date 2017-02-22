using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Guianet.Models
{
    public class ClientBrandsCls
    {
        public int? BrandId { get; set; }
        public string BrandName { get; set; }
        public string ExpireDescription { get; set; }
        public int? Distributor { get; set; }
        public int? Representative { get; set; }
        public int? Count { get; set; }
        public string CompanyName { get; set; }
        public string Type { get; set; }
        public int? NumberEdition { get; set; }
        public string Adviser { get; set; }
    }
}