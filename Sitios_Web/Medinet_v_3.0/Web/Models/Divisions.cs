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
    
    public partial class Divisions
    {
        public Divisions()
        {
            this.DivisionImages = new HashSet<DivisionImages>();
            this.DivisionCategories = new HashSet<DivisionCategories>();
            this.DivisionInformation = new HashSet<DivisionInformation>();
            this.DivisionInformation1 = new HashSet<DivisionInformation>();
        }
    
        public int DivisionId { get; set; }
        public Nullable<int> ParentId { get; set; }
        public int LaboratoryId { get; set; }
        public int CountryId { get; set; }
        public string Description { get; set; }
        public string ShortName { get; set; }
        public bool Active { get; set; }
    
        public virtual Countries Countries { get; set; }
        public virtual Countries Countries1 { get; set; }
        public virtual ICollection<DivisionImages> DivisionImages { get; set; }
        public virtual ICollection<DivisionCategories> DivisionCategories { get; set; }
        public virtual ICollection<DivisionInformation> DivisionInformation { get; set; }
        public virtual ICollection<DivisionInformation> DivisionInformation1 { get; set; }
    }
}