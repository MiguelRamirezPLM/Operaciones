using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class plm_spGetProductsToEditByDivisionMedinet3
    {
        public int? ProductId { get; set; }
        public string Brand { get; set; }
        public string ProductDescription { get; set; }
        public bool? ProductActive { get; set; }

        public int? PharmaFormId { get; set; }
        public string PharmaForm { get; set; }
        public bool? PharmaActive { get; set; }

        public int? DivisionId { get; set; }
        public string DivisionName { get; set; }
        public string DivisionShortName { get; set; }

        public int? CategoryId { get; set; }
        public string CategoryName { get; set; }

        public int? LaboratoryId { get; set; }
        public string LaboratoryName { get; set; }

        public string AdDescription { get; set; }

        public int? Participant { get; set; }
        public int? Mentionated { get; set; }
        public int? NewProduct { get; set; }
        public int? ModifiedContent { get; set; }

        public string Editions { get; set; }

        public int? Sidef { get; set; }
        public string SanitaryRegister { get; set; }
        public string SSFraction { get; set; }

        public int? ContentFamilyId { get; set; }
        public string ContentFamilyString { get; set; }

        public int? PSFamilyId { get; set; }
        public string PSFamilyString { get; set; }
        public string ProductType { get; set; }
        public byte? ProductTypeId { get; set; }
        public string HTMLContent { get; set; }
        
    }
}