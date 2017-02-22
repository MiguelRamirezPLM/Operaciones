using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models.Class
{
    public class GetICDByProductPharmaform
    {
        public int? ICDId { get; set; }
        public int? ParentId { get; set; }
        public string ICDKey { get; set; }
        public string SpanishDescription { get; set; }
        public string EnglishDescription { get; set; }
        public bool? Active { get; set; }
        public string ParentICDKey { get; set; }
    }
}