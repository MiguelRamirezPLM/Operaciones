namespace Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class PhoneTypes
    {
        [Key]
        public byte PhoneTypeId { get; set; }

        [Required]
        [StringLength(20)]
        public string Description { get; set; }

        public bool Active { get; set; }
    }
}
