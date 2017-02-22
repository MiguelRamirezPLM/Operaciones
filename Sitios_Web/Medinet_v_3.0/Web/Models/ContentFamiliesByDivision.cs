using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class ContentFamiliesByDivision
    {
        public int? FamilyId { get; set; }
        public int? PrefixId { get; set; }
        public string FamilyString { get; set; }
        public bool? Active { get; set; }
    }
}