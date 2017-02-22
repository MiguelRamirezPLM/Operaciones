using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AgroNet.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(string DivisionId, string CountryId)
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult products()
        {
            return View();
        }

        public ActionResult laboratories()
        {
            return View();
        }

        public ActionResult productspage()
        {
            return View();
        }

        public ActionResult Vew1()
        {
            return PartialView();
        }
    }
}
