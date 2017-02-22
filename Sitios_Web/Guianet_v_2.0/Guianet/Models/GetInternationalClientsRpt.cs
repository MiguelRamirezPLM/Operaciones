using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Guianet.Models
{
    public class GetInternationalClientsRpt
    {
        public int? ClientId { get; set; }
        public string CompanyName { get; set; }
        public string ShortName { get; set; }
        public int? Participant { get; set; }
        public int? Count { get; set; }
        public string Adviser { get; set; }
        public int? NumberEdition { get; set; }
    }
}