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
    
    public partial class Applications
    {
        public Applications()
        {
            this.ActivitySessions = new HashSet<ActivitySessions>();
            this.Tables = new HashSet<Tables>();
            this.ApplicationUsers = new HashSet<ApplicationUsers>();
        }
    
        public int ApplicationId { get; set; }
        public string Description { get; set; }
        public string HashKey { get; set; }
        public string URL { get; set; }
        public string RootUrl { get; set; }
        public string Version { get; set; }
        public Nullable<System.DateTime> LastUpdate { get; set; }
        public string DocumentFile { get; set; }
        public bool Active { get; set; }
    
        public virtual ICollection<ActivitySessions> ActivitySessions { get; set; }
        public virtual ICollection<Tables> Tables { get; set; }
        public virtual ICollection<ApplicationUsers> ApplicationUsers { get; set; }
    }
}
