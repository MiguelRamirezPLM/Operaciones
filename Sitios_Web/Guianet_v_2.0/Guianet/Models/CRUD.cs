using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Guianet.Models
{
    public class CRUD
    {
        public int Create = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["Create"]);
        public int Read = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["Read"]);
        public int Update = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["Update"]);
        public int Delete = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["Delete"]);
    }
}