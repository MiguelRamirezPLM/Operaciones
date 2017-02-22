using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PillBooks.Models;

namespace PillBooks.Controllers
{
    public class HomeController : Controller
    {
        MedinetPB db = new MedinetPB();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult getpillbook()
        {
            var pb = db.PillBook.OrderBy(x => x.PillBookName).ToList();

            return View(pb);
        }
    }
}