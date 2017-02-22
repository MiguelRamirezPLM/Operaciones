using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GuiaNet.Models;

namespace GuiaNet.Models
{
    public class joinmedicalproduct
    {
        public Products Products { get; set; }
        public PharmaceuticalForms PharmaceuticalForms { get; set; }
        public ProductPharmaForms ProductPharmaForms { get; set; }
        public ProductSubstances ProductSubstances { get; set; }
        public ActiveSubstances ActiveSubstances { get; set; }
        public EditionClientMedicalProducts EditionClientMedicalProducts { get; set; }
        public Presentations Presentations { get; set; }
    }   
}