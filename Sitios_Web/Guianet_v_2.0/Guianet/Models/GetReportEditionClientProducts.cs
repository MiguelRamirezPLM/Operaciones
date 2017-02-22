using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Guianet.Models
{
    public class GetReportEditionClientProducts
    {
        public int? EditionId { get; set; }
        public int? NumberEdition { get; set; }

        public int? ClientId { get; set; }
        public string CompanyName { get; set; }

        public int? ProductId { get; set; }
        public string ProductName { get; set; }

        public string RegisterSanitary { get; set; }
        public string BarCode { get; set; }

        public string CC { get; set; }
        public string SC { get; set; }
        public string PP { get; set; }
        public string MP { get; set; }
        public string SIDEF { get; set; }

        public int? Count { get; set; }
        public string Date { get; set; }
        public string DateOfReport { get; set; }

        public string UserName { get; set; }
        public string Executive { get; set; }
        public DateTime AddedDate { get; set; }
    }
}