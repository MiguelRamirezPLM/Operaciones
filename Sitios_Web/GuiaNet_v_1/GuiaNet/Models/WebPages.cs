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
    
    public partial class WebPages
    {
        public WebPages()
        {
            this.WebPageSections = new HashSet<WebPageSections>();
        }
    
        public int WebPageId { get; set; }
        public Nullable<int> ParentId { get; set; }
        public int ApplicationId { get; set; }
        public string PageDescription { get; set; }
        public string Url { get; set; }
        public Nullable<bool> Active { get; set; }
    
        public virtual Applications Applications { get; set; }
        public virtual ICollection<WebPageSections> WebPageSections { get; set; }
    }
}
