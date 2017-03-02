using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using Web.Models.Sessions;
using Web.Models;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.IO;
using System.Web.UI.WebControls;

namespace Web.Controllers.Production
{
    public class Production1Controller : Controller
    {
        //
        // GET: /Production1/

        private DataTable table = new DataTable();
        Medinet db = new Medinet();
        public ActionResult Index()
        {
            CountriesUsers _counTries = (CountriesUsers)Session["CountriesUsers"];
            if (Session["CountriesUsers"] == null)
            {
                Session.Abandon();
                return RedirectToAction("Logout", "Login");
            }
            if (_counTries.country.LongCount() >= 1)
            {
                return View();
            }
            else
            {
                return RedirectToAction("FilterIndex", "Production", new { id = _counTries.countryid });
            }
        }

        public ActionResult Content(int id, int ed, int ad, int ud)
        {
            CountriesUsers _counTries = (CountriesUsers)Session["CountriesUsers"];
            if (Session["CountriesUsers"] == null)
            {
                Session.Abandon();
                return RedirectToAction("Logout", "Login");
            }

            int Country = id;
            int Book = ed;
            int EditionType = 1; //cambiar
            int Edition = ad;
            int Division = ud;

            List<plm_spGetProductsToEditByDivisionMedinet3> LS = new List<plm_spGetProductsToEditByDivisionMedinet3>();
            LS = db.Database.SqlQuery<plm_spGetProductsToEditByDivisionMedinet3>("plm_spGetProductsToEditByDivisionMedinet3 @divisionId=" + Division + ", @countryId= " + Country + ", @bookId= " + Book + ", @editionId= " + Edition + "").ToList();

            SessionCountryByDivision _SessionCountryByDivision = new SessionCountryByDivision(id, ed, ad, ud);
            Session["SessionCountryByDivision"] = _SessionCountryByDivision;


            ViewData["CountryName"] = db.Countries.SingleOrDefault(model => model.CountryId == id).CountryName;
            ViewData["DivisionName"] = db.Divisions.FirstOrDefault(model => model.DivisionId == ud).Description;
            ViewData["BookName"] = db.Books.FirstOrDefault(model => model.BookId == ed).Description;
            ViewData["EditionNumber"] = db.Editions.FirstOrDefault(model => model.EditionId == ad).NumberEdition;
            ViewData["ShortName"] = db.Books.FirstOrDefault(model => model.BookId == ed).ShortName;

            ViewData["CountryId"] = Country;
            ViewData["BookId"] = Book;
            ViewData["EditionId"] = Edition;
            ViewData["DivisionId"] = Division;

            SessionM sessionSM = new SessionM(Country, Book, EditionType, Edition, Division);
            Session["sessionSM"] = sessionSM;
            return View(LS);
           
        }
	}
}