using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GuiaNet.Models
{
    public class JoinEditionClientSpecialities
    {
        public EditionClientSpecialities EditionClientSpecialities { get; set; }
        public Clients Clients { get; set; }
        public Specialities Specialities { get; set; }
    }
}