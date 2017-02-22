using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Guianet.Models
{
    public class GetEditionCategoryThree
    {
        public int? CategoryThreeId { get; set; }
        public string CategoryThree { get; set; }
        public byte? Level { get; set; }
        public bool? Active { get; set; }
        public string CategoryProductPage { get; set; }
        public string CategoryPSPage { get; set; }
    }
}