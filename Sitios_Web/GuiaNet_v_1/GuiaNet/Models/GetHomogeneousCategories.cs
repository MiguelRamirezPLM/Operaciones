using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GuiaNet.Models
{
    public class GetHomogeneousCategories
    {
        public int? HomogeneousCategoryId { get; set; }
        public string HomogeneousCategory { get; set; }
        public byte? HCLevel { get; set; }
        public int? HCParentId { get; set; }
        public bool? HCActive { get; set; }

        public int? LeafCategoryId { get; set; }
        public string LeafCategory { get; set; }
        public byte? LFLevel { get; set; }
        public bool? LFActive { get; set; }
    }
}