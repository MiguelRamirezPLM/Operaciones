﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models.Class
{
    public class GetProductPhysContraindications
    {
        public int? CategoryId { get; set; }
        public int? DivisionId { get; set; }
        public int? PharmaFormId { get; set; }
        public int? ProductId { get; set; }
        public int? ActiveSubstanceId { get; set; }
        public int? PhysContraindicationId { get; set; }
        public string ActiveSubstance { get; set; }
        public string PhysContraindicationName { get; set; }
    }
}