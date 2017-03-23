using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models.Class
{
    public class GetAdministrationRoutesClass
    {
        public int? RouteId { get; set; }
        public string RouteName { get; set; }
        public string ShortName { get; set; }
        public string Description { get; set; }
        public string Synonym { get; set; }
        public bool? Active { get; set; }
    }

    public class GetAdministrationRoutesByProductPharmaForm
    {
        public int? RouteId { get; set; }
        public string RouteName { get; set; }
        public string ShortName { get; set; }
        public string Description { get; set; }
        public string Synonym { get; set; }
        public bool? Active { get; set; }
        public string JSONFormat { get; set; }
    }

    public class GetAdministrationRoutes
    {
        public List<GetAdministrationRoutesClass> GetAdministrationRoutesClass { get; set; }
        public List<GetAdministrationRoutesByProductPharmaForm> GetAdministrationRoutesByProductPharmaForm { get; set; }
    }
}