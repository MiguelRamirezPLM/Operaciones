using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GuiaNet.Models
{
    public class GetProductsByClientsR
    {
        public int? ClientId { get; set; }
        public int? ClientIdParent { get; set; }
        public string CompanyName { get; set; }
        public string ShortName { get; set; }
        public int? ProductId { get; set; }
        public int? ParentIdP { get; set; }
        public string ProductName { get; set; }
        public byte? TypeId { get; set; }
        public string Paticipante { get; set; }
        public int? EditionId { get; set; }
        public int? NumberEdition { get; set; }
        public int? BookId { get; set; }
        public string BookName { get; set; }
        public int? QuantityOfProducts { get; set; }
    }
}