//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PillBooks.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class PillBookATCOMS
    {
        public int PillBookId { get; set; }
        public int TherapeuticOMSId { get; set; }
    
        public virtual TherapeuticOMS TherapeuticOMS { get; set; }
        public virtual PillBook PillBook { get; set; }
    }
}