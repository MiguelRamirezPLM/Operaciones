using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Agronet.Models;

namespace Agronet.Controllers.Filters
{
    public class FiltersController : Controller
    {
        DEAQ db = new DEAQ();

        public JsonResult getBooks(string country)
        {
            List<Books> LB = new List<Books>();

            LB = db.Database.SqlQuery<Books>("plm_spGetBooks").ToList();

            return Json(LB, JsonRequestBehavior.AllowGet);
        }

        public JsonResult getEditions(string Country, string Book)
        {
            List<Editions> LE = new List<Editions>();

            int CountryId = int.Parse(Country);
            int BookId = int.Parse(Book);

            LE = db.Database.SqlQuery<Editions>("plm_spGetEditionsByBook @CountryId=" + CountryId + ", @BookId=" + BookId + "").ToList();

            return Json(LE, JsonRequestBehavior.AllowGet);
        }

        public JsonResult getDivisions(string Country)
        {
            List<Divisions> LD = new List<Divisions>();

            int CountryId = int.Parse(Country);

            LD = db.Database.SqlQuery<Divisions>("plm_spGetDivisionsByCountry @CountryId=" + CountryId + "").ToList();

            return Json(LD, JsonRequestBehavior.AllowGet);
        }

    }
}