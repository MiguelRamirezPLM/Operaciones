using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GuiaNet.Models
{
    public class JoinClientAddressesPhones
    {
        public ClientAddresses ClientAddresses { get; set; }
        public Addresses Addresses { get; set; }
        public Clients Clients { get; set; }
        public States States { get; set; }
    }
}