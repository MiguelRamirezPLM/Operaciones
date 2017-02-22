using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MediVet.Models
{
    public class classreportproductsbydivisionSM
    {
        public string ProductName { get; set; }
        public string Description { get; set; }
        public int NumberEdition { get; set; }
        public string DivisionName { get; set; }
        public string PharmaForm { get; set; }
        public string CategoryName { get; set; }
        public string RegisterSanitary { get; set; }
        public string Type { get; set; }
        public string NewP { get; set; }
        public string Active { get; set; }

        public int CategoryId { get; set; }
        public int PharmaFormId { get; set;}
        public int ProductId { get; set; }
        public int Count { get; set; }
    }
}