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
    
    public partial class IPPAProductInteractions
    {
        public IPPAProductInteractions()
        {
            this.ProductSubstanceInteractions = new HashSet<ProductSubstanceInteractions>();
        }
    
        public int DivisionId { get; set; }
        public int CategoryId { get; set; }
        public int PharmaFormId { get; set; }
        public int ProductId { get; set; }
        public int ActiveSubstanceId { get; set; }
        public Nullable<byte> StatusId { get; set; }
        public string HTMLContent { get; set; }
        public string PlainContent { get; set; }
    
        public virtual ProductCategories ProductCategories { get; set; }
        public virtual ICollection<ProductSubstanceInteractions> ProductSubstanceInteractions { get; set; }
        public virtual ActiveSubstances ActiveSubstances { get; set; }
    }
}