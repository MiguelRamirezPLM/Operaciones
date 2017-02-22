using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class ProductImagesInfo
    {
        public int ProductImageId { get; set; }
        public int ImageSizeId { get; set; }
        public string ProductImageName { get; set; }
        public string Exist { get; set; }
    }
}