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
    
    public partial class ClientProductCategories
    {
        public ClientProductCategories()
        {
            this.EditionClientProducts = new HashSet<EditionClientProducts>();
        }
    
        public int ClientId { get; set; }
        public int ProductId { get; set; }
        public int CategoryId { get; set; }
    
        public virtual ICollection<EditionClientProducts> EditionClientProducts { get; set; }
        public virtual ClientProducts ClientProducts { get; set; }
        public virtual Categories Categories { get; set; }
    }
}
