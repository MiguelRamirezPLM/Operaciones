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
    
    public partial class PProductAgrochemicalUses
    {
        public int AgrochemicalUseId { get; set; }
        public int ProductId { get; set; }
    
        public virtual AgrochemicalUses AgrochemicalUses { get; set; }
        public virtual Products Products { get; set; }
    }
}