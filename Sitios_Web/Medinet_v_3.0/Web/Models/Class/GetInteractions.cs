using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web.Models;
using Web.Models.Class;

namespace Web.Models.Class
{
    public class GetInteractions
    {
        public List<GetProductSubstanceInteractions> ProductSubstanceInteractions { get; set; }
        public List<GetProductPharmaGroupInteractions> ProductPharmaGroupInteractions { get; set; }
        public List<GetProductotherInteractions> ProductotherInteractions { get; set; }
        public List<GetActiveSubstancesWithoutInteractions> ActiveSubstancesWithoutInteractions { get; set; }
        public List<GetPharmacologicalGroupsWithoutInteraction> PharmacologicalGroupsWithoutInteraction { get; set; }
        public List<GetOtherElemensWithoutInteractions> OtherElemensWithoutInteractions { get; set; }

        public List<GetActiveSubstancesWithoutInteractions> ActiveSubstancesByProduct { get; set; }
        public String HTMLContent { get; set; }
    }
}