//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GuiaNet.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Status
    {
        public Status()
        {
            this.ProductStatus = new HashSet<ProductStatus>();
        }
    
        public int StatusId { get; set; }
        public string StatusName { get; set; }
        public bool Active { get; set; }
    
        public virtual ICollection<ProductStatus> ProductStatus { get; set; }
    }
}