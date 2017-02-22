using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Guianet.Models
{
    public class ParticipantProductsClasif
    {
        public int? ProductId { get; set; }
        public string ProductName { get; set; }
        public string RegisterSanitary { get; set; }
        public string BarCode { get; set; }
        public int? BarCodeId { get; set; }
        public int? Qtty { get; set; }
        public int? PP { get; set; }
        public int? MP { get; set; }
        public int? CC { get; set; }
        public int? SC { get; set; }
        public int? SIDEF { get; set; }
        public int? Validation { get; set; }
        public string Page { get; set; }
        public int? HTML { get; set; }
        public int? Works { get; set; }
    }
}