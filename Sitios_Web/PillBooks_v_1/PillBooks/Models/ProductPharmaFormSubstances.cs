//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PillBooks.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class ProductPharmaFormSubstances
    {
        public int PharmaFormId { get; set; }
        public int ProductId { get; set; }
        public int ActiveSubstanceId { get; set; }
    
        public virtual ActiveSubstances ActiveSubstances { get; set; }
        public virtual ProductPharmaForms ProductPharmaForms { get; set; }
    }
}
