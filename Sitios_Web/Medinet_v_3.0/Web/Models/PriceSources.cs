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
    
    public partial class PriceSources
    {
        public PriceSources()
        {
            this.ProductPrices = new HashSet<ProductPrices>();
        }
    
        public byte PriceSourceId { get; set; }
        public string SourceName { get; set; }
        public string SourceShortName { get; set; }
        public bool Active { get; set; }
    
        public virtual ICollection<ProductPrices> ProductPrices { get; set; }
    }
}
