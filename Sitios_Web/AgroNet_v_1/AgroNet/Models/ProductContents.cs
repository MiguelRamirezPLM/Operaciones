//------------------------------------------------------------------------------
// <auto-generated>
//    Este código se generó a partir de una plantilla.
//
//    Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//    Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AgroNet.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class ProductContents
    {
        public int EditionId { get; set; }
        public int ProductId { get; set; }
        public int PharmaFormId { get; set; }
        public int DivisionId { get; set; }
        public int CategoryId { get; set; }
        public int AttributeId { get; set; }
        public string Content { get; set; }
        public string PlainContent { get; set; }
        public string HTMLContent { get; set; }
    
        public virtual ParticipantProducts ParticipantProducts { get; set; }
    }
}
