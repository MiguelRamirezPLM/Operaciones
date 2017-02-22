using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Guianet.Models
{
    public class GetProductShots
    {
        public int? EditionClientProductShotId { get; set; }
        public string ProductShot { get; set; }

        public byte? ImageSizeId { get; set; }
        public string ImageSize { get; set; }
    }
}