namespace Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class HeterogeneousCategories
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public HeterogeneousCategories()
        {
            MatchCategories = new HashSet<MatchCategories>();
            Clients = new HashSet<Clients>();
            ClientProducts = new HashSet<ClientProducts>();
        }

        [Key]
        public int HeterogeneousCategoryId { get; set; }

        public int? ParentId { get; set; }

        [Required]
        [StringLength(255)]
        public string Description { get; set; }

        public int Level { get; set; }

        public bool Active { get; set; }

        [StringLength(100)]
        public string Tratamiento { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MatchCategories> MatchCategories { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Clients> Clients { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ClientProducts> ClientProducts { get; set; }
    }
}
