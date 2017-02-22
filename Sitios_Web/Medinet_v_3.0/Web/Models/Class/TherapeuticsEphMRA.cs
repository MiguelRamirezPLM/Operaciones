using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models.Class
{
    public class TherapeuticsEphMRA
    {
        public int? TherapeuticId { get; set; }
        public int? ParentId { get; set; }
        public string TherapeuticKey { get; set; }
        public string SpanishDescription { get; set; }
        public string Description { get; set; }
        public bool? Active { get; set; }
        public int? TherapeuticSons { get; set; }
    }
}