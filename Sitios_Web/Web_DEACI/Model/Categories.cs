namespace Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Categories
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Categories()
        {
            ClientCategories = new HashSet<ClientCategories>();
            ClientProductCategories = new HashSet<ClientProductCategories>();
            MatchCategories = new HashSet<MatchCategories>();
        }

        [Key]
        public int CategoryId { get; set; }

        public byte? ParentId { get; set; }

        [Required]
        [StringLength(100)]
        public string Description { get; set; }

        public bool Active { get; set; }

        public int Level { get; set; }

        public int? IdArbol { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ClientCategories> ClientCategories { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ClientProductCategories> ClientProductCategories { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MatchCategories> MatchCategories { get; set; }

        public virtual Categories Categories1 { get; set; }

        public virtual Categories Categories2 { get; set; }
    }
}
