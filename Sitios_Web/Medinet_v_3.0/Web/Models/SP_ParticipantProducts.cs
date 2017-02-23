using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class SP_ParticipantProducts
    {
        public int EditionId { get; set; }
        public int DivisionId { get; set; }
        public int CategoryId { get; set; }
        public int PharmaFormId { get; set; }
        public int ProductId { get; set; }
        public byte? ContentTypeId { get; set; }
        public string Page { get; set; }
        public string HTMLContent { get; set; }
        public string XMLContent { get; set; }
    }
}