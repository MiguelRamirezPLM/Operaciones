﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class plm_spCRUDProductByPresentationToEdition_Result
    {
        public int? DivisionId { get; set; }
        public int? CategoryId { get; set; }
        public int? ProductId { get; set; }
        public int? PharmaFormId { get; set; }

        public int? QtyExternalPack { get; set; }
        public int? ExternalPackId { get; set; }
        public string ExternalPackName { get; set; }

        public int? QtyInternalPack { get; set; }
        public int? InternalPackId { get; set; }
        public string InternalPackName { get; set; }

        public string QtyContentUnit { get; set; }
        public int? ContentUnitId { get; set; }
        public string UnitName { get; set; }

        public string QtyWeightUnit { get; set; }
        public int? WeightUnitId { get; set; }
        public string WUnitName { get; set; }
        public string ShortName { get; set; }

        public string Presentation { get; set; }

    }
}