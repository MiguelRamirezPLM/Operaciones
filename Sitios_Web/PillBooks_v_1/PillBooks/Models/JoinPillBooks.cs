using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PillBooks.Models
{
    public class JoinPillBooks
    {
        public PillBook PillBook { get; set; }
        public List<ActiveSubstances> ActiveSubstances { get; set; }
        public List<Products> Products { get; set; }
        public List<PharmaceuticalForms> PharmaceuticalForms { get; set; }
        public List<Therapeutics> Therapeutics { get; set; }
        public List<plm_vwProductsByEdition> plm_vwProductsByEdition { get; set; }
        public List<ProductPharmaFormsByPillBook> ProductPharmaFormsByPillBook { get; set; }
        public List<TherapeuticsByPillBook> TherapeuticsByPillBook { get; set; }
        public List<ICDByPillBook> ICDByPillBook { get; set; }
        public List<INNActiveSubstances> INNActiveSubstances { get; set; }
        public List<TherapeuticOMSByPillBook> TherapeuticOMSByPillBook { get; set; }
    }
}