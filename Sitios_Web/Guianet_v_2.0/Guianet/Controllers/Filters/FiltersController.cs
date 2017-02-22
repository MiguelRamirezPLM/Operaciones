using Guianet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Guianet.Controllers.Filters
{
    public class FiltersController : Controller
    {
        GuiaEntities db = new GuiaEntities();
        PLMUsers dbusers = new PLMUsers();

        public JsonResult books(string country)
        {
            List<Books> LB = new List<Books>();

            LB = db.Database.SqlQuery<Books>("plm_spGetBooks").ToList();

            return Json(LB, JsonRequestBehavior.AllowGet);
        }

        public JsonResult editions(string country, string bookid)
        {
            int CountryId = int.Parse(country);
            int BookId = int.Parse(bookid);

            List<GetEditions> LE = new List<GetEditions>();

            LE = db.Database.SqlQuery<GetEditions>("plm_spGetEditionsByBookByCountry @CountryId=" + CountryId + ", @BookId=" + BookId + "").ToList();

            return Json(LE, JsonRequestBehavior.AllowGet);
        }

        public JsonResult getclients(string country)
        {
            int CountryId = int.Parse(country);

            List<GetClients> LC = new List<GetClients>();

            var ct = db.ClientTypes.Where(x => x.TypeName.ToUpper().Trim() == "ANUNCIANTE").ToList();

            LC = db.Database.SqlQuery<GetClients>("plm_spGetClientsByCountry @CountryId=" + CountryId + ", @ClientTypeId=" + ct[0].ClientTypeId + "").ToList();

            return Json(LC, JsonRequestBehavior.AllowGet);
        }

        public JsonResult getinternationalclients(string country)
        {
            int CountryId = int.Parse(country);

            List<GetClients> LC = new List<GetClients>();

            var ct = db.ClientTypes.Where(x => x.TypeName.ToUpper().Trim() == "INTERNACIONAL").ToList();

            LC = db.Database.SqlQuery<GetClients>("plm_spGetClientsByCountry @CountryId=" + CountryId + ", @ClientTypeId=" + ct[0].ClientTypeId + "").ToList();

            return Json(LC, JsonRequestBehavior.AllowGet);
        }

        public JsonResult getUsers(string Country)
        {
            int CountryId = int.Parse(Country);

            string HashKey = System.Configuration.ConfigurationManager.AppSettings["HashKey"];

            List<GetUsersByApplication> LS = dbusers.Database.SqlQuery<GetUsersByApplication>("plm_spGetSaleExecutives @HashKey='" + HashKey + "', @CountryId=" + CountryId + "").ToList();

            return Json(LS, JsonRequestBehavior.AllowGet);
        }

        public JsonResult getClientTypes()
        {
            List<ClientTypes> LCT = db.Database.SqlQuery<ClientTypes>("plm_spGetClientTypes").ToList();

            return Json(LCT, JsonRequestBehavior.AllowGet);
        }

        public JsonResult getclientsByCountryByClientType(string country, string CTId)
        {
            int CountryId = int.Parse(country);
            int ClientTypeId = int.Parse(CTId);

            List<GetClients> LC = new List<GetClients>();

            LC = db.Database.SqlQuery<GetClients>("plm_spGetClientsByCountry @CountryId=" + CountryId + ", @ClientTypeId=" + ClientTypeId + "").ToList();

            return Json(LC, JsonRequestBehavior.AllowGet);
        }

        public JsonResult getclientsCA(string country)
        {
            int CountryId = int.Parse(country);

            List<GetClients> LC = new List<GetClients>();

            var ct = db.ClientTypes.Where(x => x.TypeName.ToUpper().Trim() == "ANÁLISIS CLÍNICOS").ToList();

            LC = db.Database.SqlQuery<GetClients>("plm_spGetClientsByCountry @CountryId=" + CountryId + ", @ClientTypeId=" + ct[0].ClientTypeId + "").ToList();

            return Json(LC, JsonRequestBehavior.AllowGet);
        }
    }
}