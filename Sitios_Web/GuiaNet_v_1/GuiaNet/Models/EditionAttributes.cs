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
    
    public partial class EditionAttributes
    {
        public EditionAttributes()
        {
            this.ProductContents = new HashSet<ProductContents>();
            this.EditionAttributeGroup = new HashSet<EditionAttributeGroup>();
        }
    
        public int AttributeId { get; set; }
        public int EditionId { get; set; }
        public bool Active { get; set; }
    
        public virtual ICollection<ProductContents> ProductContents { get; set; }
        public virtual Attributes Attributes { get; set; }
        public virtual ICollection<EditionAttributeGroup> EditionAttributeGroup { get; set; }
        public virtual Editions Editions { get; set; }
    }
}
