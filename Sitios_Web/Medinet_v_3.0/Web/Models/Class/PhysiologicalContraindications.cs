using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models.Class
{
    public class PhysiologicalContraindications
    {
        public int? PhysContraindicationId { get; set; }
        public string PhysContraindicationName { get; set; }
        public bool? Active { get; set; }
    }
}