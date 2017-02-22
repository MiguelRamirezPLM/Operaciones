using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MediVet.Models
{
    public class JoinProductShots
    {
        public EditionProductShotsImageSizes EditionProductShotsImageSizes { get; set; }
        public EditionProductShots EditionProductShots { get; set; }
        public ImageSizes ImageSizes { get; set; }
    }
}