using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Guianet.Models
{
    public class Work
    {
        public int? WorkId { get; set; }
        public int? ProductId { get; set; }
        public string ProductName { get; set; }
        public int? ClientId { get; set; }
        public string CompanyName { get; set; }
        public string WorkDescription { get; set; }
        public string WorkDescriptionLevelThree { get; set; }
        public string Added { get; set; }
        public string UserName { get; set; }
        public string Module { get; set; }
        public bool? Active { get; set; }
        public DateTime AddedDate { get; set; }
    }
}