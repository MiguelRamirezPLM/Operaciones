using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GuiaNet.Models
{
    public class ParticipantProductsClas
    {
        public int? ProductId { get; set; }
        public string ProductName { get; set; }
        public int? Qtty { get; set; }
        public int? QttyHC { get; set; }
        public bool? ProductWithoutDescription { get; set; }
        public bool? ProductWithoutName { get; set; }
        public bool? NoProductInformation { get; set; }
        public bool? RequierdCategoryOrProductLline { get; set; }
        public bool? DuplicatedProduct { get; set; }
    }
}