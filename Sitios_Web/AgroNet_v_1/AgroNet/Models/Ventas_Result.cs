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
    
    public partial class Ventas_Result
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public string Register { get; set; }
        public string DivisionName { get; set; }
        public string ShortName { get; set; }
        public int DivisionId { get; set; }
        public string PharmaForm { get; set; }
        public string CategoryName { get; set; }
        public Nullable<int> ParticipantProducts { get; set; }
        public Nullable<int> MentionatedProducts { get; set; }
        public Nullable<int> NewProducts { get; set; }
        public bool Active { get; set; }
    }
}