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
    
    public partial class Editions
    {
        public Editions()
        {
            this.EditionDivisionAds = new HashSet<EditionDivisionAds>();
            this.EditionDivisionProducts = new HashSet<EditionDivisionProducts>();
            this.ParticipantProducts = new HashSet<ParticipantProducts>();
            this.MentionatedProducts = new HashSet<MentionatedProducts>();
            this.Newproducts = new HashSet<Newproducts>();
        }
    
        public int EditionId { get; set; }
        public Nullable<int> CountryId { get; set; }
        public Nullable<int> ParentId { get; set; }
        public Nullable<int> BookId { get; set; }
        public int NumberEdition { get; set; }
        public string ISBN { get; set; }
        public string BarCode { get; set; }
        public bool Active { get; set; }
    
        public virtual Books Books { get; set; }
        public virtual ICollection<EditionDivisionAds> EditionDivisionAds { get; set; }
        public virtual ICollection<EditionDivisionProducts> EditionDivisionProducts { get; set; }
        public virtual ICollection<ParticipantProducts> ParticipantProducts { get; set; }
        public virtual Editions Editions1 { get; set; }
        public virtual ICollection<MentionatedProducts> MentionatedProducts { get; set; }
        public virtual ICollection<Newproducts> Newproducts { get; set; }
        public virtual Countries Countries { get; set; }
    }
}