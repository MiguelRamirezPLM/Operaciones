using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Guianet
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
            Session["SessionAddAdverts"] = null;
            Session["SessionAddressId"] = null;
            Session["SessionAddressIdProd"] = null;
            Session["SessionAdvertsProd"] = null;
            Session["SessionAttributeGroup"] = null;
            Session["SessionBranchProd"] = null;
            Session["SessionBranchSM"] = null;
            Session["SessionBrandsProd"] = null;
            Session["SessionBrandsSM"] = null;
            Session["SessionBrnachId"] = null;
            Session["SessionClasification"] = null;
            Session["SessionEditionClients"] = null;
            Session["SessionFileName"] = null;
            Session["SessionProductId"] = null;
            Session["SessionProduction"] = null;
            Session["SessionReports"] = null;
            Session["SessionSegmentHTML"] = null;
            Session["SessionSpecialitiesSM"] = null;
            Session["SearchAdvertsAsocSM"] = null;
            Session["SearchAdvertsSM"] = null;
            Session["SearchBrandsAsocSM"] = null;
            Session["SearchBrandsSM"] = null;
            Session["SearchCategoryCL"] = null;
            Session["SearchCategorySM"] = null;
            Session["SearchDate"] = null;
            Session["SearchProductNameCL"] = null;
            Session["SearchProductNameSM"] = null;
        }
    }
}
