using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Web.Models;

namespace Web.Controllers.Manual
{
    public class ManualProducciónController : Controller
    {
        DEACI_20150917Entities db = new DEACI_20150917Entities();
        PLMUsers plm = new PLMUsers();
        public ActionResult Completo()
        {
            CountriesUsers p = (CountriesUsers)Session["CountriesUsers"];
            if (Session["CountriesUsers"] == null)
            {
                Session.Abandon();
                return RedirectToAction("Login", "Login");
            }
            else
            {
                var _UserNickName = (from _us in plm.Users
                                     where _us.UserId == p.userId
                                     select _us).ToList();
                ViewData["Nombre"] = _UserNickName.SingleOrDefault().NickName;
            }
            return View();
        }
        public ActionResult ManualPDF()
        {
            return View();
        }
        public ActionResult Manual_DEACI()
        {
            return new Rotativa.ActionAsPdf("ManualPDF");
        }
        public FileResult PDF()
        {
            var filepath = System.IO.Path.Combine(Server.MapPath("/Manual/Manual_DEACI (Producción).pdf"));
            return File(filepath, "Manual_DEACI (Producción).pdf");
        }
	}
}