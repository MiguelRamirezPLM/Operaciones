//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MediVet.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class ProductPharmaForms
    {
        public ProductPharmaForms()
        {
            this.ProductCategories = new HashSet<ProductCategories>();
        }
    
        public int ProductId { get; set; }
        public int PharmaFormId { get; set; }
        public bool Active { get; set; }
    
        public virtual PharmaceuticalForms PharmaceuticalForms { get; set; }
        public virtual ICollection<ProductCategories> ProductCategories { get; set; }
        public virtual Products Products { get; set; }
    }
}
