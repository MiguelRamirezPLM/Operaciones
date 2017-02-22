using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Guianet.Models
{
    public class GetReportEditionClients
    {
        public int? EditionId { get; set; }
        public int? NumberEdition { get; set; }

        public int? ClientId { get; set; }
        public string CompanyName { get; set; }

        public byte? ClientTypeId { get; set; }
        public string TypeName { get; set; }

        public string Page { get; set; }

        public int? Count { get; set; }
        public string Date { get; set; }

        public string UserName { get; set; }

    }
}