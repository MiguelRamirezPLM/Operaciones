namespace Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ClientBrands
    {
        public byte? ClientBrandTypeId { get; set; }

        public bool Active { get; set; }

        [StringLength(50)]
        public string Page { get; set; }

        [StringLength(100)]
        public string ExpireDescription { get; set; }

        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int EditionId { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ClientId { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int BrandId { get; set; }

        [Key]
        [Column(Order = 3)]
        public byte ClientTypeId { get; set; }

        public virtual Brands Brands { get; set; }

        public virtual ClientBrandTypes ClientBrandTypes { get; set; }

        public virtual EditionClients EditionClients { get; set; }
    }
}
