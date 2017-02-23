namespace Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ActiveSubstances
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ActiveSubstances()
        {
            ProductSubstances = new HashSet<ProductSubstances>();
        }

        [Key]
        public int ActiveSubstanceId { get; set; }

        [Required]
        [StringLength(255)]
        public string ActiveSubstance { get; set; }

        public bool Active { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductSubstances> ProductSubstances { get; set; }
    }
}
