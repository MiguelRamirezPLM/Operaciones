using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Guianet.Models
{
    public class GetClients
    {
        public int? ClientId { get; set; }
        public string CompanyName { get; set; }
        public string ShortName { get; set; }
        public byte? AlphabetId { get; set; }
        public byte? CountryId { get; set; }
        public int? Participant { get; set; }

        public int? CountAddress { get; set; }
        public int? Location { get; set; }
    }
}