using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models.Class
{
    public class ActiveSubstances
    {
        public int? ActiveSubstanceId { get; set; }
        public string Description { get; set; }
        public string EnglishDescription { get; set; }
        public bool Active { get; set; }
        public bool Enunciative { get; set; }
    }

    public class GetActiveSubstances
    {
        public int? ActiveSubstanceId { get; set; }
        public string Description { get; set; }
        public string EnglishDescription { get; set; }
        public bool Active { get; set; }
        public string JSONFormat { get; set; }
    }
}