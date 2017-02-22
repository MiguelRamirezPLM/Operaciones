using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MediVet.Models
{
    public class Letters
    {
        public string Letter { get; set; }
        public int? ParentId { get; set; }
        public string TherapeuticName { get; set; }
        public int TherapeuticId { get; set; }
        public int Therapeutic { get; set; }
    }
}