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
    
    public partial class Books
    {
        public Books()
        {
            this.Editions = new HashSet<Editions>();
            this.Editions1 = new HashSet<Editions>();
        }
    
        public int BookId { get; set; }
        public string Description { get; set; }
        public string ShortName { get; set; }
        public bool Active { get; set; }
    
        public virtual ICollection<Editions> Editions { get; set; }
        public virtual ICollection<Editions> Editions1 { get; set; }
    }
}