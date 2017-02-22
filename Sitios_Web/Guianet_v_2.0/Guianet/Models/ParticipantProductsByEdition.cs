using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Guianet.Models
{
    public class ParticipantProductsByEdition
    {
        public int? ProductId { get; set; }
        public string ProductName { get; set; }

        public int? ClientId { get; set; }
        public string CompanyName { get; set; }
        public string ShortName { get; set; }

        public int? HTMLContent { get; set; }
        public int? Content { get; set; }
    }
}