namespace Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Editions
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Editions()
        {
            EditionClientProducts = new HashSet<EditionClientProducts>();
            EditionClients = new HashSet<EditionClients>();
            ParticipantProducts = new HashSet<ParticipantProducts>();
            ClientCategories = new HashSet<ClientCategories>();
            ClientProductSubstances = new HashSet<ClientProductSubstances>();
        }

        [Key]
        public int EditionId { get; set; }

        public int NumberEdition { get; set; }

        [Required]
        [StringLength(20)]
        public string ISBN { get; set; }

        [StringLength(80)]
        public string BarCode { get; set; }

        public bool Active { get; set; }

        public byte CountryId { get; set; }

        public int? ParentId { get; set; }

        public int BookId { get; set; }

        [StringLength(10)]
        public string EditionCode { get; set; }

        public virtual Books Books { get; set; }

        public virtual Countries Countries { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EditionClientProducts> EditionClientProducts { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EditionClients> EditionClients { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ParticipantProducts> ParticipantProducts { get; set; }

        public virtual Editions Editions1 { get; set; }

        public virtual Editions Editions2 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ClientCategories> ClientCategories { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ClientProductSubstances> ClientProductSubstances { get; set; }
    }
}
