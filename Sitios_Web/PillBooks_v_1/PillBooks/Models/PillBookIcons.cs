//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PillBooks.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class PillBookIcons
    {
        public PillBookIcons()
        {
            this.EditionPillBookAttributes = new HashSet<EditionPillBookAttributes>();
        }
    
        public byte PillBookIconId { get; set; }
        public string PillBookIcon { get; set; }
        public bool Active { get; set; }
        public string BaseURL { get; set; }
        public byte PillBookIconAnswerId { get; set; }
    
        public virtual ICollection<EditionPillBookAttributes> EditionPillBookAttributes { get; set; }
        public virtual PillBookIconAnswers PillBookIconAnswers { get; set; }
    }
}
