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
    
    public partial class WebPageSectionMenues
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public WebPageSectionMenues()
        {
            this.Roles = new HashSet<Roles>();
        }
    
        public int WebPageId { get; set; }
        public byte WebSectionId { get; set; }
        public int MenuId { get; set; }
        public string Url { get; set; }
        public string ImageName { get; set; }
        public byte MenuOrder { get; set; }
        public bool Active { get; set; }
    
        public virtual Menues Menues { get; set; }
        public virtual WebPageSections WebPageSections { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Roles> Roles { get; set; }
    }
}