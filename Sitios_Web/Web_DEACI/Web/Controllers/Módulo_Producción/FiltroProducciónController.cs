using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Web.Models;

namespace Web.Controllers.Módulo_Producción
{
    public class FiltroProducciónController : Controller
    {
        private DEACI_20150917Entities db = new DEACI_20150917Entities();

        public ActionResult Pais()
        {
            var Countries = (from c in db.Countries
                                 select new Union_Companies_CompanyTypes_CompanyPhones_Cities
                                 { 
                                     Countries = c
                                 });
            return View(Countries);
        }
        public ActionResult País_Obra(int id)
        {
            var Obra = (from co in db.Countries
                        join ed in db.Editions
                        on co.CountryId equals ed.CountryId
                        where co.CountryId == id
                        select new Union_Companies_CompanyTypes_CompanyPhones_Cities
                        { 
                            Countries = co,
                            Editions = ed
                        });
            var x = (from c in db.Countries
                     where c.CountryId == id
                     select new Union_Companies_CompanyTypes_CompanyPhones_Cities
                     {
                         Countries = c
                     });
            ViewData["País"] = x.FirstOrDefault().Countries.CountryName;
            return View(Obra);
        }
        public ActionResult País_Obra_Cliente(int id, string palabra)
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
            ViewData["Number"] = v.SingleOrDefault().Editions.EditionId;
            return View(w);
        }
	}
}