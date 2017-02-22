using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Guianet.Models
{
    public class GetBrandsByClient
    {
        public string CompanyName { get; set; }
        public string ShortName { get; set; }
        public string CompanyNameClient { get; set; }
        public int? Count { get; set; }
        public string Adviser { get; set; }
    }
}