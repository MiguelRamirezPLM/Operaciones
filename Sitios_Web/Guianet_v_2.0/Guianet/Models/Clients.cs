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
    
    public partial class Clients
    {
        public Clients()
        {
            this.EditionClients = new HashSet<EditionClients>();
            this.ClientProducts = new HashSet<ClientProducts>();
            this.ClientAddresses = new HashSet<ClientAddresses>();
            this.ClientAdverts = new HashSet<ClientAdverts>();
            this.InternationalClientProducts = new HashSet<InternationalClientProducts>();
            this.ClientProductAdverts = new HashSet<ClientProductAdverts>();
        }
    
        public int ClientId { get; set; }
        public Nullable<int> ClientIdParent { get; set; }
        public string CompanyName { get; set; }
        public string Image { get; set; }
        public bool Active { get; set; }
        public string ShortName { get; set; }
        public string Description { get; set; }
        public Nullable<byte> AlphabetId { get; set; }
        public Nullable<byte> CountryId { get; set; }
        public string Code { get; set; }
    
        public virtual Countries Countries { get; set; }
        public virtual Clients Clients1 { get; set; }
        public virtual Clients Clients2 { get; set; }
        public virtual ICollection<EditionClients> EditionClients { get; set; }
        public virtual Alphabet Alphabet { get; set; }
        public virtual ICollection<ClientProducts> ClientProducts { get; set; }
        public virtual ICollection<ClientAddresses> ClientAddresses { get; set; }
        public virtual ICollection<ClientAdverts> ClientAdverts { get; set; }
        public virtual ICollection<InternationalClientProducts> InternationalClientProducts { get; set; }
        public virtual ICollection<ClientProductAdverts> ClientProductAdverts { get; set; }
    }
}