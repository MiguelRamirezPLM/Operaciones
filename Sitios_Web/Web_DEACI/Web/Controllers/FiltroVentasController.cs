using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Web.Models;

namespace Web.Controllers
{
    public class FiltroVentasController : Controller
    {
        private DEACI_20150917Entities db = new DEACI_20150917Entities();

        public ActionResult Pais()
        {
            var Countries = (from co in db.Countries
                             select new Union_Companies_CompanyTypes_CompanyPhones_Cities
                             {
                                 Countries = co
                             });
            return View(Countries);
        }
        public ActionResult Pais_Obra(int id)
        {
            var po = (from co in db.Countries
                      join ed in db.Editions
                          on co.CountryId equals ed.CountryId
                      where co.CountryId == id
                      select new Union_Companies_CompanyTypes_CompanyPhones_Cities
                      {
                          Countries = co,
                          Editions = ed
                      });
            var v = (from c in db.Countries
                     where c.CountryId == id
                     select new Union_Companies_CompanyTypes_CompanyPhones_Cities
                     {
                         Countries = c
                     });
            ViewData["País"] = v.FirstOrDefault().Countries.CountryName;
            return View(po);
        }
        public ActionResult Pais_Obra_Cliente(int id, string palabra)
        {
            var w = (from co in db.Countries
                     join ed in db.Editions
                     on co.CountryId equals ed.CountryId
                     join ce in db.CompanyEditions
                     on ed.EditionId equals ce.EditionId
                     join c in db.Companies
                     on ce.CompanyId equals c.CompanyId
                     where ce.EditionId == id
                     select new Union_Companies_CompanyTypes_CompanyPhones_Cities
                     {
                         Countries = co,
                         Editions = ed,
                         CompanyEditions = ce,
                         Companies = c
                     });
            if (!String.IsNullOrEmpty(palabra))
            {
                w = w.Where(j => j.Companies.CompanyName.Contains(palabra));
            }
            var v = (from c in db.Countries
                     join ed in db.Editions
                     on c.CountryId equals ed.CountryId 
                     where ed.EditionId == id
                     select new Union_Companies_CompanyTypes_CompanyPhones_Cities
                     {
                         Countries = c,
                         Editions = ed
                     });
            ViewData["País"] = v.FirstOrDefault().Countries.CountryName;
            ViewData["Country"] = v.SingleOrDefault().Countries.CountryId;
            var x = (from c in db.Countries
                     join ed in db.Editions
                     on c.CountryId equals ed.CountryId
                     where ed.EditionId == id
                     select new Union_Companies_CompanyTypes_CompanyPhones_Cities
                     {
                         Countries = c,
                         Editions = ed
                     });
            ViewData["Edición"] = v.FirstOrDefault().Editions.NumberEdition;
            return View(w);
        }
	}
}