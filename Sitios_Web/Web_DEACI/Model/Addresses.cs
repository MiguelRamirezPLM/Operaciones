namespace Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Addresses
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Addresses()
        {
            Clients = new HashSet<Clients>();
        }

        [Required]
        [StringLength(255)]
        public string Address { get; set; }

        [StringLength(30)]
        public string ZipCode { get; set; }

        [StringLength(100)]
        public string City { get; set; }

        [StringLength(255)]
        public string Email { get; set; }

        [StringLength(100)]
        public string Web { get; set; }

        public bool Active { get; set; }

        public byte CountryId { get; set; }

        [Key]
        public int AddressId { get; set; }

        [StringLength(255)]
        public string Suburb { get; set; }

        public int? StateId { get; set; }

        public virtual Countries Countries { get; set; }

        public virtual States States { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Clients> Clients { get; set; }
    }
}
