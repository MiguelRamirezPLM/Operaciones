using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Guianet.Models
{
    public class GetClientPhones
    {
        public byte? PhoneTypeId { get; set; }
        public string Description { get; set; }
        public int? ClientPhoneId { get; set; }
        public string Lada { get; set; }
        public string Number { get; set; }
    }
}