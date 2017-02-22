using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GuiaNet.Models
{
    public class GetProductsByEditionR
    {
        public int? EditionId {get;set;}
		public int? NumberEdition {get;set;}
		public int? ClientId {get;set;}
		public int? ClientIdParent {get;set;}
        public string CompanyName { get; set; }
		public string ProductName {get;set;}
        public int? QuantityOfProducts { get; set; }
    }
}