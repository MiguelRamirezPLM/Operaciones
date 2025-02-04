//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MediVet.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Products
    {
        public Products()
        {
            this.ProductPharmaForms = new HashSet<ProductPharmaForms>();
            this.ProductTypes = new HashSet<ProductTypes>();
            this.Stocks = new HashSet<Stocks>();
            this.ProductEquipment = new HashSet<ProductEquipment>();
            this.ProductSpecies = new HashSet<ProductSpecies>();
            this.ProductSubstances = new HashSet<ProductSubstances>();
            this.ProductTherapeuticUses = new HashSet<ProductTherapeuticUses>();
            this.DivisionProducts = new HashSet<DivisionProducts>();
        }
    
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public byte CountryId { get; set; }
        public int LaboratoryId { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; }
        public Nullable<int> IdAccess { get; set; }
        public string Participant { get; set; }
    
        public virtual Laboratories Laboratories { get; set; }
        public virtual ICollection<ProductPharmaForms> ProductPharmaForms { get; set; }
        public virtual ICollection<ProductTypes> ProductTypes { get; set; }
        public virtual ICollection<Stocks> Stocks { get; set; }
        public virtual Countries Countries { get; set; }
        public virtual ICollection<ProductEquipment> ProductEquipment { get; set; }
        public virtual ICollection<ProductSpecies> ProductSpecies { get; set; }
        public virtual ICollection<ProductSubstances> ProductSubstances { get; set; }
        public virtual ICollection<ProductTherapeuticUses> ProductTherapeuticUses { get; set; }
        public virtual ICollection<DivisionProducts> DivisionProducts { get; set; }
    }
}
