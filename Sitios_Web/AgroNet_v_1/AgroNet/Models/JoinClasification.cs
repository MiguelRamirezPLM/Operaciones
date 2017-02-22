using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgroNet.Models
{
    public class JoinClasification
    {
        public Crops Crops { get; set; }
        public ActiveSubstances ActiveSubstances { get; set; }
        public AgrochemicalUses AgrochemicalUses { get; set; }
    }
}