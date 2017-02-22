using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Guianet.Models
{
    public class GetAddedWorksByClientId
    {
        public int? WorkId { get; set; }
        public int? EditionId { get; set; }
        public int? NumberEdition { get; set; }
        public int? ClientId { get; set; }
        public string CompanyName { get; set; }
        public int? ProductId { get; set; }
        public string ProductName { get; set; }
        public string WorkDescription { get; set; }
        public string WorkDescriptionLevelThree { get; set; }
        public string AddedDate { get; set; }
        public string UserName { get; set; }
        public string Module { get; set; }
        public string Details { get; set; }
        public bool Active { get; set; }

        public int? Count { get; set; }
        public int? CountP { get; set; }
        public string Adviser { get; set; }
        public string Adviser1 { get; set; }

    }
}