namespace Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class PharmaceuticalForms
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PharmaceuticalForms()
        {
            ProductPharmaForms = new HashSet<ProductPharmaForms>();
        }

        [Key]
        public int PharmaFormId { get; set; }

        [Required]
        [StringLength(50)]
        public string PharmaForm { get; set; }

        public bool Active { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductPharmaForms> ProductPharmaForms { get; set; }
    }
}
