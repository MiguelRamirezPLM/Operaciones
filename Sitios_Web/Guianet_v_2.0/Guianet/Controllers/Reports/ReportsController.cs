using Guianet.Models.Sessions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Guianet.Controllers.Reports
{
    public class ReportsController : Controller
    {
        public ActionResult Index(string CountryId, string UserId, string EditionId, string BookId)
        {
            if(!Request.IsAuthenticated)
            {
                return RedirectToAction("Logout","Login");
            }

            SessionReports ind = (SessionReports)Session["SessionReports"];
            Session["SearchDate"] = null;
            if (CountryId != null)
            {
                int Country = int.Parse(CountryId);
                int User = int.Parse(UserId);
                int Edition = int.Parse(EditionId);
                int Book = int.Parse(BookId);

                SessionReports SessionReports = new SessionReports(Country, User, Edition, Book);
                Session["SessionReports"] = SessionReports;

                return View();
            }

            if (ind != null)
            {
                int Country = ind.CId;
                int User = ind.UId;
                int Edition = ind.EId;
                int Book = ind.BId;

                SessionReports SessionReports = new SessionReports(Country, User, Edition, Book);
                Session["SessionReports"] = SessionReports;

                return View();

            }
            else
            {
                return View();
            }
        }

        public JsonResult GetDate(string Date)
        {
            SearchDate SearchDate = new SearchDate(Date);
            Session["SearchDate"] = SearchDate;

            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}