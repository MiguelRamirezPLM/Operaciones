//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Web.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Presentations
    {
        public Presentations()
        {
            this.ProductImages = new HashSet<ProductImages>();
            this.EditionPresentations = new HashSet<EditionPresentations>();
        }
    
        public int PresentationId { get; set; }
        public int DivisionId { get; set; }
        public int CategoryId { get; set; }
        public int ProductId { get; set; }
        public int PharmaFormId { get; set; }
        public Nullable<int> QtyExternalPack { get; set; }
        public Nullable<int> ExternalPackId { get; set; }
        public Nullable<int> QtyInternalPack { get; set; }
        public Nullable<int> InternalPackId { get; set; }
        public string QtyContentUnit { get; set; }
        public Nullable<int> ContentUnitId { get; set; }
        public string QtyWeightUnit { get; set; }
        public Nullable<int> WeightUnitId { get; set; }
        public string Presentation { get; set; }
        public bool Active { get; set; }
        public string JSONFormat { get; set; }
    
        public virtual ContentUnits ContentUnits { get; set; }
        public virtual ExternalPacks ExternalPacks { get; set; }
        public virtual InternalPacks InternalPacks { get; set; }
        public virtual ProductCategories ProductCategories { get; set; }
        public virtual WeightUnits WeightUnits { get; set; }
        public virtual ICollection<ProductImages> ProductImages { get; set; }
        public virtual ICollection<EditionPresentations> EditionPresentations { get; set; }
    }
}