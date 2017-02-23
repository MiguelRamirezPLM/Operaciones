namespace Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Advertisements
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Advertisements()
        {
            EditionClientSpecialities = new HashSet<EditionClientSpecialities>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int AdvertId { get; set; }

        public int Order { get; set; }

        public bool Active { get; set; }

        [Required]
        [StringLength(200)]
        public string AdvertName { get; set; }

        [Required]
        [StringLength(255)]
        public string BaseUrl { get; set; }

        public byte AdvertTypeId { get; set; }

        public virtual AdvertTypes AdvertTypes { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EditionClientSpecialities> EditionClientSpecialities { get; set; }
    }
}
