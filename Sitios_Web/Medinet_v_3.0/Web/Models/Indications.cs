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
    
    public partial class Indications
    {
        public Indications()
        {
            this.Indications1 = new HashSet<Indications>();
            this.ProductIndications = new HashSet<ProductIndications>();
        }
    
        public int IndicationId { get; set; }
        public Nullable<int> ParentId { get; set; }
        public string IndicationKey { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; }
    
        public virtual ICollection<Indications> Indications1 { get; set; }
        public virtual Indications Indications2 { get; set; }
        public virtual ICollection<ProductIndications> ProductIndications { get; set; }
    }
}
