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
    
    public partial class Products
    {
        public Products()
        {
            this.ProductPharmaForms = new HashSet<ProductPharmaForms>();
        }
    
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public bool Active { get; set; }
        public Nullable<int> ParentId { get; set; }
        public Nullable<byte> TypeId { get; set; }
        public Nullable<byte> AlphabetId { get; set; }
        public Nullable<int> ProductId_Anterior { get; set; }
    
        public virtual Alphabet Alphabet { get; set; }
        public virtual ICollection<ProductPharmaForms> ProductPharmaForms { get; set; }
        public virtual Products Products1 { get; set; }
        public virtual Products Products2 { get; set; }
        public virtual ProductTypes ProductTypes { get; set; }
    }
}