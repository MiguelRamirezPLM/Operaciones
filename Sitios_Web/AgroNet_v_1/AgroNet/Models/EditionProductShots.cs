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
    
    public partial class EditionProductShots
    {
        public int EditionProductShotId { get; set; }
        public Nullable<int> EditionId { get; set; }
        public Nullable<int> DivisionId { get; set; }
        public Nullable<int> CategoryId { get; set; }
        public Nullable<int> PharmaFormId { get; set; }
        public Nullable<int> ProductId { get; set; }
        public Nullable<byte> PSTypeId { get; set; }
        public string ProductShot { get; set; }
        public Nullable<byte> QtyCells { get; set; }
        public bool Active { get; set; }
    
        public virtual EditionDivisionProducts EditionDivisionProducts { get; set; }
        public virtual ProductShotTypes ProductShotTypes { get; set; }
    }
}
