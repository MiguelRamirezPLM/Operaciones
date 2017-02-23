using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class ReadProductCategories
    {
        public int? DivisionId { get; set; }
        public int? CategoryId { get; set; }
        public int? PharmaFormId { get; set; }
        public int? ProductId { get; set; }
        public bool? TechnicalSheet { get; set; }
    }
}