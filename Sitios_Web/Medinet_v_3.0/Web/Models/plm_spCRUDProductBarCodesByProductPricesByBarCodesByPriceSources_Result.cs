using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class plm_spCRUDProductBarCodesByProductPricesByBarCodesByPriceSources_Result
    {
        public int PresentationId { get; set; }
        public int DivisionId { get; set; }
        public int CategoryId { get; set; }
        public int ProductId { get; set; }
        public int PharmaFormId { get; set; }
        public string Brand { get; set; }
        public string Presentation { get; set; }
        public int BarCodeId { get; set; }
        public decimal? AveragePrice { get; set; }
        public double? Price { get; set; }
        public string BarCode { get; set; }
        public byte? PriceSourceId { get; set; }
        public string SourceName { get; set; }

    }
}