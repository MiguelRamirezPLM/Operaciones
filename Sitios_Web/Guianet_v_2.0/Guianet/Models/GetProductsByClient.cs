using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Guianet.Models
{
    public class GetProductsByClient
    {
        public int? ProductId { get; set; }
        public string ProductName { get; set; }
        public int? ClientId { get; set; }
        public string RegisterSanitary { get; set; }
        public int? BarCodeId { get; set; }
        public string BarCode { get; set; }
        public int? SIDEF { get; set; }
        public int? PP { get; set; }
        public int? MP { get; set; }
        public int? CC { get; set; }
        public int? SC { get; set; }
        public int? Count { get; set; }
        public string CompanyName { get; set; }
        public int? NumberEdition { get; set; }
        public string Description { get; set; }
    }
}