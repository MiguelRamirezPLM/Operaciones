using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models.Class
{
    public class ClasificationSubstances
    {
        public List<Foods> Foods { get; set; }
        public List<Foods> FoodsAsoc { get; set; }
    }

    public class ClasificationSeverities
    {
        public List<IMASeverities> IMASeverities { get; set; }
        public List<IMASeverities> IMASeveritiesAsoc { get; set; }
    }

    public class DetailsOfSeverity
    {
        public string ClinicalReference { get; set; }
        public List<ClinicalReferences> LSClinicalReferences { get; set; }
    }
}