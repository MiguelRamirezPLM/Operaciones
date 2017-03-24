using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models.Class
{
    public class GetActiveSubstancesWithoutInteractions
    {
        public int? ActiveSubstanceId { get; set; }
        public string Description { get; set; }
        public string EnglishDescription { get; set; }
        public bool? Active { get; set; }
        public bool? Enunciative { get; set; }
    }

    public class GetActiveSubstancesCls
    {
        public List<ActiveSubstances> GetActiveSubstancesWithoutInteractions { get; set; }
        public List<ActiveSubstances> ActiveSubstances { get; set; }
    }
}