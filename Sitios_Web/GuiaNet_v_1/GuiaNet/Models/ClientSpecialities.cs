using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GuiaNet.Models
{
    public class ClientSpecialities
    {
        public int? SpecialityId { get; set; }
        public string Description { get; set; }
        public int? ClientId { get; set; }
        public string CompanyName { get; set; }
        public bool? Active { get; set; }
        public string Quantity { get; set; }
        public string AdversDescription { get; set; }
    }
}