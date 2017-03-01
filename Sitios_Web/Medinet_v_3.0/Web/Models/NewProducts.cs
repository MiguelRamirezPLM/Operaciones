using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class NewProducts
    {
        public int EditionId { get; set; }
        public int DivisionId { get; set; }
        public int CategoryId { get; set; }
        public int PharmaFormId { get; set; }
        public int ProductId { get; set; }

        public virtual ProductCategories ProductCategories { get; set; }
        public virtual Editions Editions { get; set; }
    }
}