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
    
    public partial class PharmaceuticalForms
    {
        public PharmaceuticalForms()
        {
            this.ProductPharmaForms = new HashSet<ProductPharmaForms>();
        }
    
        public int PharmaFormId { get; set; }
        public string PharmaFormKey { get; set; }
        public string Description { get; set; }
        public string EnglishDescription { get; set; }
        public bool Active { get; set; }
    
        public virtual ICollection<ProductPharmaForms> ProductPharmaForms { get; set; }
    }
}
