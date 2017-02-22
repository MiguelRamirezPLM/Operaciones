using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgroNet.Models
{
    public class joinDivisionsCountries
    {
        public Divisions Divisions { get; set; }
        public Countries Countries { get; set; }
        public DivisionImagesSizes DivisionImagesSizes { get; set; }
        public ImageSizes ImageSizes { get; set; }
        public DivisionImages DivisionImages { get; set; }
    }
}