using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models.Class
{
    public class plm_spGetProductsbyDivisionbyCountry
    {
        public string Division { get; set; }
        public int Divisionid { get; set; }
        public int CategoryId { get; set; }
        public int PharmaFormId { get; set; }
        public int ProductId { get; set; }
        public string Product { get; set; }
        public string PharmaceuticalForm { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public string ProductType { get; set; }

        public int? ActiveSubstances { get; set; }
        public int? TherapeuticUses { get; set; }
        public int? ProductICD { get; set; }
        public int? ProductTherapeuticOMS { get; set; }
        public int? ProductPharmaFormRoutes { get; set; }
        public int? ProductIndications { get; set; }

        public int? StatusIdI { get; set; }
        public string Interactions { get; set; }

        public int? StatusIdC { get; set; }
        public string Contraindications { get; set; }
    }
}