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
    
    public partial class tmpCodeErrors
    {
        public int ErrorId { get; set; }
        public int ApplicationId { get; set; }
        public string Folio { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public string Message { get; set; }
        public string StackTrace { get; set; }
        public short Status { get; set; }
    }
}
