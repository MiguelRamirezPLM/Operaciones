using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Guianet.Models
{
    public class GerReassingCategories
    {
        public int? ClientId { get; set; }
        public int? ProductId { get; set; }

        public int? CategoryThreeId { get; set; }
        public string CategoryThree { get; set; }

        public int? LeafCategoryId { get; set; }
        public string LeafCategory { get; set; }

        public string ProductName { get; set; }
        public string CompanyName { get; set; }
        public int? NumberEdition { get; set; }

        public string Reference { get; set; }
        public string Module { get; set; }

        public string Adviser { get; set; }
    }
}