using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Guianet.Models
{
    public class plm_vwProductsByClientRegisterandBarcode
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public Nullable<byte> TypeId { get; set; }
        public int ClientId { get; set; }
        public string RegisterSanitary { get; set; }
        public Nullable<int> BarCodeId { get; set; }
        public string BarCode { get; set; }
        public int PharmaFormId { get; set; }
        public string PharmaForm { get; set; }
        public int ActiveSubstanceId { get; set; }
        public string ActiveSubstance { get; set; }
        public int PresentationId { get; set; }
        public string Description { get; set; }
        public int SIDEF { get; set; }
        public int PP { get; set; }
        public int MP { get; set; }
    }
}