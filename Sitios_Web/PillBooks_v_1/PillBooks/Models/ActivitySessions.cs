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
    
    public partial class ActivitySessions
    {
        public int ActivitySessionId { get; set; }
        public int ApplicationId { get; set; }
        public int UserId { get; set; }
        public System.DateTime Date { get; set; }
    
        public virtual Applications Applications { get; set; }
        public virtual Users Users { get; set; }
    }
}
