using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models.Class
{
    public class ProductContraindicationsByICD
    {
        public int? CategoryId { get; set; }
        public int? DivisionId { get; set; }
        public int? PharmaFormId { get; set; }
        public int? ProductId { get; set; }
        public int? ActiveSubstanceId { get; set; }
        public int? ICDId { get; set; }
        public string ActiveSubstance { get; set; }
        public string SpanishDescription { get; set; }
        public string ICDKey { get; set; }
        public string ParentICDKey { get; set; }
    }
}