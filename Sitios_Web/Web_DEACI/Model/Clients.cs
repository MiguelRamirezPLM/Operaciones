namespace Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Clients
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Clients()
        {
            ClientCategories = new HashSet<ClientCategories>();
            EditionClients = new HashSet<EditionClients>();
            Addresses = new HashSet<Addresses>();
            HeterogeneousCategories = new HashSet<HeterogeneousCategories>();
        }

        [Key]
        public int ClientId { get; set; }

        public int? ClientIdParent { get; set; }

        [Required]
        [StringLength(10)]
        public string ClientCode { get; set; }

        [Required]
        [StringLength(200)]
        public string CompanyName { get; set; }

        [StringLength(50)]
        public string Image { get; set; }

        public bool Active { get; set; }

        [StringLength(200)]
        public string ShortName { get; set; }

        public string Description { get; set; }

        public byte? AlphabetId { get; set; }

        public int? ANUNCIANTEID { get; set; }

        public byte CountryId { get; set; }

        public virtual Alphabet Alphabet { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ClientCategories> ClientCategories { get; set; }

        public virtual Countries Countries { get; set; }

        public virtual Clients Clients1 { get; set; }

        public virtual Clients Clients2 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EditionClients> EditionClients { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Addresses> Addresses { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HeterogeneousCategories> HeterogeneousCategories { get; set; }
    }
}
