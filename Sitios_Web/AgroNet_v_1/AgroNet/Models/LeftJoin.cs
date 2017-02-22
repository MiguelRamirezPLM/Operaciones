using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgroNet.Models
{
    public class LeftJoin
    {
        public PProductSubstances PProductSubstances { get; set; }
        public ActiveSubstances ActiveSubstances { get; set; }
    }
}