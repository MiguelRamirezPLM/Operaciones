namespace Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ProductContents
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int AttributeId { get; set; }

        [Column(TypeName = "text")]
        public string PlainContent { get; set; }

        [Column(TypeName = "text")]
        public string Content { get; set; }

        [Column(TypeName = "text")]
        public string HtmlContent { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int EditionId { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ClientId { get; set; }

        [Key]
        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ProductId { get; set; }

        public virtual EditionAttributes EditionAttributes { get; set; }

        public virtual ParticipantProducts ParticipantProducts { get; set; }
    }
}
