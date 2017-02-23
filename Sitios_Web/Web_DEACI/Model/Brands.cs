namespace Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Brands
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Brands()
        {
            ClientBrands = new HashSet<ClientBrands>();
        }

        [Key]
        public int BrandId { get; set; }

        [Required]
        [StringLength(100)]
        public string BrandName { get; set; }

        [StringLength(60)]
        public string Logo { get; set; }

        public bool Active { get; set; }

        [StringLength(100)]
        public string BaseURL { get; set; }

        public byte? AlphabetId { get; set; }

        public virtual Alphabet Alphabet { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ClientBrands> ClientBrands { get; set; }
    }
}
