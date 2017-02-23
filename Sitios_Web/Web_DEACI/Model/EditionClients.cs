namespace Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class EditionClients
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public EditionClients()
        {
            ClientBrands = new HashSet<ClientBrands>();
            EditionClientSpecialities = new HashSet<EditionClientSpecialities>();
        }

        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int EditionId { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ClientId { get; set; }

        [StringLength(100)]
        public string Page { get; set; }

        [Key]
        [Column(Order = 2)]
        public byte ClientTypeId { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ClientBrands> ClientBrands { get; set; }

        public virtual Clients Clients { get; set; }

        public virtual ClientTypes ClientTypes { get; set; }

        public virtual Editions Editions { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EditionClientSpecialities> EditionClientSpecialities { get; set; }
    }
}
