using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GuiaNet.Models
{
    public class GetProductStatusByClient
    {
        public int? ProductId { get; set; }
        public string ProductName { get; set; }
        public int? StatusId { get; set; }
        public string StatusName { get; set; }
        public int? ClientId {get;set;}
        public string CompanyName { get; set; }
        public int? NumberEdition { get; set; }
    }
}