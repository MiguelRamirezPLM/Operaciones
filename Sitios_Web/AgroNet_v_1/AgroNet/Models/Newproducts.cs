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
    
    public partial class Newproducts
    {
        public int ProductId { get; set; }
        public int PharmaFormId { get; set; }
        public int DivisionId { get; set; }
        public int CategoryId { get; set; }
        public int EditionId { get; set; }
    
        public virtual Editions Editions { get; set; }
        public virtual ProductCategories ProductCategories { get; set; }
    }
}