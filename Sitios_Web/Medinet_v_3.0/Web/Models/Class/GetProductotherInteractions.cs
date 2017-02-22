using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models.Class
{
    public class GetProductotherInteractions
    {
        public int? CategoryId { get; set; }
        public int? DivisionId { get; set; }
        public int? PharmaFormId { get; set; }
        public int? ProductId { get; set; }
        public int? ActiveSubstanceId { get; set; }
        public int? ElementId { get; set; }
        public string ActiveSubstance { get; set; }
        public string ElementName { get; set; }

    }
}