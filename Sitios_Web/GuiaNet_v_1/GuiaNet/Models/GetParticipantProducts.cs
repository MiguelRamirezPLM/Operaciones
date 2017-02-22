using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GuiaNet.Models
{
    public class GetParticipantProducts
    {
        public int? ProductId { get; set; }
        public string ProductName { get; set; }
        public int? Qtty { get; set; }
        public int? QttyHC { get; set; }
        public string CompanyName { get; set; }
        public int? NumberEdition { get; set; }
        public string StatusName { get; set; }
    }
}