namespace Web.Models
{
    using System;

    public partial class plm_spGetEditionsByBook_Result
    {
        public int EditionId { get; set; }
        public Nullable<int> ParentId { get; set; }
        public int CountryId { get; set; }
        public int BookId { get; set; }
        public int NumberEdition { get; set; }
        public string ISBN { get; set; }
        public string BarCode { get; set; }
        public bool Active { get; set; }
    }
}
