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
    
    public partial class ProductStatus
    {
        public int ClientId { get; set; }
        public int ProductId { get; set; }
        public int StatusId { get; set; }
    
        public virtual Status Status { get; set; }
        public virtual ClientProducts ClientProducts { get; set; }
    }
}
