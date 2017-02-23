using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class GetBookToEditionByContry_Result
    {

        public int BookId { get; set; }
        public int EditionId { get; set; }
        public string Description { get; set; }
        public string TypeName { get; set; }
        public string ShortName { get; set; }
        public string ISBN { get; set; }
        public int NumberEdition { get; set; }
        public int Participant { get; set; }
    
    }
}