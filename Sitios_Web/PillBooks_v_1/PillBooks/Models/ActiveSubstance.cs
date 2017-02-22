using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PillBooks.Models
{
    public class ActiveSubstance
    {
        public int ActiveSubstanceId { get; set; }
        public string Description { get; set; }
        public string EnglishDescription { get; set; }
        public bool Active { get; set; }
    }
}