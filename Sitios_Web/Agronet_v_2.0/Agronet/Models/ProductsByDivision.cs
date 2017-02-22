using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Agronet.Models
{
    public class ProductsByDivision
    {
        public int? ProductId { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public string Register { get; set; }

        public int? PharmaFormId { get; set; }
        public string PharmaForm { get; set; }

        public int? CategoryId { get; set; }
        public string CategoryName { get; set; }

        public int? SIDEF { get; set; }
        public int? PP { get; set; }
        public int? MP { get; set; }
        public int? NP { get; set; }
        public int? CC { get; set; }
    }
}