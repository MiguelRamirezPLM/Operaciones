using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Guianet.Models
{
    public class GetInternationalClients
    {
        public int? ClientId { get; set; }
        public string CompanyName { get; set; }
        public string ShortName { get; set; }
        public int? Participant { get; set; }
    }
}