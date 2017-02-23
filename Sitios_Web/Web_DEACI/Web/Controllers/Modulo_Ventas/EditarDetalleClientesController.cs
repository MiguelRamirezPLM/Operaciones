using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Web.Models;

namespace Web.Controllers.Pruebas
{
    public class EditarDetalleClientesController : Controller
    {
        private DEACI_20150917Entities db = new DEACI_20150917Entities();

        public ActionResult DetalleCliente(int id, int ed)
        {
            var Number = (from e in db.Editions
                          where e.EditionId ==id
                          select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Editions = e });
            ViewData["Number"] = Number.SingleOrDefault().Editions.EditionId;
            var edition = (from e in db.Editions
                           where e.EditionId == id
                           select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Editions = e });
            ViewData["Edición"] = edition.SingleOrDefault().Editions.NumberEdition;
            var o = (from c in db.Companies
                     where c.CompanyId == ed
                     select new Union_companies_cb { Companies = c });
            ViewData["CompanyName"] = o.FirstOrDefault().Companies.CompanyName;
            ViewData["CompanyId"] = o.FirstOrDefault().Companies.CompanyId;
            ViewData["CompanyShortName"] = o.FirstOrDefault().Companies.CompanyShortName;
            ViewData["Address"] = o.FirstOrDefault().Companies.Address;
            ViewData["Suburb"] = o.FirstOrDefault().Companies.Suburb;
            ViewData["Ubication"] = o.FirstOrDefault().Companies.Ubication;
            ViewData["PostalCode"] = o.FirstOrDefault().Companies.PostalCode;
            ViewData["Email"] = o.FirstOrDefault().Companies.Email;
            ViewData["Web"] = o.FirstOrDefault().Companies.Web;
            ViewData["Contact"] = o.FirstOrDefault().Companies.Contact;
            var l = (from c in db.Companies
                     join ct in db.CompanyTypes
                     on c.CompanyTypeId equals ct.CompanyTypeId
                     where c.CompanyId == ed
                     select new Union_companies_cb { Companies = c, CompanyTypes = ct });
            ViewData["CompanyTypes.Description"] = l.FirstOrDefault().CompanyTypes.Description;
            var rt = (from c in db.Companies
                      join cp in db.CompanyPhones
                      on c.CompanyId equals cp.CompanyId
                      join pt in db.PhoneTypes
                      on cp.PhoneTypeId equals pt.PhoneTypeId
                      where c.CompanyId == ed
                      select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Companies = c, CompanyPhones = cp, PhoneTypes = pt });
            return View(rt);
        }
        public ActionResult Edit(int id, int ed)
        {
            var ff = (from e in db.Editions
                      where e.EditionId == id
                      select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Editions = e });
            ViewData["Number"] = ff.SingleOrDefault().Editions.EditionId;
            ViewData["Edición"] = ff.SingleOrDefault().Editions.NumberEdition;
            Companies companies = db.Companies.Find(ed);

            if (companies == null)
            {
                return HttpNotFound();
            }
            ViewBag.CompanyTypeId = new SelectList(
                                                   db.CompanyTypes, "CompanyTypeId", "Description",
                                                   companies.CompanyTypeId);
            ViewBag.CompanyIdParent = new SelectList(
                                                   db.Companies, "CompanyId", "CompanyName",
                                                   companies.CompanyIdParent);
            ViewBag.CityId = new SelectList(
                                           db.Cities, "CityId", "Name",
                                           companies.CityId);
            return View(companies);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit
        (int id, int ed, [Bind(Include="CompanyId,CompanyTypeId,CompanyIdParent,Address,Suburb,Ubication,PostalCode,Email,Web,Contact,CompanyName,CompanyShortName,CityId,Client_ID,Active")] Companies companies)
        {
            var cc = (from c in db.Companies
                      where c.CompanyId == ed
                      select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Companies = c });
            var ff = (from e in db.Editions
                      where e.EditionId == id
                      select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Editions = e });
            ViewData["Number"] = ff.SingleOrDefault().Editions.EditionId;
            ViewData["Edición"] = ff.SingleOrDefault().Editions.NumberEdition;
            if (ModelState.IsValid)
            {
                db.Entry(companies).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("/DetalleCliente/" + ff.SingleOrDefault().Editions.EditionId + "/" + cc.SingleOrDefault().Companies.CompanyId);
            }
            ViewBag.CompanyTypeId = new SelectList(
                                                   db.CompanyTypes, "CompanyTypeId", "Description",
                                                   companies.CompanyTypeId);
            ViewBag.CompanyIdParent = new SelectList(
                                                   db.Companies, "CompanyId", "CompanyName",
                                                   companies.CompanyIdParent);
            return View(companies);
        }                                //Edición    Cliente   Tipo de teléfono
        public ActionResult EditarTeléfonos(int id, int ed, int ad)
        {
            CompanyPhones companyphones = db.CompanyPhones.SingleOrDefault(x => x.CompanyId == ed && x.PhoneTypeId == ad);
            var cc = (from c in db.Companies
                      where c.CompanyId == ed
                      select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Companies = c });
            ViewData["CCC"] = cc.SingleOrDefault().Companies.CompanyName;
            var ee = (from e in db.Editions
                      where e.EditionId == id
                      select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Editions = e });
            var pp = (from c in db.Companies
                      join cp in db.CompanyPhones
                      on c.CompanyId equals cp.CompanyId
                      join pt in db.PhoneTypes
                      on cp.PhoneTypeId equals pt.PhoneTypeId
                      where c.CompanyId == ed
                      && cp.PhoneTypeId == ad
                      select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Companies = c, CompanyPhones = cp, PhoneTypes = pt });
            ViewData["Teléfono"] = pp.SingleOrDefault().PhoneTypes.Description;
            ViewData["Number"] = ee.SingleOrDefault().Editions.EditionId;
            ViewData["Edición"] = ee.SingleOrDefault().Editions.NumberEdition;
            ViewBag.CompanyId = new SelectList(db.Companies, "CompanyId", "CompanyName", ed);
            return View(companyphones);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditarTeléfonos(int id, int ed, int ad, [Bind(Include = "PhoneTypeId,CompanyId,PhoneValue")] CompanyPhones companyphones)
        {
            var ee = (from e in db.Editions
                      where e.EditionId == id
                      select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Editions = e });
            ViewData["Number"] = ee.SingleOrDefault().Editions.EditionId;
            ViewData["Edición"] = ee.SingleOrDefault().Editions.NumberEdition;
            if (ModelState.IsValid)
            {
                var pppp = (from cs in db.CompanyPhones
                            where cs.CompanyId == companyphones.CompanyId
                            select cs).ToList();
                foreach (CompanyPhones css in pppp)
                {
                    var eliminar = db.CompanyPhones.SingleOrDefault
                        (x => x.CompanyId == companyphones.CompanyId && x.PhoneTypeId == companyphones.PhoneTypeId);
                }
                if (pppp.Count >= 1)
                {
                    var rrrr = (from cs in db.CompanyPhones
                                where cs.CompanyId == companyphones.CompanyId && cs.PhoneTypeId == companyphones.PhoneTypeId
                                select cs).ToList();
                    foreach (CompanyPhones css in rrrr)
                    {
                        var eliminar = db.CompanyPhones.SingleOrDefault
                        (x => x.CompanyId == companyphones.CompanyId && x.PhoneTypeId == companyphones.PhoneTypeId);
                        db.CompanyPhones.Remove(eliminar);
                        db.SaveChanges();
                        db.Entry(companyphones).State = EntityState.Added;
                        db.SaveChanges();
                    }
                }
                else
                {
                    db.Entry(companyphones).State = EntityState.Added;
                    db.SaveChanges();
                }
                return RedirectToAction("/DetalleCliente/" + ee.SingleOrDefault().Editions.EditionId + "/" + companyphones.CompanyId);
            }
            return View();
        }
        public ActionResult AgregarTeléfono(int id, int ed)
        {
            var cc = (from c in db.Companies
                      where c.CompanyId == ed
                      select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Companies = c });
            ViewData["CCC"] = cc.SingleOrDefault().Companies.CompanyName;
            ViewData["CID"] = cc.SingleOrDefault().Companies.CompanyId;
            var ee = (from e in db.Editions
                      where e.EditionId == id
                      select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Editions = e });
            ViewData["Number"] = ee.SingleOrDefault().Editions.EditionId;
            ViewData["Edición"] = ee.SingleOrDefault().Editions.NumberEdition;
            ViewBag.CompanyId = new SelectList(db.Companies, "CompanyId", "CompanyName", ed);
            ViewBag.PhoneTypeId = new SelectList(db.PhoneTypes, "PhoneTypeId", "Description");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AgregarTeléfono(int id, int ed, [Bind(Include = "PhoneTypeId,CompanyId,PhoneValue")] CompanyPhones companyphones)
        {
            var ee = (from e in db.Editions
                      where e.EditionId == id
                      select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Editions = e });
            ViewData["Number"] = ee.SingleOrDefault().Editions.EditionId;
            ViewData["Edición"] = ee.SingleOrDefault().Editions.NumberEdition;
            if (ModelState.IsValid)
            {
                    db.Entry(companyphones).State = EntityState.Added;
                    db.SaveChanges();

                return RedirectToAction("/DetalleCliente/" + ee.SingleOrDefault().Editions.EditionId + "/" + companyphones.CompanyId);
            }
            return View();
        }
        public ActionResult EliminarTeléfono(int id, int ed, int ad)
        {
            CompanyPhones companyphones = db.CompanyPhones.SingleOrDefault(x => x.CompanyId == ed && x.PhoneTypeId == ad);
            var cc = (from c in db.Companies
                      where c.CompanyId == ed
                      select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Companies = c });
            ViewData["CCC"] = cc.SingleOrDefault().Companies.CompanyName;
            var ee = (from e in db.Editions
                      where e.EditionId == id
                      select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Editions = e });
            var pp = (from c in db.Companies
                      join cp in db.CompanyPhones
                      on c.CompanyId equals cp.CompanyId
                      join pt in db.PhoneTypes
                      on cp.PhoneTypeId equals pt.PhoneTypeId
                      where c.CompanyId == ed
                      && cp.PhoneTypeId == ad
                      select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Companies = c, CompanyPhones = cp, PhoneTypes = pt });
            ViewData["Teléfono"] = pp.SingleOrDefault().PhoneTypes.Description;
            ViewData["Number"] = ee.SingleOrDefault().Editions.EditionId;
            ViewData["Edición"] = ee.SingleOrDefault().Editions.NumberEdition;
            ViewBag.CompanyId = new SelectList(db.Companies, "CompanyId", "CompanyName", ed);
            return View(companyphones);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EliminarTeléfono(int id, int ed, int ad, [Bind(Include = "PhoneTypeId,CompanyId,PhoneValue")] CompanyPhones companyphones)
        {
            var ee = (from e in db.Editions
                      where e.EditionId == id
                      select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Editions = e });
            ViewData["Number"] = ee.SingleOrDefault().Editions.EditionId;
            ViewData["Edición"] = ee.SingleOrDefault().Editions.NumberEdition;
            if (ModelState.IsValid)
            {
                    db.Entry(companyphones).State = EntityState.Deleted;
                    db.SaveChanges();
                
                return RedirectToAction("/DetalleCliente/" + ee.SingleOrDefault().Editions.EditionId + "/" + companyphones.CompanyId);
            }
            return View();
        }
    }
}
