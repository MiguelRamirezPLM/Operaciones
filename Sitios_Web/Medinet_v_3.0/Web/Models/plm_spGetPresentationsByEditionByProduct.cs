using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class plm_spGetPresentationsByEditionByProduct
    {
        public int PresentationId { get; set; }
        public int DivisionId { get; set; }
        public string DivisionName { get; set; }
        public string DivisionShortName { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int ProductId { get; set; }
        public string Brand { get; set; }
        public int PharmaFormId { get; set; }
        public string PharmaForm { get; set; }
        public int? QtyExternalPack { get; set; }
        public int? ExternalPackId { get; set; }
        public string ExternalPackName { get; set; }
        public int? QtyInternalPack { get; set; }
        public int? InternalPackId { get; set; }
        public string InternalPackName { get; set; }
        public string QtyContentUnit { get; set; }
        public int? ContentUnitId { get; set; }
        public string ContentUnitName { get; set; }
        public string QtyWeightUnit { get; set; }
        public int? WeightUnitId { get; set; }
        public string WeightUnitName { get; set; }
        public string Presentation { get; set; }
        public string Editions { get; set; }
        public int? ProductImageId { get; set; }
        public string ProductShot { get; set; }
        public string View { get; set; }
        public long LongCount { get; set; }
    }
}