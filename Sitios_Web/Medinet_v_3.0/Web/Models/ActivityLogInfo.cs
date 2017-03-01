using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class ActivityLogInfo
    {

        public int ActivityLogId { get; set; }
        public DateTime Date { get; set; }
        public string FieldsAffected { get; set; }
        public string HashKey { get; set; }
        public int OperationId { get; set; }
        public string PrimaryKeyAffected { get; set; }
        public int TableId { get; set; }
        public int UserId { get; set; }
 
    }
}