using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Postal;

namespace Web.Models
{
    public class SendEmail
    {
        public string To { get; set; }
        public string UserName { get; set; }
        public string Comment { get; set; }
    }
}