using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GuiaNet.Models
{
    public class GetBrandsByClients
    {
        public int BrandId { get; set; }
        public string BrandName { get; set; }
        public int ClientId { get; set; }
        public string CompanyName { get; set; }
        public int EditionId { get; set; }
        public int NumberEdition { get; set; }
        public int Count { get; set; }
        public string Description { get; set; }
        public string Logo { get; set; }
    }
}