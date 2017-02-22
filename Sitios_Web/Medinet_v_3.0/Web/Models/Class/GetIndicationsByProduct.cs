using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models.Class
{
    public class GetIndicationsByProduct
    {
        public int? IndicationId { get; set; }
        public int? ParentId { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; }
    }

    public class GetIndications
    {
        public List<GetIndicationsByProduct> GetIndicationsByProduct { get; set; }
        public List<GetIndicationsByProduct> GetIndicationssWithoutProduct { get; set; }
    }
}