using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models.Class
{
    public class GetHypersensibilities
    {
        public int? HypersensibilityId { get; set; }
        public string HypersensibilityName { get; set; }
    }

    public class GetProductHypersensibilities
    {
        public int? CategoryId { get; set; }
        public int? DivisionId { get; set; }
        public int? PharmaFormId { get; set; }
        public int? ProductId { get; set; }
        public int? ActiveSubstanceId { get; set; }
        public int? HypersensibilityId { get; set; }
        public string ActiveSubstance { get; set; }
        public string HypersensibilityName { get; set; }
    }
}