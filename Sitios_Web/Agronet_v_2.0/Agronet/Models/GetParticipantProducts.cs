using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Agronet.Models
{
    public class GetParticipantProducts
    {
        public int? CountryId { get; set; }
        public string CountryName { get; set; }
        public int? LaboratoryId { get; set; }
        public string LaboratoryName { get; set; }
        public bool? LabActive { get; set; }
        public int? DivisionId { get; set; }
        public string DivisionName { get; set; }
        public string DivisionShortName { get; set; }
        public bool? DivisionActive { get; set; }
        public int? CategoryId { get; set; }
        public string CategoryName { get; set; }
        public bool? CategoryActive { get; set; }
        public int? ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public string Register { get; set; }
        public bool? ProductActive { get; set; }
        public int? PharmaFormId { get; set; }
        public string PharmaForm { get; set; }
        public bool? PharmaActive { get; set; }
        public int? EditionId { get; set; }
        public int? NumberEdition { get; set; }
        public string ISBN { get; set; }
        public string BarCode { get; set; }
        public bool? EditionActive { get; set; }
        public string TypeInEdition { get; set; }
        public string Page { get; set; }
        public int? BookId { get; set; }
        public string BookName { get; set; }
        public bool? BookActive { get; set; }
        public string NewProduct { get; set; }
        public string ContentType { get; set; }
        public int? NumberOfActiveSubstances { get; set; }
        public int? NumberOfCrops { get; set; }
        public int? NumberOfSeeds { get; set; }
        public int? NumberOfAgrochemicalUses { get; set; }
        public int? SIDEF { get; set; }
    }
}