//------------------------------------------------------------------------------
// <auto-generated>
//    Este código se generó a partir de una plantilla.
//
//    Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//    Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AgroNet.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class ImageTypes
    {
        public ImageTypes()
        {
            this.DivisionImages = new HashSet<DivisionImages>();
        }
    
        public byte ImageTypeId { get; set; }
        public string TypeName { get; set; }
        public bool Active { get; set; }
    
        public virtual ICollection<DivisionImages> DivisionImages { get; set; }
    }
}
