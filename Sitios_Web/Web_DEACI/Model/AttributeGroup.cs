namespace Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AttributeGroup")]
    public partial class AttributeGroup
    {
        public int AttributeGroupId { get; set; }

        [Required]
        [StringLength(200)]
        public string Description { get; set; }

        public bool Active { get; set; }

        public int AttributeGroupOrder { get; set; }

        [StringLength(6)]
        public string AttributeGroupKey { get; set; }
    }
}
