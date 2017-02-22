using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GuiaNet.Models
{
    public class GetSpecialitiesbyClient
    {
        public int SpecialityId { get; set; }
        public string Description { get; set; }
        public int ClientId { get; set; }
        public string CompanyName { get; set; }
        public int EditionId { get; set; }
        public int NumberEdition { get; set; }
        public int Count { get; set; }
        public string AdversDescription { get; set; }
        public string Quantity { get; set; }
        public string CId { get; set; }
    }
}