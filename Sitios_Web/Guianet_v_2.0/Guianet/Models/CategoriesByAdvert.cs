using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Guianet.Models
{
    public class CategoriesByAdvert
    {
        public int? CategoryThreeId { get; set; }
        public string CategoryThree { get; set; }
        public string Editions { get; set; }
        public int? PP { get; set; }
        public byte? AdvertTypeId { get; set; }
        public string Description { get; set; }
    }
}