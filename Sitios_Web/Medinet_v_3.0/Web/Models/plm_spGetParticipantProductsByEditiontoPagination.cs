using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class plm_spGetParticipantProductsByEditiontoPagination
    {
        public int? ProductId { get; set; }
        public string Brand { get; set; }
        public string ProductDescription { get; set; }
        public bool? ProductActive { get; set; }

        public int? PharmaFormId { get; set; }
        public string PharmaForm { get; set; }
        public bool? PharmaActive { get; set; }

        public int? DivisionId { get; set; }
        public string DivisionName { get; set; }
        public string DivisionShortName { get; set; }

        public int? CategoryId { get; set; }
        public string CategoryName { get; set; }

        public string ProductType { get; set; }
        public string AdDescription { get; set; }

        public int? Participant { get; set; }
        public int? Mentionated { get; set; }
        public int? ModifiedContent { get; set; }


        public string Page { get; set; }
    }
}