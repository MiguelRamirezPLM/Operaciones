using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgroNet.Models
{
    public class joinProductEditions
    {
        public Products Products { get; set; }
        public Divisions Divisions { get; set; }
        public PharmaForms PharmaForms { get; set; }
        public Categories Categories { get; set; }
        public ProductPharmaForms ProductPharmaForms { get; set; }
        public ProductCategories ProductCategories { get; set; }
        public Countries Countries { get; set; }
        public ParticipantProducts ParticipantProducts { get; set; }
    }
}