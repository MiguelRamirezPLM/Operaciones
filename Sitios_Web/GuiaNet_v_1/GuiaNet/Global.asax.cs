using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace GuiaNet
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Session_Start(object sender, EventArgs e)
        {
            Session["sessionrecatid"] = null;
            Session["seachcategory"] = null;
            Session["searchhcategory"] = null;
            Session["sessionrecategorization"] = null;
            Session["sessionclassif"] = null;
            Session["searchcategoryref"] = null;
            Session["CountriesUsers"] = null;
            Session["searchcategoryreference"] = null;
            Session["sessionproduction"] = null;
            Session["attrproduction"] = null;
            Session["indexsalesmodule"] = null;
            Session["indexclasification"] = null;
            Session["sessionmedicalproducts"] = null;
            Session["sessioninformationlaboratoryproducts"] = null;
            Session["sessionmedicalproducts"] = null;
            Session["seachheterogeneouscategories"] = null;
            Session["flaghetcat"] = null;
            Session["sessionlistbranch"] = null;
            Session["sessionclientspecialities"] = null;
            Session["sessionclientid"] = null;
            Session["sessionclientbranchProd"] = null;
            Session["SearchCategory"] = null;
            Session["sessionClasificationHC"] = null;
        }
    }
}
