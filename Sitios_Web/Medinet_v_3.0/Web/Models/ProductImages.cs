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
    
    public partial class ProductImages
    {
        public ProductImages()
        {
            this.ProductImageSizes = new HashSet<ProductImageSizes>();
        }
    
        public int ProductImageId { get; set; }
        public string ProductShot { get; set; }
        public System.DateTime LastUpdate { get; set; }
        public string BaseURL { get; set; }
        public bool Active { get; set; }
        public Nullable<int> PresentationId { get; set; }
    
        public virtual Presentations Presentations { get; set; }
        public virtual ICollection<ProductImageSizes> ProductImageSizes { get; set; }
    }
}