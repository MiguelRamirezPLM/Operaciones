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
    
    public partial class Countries
    {
        public Countries()
        {
            this.Clients = new HashSet<Clients>();
            this.Editions = new HashSet<Editions>();
            this.Addresses = new HashSet<Addresses>();
            this.States = new HashSet<States>();
        }
    
        public byte CountryId { get; set; }
        public string CountryName { get; set; }
        public bool Active { get; set; }
        public Nullable<byte> CountryCode { get; set; }
        public string ID { get; set; }
        public string CountryLada { get; set; }
    
        public virtual ICollection<Clients> Clients { get; set; }
        public virtual ICollection<Editions> Editions { get; set; }
        public virtual ICollection<Addresses> Addresses { get; set; }
        public virtual ICollection<States> States { get; set; }
    }
}