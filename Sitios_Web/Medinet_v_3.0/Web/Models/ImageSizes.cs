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
    
    public partial class ImageSizes
    {
        public ImageSizes()
        {
            this.ProductImageSizes = new HashSet<ProductImageSizes>();
        }
    
        public byte ImageSizeId { get; set; }
        public string Size { get; set; }
        public bool Active { get; set; }
    
        public virtual ICollection<ProductImageSizes> ProductImageSizes { get; set; }
    }
}
