using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Guianet.Models
{
    public class AdvertsByClient
    {
        public int? ClientId { get; set; }
        public string CompanyName { get; set; }

        public int? EditionId { get; set; }
        public int? NumberEdition { get; set; }

        public int? AdvertId { get; set; }
        public string AdvertName { get; set; }
        public string AdvertDescription { get; set; }
        public string AdvertFile { get; set; }

        public int? CategoryThreeId { get; set; }
        public string CategoryThree { get; set; }
    }
}