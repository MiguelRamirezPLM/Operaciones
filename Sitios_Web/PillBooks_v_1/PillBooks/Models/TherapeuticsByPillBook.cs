using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PillBooks.Models
{
    public class TherapeuticsByPillBook
    {
        public int? TherapeuticIdNivel4 { get; set; }
        public int? ParentIdNivel4 { get; set; }
        public string SpanishDescriptionNivel4 { get; set; }
        public bool? ActiveNivel4 { get; set; }
        public string TherapeuticKeyNivel4 {get;set;}

        public int? TherapeuticIdNivel3 { get; set; }
        public int? ParentIdNivel3 { get; set; }
        public string SpanishDescriptionNivel3 { get; set; }
        public bool? ActiveNivel3 { get; set; }
        public string TherapeuticKeyNivel3 { get; set; }

        public int? TherapeuticIdNivel2 { get; set; }
        public int? ParentIdNivel2 { get; set; }
        public string SpanishDescriptionNivel2 { get; set; }
        public bool? ActiveNivel2 { get; set; }
        public string TherapeuticKeyNivel2 { get; set; }

        public int? TherapeuticIdNivel1 { get; set; }
        public int? ParentIdNivel1 { get; set; }
        public string SpanishDescriptionNivel1 { get; set; }
        public bool? ActiveNivel1 { get; set; }
        public string TherapeuticKeyNivel1 { get; set; }
    }


    public class level1
    {
        public int? TherapeuticIdNivel1 { get; set; }
        public int? ParentIdNivel1 { get; set; }
        public string SpanishDescriptionNivel1 { get; set; }
        public bool? ActiveNivel1 { get; set; }
    }

    public class level2
    {
        public int? TherapeuticIdNivel2 { get; set; }
        public int? ParentIdNivel2 { get; set; }
        public string SpanishDescriptionNivel2 { get; set; }
        public bool? ActiveNivel2 { get; set; }

    }

    public class level3
    {
        public int? TherapeuticIdNivel3 { get; set; }
        public int? ParentIdNivel3 { get; set; }
        public string SpanishDescriptionNivel3 { get; set; }
        public bool? ActiveNivel3 { get; set; }
    }

    public class level4
    {
        public int? TherapeuticIdNivel4 { get; set; }
        public int? ParentIdNivel4 { get; set; }
        public string SpanishDescriptionNivel4 { get; set; }
        public bool? ActiveNivel4 { get; set; }
    }
}