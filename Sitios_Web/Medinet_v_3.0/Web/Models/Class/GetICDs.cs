using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models.Class
{
    public class GetICDs
    {
        public int? ICDId { get; set; }
        public int? ParentId { get; set; }
        public string ICDKey { get; set; }
        public string SpanishDescription { get; set; }
        public string EnglishDescription { get; set; }
        public bool? Active { get; set; }
        public int? ICDsONS { get; set; }
    }

    public class GetContraindications
    {
        public List<GetActiveSubstancesWithoutInteractions> GetActiveSubstancesWithoutInteractions { get; set; }
        public List<GetICDs> GetICDs { get; set; }
        public List<GetICDByProductPharmaform> GetICDByProductPharmaform { get; set; }
        public List<GetProductPhysContraindications> GetProductPhysContraindications { get; set; }
        public List<GetPhysiologicalContraindications> GetPhysiologicalContraindications { get; set; }
        public List<GetPharmaContraindications> GetPharmaContraindications { get; set; }
        public List<ProductPharmaContraindications> GetProductPharmaContraindications { get; set; }
        public List<GetPharmacologicalGroupsWithoutInteraction> GetPharmacologicalGroupsWithoutInteraction { get; set; }
        public List<GetProductPharmaGroupInteractions> GetProductPharmaGroupInteractions { get; set; }
        public List<GetHypersensibilities> GetHypersensibilities { get; set; }
        public List<GetProductHypersensibilities> GetProductHypersensibilities { get; set; }
        public List<GetActiveSubstances> GetActiveSubstances { get; set; }
        public List<GetProductSubstanceContraindications> GetProductSubstanceContraindications { get; set; }
        public List<GetOtherElemensWithoutInteractions> GetOtherElemensWithoutInteractions { get; set; }
        public List<GetProductotherInteractions> GetProductOtherInteractions { get; set; }
        public List<GetProductCommentContraindications> GetProductCommentContraindications { get; set; }
        public String HTMLContraindications { get; set; }
    }
}