//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Web.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class ActiveSubstances
    {
        public ActiveSubstances()
        {
            this.ProductSubstanceInteractions = new HashSet<ProductSubstanceInteractions>();
            this.IPPAProductInteractions = new HashSet<IPPAProductInteractions>();
        }
    
        public int ActiveSubstanceId { get; set; }
        public string SubstanceKey { get; set; }
        public string Description { get; set; }
        public string EnglishDescription { get; set; }
        public bool Active { get; set; }
        public bool Enunciative { get; set; }
        public string JSONFormat { get; set; }
    
        public virtual ICollection<ProductSubstanceInteractions> ProductSubstanceInteractions { get; set; }
        public virtual ICollection<IPPAProductInteractions> IPPAProductInteractions { get; set; }
    }
}