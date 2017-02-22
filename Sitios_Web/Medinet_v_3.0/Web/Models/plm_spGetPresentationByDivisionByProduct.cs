using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class plm_spGetPresentationByDivisionByProduct
    {
            public int DivisionId { get; set; }
			public string DivisionName { get; set; }
					
			public int CategoryId { get; set; }
			public string CategoryName { get; set; }
					
			public int ProductId { get; set; }
			public string Brand { get; set; }
			public string ProductDescription { get; set; }
					
			public int PharmaFormId { get; set; }
			public string PharmaForm { get; set; }
					
			public int PresentationId { get; set; }
			public string Presentation { get; set; }

            public int? ProductImageId { get; set; }
            public string ProductShot { get; set; }
			public int QuantityofImages { get; set; }
    }
}