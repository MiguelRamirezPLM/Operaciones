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
    
    public partial class BarCodes
    {
        public BarCodes()
        {
            this.ClientProductBarCodes = new HashSet<ClientProductBarCodes>();
        }
    
        public int BarCodeId { get; set; }
        public string BarCode { get; set; }
        public bool Active { get; set; }
    
        public virtual ICollection<ClientProductBarCodes> ClientProductBarCodes { get; set; }
    }
}