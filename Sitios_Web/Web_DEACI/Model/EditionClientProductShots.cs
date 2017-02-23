namespace Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class EditionClientProductShots
    {
        [Key]
        public int EditionClientProductShotId { get; set; }

        public bool Active { get; set; }

        [Required]
        [StringLength(100)]
        public string ProductShot { get; set; }

        public int ClientId { get; set; }

        public int ProductId { get; set; }

        public int EditionId { get; set; }

        public virtual ParticipantProducts ParticipantProducts { get; set; }
    }
}
