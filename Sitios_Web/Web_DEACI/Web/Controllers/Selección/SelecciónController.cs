using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Models;
namespace Web.Controllers.Selección
{
    public class SelecciónController : Controller
    {
        PLMUsers plm = new PLMUsers();
        public ActionResult Selección()
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
                                     select _us);
                ViewData["Nombre"] = _UserNickName.SingleOrDefault().NickName;
            }
            return View();
        }
	}
}