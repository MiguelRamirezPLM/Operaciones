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
    
    public partial class ProductAdverts
    {
        public ProductAdverts()
        {
            this.ClientProductAdverts = new HashSet<ClientProductAdverts>();
        }
    
        public int ProductAdvertId { get; set; }
        public string ProductAdvertName { get; set; }
        public bool Active { get; set; }
        public string Description { get; set; }
        public string BaseURL { get; set; }
        public int Order { get; set; }
    
        public virtual ICollection<ClientProductAdverts> ClientProductAdverts { get; set; }
    }
}
