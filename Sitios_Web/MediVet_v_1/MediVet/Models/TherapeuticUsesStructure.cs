using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MediVet.Models
{
    public class TherapeuticUsesStructure
    {
        	public int? TherapeuticId4 {get;set;}
            public int? ParentId4 { get; set; }
			public string TherapeuticName4 {get;set;}
            public int? Nivel4 { get; set; }
			public bool? Active4 {get;set;}

            public int? TherapeuticId3 { get; set; }
            public int? ParentId3 { get; set; }
			public string TherapeuticName3 {get;set;}
            public int? Nivel3 { get; set; }
            public bool? Active3 { get; set; }

            public int? TherapeuticId2 { get; set; }
            public int? ParentId2 { get; set; }
			public string TherapeuticName2 {get;set;}
            public int? Nivel2 { get; set; }
            public bool? Active2 { get; set; }

            public int? TherapeuticId1 { get; set; }
            public int? ParentId1 { get; set; }
			public string TherapeuticName1 {get;set;}
            public int? Nivel1 { get; set; }
            public bool? Active1 { get; set; }

            public int? TherapeuticId { get; set; }
            public int? ParentId { get; set; }
            public string TherapeuticName { get; set; }
            public int? Nivel { get; set; }
            public bool? Active { get; set; }
    }
}