//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Guianet.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class InternationalProducts
    {
        public InternationalProducts()
        {
            this.InternationalClientProducts = new HashSet<InternationalClientProducts>();
        }
    
        public int InternationalProductId { get; set; }
        public string InternationalProductName { get; set; }
        public bool Active { get; set; }
        public Nullable<byte> AlphabetId { get; set; }
    
        public virtual Alphabet Alphabet { get; set; }
        public virtual ICollection<InternationalClientProducts> InternationalClientProducts { get; set; }
    }
}
