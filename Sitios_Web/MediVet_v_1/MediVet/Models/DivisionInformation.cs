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
    
    public partial class DivisionInformation
    {
        public int DivisionInfId { get; set; }
        public string Address { get; set; }
        public string Suburb { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Telephone { get; set; }
        public string Fax { get; set; }
        public string Web { get; set; }
        public string Image { get; set; }
        public int DivisionId { get; set; }
        public string Email { get; set; }
        public string Lada { get; set; }
        public string Text { get; set; }
        public bool Active { get; set; }
    
        public virtual Divisions Divisions { get; set; }
    }
}
