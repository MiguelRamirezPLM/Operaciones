namespace Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Attributes
    {
        [Key]
        public int AttributeId { get; set; }

        [Required]
        [StringLength(200)]
        public string Description { get; set; }

        public bool Active { get; set; }
    }
}
