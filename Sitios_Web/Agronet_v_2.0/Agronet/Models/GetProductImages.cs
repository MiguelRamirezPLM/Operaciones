using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Agronet.Models
{
    public class GetProductImages
    {
        public int? ProductImageId { get; set; }
        public string ProductShot { get; set; }

        public int? ImageSizeId { get; set; }
        public string Size { get; set; }
    }
}