using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GuiaNet.Models
{
    public class StatusByProduct
    {
        public int? StatusId { get; set; }
        public string StatusName { get; set; }
        public bool? Active { get; set; }
        public int? Reference { get; set; }
    }
}