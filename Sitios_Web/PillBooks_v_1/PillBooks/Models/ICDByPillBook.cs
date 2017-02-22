using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PillBooks.Models
{
    public class ICDByPillBook
    {
        public int? ICDIdN1 { get; set; }
        public string ICDKeyN1 { get; set; }
        public int? ParentIdN1 { get; set; }
        public string SpanishDescriptionN1 { get; set; }
        public int? LevelN1 { get; set; }
        public bool? ActiveN1 { get; set; }

        public int? ICDIdN2 { get; set; }
        public string ICDKeyN2 { get; set; }
        public int? ParentIdN2 { get; set; }
        public string SpanishDescriptionN2 { get; set; }
        public int? LevelN2 { get; set; }
        public bool? ActiveN2 { get; set; }

        public int? ICDIdN3 { get; set; }
        public string ICDKeyN3 { get; set; }
        public int? ParentIdN3 { get; set; }
        public string SpanishDescriptionN3 { get; set; }
        public int? LevelN3 { get; set; }
        public bool? ActiveN3 { get; set; }
    }
}