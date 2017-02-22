using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Guianet.Models
{
    public class GetProductsByClientReport
    {
        public int? ProductId { get; set; }
        public string ProductName { get; set; }
        public int? ClientId { get; set; }
        public string RegisterSanitary { get; set; }
        public int? BarCodeId { get; set; }
        public string BarCode { get; set; }
        public string SIDEF { get; set; }
        public string PP { get; set; }
        public string MP { get; set; }
        public string CC { get; set; }
        public string SC { get; set; }
        public string CompanyName { get; set; }
        public int? EditionId { get; set; }
        public int? Count { get; set; }
        public string CategoryThree { get; set; }
        public string LeafCategory { get; set; }
        public string Adviser { get; set; }

        public int? CountProduct { get; set; }
    }
}