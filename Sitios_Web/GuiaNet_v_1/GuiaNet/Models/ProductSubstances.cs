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
    
    public partial class ProductSubstances
    {
        public ProductSubstances()
        {
            this.ClientProductSubstances = new HashSet<ClientProductSubstances>();
        }
    
        public int ProductId { get; set; }
        public int PharmaFormId { get; set; }
        public int ActiveSubstanceId { get; set; }
        public int PresentationId { get; set; }
    
        public virtual ActiveSubstances ActiveSubstances { get; set; }
        public virtual Presentations Presentations { get; set; }
        public virtual ProductPharmaForms ProductPharmaForms { get; set; }
        public virtual ICollection<ClientProductSubstances> ClientProductSubstances { get; set; }
    }
}
