using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GuiaNet.Models
{
    public class ClientBrandsCls
    {
        public int? BrandId { get; set; }
        public string BrandName { get; set; }
        public int? ClientId { get; set; }
        public bool? Active { get; set; }
        public byte? ClientTypeId { get; set; }
        public byte? ClientBrandTypeId { get; set; }
        public string ExpireDescription { get; set; }
    }
}