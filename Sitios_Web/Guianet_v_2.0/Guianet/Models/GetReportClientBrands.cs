using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Guianet.Models
{
    public class GetReportClientBrands
    {
        public int? EditionId { get; set; }
        public int? NumberEdition { get; set; }

        public int? ClientId { get; set; }
        public string CompanyName { get; set; }

        public int? BrandId { get; set; }
        public string BrandName { get; set; }

        public byte? ClientBrandTypeId {get;set;}
        public string Description { get; set; }

        public int? Count { get; set; }
        public string Date { get; set; }
        public string DateOfReport { get; set; }

        public string UserName { get; set; }
        public string Executive { get; set; }
        public DateTime AddedDate { get; set; }
    }
}