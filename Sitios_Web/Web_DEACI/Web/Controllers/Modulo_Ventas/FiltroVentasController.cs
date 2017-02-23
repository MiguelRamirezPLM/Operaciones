using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Web.Models;
using PagedList;
using System.Data.SqlClient;

namespace Web.Controllers
{
    public class FiltroVentasController : Controller
    {
        private DEACI_20150917Entities db = new DEACI_20150917Entities();
        public GeteditionValues GeteditionValues = new Models.GeteditionValues();
        PLMUsers plm = new PLMUsers();
        public ActionResult Pais()
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
        public ActionResult _Obras(int id)
        {
            Editions _Editions = db.Editions.FirstOrDefault(model => model.CountryId == id);
            if (_Editions == null)
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
                var _Countries = (from _co in db.Countries
                                  where _co.CountryId == id
                                  select _co);
                ViewData["Error"] = _Countries.SingleOrDefault().CountryName;
                var _1Books = (from _b in db.Books
                               where _b.BookId == id
                               select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Books = _b });
                return View(_1Books);
            }
            CountriesUsers p1 = (CountriesUsers)Session["CountriesUsers"];
            if (Session["CountriesUsers"] == null)
            {
                Session.Abandon();
                return RedirectToAction("Login", "Login");
            }
            else
            {
                var _UserNickName = (from _us in plm.Users
                                     where _us.UserId == p1.userId
                                     select _us);
                ViewData["Nombre"] = _UserNickName.SingleOrDefault().NickName;
            }
            Books _Books = new Books();
            _Books.BookId = _Editions.BookId;
            var _Book = (from _b in db.Books
                         where _b.BookId == _Books.BookId
                         select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Books = _b });
            ViewData["País"] = _Editions.Countries.CountryName;
            var _2Countries = (from _co in db.Countries
                               where _co.CountryId == id
                               select _co);
            ViewData["CountryId"] = _2Countries.SingleOrDefault().CountryId;
            return View(_Book);
        }
        public ActionResult _Ediciones(int id, int ed)
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
            var _Editions = (from _e in db.Editions
                             join _b in db.Books
                             on _e.BookId equals _b.BookId
                             where _e.BookId == ed
                             select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Editions = _e, Books = _b });
            ViewData["CountryId"] = id;
            ViewData["BookId"] = _Editions.FirstOrDefault().Books.BookId;
            ViewData["Obra"] = _Editions.FirstOrDefault().Books.BookName;
            var _Country = (from _co in db.Countries
                            where _co.CountryId == id
                            select _co);
            ViewData["País"] = _Country.SingleOrDefault().CountryName;
            return View(_Editions);
        }
        public ActionResult _TipoCliente(int id, int ad, int ed)
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
            ViewData["CountryId"] = id;
            ViewData["BookId"] = ed;
            var _1Country = (from _co in db.Countries
                             where _co.CountryId == id
                             select _co);
            ViewData["País"] = _1Country.SingleOrDefault().CountryName;
            var _1Book = (from _b in db.Books
                          where _b.BookId == ed
                          select _b);
            ViewData["Obra"] = _1Book.SingleOrDefault().BookName;
            var _1Edition = (from _e in db.Editions
                             where _e.EditionId == ad
                             select _e);
            ViewData["Edición"] = _1Edition.SingleOrDefault().NumberEdition;
            ViewData["EditionId"] = _1Edition.SingleOrDefault().EditionId;
            var _CompanyTypes = (from _ct in db.CompanyTypes
                                 where _ct.Active == true
                                 select new Union_Companies_CompanyTypes_CompanyPhones_Cities { CompanyTypes = _ct }).ToList();
            return View(_CompanyTypes);
        }
        public ActionResult _Clientes(int id, int ed, int ad, int? ud, string palabra)
        {
            var _valuelabs = (from _ct in db.CompanyTypes
                              where _ct.CompanyTypeId == ud
                              select _ct).ToList();
            if (_valuelabs.SingleOrDefault().Description == "LABORATORIO")
            {
                return RedirectToAction("/Laboratorios/" + id + "/" + ed + "/" + ad + "/" + ud);
            }
            var _storeprocedure = db.Database.SqlQuery<plm_spGetCompaniesByEditionIdByCompanyTypeId_Result>
                ("plm_spGetCompaniesByEditionIdByCompanyTypeId @EditionId=" + ad + ", @CompanyTypeId=" + ud + "").ToList();
            CountriesUsers p1 = (CountriesUsers)Session["CountriesUsers"];
            if (Session["CountriesUsers"] == null)
            {
                Session.Abandon();
                return RedirectToAction("Login", "Login");
            }
            else
            {
                var _UserNickName = (from _us in plm.Users
                                     where _us.UserId == p1.userId
                                     select _us);
                ViewData["Nombre"] = _UserNickName.SingleOrDefault().NickName;
            }
            ViewData["CountryId"] = id;
            ViewData["BookId"] = ed;
            var _Country = (from _co in db.Countries
                            where _co.CountryId == id
                            select _co);
            ViewData["País"] = _Country.SingleOrDefault().CountryName;
            var _Book = (from _b in db.Books
                         where _b.BookId == ed
                         select _b);
            ViewData["Obra"] = _Book.SingleOrDefault().BookName;
            var _Edition = (from _e in db.Editions
                            where _e.EditionId == ad
                            select _e);
            ViewData["Edición"] = _Edition.SingleOrDefault().NumberEdition;
            ViewData["EditionId"] = ad;
            var _CompanyTypes = (from _ct in db.CompanyTypes
                                 where _ct.CompanyTypeId == ud
                                 select _ct).ToList();
            ViewData["TipoCliente"] = _CompanyTypes.FirstOrDefault().Description;
            return View(_storeprocedure);
        }                                                  
        public ActionResult Laboratorios(int id, int ed, int ad, int ud, string palabra, string sortOrder, string currentFilter, string searchString, int? page)
        {
            SessionEditionId _SessionEditionId = new SessionEditionId(ad, ud);
            Session["SessionEditionId"] = _SessionEditionId;
            CountriesUsers p1 = (CountriesUsers)Session["CountriesUsers"];
            if (Session["CountriesUsers"] == null)
            {
                Session.Abandon();
                return RedirectToAction("Login", "Login");
            }
            else
            {
                var _UserNickName = (from _us in plm.Users
                                     where _us.UserId == p1.userId
                                     select _us);
                ViewData["Nombre"] = _UserNickName.SingleOrDefault().NickName;
            }
            ViewData["CountryId"] = id;
            ViewData["BookId"] = ed;
            var _Country = (from _co in db.Countries
                            where _co.CountryId == id
                            select _co);
            ViewData["País"] = _Country.SingleOrDefault().CountryName;
            var _Book = (from _b in db.Books
                         where _b.BookId == ed
                         select _b);
            ViewData["Obra"] = _Book.SingleOrDefault().BookName;
            var _Edition = (from _e in db.Editions
                            where _e.EditionId == ad
                            select _e);
            ViewData["Edición"] = _Edition.SingleOrDefault().NumberEdition;
            ViewData["Number"] = ad;
            var _EditionCompanies = (from _c in db.Companies
                                     join _ce in db.CompanyEditions
                                     on _c.CompanyId equals _ce.CompanyId
                                     join _ct in db.CompanyTypes
                                     on _ce.CompanyTypeId equals _ct.CompanyTypeId
                                     where _ct.CompanyTypeId == ud
                                     orderby _c.CompanyName
                                     select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Companies = _c, CompanyTypes = _ct}).Distinct().OrderBy(model => model.Companies.CompanyId).AsQueryable();
            ViewData["TipoCliente"] = _EditionCompanies.FirstOrDefault().CompanyTypes.Description;
            if (palabra != null)
            {
                page = 1;
            }
            else
            {
                palabra = currentFilter;
            }
            ViewBag.CurrentFilter = palabra;
            if (palabra != null)
            {
                if (palabra.LongCount() <= 3)
                {
                    if (!String.IsNullOrEmpty(palabra))
                    {
                        _EditionCompanies = _EditionCompanies.Where(j => j.Companies.CompanyName.StartsWith(palabra));
                    }
                }
                if (palabra.LongCount() > 3)
                {
                    if (!String.IsNullOrEmpty(palabra))
                    {
                        _EditionCompanies = _EditionCompanies.Where(j => j.Companies.CompanyName.Contains(palabra));
                    }
                }
            }
            int pageSize = 11;
            int pageNumber = (page ?? 1);
            var _LastInformation = (from _ed in db.Editions
                                    select _ed).ToList();
            var _EditionValue = (from _ed in db.Editions
                                     where _ed.EditionId == ad
                                     select _ed).ToList();
            if (_LastInformation.LastOrDefault().NumberEdition == _EditionValue.SingleOrDefault().NumberEdition)
            {
                ViewData["LastInformation"] = "True";
            }
            return View(_EditionCompanies.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult EditarCliente(int id, int ed)
        {
            SessionEditionId _SessionEditionId = new SessionEditionId(id, ed);
            Session["SessionEditionId"] = _SessionEditionId;
            var _Companies = db.Companies.Find(ed);
            return View(_Companies);
        }
        public ActionResult EditarTeléfonos(int id, int ed)
        {
            var _CompanyPhones = (from _c in db.Companies
                                  join _cp in db.CompanyPhones
                                  on _c.CompanyId equals _cp.CompanyId
                                  join _pt in db.PhoneTypes
                                  on _cp.PhoneTypeId equals _pt.PhoneTypeId
                                  where _cp.CompanyId == id && _cp.PhoneTypeId == ed
                                  select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Companies = _c, CompanyPhones = _cp, PhoneTypes = _pt }).ToList();
            var _Companies = (from _c in db.Companies
                              where _c.CompanyId == id
                              select _c).ToList();
            ViewData["CompanyName"] = _Companies.SingleOrDefault().CompanyName;
            return View(_CompanyPhones);
        }
        public ActionResult EditarTeléfono(string CompanyId, string PhoneTypeEdit, string PhoneValueEdit)
        {
            int _CompanyId = int.Parse(CompanyId);
            byte _PhoneTypeId = byte.Parse(PhoneTypeEdit);
            CompanyPhones _CompanyPhones = new CompanyPhones();
            _CompanyPhones.CompanyId = _CompanyId;
            _CompanyPhones.PhoneTypeId = _PhoneTypeId;
            _CompanyPhones.PhoneValue = PhoneValueEdit;
            db.Entry(_CompanyPhones).State = EntityState.Modified;
            db.SaveChanges();
            var _Aplication = (from _ap in plm.Applications
                               where _ap.Description == "Sitio DEACI"
                               select _ap).ToList();
            foreach (var _App in _Aplication)
            {
                var _Tables = (from _tab in plm.Tables
                               where _tab.ApplicationId == _App.ApplicationId
                               && _tab.Description == "Phones"
                               select _tab).ToList();
                foreach (var _Tabs in _Tables)
                {
                    var _ApplicationId = _App.ApplicationId;
                    CountriesUsers p = (CountriesUsers)Session["CountriesUsers"];
                    ActivityLogs _ActivityLogs = new ActivityLogs();
                    int _OperationId = 2; // Modificar
                    _ActivityLogs.UserId = p.userId;
                    _ActivityLogs.TableId = _Tabs.TableId; // Phones
                    _ActivityLogs.OperationId = _OperationId;
                    _ActivityLogs.Date = DateTime.Now;
                    _ActivityLogs.PrimaryKeyAffected = ("(CompanyId," + _CompanyPhones.CompanyId + "),(" + "PhoneTypeId," + _CompanyPhones.PhoneTypeId + ")");
                    _ActivityLogs.FieldsAffected = ("(PhoneValue," + _CompanyPhones.PhoneValue + ")");
                    plm.Entry(_ActivityLogs).State = EntityState.Added;
                    plm.SaveChanges();
                }
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        public ActionResult EliminarTeléfono(int CompanyId, byte PhoneTypeId)
        {
            CompanyPhones _CompanyPhones = new CompanyPhones();
            _CompanyPhones.CompanyId = CompanyId;
            _CompanyPhones.PhoneTypeId = PhoneTypeId;
            db.Entry(_CompanyPhones).State = EntityState.Deleted;
            db.SaveChanges();
            var _Aplication = (from _ap in plm.Applications
                               where _ap.Description == "Sitio DEACI"
                               select _ap).ToList();
            foreach (var _App in _Aplication)
            {
                var _Tables = (from _tab in plm.Tables
                               where _tab.ApplicationId == _App.ApplicationId
                               && _tab.Description == "Phones"
                               select _tab).ToList();
                foreach (var _Tabs in _Tables)
                {
                    var _ApplicationId = _App.ApplicationId;
                    CountriesUsers p = (CountriesUsers)Session["CountriesUsers"];
                    ActivityLogs _ActivityLogs = new ActivityLogs();
                    int _OperationId = 4; // Eliminar
                    _ActivityLogs.UserId = p.userId;
                    _ActivityLogs.TableId = _Tabs.TableId; // Phones
                    _ActivityLogs.OperationId = _OperationId;
                    _ActivityLogs.Date = DateTime.Now;
                    _ActivityLogs.PrimaryKeyAffected = ("(CompanyId," + _CompanyPhones.CompanyId + "),(" + "PhoneTypeId," + _CompanyPhones.PhoneTypeId + ")");
                    _ActivityLogs.FieldsAffected = null;
                    plm.Entry(_ActivityLogs).State = EntityState.Added;
                    plm.SaveChanges();
                }
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        public ActionResult AgregarTeléfonos(int id)
        {
            var _Companies = db.Companies.Find(id);
            return View(_Companies);
        }
        public ActionResult AgregarTeléfono(string CompanyId, string PhoneTypeId, string PhoneValue)
        {
            int _CompanyId = int.Parse(CompanyId);
            byte _PhoneTypeId = byte.Parse(PhoneTypeId);
            var _Value = (from _cp in db.CompanyPhones
                          where _cp.CompanyId == _CompanyId && _cp.PhoneTypeId == _PhoneTypeId
                          select _cp);
            if (_Value.LongCount() > 0)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            else
            {
                CompanyPhones _CompanyPhones = new CompanyPhones();
                _CompanyPhones.CompanyId = _CompanyId;
                _CompanyPhones.PhoneTypeId = _PhoneTypeId;
                _CompanyPhones.PhoneValue = PhoneValue;
                db.Entry(_CompanyPhones).State = EntityState.Added;
                db.SaveChanges();
                var _Aplication = (from _ap in plm.Applications
                                   where _ap.Description == "Sitio DEACI"
                                   select _ap).ToList();
                foreach (var _App in _Aplication)
                {
                    var _Tables = (from _tab in plm.Tables
                                   where _tab.ApplicationId == _App.ApplicationId
                                   && _tab.Description == "Phones"
                                   select _tab).ToList();
                    foreach (var _Tabs in _Tables)
                    {
                        var _ApplicationId = _App.ApplicationId;
                        CountriesUsers p = (CountriesUsers)Session["CountriesUsers"];
                        ActivityLogs _ActivityLogs = new ActivityLogs();
                        int _OperationId = 1; // Agregar
                        _ActivityLogs.UserId = p.userId;
                        _ActivityLogs.TableId = _Tabs.TableId; // Phones
                        _ActivityLogs.OperationId = _OperationId;
                        _ActivityLogs.Date = DateTime.Now;
                        _ActivityLogs.PrimaryKeyAffected = ("(CompanyId," + _CompanyPhones.CompanyId + "),(" + "PhoneTypeId," + _CompanyPhones.PhoneTypeId + ")");
                        _ActivityLogs.FieldsAffected = ("(PhoneValue," + _CompanyPhones.PhoneValue + ")");
                        plm.Entry(_ActivityLogs).State = EntityState.Added;
                        plm.SaveChanges();
                    }
                }
                return Json(true, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult ModificarCliente(string CompanyId, string CompanyName, string CompanyShortName, string CompanytypeId, string EditionId, string CompanyIdParent)
        {
            int _EditionId = int.Parse(EditionId);
            int _CompanyId = int.Parse(CompanyId);
            Companies _Companies = new Companies();
            if (CompanyIdParent == "Seleccione...")
            { _Companies.CompanyIdParent = null;
            }
            else
            { int _CompanyIdParent = int.Parse(CompanyIdParent);
              _Companies.CompanyIdParent = _CompanyIdParent; }
            if (CompanyShortName == "") { CompanyShortName = null; }
            byte _CompanyTypeId = byte.Parse(CompanytypeId);
            _Companies.CompanyId = _CompanyId;
            _Companies.CompanyName = CompanyName;
            _Companies.CompanyShortName = CompanyShortName;
            _Companies.CompanyIdParent = null;
            _Companies.Active = true;
            db.Entry(_Companies).State = EntityState.Modified;
            db.SaveChanges();
            CompanyEditions _CompanyEditions = new CompanyEditions();
            _CompanyEditions.EditionId = _EditionId;
            _CompanyEditions.CompanyId = _CompanyId;
            _CompanyEditions.CompanyTypeId = _CompanyTypeId;
            _CompanyEditions.HtmlFile = null;
            _CompanyEditions.HtmlContent = null;
            _CompanyEditions.CloseClient = true;
            _CompanyEditions.Page = null;
            db.Entry(_CompanyEditions).State = EntityState.Modified;
            db.SaveChanges();
            var _Aplication = (from _ap in plm.Applications
                               where _ap.Description == "Sitio DEACI"
                               select _ap).ToList();
            foreach (var _App in _Aplication)
            {
                var _Tables = (from _tab in plm.Tables
                               where _tab.ApplicationId == _App.ApplicationId
                               && _tab.Description == "Companies"
                               select _tab).ToList();
                foreach (var _Tabs in _Tables)
                {
                    var _ApplicationId = _App.ApplicationId;
                    CountriesUsers p = (CountriesUsers)Session["CountriesUsers"];
                    ActivityLogs _ActivityLogs = new ActivityLogs();
                    int _OperationId = 2; // Modificar
                    _ActivityLogs.UserId = p.userId;
                    _ActivityLogs.TableId = _Tabs.TableId; // Companies
                    _ActivityLogs.OperationId = _OperationId;
                    _ActivityLogs.Date = DateTime.Now;
                    _ActivityLogs.PrimaryKeyAffected = ("(CompanyId," + _Companies.CompanyId + "),(" + "EditionId," + _EditionId + ")");
                    _ActivityLogs.FieldsAffected = ("(CompanyName," + _Companies.CompanyName + "),(" + "CompanyShortName," + _Companies.CompanyShortName + "),(" + "CompanyIdParent," + _Companies.CompanyIdParent + "),(" + "CompanyTypeId," + _CompanyEditions.CompanyTypeId + ")");
                    plm.Entry(_ActivityLogs).State = EntityState.Added;
                    plm.SaveChanges();
                }
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        public ActionResult AgregarDirecciones(int id) 
        {
            var _Companies = db.Companies.Find(id);
            return View(_Companies);
        }
        public ActionResult AgregarDirección(string CompanyId, string Address, string Suburb, string Ubication, string ZipCode, string Email, string Web, string Contact, string CityId, string Location)
        {
            int _CompanyId = int.Parse(CompanyId);
            Addresses _Addresses = new Addresses();
            _Addresses.Address = Address;
            if (Suburb == "")
            { _Addresses.Suburb = null; }
            else
            { _Addresses.Suburb = Suburb; }
            if (Ubication == "")
            { _Addresses.Ubication = null; }
            else
            { _Addresses.Ubication = Ubication; }
            if (ZipCode == "")
            { _Addresses.ZipCode = null; }
            else
            { _Addresses.ZipCode = ZipCode; }
            if (Email == "")
            { _Addresses.Email = null; }
            else
            { _Addresses.Email = Email; }
            if (Web == "")
            { _Addresses.Web = null; }
            else
            { _Addresses.Web = Web; }
            if (Contact == "")
            { _Addresses.Contact = null; }
            else
            { _Addresses.Contact = Contact; }
            if (CityId != "Seleccione...")
            {
                int _CityId = int.Parse(CityId);
                _Addresses.CityId = _CityId;
            }
            else
            { _Addresses.CityId = null; }
            if (Location == "")
            { _Addresses.Location = null; }
            else
            { _Addresses.Location = Location; }
            db.Entry(_Addresses).State = EntityState.Added;
            db.SaveChanges();
            CompanyAddresses _CompanyAddress = new CompanyAddresses();
            _CompanyAddress.AddressId = _Addresses.AddressId;
            _CompanyAddress.CompanyId = _CompanyId;
            db.Entry(_CompanyAddress).State = EntityState.Added;
            db.SaveChanges();
            var _Aplication = (from _ap in plm.Applications
                               where _ap.Description == "Sitio DEACI"
                               select _ap).ToList();
            foreach (var _App in _Aplication)
            {
                var _Tables = (from _tab in plm.Tables
                               where _tab.ApplicationId == _App.ApplicationId
                               && _tab.Description == "Addresses"
                               select _tab).ToList();
                foreach (var _Tabs in _Tables)
                {
                    var _ApplicationId = _App.ApplicationId;
                    CountriesUsers p = (CountriesUsers)Session["CountriesUsers"];
                    ActivityLogs _ActivityLogs = new ActivityLogs();
                    int _OperationId = 1; // Agregar
                    _ActivityLogs.UserId = p.userId;
                    _ActivityLogs.TableId = _Tabs.TableId; // Addresses
                    _ActivityLogs.OperationId = _OperationId;
                    _ActivityLogs.Date = DateTime.Now;
                    _ActivityLogs.PrimaryKeyAffected = ("(CompanyId," + _CompanyAddress.CompanyId + "),(" + "AddressId," + _CompanyAddress.AddressId + ")");
                    _ActivityLogs.FieldsAffected = ("(Address," + _Addresses.Address + "),(Suburb," + _Addresses.Suburb + "),(Ubication," + _Addresses.Ubication + "),(ZipCode," + _Addresses.ZipCode + "),(Email," + _Addresses.Email + "),(Web," + Web + "),(Contact," + _Addresses.Address + "),(CityId," + _Addresses.CityId + "),");
                    plm.Entry(_ActivityLogs).State = EntityState.Added;
                    plm.SaveChanges();
                }
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        public ActionResult EditarDirecciones(int id, int ed)
        {
            var _Address = (from _c in db.Companies
                            join _ca in db.CompanyAddresses
                                on _c.CompanyId equals _ca.CompanyId
                            join _ad in db.Addresses
                                on _ca.AddressId equals _ad.AddressId
                            where _ca.CompanyId == id
                            && _ca.AddressId == ed
                            select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Companies = _c, CompanyAddress = _ca, Addresses = _ad });
            return View(_Address);
        }
        public ActionResult EditarDirección(string AddressId, string Address, string Suburb, string Ubication, string ZipCode, string Email, string Web, string Contact, string CityId, string Location)
        {
            Addresses _Addresses = new Addresses();
            _Addresses.Address = Address;
            if (Suburb == "")
            { _Addresses.Suburb = null; }
            else
            { _Addresses.Suburb = Suburb; }
            if (Ubication == "")
            { _Addresses.Ubication = null; }
            else
            { _Addresses.Ubication = Ubication; }
            if (ZipCode == "")
            { _Addresses.ZipCode = null; }
            else
            { _Addresses.ZipCode = ZipCode; }
            if (Email == "")
            { _Addresses.Email = null; }
            else
            { _Addresses.Email = Email; }
            if (Web == "")
            { _Addresses.Web = null; }
            else
            { _Addresses.Web = Web; }
            if (Contact == "")
            { _Addresses.Contact = null; }
            else
            { _Addresses.Contact = Contact; }
            if (CityId != "Seleccione...")
            {
                int _CityId = int.Parse(CityId);
                _Addresses.CityId = _CityId;
            }
            else
            { _Addresses.CityId = null; }
            if (Location == "")
            { _Addresses.Location = null; }
            else
            { _Addresses.Location = Location; }
            int _AddressId = int.Parse(AddressId);
            _Addresses.AddressId = _AddressId;
            db.Entry(_Addresses).State = EntityState.Modified;
            db.SaveChanges();
            var _Aplication = (from _ap in plm.Applications
                               where _ap.Description == "Sitio DEACI"
                               select _ap).ToList();
            foreach (var _App in _Aplication)
            {
                var _Tables = (from _tab in plm.Tables
                               where _tab.ApplicationId == _App.ApplicationId
                               && _tab.Description == "Addresses"
                               select _tab).ToList();
                foreach (var _Tabs in _Tables)
                {
                    var _ApplicationId = _App.ApplicationId;
                    CountriesUsers p = (CountriesUsers)Session["CountriesUsers"];
                    ActivityLogs _ActivityLogs = new ActivityLogs();
                    int _OperationId = 2; // Modificar
                    _ActivityLogs.UserId = p.userId;
                    _ActivityLogs.TableId = _Tabs.TableId; // Addresses
                    _ActivityLogs.OperationId = _OperationId;
                    _ActivityLogs.Date = DateTime.Now;
                    _ActivityLogs.PrimaryKeyAffected = ("(" + "AddressId," + _AddressId + ")");
                    _ActivityLogs.FieldsAffected = ("(Address," + _Addresses.Address + "),(Suburb," + _Addresses.Suburb + "),(Ubication," + _Addresses.Ubication + "),(ZipCode," + _Addresses.ZipCode + "),(Email," + _Addresses.Email + "),(Web," + Web + "),(Contact," + _Addresses.Address + "),(CityId," + _Addresses.CityId + "),");
                    plm.Entry(_ActivityLogs).State = EntityState.Added;
                    plm.SaveChanges();
                }
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        public ActionResult EliminarDirección(int CompanyId, int AddressId)
        {
            var _deletecompanyaddress = db.CompanyAddresses.SingleOrDefault(model => model.CompanyId == CompanyId && model.AddressId == AddressId);
            db.Entry(_deletecompanyaddress).State = EntityState.Deleted;
            db.SaveChanges();
            //var _deleteaddess = db.Addresses.SingleOrDefault(model => model.AddressId == _AddressId);
            //db.Entry(_deleteaddess).State = EntityState.Deleted;
            //db.SaveChanges();
            var _Aplication = (from _ap in plm.Applications
                               where _ap.Description == "Sitio DEACI"
                               select _ap).ToList();
            foreach (var _App in _Aplication)
            {
                var _Tables = (from _tab in plm.Tables
                               where _tab.ApplicationId == _App.ApplicationId
                               && _tab.Description == "Addresses"
                               select _tab).ToList();
                foreach (var _Tabs in _Tables)
                {
                    var _ApplicationId = _App.ApplicationId;
                    CountriesUsers p = (CountriesUsers)Session["CountriesUsers"];
                    ActivityLogs _ActivityLogs = new ActivityLogs();
                    int _OperationId = 4; // Modificar
                    _ActivityLogs.UserId = p.userId;
                    _ActivityLogs.TableId = _Tabs.TableId; // Addresses
                    _ActivityLogs.OperationId = _OperationId;
                    _ActivityLogs.Date = DateTime.Now;
                    _ActivityLogs.PrimaryKeyAffected = ("(CompanyId," + _deletecompanyaddress.CompanyId + "),(" + "AddressId," + _deletecompanyaddress.AddressId + ")");
                    _ActivityLogs.FieldsAffected = null;
                    plm.Entry(_ActivityLogs).State = EntityState.Added;
                    plm.SaveChanges();
                }
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ObtenerCiudades(int id)
        {
            Cities _Ciudades = new Cities();
            List<Cities> _Cities = new List<Cities>();
            var _Cit = (from _c in db.Cities
                           where _c.StateId == id
                           select _c).ToList();
            foreach (var _a in _Cit)
            {
                _Ciudades = new Cities();
                _Ciudades.CityId = _a.CityId;
                _Ciudades.StateId = _a.StateId;
                _Ciudades.Name = _a.Name;
                _Ciudades.Active = _a.Active;
                _Cities.Add(_Ciudades);
            }
            return Json(_Cities, JsonRequestBehavior.AllowGet);
        }
        [ValidateInput(false)]
        public ActionResult AgregarClienteEdición(string CompanyId, string EditionId, string CompanyTypeId)
        {
            int _CompanyId = int.Parse(CompanyId);
            int _EditionId = int.Parse(EditionId);
            byte _CompanyTypeId = byte.Parse(CompanyTypeId);
            var _ValueParentParticipant = (from _c in db.Companies
                                           where _c.CompanyId == _CompanyId
                                           select _c).ToList();
            foreach (var a in _ValueParentParticipant)
            {
                if (a.CompanyIdParent != null)
                {
                    var _ParentValue = (from _c in db.Companies
                                        join _ce in db.CompanyEditions
                                            on _c.CompanyId equals _ce.CompanyId
                                        where _c.CompanyId == a.CompanyIdParent
                                        && _ce.EditionId == _EditionId
                                        select new { _c, _ce }).ToList();
                    if (_ParentValue.LongCount() == 0)
                    {
                        return Json(false, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            var _Value = (from _ce in db.CompanyEditions
                          where _ce.EditionId == _EditionId
                          && _ce.CompanyId == _CompanyId
                          select _ce).ToList();
            if (_Value.Count() > 0)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            else
            {
                CompanyEditions _CompanyEditions = new CompanyEditions();
                _CompanyEditions.CompanyId = _CompanyId;
                _CompanyEditions.EditionId = _EditionId;
                _CompanyEditions.HtmlFile = null;
                _CompanyEditions.HtmlContent = null;
                _CompanyEditions.CompanyTypeId = _CompanyTypeId;
                _CompanyEditions.CloseClient = true;
                db.Entry(_CompanyEditions).State = EntityState.Added;
                db.SaveChanges();
                var _Aplication = (from _ap in plm.Applications
                                   where _ap.Description == "Sitio DEACI"
                                   select _ap).ToList();
                foreach (var _App in _Aplication)
                {
                    var _Tables = (from _tab in plm.Tables
                                   where _tab.ApplicationId == _App.ApplicationId
                                   && _tab.Description == "CompanyEditions"
                                   select _tab).ToList();
                    foreach (var _Tabs in _Tables)
                    {
                        var _ApplicationId = _App.ApplicationId;
                        CountriesUsers p = (CountriesUsers)Session["CountriesUsers"];
                        ActivityLogs _ActivityLogs = new ActivityLogs();
                        int _OperationId = 1; // Agregar
                        _ActivityLogs.UserId = p.userId;
                        _ActivityLogs.TableId = _Tabs.TableId; // CompanyEditions
                        _ActivityLogs.OperationId = _OperationId;
                        _ActivityLogs.Date = DateTime.Now;
                        _ActivityLogs.PrimaryKeyAffected = ("(CompanyId," + _CompanyEditions.CompanyId + "),(" + "EditionId," + _CompanyEditions.EditionId + "),(" + "CompanyTypeId," + _CompanyEditions.CompanyTypeId + ")");
                        _ActivityLogs.FieldsAffected = null;
                        plm.Entry(_ActivityLogs).State = EntityState.Added;
                        plm.SaveChanges();
                    }
                }
                return Json(true, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult EliminarEdiciónCliente(int EditionId, int CompanyId)
        {
            var _ValueError = db.CompanyEditions.Where(model => model.CompanyId == CompanyId && model.EditionId == EditionId).ToList();
            if (_ValueError.LongCount() == 0)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            var _ValueParticipantClient = (from _c in db.Companies
                                           where _c.CompanyId == CompanyId
                                           select _c).ToList();
            foreach (var ClientParent in _ValueParticipantClient)
            {
                if (ClientParent.CompanyIdParent == null)
                {
                    var _Client = (from _c in db.Companies
                                   join _ce in db.CompanyEditions
                                   on _c.CompanyId equals _ce.CompanyId
                                   where _c.CompanyIdParent == ClientParent.CompanyId && _ce.EditionId == EditionId
                                   select _c).ToList();
                    foreach (var ClientSon in _Client)
                    {
                        var _deleteClient = db.CompanyEditions.SingleOrDefault(model => model.CompanyId == ClientSon.CompanyId && model.EditionId == EditionId);
                        var _EditionsFile = (from _ed in db.Editions
                                             where _ed.EditionId == EditionId
                                             select _ed).ToList();
                        db.CompanyEditions.Remove(_deleteClient);
                        db.SaveChanges();
                        var _Aplication = (from _ap in plm.Applications
                                           where _ap.Description == "Sitio DEACI"
                                           select _ap).ToList();
                        foreach (var _App in _Aplication)
                        {
                            var _Tables = (from _tab in plm.Tables
                                           where _tab.ApplicationId == _App.ApplicationId
                                           && _tab.Description == "CompanyEditions"
                                           select _tab).ToList();
                            foreach (var _Tabs in _Tables)
                            {
                                var _ApplicationId = _App.ApplicationId;
                                CountriesUsers p = (CountriesUsers)Session["CountriesUsers"];
                                ActivityLogs _ActivityLogs = new ActivityLogs();
                                int _OperationId = 4; // Eliminar
                                _ActivityLogs.UserId = p.userId;
                                _ActivityLogs.TableId = _Tabs.TableId; // CompanyEditions
                                _ActivityLogs.OperationId = _OperationId;
                                _ActivityLogs.Date = DateTime.Now;
                                _ActivityLogs.PrimaryKeyAffected = ("(CompanyId," + _deleteClient.CompanyId + "),(EditionId," + _deleteClient.EditionId + ")");
                                _ActivityLogs.FieldsAffected = null;
                                plm.Entry(_ActivityLogs).State = EntityState.Added;
                                plm.SaveChanges();
                            }
                        }
                        var _ProductsClientEditions = (from _c in db.Companies
                                                       join _p in db.Products
                                                       on _c.CompanyId equals _p.CompanyId
                                                       join _pe in db.ProductEditions
                                                       on _p.ProductId equals _pe.ProductId
                                                       where _c.CompanyId == ClientSon.CompanyId && _pe.EditionId == EditionId
                                                       select _pe).ToList();
                        if (_ProductsClientEditions.LongCount() > 0)
                        {
                            foreach (var _prod in _ProductsClientEditions)
                            {
                                var _DeleteProductEditions = db.ProductEditions.SingleOrDefault(model => model.EditionId == _prod.EditionId && model.ProductId == _prod.ProductId);
                                var _EditionsFileSon = (from _ed in db.Editions
                                                        where _ed.EditionId == _DeleteProductEditions.EditionId
                                                        select _ed).ToList();
                                db.ProductEditions.Remove(_DeleteProductEditions);
                                db.SaveChanges();
                                var _Aplication1 = (from _ap in plm.Applications
                                                   where _ap.Description == "Sitio DEACI"
                                                   select _ap).ToList();
                                foreach (var _App in _Aplication1)
                                {
                                    var _Tables = (from _tab in plm.Tables
                                                   where _tab.ApplicationId == _App.ApplicationId
                                                   && _tab.Description == "ProductEditions"
                                                   select _tab).ToList();
                                    foreach (var _Tabs in _Tables)
                                    {
                                        var _ApplicationId = _App.ApplicationId;
                                        CountriesUsers p = (CountriesUsers)Session["CountriesUsers"];
                                        ActivityLogs _ActivityLogs = new ActivityLogs();
                                        int _OperationId = 4; // Eliminar
                                        _ActivityLogs.UserId = p.userId;
                                        _ActivityLogs.TableId = _Tabs.TableId; // ProductEditions
                                        _ActivityLogs.OperationId = _OperationId;
                                        _ActivityLogs.Date = DateTime.Now;
                                        _ActivityLogs.PrimaryKeyAffected = ("(ProductId," + _DeleteProductEditions.ProductId + "),(" + "EditionId," + _DeleteProductEditions.EditionId + ")");
                                        _ActivityLogs.FieldsAffected = null;
                                        plm.Entry(_ActivityLogs).State = EntityState.Added;
                                        plm.SaveChanges();
                                    }
                                }
                            }
                        }
                        var _CompanyBrandEditionsClien = (from _cbe in db.CompanyBrandEditions
                                                          where _cbe.CompanyId == ClientSon.CompanyId && _cbe.EditionId == EditionId
                                                          select _cbe).ToList();
                        if (_CompanyBrandEditionsClien.LongCount() > 0)
                        {
                            foreach (var _cbec in _CompanyBrandEditionsClien)
                            {
                                var _DeleteCompanyBrandEditionClient = db.CompanyBrandEditions.SingleOrDefault(model => model.EditionId == _cbec.EditionId
                                && model.CompanyId == _cbec.CompanyId && model.BrandId == _cbec.BrandId);
                                db.CompanyBrandEditions.Remove(_DeleteCompanyBrandEditionClient);
                                db.SaveChanges();
                                var _Aplication2 = (from _ap in plm.Applications
                                                   where _ap.Description == "Sitio DEACI"
                                                   select _ap).ToList();
                                foreach (var _App in _Aplication2)
                                {
                                    var _Tables = (from _tab in plm.Tables
                                                   where _tab.ApplicationId == _App.ApplicationId
                                                   && _tab.Description == "CompanyBrandEditions"
                                                   select _tab).ToList();
                                    foreach (var _Tabs in _Tables)
                                    {
                                        var _ApplicationId = _App.ApplicationId;
                                        CountriesUsers p = (CountriesUsers)Session["CountriesUsers"];
                                        ActivityLogs _ActivityLogs = new ActivityLogs();
                                        int _OperationId = 4; // Eliminar
                                        _ActivityLogs.UserId = p.userId;
                                        _ActivityLogs.TableId = _Tabs.TableId; // CompanyBrandEditions
                                        _ActivityLogs.OperationId = _OperationId;
                                        _ActivityLogs.Date = DateTime.Now;
                                        _ActivityLogs.PrimaryKeyAffected = ("(BrandId," + _DeleteCompanyBrandEditionClient.BrandId + "),(EditionId," + _DeleteCompanyBrandEditionClient.EditionId + "),(CompanyId," + _DeleteCompanyBrandEditionClient.CompanyId + ")");
                                        _ActivityLogs.FieldsAffected = null;
                                        plm.Entry(_ActivityLogs).State = EntityState.Added;
                                        plm.SaveChanges();
                                    }
                                }
                            }
                        }
                        var _Adver = (from _ade in db.AdvertisementEditions
                                      where _ade.CompanyId == ClientSon.CompanyId
                                      && _ade.EditionId == EditionId
                                      select _ade).ToList();
                        foreach (var _Ade in _Adver)
                        {
                            var _DeleteAdver = db.AdvertisementEditions.SingleOrDefault(model => model.CompanyId == _Ade.CompanyId && _Ade.EditionId == _Ade.EditionId);
                            var _AdversFiles = (from _ad in db.Advertisements
                                                where _ad.AdvId == _Ade.AdvId
                                                select _ad).ToList();
                            db.AdvertisementEditions.Remove(_DeleteAdver);
                            db.SaveChanges();
                            var _Aplication3 = (from _ap in plm.Applications
                                               where _ap.Description == "Sitio DEACI"
                                               select _ap).ToList();
                            foreach (var _App in _Aplication3)
                            {
                                var _Tables = (from _tab in plm.Tables
                                               where _tab.ApplicationId == _App.ApplicationId
                                               && _tab.Description == "AdvertisementEditions"
                                               select _tab).ToList();
                                foreach (var _Tabs in _Tables)
                                {
                                    var _ApplicationId = _App.ApplicationId;
                                    CountriesUsers p = (CountriesUsers)Session["CountriesUsers"];
                                    ActivityLogs _ActivityLogs = new ActivityLogs();
                                    int _OperationId = 4; // Eliminar
                                    _ActivityLogs.UserId = p.userId;
                                    _ActivityLogs.TableId = _Tabs.TableId; // AdvertisementEditions
                                    _ActivityLogs.OperationId = _OperationId;
                                    _ActivityLogs.Date = DateTime.Now;
                                    _ActivityLogs.PrimaryKeyAffected = ("(AdvId," + _DeleteAdver.AdvId + "),(EditionId," + _DeleteAdver.EditionId + "),(CompanyId," + _DeleteAdver.CompanyId + ")");
                                    _ActivityLogs.FieldsAffected = null;
                                    plm.Entry(_ActivityLogs).State = EntityState.Added;
                                    plm.SaveChanges();
                                }
                            }
                        }
                    }
                }
            }
            CompanyEditions _CompanyEditions = new CompanyEditions();
            _CompanyEditions.EditionId = EditionId;
            _CompanyEditions.CompanyId = CompanyId;
            var _eliminarCompanyEditions = db.CompanyEditions.SingleOrDefault(model => model.EditionId == _CompanyEditions.EditionId
            && model.CompanyId == _CompanyEditions.CompanyId);
            var _EditionsFileClient = (from _ed in db.Editions
                                 where _ed.EditionId == EditionId
                                 select _ed).ToList();
            db.CompanyEditions.Remove(_eliminarCompanyEditions);
            db.SaveChanges();
            var _Aplication4 = (from _ap in plm.Applications
                               where _ap.Description == "Sitio DEACI"
                               select _ap).ToList();
            foreach (var _App in _Aplication4)
            {
                var _Tables = (from _tab in plm.Tables
                               where _tab.ApplicationId == _App.ApplicationId
                               && _tab.Description == "CompanyEditions"
                               select _tab).ToList();
                foreach (var _Tabs in _Tables)
                {
                    var _ApplicationId = _App.ApplicationId;
                    CountriesUsers p = (CountriesUsers)Session["CountriesUsers"];
                    ActivityLogs _ActivityLogs = new ActivityLogs();
                    int _OperationId = 4; // Eliminar
                    _ActivityLogs.UserId = p.userId;
                    _ActivityLogs.TableId = _Tabs.TableId; // CompanyEditions
                    _ActivityLogs.OperationId = _OperationId;
                    _ActivityLogs.Date = DateTime.Now;
                    _ActivityLogs.PrimaryKeyAffected = ("(CompanyId," + _eliminarCompanyEditions.CompanyId + "),(EditionId," + _eliminarCompanyEditions.EditionId + ")");
                    _ActivityLogs.FieldsAffected = null;
                    plm.Entry(_ActivityLogs).State = EntityState.Added;
                    plm.SaveChanges();
                }
            }
            var _producteditions = (from _c in db.Companies
                                    join _p in db.Products
                                    on _c.CompanyId equals _p.CompanyId
                                    join _pe in db.ProductEditions
                                    on _p.ProductId equals _pe.ProductId
                                    where _c.CompanyId == _CompanyEditions.CompanyId && _pe.EditionId == _CompanyEditions.EditionId
                                    select _pe).ToList();
            if (_producteditions.LongCount() > 0)
            {
                foreach (ProductEditions _pre in _producteditions)
                {
                    ProductEditions _productdeleteditions = new ProductEditions();
                    _productdeleteditions.ProductId = _pre.ProductId;
                    _productdeleteditions.EditionId = _pre.EditionId;
                    _productdeleteditions.HtmlFile = _pre.HtmlFile;
                    _productdeleteditions.HtmlContent = _pre.HtmlContent;
                    var _deleteProductEditions = db.ProductEditions.SingleOrDefault(model => model.ProductId == _pre.ProductId
                    && model.EditionId == _pre.EditionId);
                    var _EditionsFileSon = (from _ed in db.Editions
                                            where _ed.EditionId == _deleteProductEditions.EditionId
                                            select _ed).ToList();
                    db.ProductEditions.Remove(_deleteProductEditions);
                    db.SaveChanges();
                    var _Aplication = (from _ap in plm.Applications
                                       where _ap.Description == "Sitio DEACI"
                                       select _ap).ToList();
                    foreach (var _App in _Aplication)
                    {
                        var _Tables = (from _tab in plm.Tables
                                       where _tab.ApplicationId == _App.ApplicationId
                                       && _tab.Description == "ProductEditions"
                                       select _tab).ToList();
                        foreach (var _Tabs in _Tables)
                        {
                            var _ApplicationId = _App.ApplicationId;
                            CountriesUsers p = (CountriesUsers)Session["CountriesUsers"];
                            ActivityLogs _ActivityLogs = new ActivityLogs();
                            int _OperationId = 4; // Eliminar
                            _ActivityLogs.UserId = p.userId;
                            _ActivityLogs.TableId = _Tabs.TableId; // ProductEditions
                            _ActivityLogs.OperationId = _OperationId;
                            _ActivityLogs.Date = DateTime.Now;
                            _ActivityLogs.PrimaryKeyAffected = ("(ProductId," + _deleteProductEditions.ProductId + "),(" + "EditionId," + _deleteProductEditions.EditionId + ")");
                            _ActivityLogs.FieldsAffected = null;
                            plm.Entry(_ActivityLogs).State = EntityState.Added;
                            plm.SaveChanges();
                        }
                    }
                }
            }
            var _companybrandeditions = (from _cbe in db.CompanyBrandEditions
                                         where _cbe.CompanyId == _CompanyEditions.CompanyId && _cbe.EditionId == _CompanyEditions.EditionId
                                         select _cbe).ToList();
            if (_companybrandeditions.LongCount() > 0)
            {
                foreach (CompanyBrandEditions _cbre in _companybrandeditions)
                {
                    CompanyBrandEditions _companybrandeleteditions = new CompanyBrandEditions();
                    _companybrandeleteditions.EditionId = _CompanyEditions.EditionId;
                    _companybrandeleteditions.CompanyId = _CompanyEditions.CompanyId;
                    _companybrandeleteditions.BrandId = _cbre.BrandId;
                    var _eliminarCompanyBrandEditions = db.CompanyBrandEditions.SingleOrDefault(model => model.EditionId == _CompanyEditions.EditionId
                    && model.CompanyId == _CompanyEditions.CompanyId && model.BrandId == _cbre.BrandId);
                    db.CompanyBrandEditions.Remove(_eliminarCompanyBrandEditions);
                    db.SaveChanges();
                    var _Aplication = (from _ap in plm.Applications
                                       where _ap.Description == "Sitio DEACI"
                                       select _ap).ToList();
                    foreach (var _App in _Aplication)
                    {
                        var _Tables = (from _tab in plm.Tables
                                       where _tab.ApplicationId == _App.ApplicationId
                                       && _tab.Description == "CompanyBrandEditions"
                                       select _tab).ToList();
                        foreach (var _Tabs in _Tables)
                        {
                            var _ApplicationId = _App.ApplicationId;
                            CountriesUsers p = (CountriesUsers)Session["CountriesUsers"];
                            ActivityLogs _ActivityLogs = new ActivityLogs();
                            int _OperationId = 4; // Eliminar
                            _ActivityLogs.UserId = p.userId;
                            _ActivityLogs.TableId = _Tabs.TableId; // CompanyBrandEditions
                            _ActivityLogs.OperationId = _OperationId;
                            _ActivityLogs.Date = DateTime.Now;
                            _ActivityLogs.PrimaryKeyAffected = ("(BrandId," + _eliminarCompanyBrandEditions.BrandId + "),(EditionId," + _eliminarCompanyBrandEditions.EditionId + "),(CompanyId," + _eliminarCompanyBrandEditions.CompanyId + ")");
                            _ActivityLogs.FieldsAffected = null;
                            plm.Entry(_ActivityLogs).State = EntityState.Added;
                            plm.SaveChanges();
                        }
                    }
                }
            }
            var _Advers = (from _ade in db.AdvertisementEditions
                           where _ade.EditionId == EditionId
                           && _ade.CompanyId == CompanyId
                           select _ade).ToList();
            foreach (var _Advrs in _Advers)
            {
                var _DeleteAdvs = db.AdvertisementEditions.SingleOrDefault(model => model.CompanyId == _Advrs.CompanyId
                && model.EditionId == _Advrs.EditionId && model.AdvId == _Advrs.AdvId);
                var _AdversFiles = (from _ad in db.Advertisements
                                    where _ad.AdvId == _Advrs.AdvId
                                    select _ad).ToList();
                foreach (var _addss in _AdversFiles)
                {
                    if (_addss.AdvFile != null)
                    {
                        var imageName = "";
                        imageName = _AdversFiles.SingleOrDefault().AdvFile;
                        var _Editions = (from _ed in db.Editions
                                         where _ed.EditionId == EditionId
                                         select _ed).ToList();
                    }
                }
                db.AdvertisementEditions.Remove(_DeleteAdvs);
                db.SaveChanges();
                var _Aplication = (from _ap in plm.Applications
                                   where _ap.Description == "Sitio DEACI"
                                   select _ap).ToList();
                foreach (var _App in _Aplication)
                {
                    var _Tables = (from _tab in plm.Tables
                                   where _tab.ApplicationId == _App.ApplicationId
                                   && _tab.Description == "AdvertisementEditions"
                                   select _tab).ToList();
                    foreach (var _Tabs in _Tables)
                    {
                        var _ApplicationId = _App.ApplicationId;
                        CountriesUsers p = (CountriesUsers)Session["CountriesUsers"];
                        ActivityLogs _ActivityLogs = new ActivityLogs();
                        int _OperationId = 4; // Eliminar
                        _ActivityLogs.UserId = p.userId;
                        _ActivityLogs.TableId = _Tabs.TableId; // AdvertisementEditions
                        _ActivityLogs.OperationId = _OperationId;
                        _ActivityLogs.Date = DateTime.Now;
                        _ActivityLogs.PrimaryKeyAffected = ("(AdvId," + _DeleteAdvs.AdvId + "),(EditionId," + _DeleteAdvs.EditionId + "),(CompanyId," + _DeleteAdvs.CompanyId + ")");
                        _ActivityLogs.FieldsAffected = null;
                        plm.Entry(_ActivityLogs).State = EntityState.Added;
                        plm.SaveChanges();
                    }
                }
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        public ActionResult AgregarSeccionesCliente(int id)
        {
            var _Client = (from _c in db.Companies
                           where _c.CompanyId == id
                           select _c).ToList();
            ViewData["CompanyName"] = _Client.SingleOrDefault().CompanyName;
            ViewData["CompanyId"] = _Client.SingleOrDefault().CompanyId;
            return View();
        }
        public ActionResult EliminarSecciónCliente(string CompanyId, string SectionId)
        {
            var _CompanyId = int.Parse(CompanyId);
            var _SectionId = int.Parse(SectionId);
            CompanySections _CompanySections = new CompanySections();
            _CompanySections.CompanyId = _CompanyId;
            _CompanySections.SectionId = _SectionId;
            db.Entry(_CompanySections).State = EntityState.Deleted;
            db.SaveChanges();
            var _Aplicationn = (from _ap in plm.Applications
                                where _ap.Description == "Sitio DEACI"
                                select _ap).ToList();
            foreach (var _App in _Aplicationn)
            {
                var _Tables = (from _tab in plm.Tables
                               where _tab.ApplicationId == _App.ApplicationId
                               && _tab.Description == "CompanySections"
                               select _tab).ToList();
                foreach (var _Tabs in _Tables)
                {
                    var _ApplicationId = _App.ApplicationId;
                    CountriesUsers p = (CountriesUsers)Session["CountriesUsers"];
                    ActivityLogs _ActivityLogs = new ActivityLogs();
                    int _OperationId = 4; // Eliminar
                    _ActivityLogs.UserId = p.userId;
                    _ActivityLogs.TableId = _Tabs.TableId; // CompanySections
                    _ActivityLogs.OperationId = _OperationId;
                    _ActivityLogs.Date = DateTime.Now;
                    _ActivityLogs.PrimaryKeyAffected = ("(CompanyId," + _CompanySections.CompanyId + "),(SectionId," + _CompanySections.SectionId + ")");
                    _ActivityLogs.FieldsAffected = null;
                    plm.Entry(_ActivityLogs).State = EntityState.Added;
                    plm.SaveChanges();
                }
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        public ActionResult AgregarClienteSección(string CompanyId, string SectionId)
        {
            int _CompanyId = int.Parse(CompanyId);
            int _SectionId = int.Parse(SectionId);
            var _value = (from _cs in db.CompanySections
                          where _cs.CompanyId == _CompanyId && _cs.SectionId == _SectionId
                          select _cs).ToList();
            if (_value.LongCount() > 0)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            CompanySections _CompanySections = new CompanySections();
            _CompanySections.CompanyId = _CompanyId;
            _CompanySections.SectionId = _SectionId;
            _CompanySections.Active = true;
            db.Entry(_CompanySections).State = EntityState.Added;
            db.SaveChanges();
            var _Aplication = (from _ap in plm.Applications
                               where _ap.Description == "Sitio DEACI"
                               select _ap).ToList();
            foreach (var _App in _Aplication)
            {
                var _Tables = (from _tab in plm.Tables
                               where _tab.ApplicationId == _App.ApplicationId
                               && _tab.Description == "CompanySections"
                               select _tab).ToList();
                foreach (var _Tabs in _Tables)
                {
                    var _ApplicationId = _App.ApplicationId;
                    CountriesUsers p = (CountriesUsers)Session["CountriesUsers"];
                    ActivityLogs _ActivityLogs = new ActivityLogs();
                    int _OperationId = 1; // Agregar
                    _ActivityLogs.UserId = p.userId;
                    _ActivityLogs.TableId = _Tabs.TableId; // CompanySections
                    _ActivityLogs.OperationId = _OperationId;
                    _ActivityLogs.Date = DateTime.Now;
                    _ActivityLogs.PrimaryKeyAffected = ("(CompanyId," + _CompanySections.CompanyId + "),(SectionId," + _CompanySections.SectionId + ")");
                    _ActivityLogs.FieldsAffected = null;
                    plm.Entry(_ActivityLogs).State = EntityState.Added;
                    plm.SaveChanges();
                }
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ReporteEdición(int id, int ed, string palabra, string sortOrder, string currentFilter, string searchString, int? page)
        {
            SessionEditionId _SessionEditionId = new SessionEditionId(id, ed);
            Session["SessionEditionId"] = _SessionEditionId;
            var _Country = (from _ed in db.Editions
                            join _co in db.Countries
                            on _ed.CountryId equals _co.CountryId
                            where _ed.EditionId == id
                            select _co).ToList();
            ViewData["País"] = _Country.SingleOrDefault().CountryName;
            var _CountryBook = (from _ed in db.Editions
                                join _bo in db.Books
                                on _ed.BookId equals _bo.BookId
                                where _ed.EditionId == id
                                select _bo).ToList();
            ViewData["Obra"] = _CountryBook.SingleOrDefault().BookName;
            var _Editions = (from _ed in db.Editions
                             where _ed.EditionId == id
                             select _ed).ToList();
            ViewData["Edición"] = _Editions.SingleOrDefault().NumberEdition;
            ViewData["Number"] = _Editions.SingleOrDefault().EditionId;
            ViewData["CountryId"] = _Country.FirstOrDefault().CountryId;
            ViewData["BookId"] = _CountryBook.FirstOrDefault().BookId;
            var _Edition = (from _ed in db.Editions
                            join _ce in db.CompanyEditions
                            on _ed.EditionId equals _ce.EditionId
                            join _c in db.Companies
                            on _ce.CompanyId equals _c.CompanyId
                            where _ed.EditionId == id
                            && _ce.EditionId == id
                            orderby _ce.CompanyTypes.Description
                            select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Companies = _c, CompanyEditions = _ce, Editions = _ed });
            var _CompanyEditions = (from _ce in db.CompanyEditions
                                    where _ce.EditionId == id
                                    select _ce).ToList();
            if (_CompanyEditions.LongCount() == 0)
            {
                ViewData["CompanyNull"] = "";
            }
            var _Count = _CompanyEditions.LongCount();
            ViewData["CountClients"] = _Count;
            var _Products = (from _pe in db.ProductEditions
                             where _pe.EditionId == id
                             select _pe).ToList();
            var _CountProducts = _Products.LongCount();
            ViewData["CountProducts"] = _CountProducts;
            var _Brands = (from _cbe in db.CompanyBrandEditions
                           where _cbe.EditionId == id
                           select _cbe).ToList();
            var _CountBrands = _Brands.LongCount();
            ViewData["CountBrands"] = _CountBrands;
            var _Advers = (from _ade in db.EditionCompanySectionAdvers
                           where _ade.EditionId == id
                           select _ade).ToList();
            var _CountAdver = _Advers.LongCount();
            ViewData["CountAdvers"] = _CountAdver;
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            if (palabra != null)
            {
                page = 1;
            }
            else
            {
                palabra = currentFilter;
            }
            ViewBag.CurrentFilter = palabra;
            if (palabra != null)
            {
                if (palabra.LongCount() <= 3)
                {
                    if (!String.IsNullOrEmpty(palabra))
                    {
                        _Edition = _Edition.Where(j => j.Companies.CompanyName.StartsWith(palabra));
                    }
                }
                if (palabra.LongCount() > 3)
                {
                    if (!String.IsNullOrEmpty(palabra))
                    {
                        _Edition = _Edition.Where(j => j.Companies.CompanyName.Contains(palabra));
                    }
                }
            }
            ViewBag.CurrentFilter = palabra;
            int pageSize = 100;
            int pageNumber = (page ?? 1);
            return View(_Edition.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult ReporteEdiciónPDF(int id, int ed)
        {
            SessionEditionId _SessionEditionId = new SessionEditionId(id, ed);
            Session["SessionEditionId"] = _SessionEditionId;
            var _Country = (from _ed in db.Editions
                            join _co in db.Countries
                            on _ed.CountryId equals _co.CountryId
                            where _ed.EditionId == id
                            select _co).ToList();
            ViewData["País"] = _Country.SingleOrDefault().CountryName;
            var _CountryBook = (from _ed in db.Editions
                                join _bo in db.Books
                                on _ed.BookId equals _bo.BookId
                                where _ed.EditionId == id
                                select _bo).ToList();
            ViewData["Obra"] = _CountryBook.SingleOrDefault().BookName;
            var _Editions = (from _ed in db.Editions
                             where _ed.EditionId == id
                             select _ed).ToList();
            ViewData["Edición"] = _Editions.SingleOrDefault().NumberEdition;
            ViewData["Number"] = _Editions.SingleOrDefault().EditionId;
            var _Edition = (from _ed in db.Editions
                            join _ce in db.CompanyEditions
                            on _ed.EditionId equals _ce.EditionId
                            join _c in db.Companies
                            on _ce.CompanyId equals _c.CompanyId
                            where _ed.EditionId == id
                            && _ce.EditionId == id
                            orderby _ce.CompanyTypes.Description
                            select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Companies = _c, CompanyEditions = _ce, Editions = _ed });
            var _CompanyEditions = (from _ce in db.CompanyEditions
                                    where _ce.EditionId == id
                                    select _ce).ToList();
            if (_CompanyEditions.LongCount() == 0)
            {
                ViewData["CompanyNull"] = "";
            }
            var _Count = _CompanyEditions.LongCount();
            ViewData["CountClients"] = _Count;
            var _Products = (from _pe in db.ProductEditions
                             where _pe.EditionId == id
                             select _pe).ToList();
            var _CountProducts = _Products.LongCount();
            ViewData["CountProducts"] = _CountProducts;
            var _Brands = (from _cbe in db.CompanyBrandEditions
                           where _cbe.EditionId == id
                           select _cbe).ToList();
            var _CountBrands = _Brands.LongCount();
            ViewData["CountBrands"] = _CountBrands;
            var _Advers = (from _ade in db.EditionCompanySectionAdvers
                           where _ade.EditionId == id
                           select _ade).ToList();
            var _CountAdver = _Advers.LongCount();
            ViewData["CountAdvers"] = _CountAdver;
            return View(_Edition);
        }
        public ActionResult ReporteProductosEdición(int id, int ed, string palabra, string sortOrder, string currentFilter, string searchString, int? page)
        {
            SessionEditionId _SessionEditionId = new SessionEditionId(id, ed);
            Session["SessionEditionId"] = _SessionEditionId;
            var _Country = (from _ed in db.Editions
                            join _co in db.Countries
                            on _ed.CountryId equals _co.CountryId
                            where _ed.EditionId == id
                            select _co).ToList();
            ViewData["País"] = _Country.SingleOrDefault().CountryName;
            var _CountryBook = (from _ed in db.Editions
                                join _bo in db.Books
                                on _ed.BookId equals _bo.BookId
                                where _ed.EditionId == id
                                select _bo).ToList();
            ViewData["Obra"] = _CountryBook.SingleOrDefault().BookName;
            var _Editions = (from _ed in db.Editions
                             where _ed.EditionId == id
                             select _ed).ToList();
            ViewData["Edición"] = _Editions.SingleOrDefault().NumberEdition;
            ViewData["Number"] = _Editions.SingleOrDefault().EditionId;
            ViewData["CountryId"] = _Country.FirstOrDefault().CountryId;
            ViewData["BookId"] = _CountryBook.FirstOrDefault().BookId;
            var _ProductEditions = (from _p in db.Products
                                    join _pt in db.ProductTypes
                                    on _p.ProductTypeId equals _pt.ProductTypeId
                                    join _pe in db.ProductEditions
                                    on _p.ProductId equals _pe.ProductId
                                    join _c in db.Companies
                                    on _p.CompanyId equals _c.CompanyId
                                    where _pe.EditionId == id
                                    orderby _pt.Description == "PRODUCTO (TEXTO EDITORIAL)", _pt.Description == "(PRODUCTO) en TERCERA", _pt.Description == "AGREGADO", _c.CompanyName, _p.ProductName
                                    select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Companies = _c, ProductEditions = _pe, Products = _p, ProductTypes = _pt });
            var _ProductEditionsCount = (from _pe in db.ProductEditions
                                         where _pe.EditionId == id
                                         select _pe).ToList();
            var _CountProducts = _ProductEditionsCount.LongCount();


            ///

            var _ProductsFT = (from _p in db.Products
                               join _pt in db.ProductTypes
                                   on _p.ProductTypeId equals _pt.ProductTypeId
                               join _pe in db.ProductEditions
                                   on _p.ProductId equals _pe.ProductId
                               where _pe.EditionId == id
                               && _pt.Description != "(PRODUCTO) en TERCERA"
                               && _pt.Description != "AGREGADO"
                               && _pt.Description != "PRODUCTO (TEXTO EDITORIAL)"
                               && _pt.Description != "MARCA"
                               select _p).ToList();
            var _CountProductsFT = _ProductsFT.LongCount();
            ViewData["FICHATÉCNICA"] = _CountProductsFT;
            var _ProductsAG = (from _p in db.Products
                               join _pt in db.ProductTypes
                                   on _p.ProductTypeId equals _pt.ProductTypeId
                               join _pe in db.ProductEditions
                                   on _p.ProductId equals _pe.ProductId
                               where _pe.EditionId == id
                               && _pt.Description == "AGREGADO"
                               select _p).ToList();
            var _CountProductsAG = _ProductsAG.LongCount();
            ViewData["AGREGADO"] = _CountProductsAG;
            var _ProductsTE = (from _p in db.Products
                               join _pt in db.ProductTypes
                                   on _p.ProductTypeId equals _pt.ProductTypeId
                               join _pe in db.ProductEditions
                                   on _p.ProductId equals _pe.ProductId
                               where _pe.EditionId == id
                               && _pt.Description == "(PRODUCTO) en TERCERA"
                               select _p).ToList();
            var _CountProductsTE = _ProductsTE.LongCount();
            ViewData["TERCERA"] = _CountProductsTE;
            var _ProductsTED = (from _p in db.Products
                                join _pt in db.ProductTypes
                                on _p.ProductTypeId equals _pt.ProductTypeId
                                join _pe in db.ProductEditions
                                on _p.ProductId equals _pe.ProductId
                                where _pe.EditionId == id
                                && _pt.Description == "PRODUCTO (TEXTO EDITORIAL)"
                                select _p).ToList();
            var _CountProductsTED = _ProductsTED.LongCount();
            ViewData["EDITORIAL"] = _CountProductsTED;

            ///

            if (_ProductEditionsCount.LongCount() == 0)
            {
                ViewData["CompanyNull"] = "";
            }
            ViewData["CountProductEditions"] = _CountProducts;
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            if (palabra != null)
            {
                page = 1;
            }
            else
            {
                palabra = currentFilter;
            }
            ViewBag.CurrentFilter = palabra;
            if (palabra != null)
            {
                if (palabra.LongCount() <= 3)
                {
                    if (!String.IsNullOrEmpty(palabra))
                    {
                        _ProductEditions = _ProductEditions.Where(j => j.Products.ProductName.StartsWith(palabra));
                    }
                }
                if (palabra.LongCount() > 3)
                {
                    if (!String.IsNullOrEmpty(palabra))
                    {
                        _ProductEditions = _ProductEditions.Where(j => j.Products.ProductName.Contains(palabra));
                    }
                }
            }
            ViewBag.CurrentFilter = palabra;
            int pageSize = 100;
            int pageNumber = (page ?? 1);
            return View(_ProductEditions.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult ReporteProductosEdiciónPDF(int id, int ed)
        {
            SessionEditionId _SessionEditionId = new SessionEditionId(id, ed);
            Session["SessionEditionId"] = _SessionEditionId;
            var _Country = (from _ed in db.Editions
                            join _co in db.Countries
                            on _ed.CountryId equals _co.CountryId
                            where _ed.EditionId == id
                            select _co).ToList();
            ViewData["País"] = _Country.SingleOrDefault().CountryName;
            var _CountryBook = (from _ed in db.Editions
                                join _bo in db.Books
                                on _ed.BookId equals _bo.BookId
                                where _ed.EditionId == id
                                select _bo).ToList();
            ViewData["Obra"] = _CountryBook.SingleOrDefault().BookName;
            var _Editions = (from _ed in db.Editions
                             where _ed.EditionId == id
                             select _ed).ToList();
            ViewData["Edición"] = _Editions.SingleOrDefault().NumberEdition;
            ViewData["Number"] = _Editions.SingleOrDefault().EditionId;
            var _ProductEditions = (from _p in db.Products
                                    join _pt in db.ProductTypes
                                    on _p.ProductTypeId equals _pt.ProductTypeId
                                    join _pe in db.ProductEditions
                                    on _p.ProductId equals _pe.ProductId
                                    join _c in db.Companies
                                    on _p.CompanyId equals _c.CompanyId
                                    where _pe.EditionId == id
                                    orderby _pt.Description == "PRODUCTO (TEXTO EDITORIAL)", _pt.Description == "(PRODUCTO) en TERCERA", _pt.Description == "AGREGADO", _c.CompanyName, _p.ProductName
                                    select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Companies = _c, ProductEditions = _pe, Products = _p, ProductTypes = _pt });
            var _ProductEditionsCount = (from _pe in db.ProductEditions
                                         where _pe.EditionId == id
                                         select _pe).ToList();
            var _CountProducts = _ProductEditionsCount.LongCount();
            ///

            var _ProductsFT = (from _p in db.Products
                               join _pt in db.ProductTypes
                                   on _p.ProductTypeId equals _pt.ProductTypeId
                               join _pe in db.ProductEditions
                                   on _p.ProductId equals _pe.ProductId
                               where _pe.EditionId == id
                               && _pt.Description != "(PRODUCTO) en TERCERA"
                               && _pt.Description != "AGREGADO"
                               && _pt.Description != "PRODUCTO (TEXTO EDITORIAL)"
                               && _pt.Description != "MARCA"
                               select _p).ToList();
            var _CountProductsFT = _ProductsFT.LongCount();
            ViewData["FICHATÉCNICA"] = _CountProductsFT;
            var _ProductsAG = (from _p in db.Products
                               join _pt in db.ProductTypes
                                   on _p.ProductTypeId equals _pt.ProductTypeId
                               join _pe in db.ProductEditions
                                   on _p.ProductId equals _pe.ProductId
                               where _pe.EditionId == id
                               && _pt.Description == "AGREGADO"
                               select _p).ToList();
            var _CountProductsAG = _ProductsAG.LongCount();
            ViewData["AGREGADO"] = _CountProductsAG;
            var _ProductsTE = (from _p in db.Products
                               join _pt in db.ProductTypes
                                   on _p.ProductTypeId equals _pt.ProductTypeId
                               join _pe in db.ProductEditions
                                   on _p.ProductId equals _pe.ProductId
                               where _pe.EditionId == id
                               && _pt.Description == "(PRODUCTO) en TERCERA"
                               select _p).ToList();
            var _CountProductsTE = _ProductsTE.LongCount();
            ViewData["TERCERA"] = _CountProductsTE;
            var _ProductsTED = (from _p in db.Products
                                join _pt in db.ProductTypes
                                on _p.ProductTypeId equals _pt.ProductTypeId
                                join _pe in db.ProductEditions
                                on _p.ProductId equals _pe.ProductId
                                where _pe.EditionId == id
                                && _pt.Description == "PRODUCTO (TEXTO EDITORIAL)"
                                select _p).ToList();
            var _CountProductsTED = _ProductsTED.LongCount();
            ViewData["EDITORIAL"] = _CountProductsTED;

            ///
            if (_ProductEditionsCount.LongCount() == 0)
            {
                ViewData["CompanyNull"] = "";
            }
            ViewData["CountProductEditions"] = _CountProducts;
            return View(_ProductEditions);
        }
        public ActionResult ReporteProductosSección(int id, int ed, string palabra, string sortOrder, string currentFilter, string searchString, int? page)
        {
            SessionEditionId _SessionEditionId = new SessionEditionId(id, ed);
            Session["SessionEditionId"] = _SessionEditionId;
            var _Country = (from _ed in db.Editions
                            join _co in db.Countries
                            on _ed.CountryId equals _co.CountryId
                            where _ed.EditionId == id
                            select _co).ToList();
            ViewData["País"] = _Country.SingleOrDefault().CountryName;
            var _CountryBook = (from _ed in db.Editions
                                join _bo in db.Books
                                on _ed.BookId equals _bo.BookId
                                where _ed.EditionId == id
                                select _bo).ToList();
            ViewData["Obra"] = _CountryBook.SingleOrDefault().BookName;
            var _Editions = (from _ed in db.Editions
                             where _ed.EditionId == id
                             select _ed).ToList();
            ViewData["Edición"] = _Editions.SingleOrDefault().NumberEdition;
            ViewData["Number"] = _Editions.SingleOrDefault().EditionId;
            ViewData["CountryId"] = _Country.FirstOrDefault().CountryId;
            ViewData["BookId"] = _CountryBook.FirstOrDefault().BookId;
            List<plm_spGetProductsBySectionsByEditionS_Result> _storeprocedure = db.Database.SqlQuery<plm_spGetProductsBySectionsByEditionS_Result>
                ("plm_spGetProductsBySectionsByEditionS @EditionId=" + id + "").OrderBy(model => model.ProductName).ToList();
            var _ProductEditionsCount = (from _pe in db.ProductEditions
                                         where _pe.EditionId == id
                                         select _pe).ToList();
            var _CountProducts = _ProductEditionsCount.LongCount();
            var _ProductsFT = (from _p in db.Products
                               join _pt in db.ProductTypes
                                   on _p.ProductTypeId equals _pt.ProductTypeId
                               join _pe in db.ProductEditions
                                   on _p.ProductId equals _pe.ProductId
                               where _pe.EditionId == id
                               && _pt.Description != "(PRODUCTO) en TERCERA"
                               && _pt.Description != "AGREGADO"
                               && _pt.Description != "PRODUCTO (TEXTO EDITORIAL)"
                               && _pt.Description != "MARCA"
                               select _p).ToList();
            var _CountProductsFT = _ProductsFT.LongCount();
            ViewData["FICHATÉCNICA"] = _CountProductsFT;
            var _ProductsAG = (from _p in db.Products
                               join _pt in db.ProductTypes
                                   on _p.ProductTypeId equals _pt.ProductTypeId
                               join _pe in db.ProductEditions
                                   on _p.ProductId equals _pe.ProductId
                               where _pe.EditionId == id
                               && _pt.Description == "AGREGADO"
                               select _p).ToList();
            var _CountProductsAG = _ProductsAG.LongCount();
            ViewData["AGREGADO"] = _CountProductsAG;
            var _ProductsTE = (from _p in db.Products
                               join _pt in db.ProductTypes
                                   on _p.ProductTypeId equals _pt.ProductTypeId
                               join _pe in db.ProductEditions
                                   on _p.ProductId equals _pe.ProductId
                               where _pe.EditionId == id
                               && _pt.Description == "(PRODUCTO) en TERCERA"
                               select _p).ToList();
            var _CountProductsTE = _ProductsTE.LongCount();
            ViewData["TERCERA"] = _CountProductsTE;
            var _ProductsTED = (from _p in db.Products
                                join _pt in db.ProductTypes
                                on _p.ProductTypeId equals _pt.ProductTypeId
                                join _pe in db.ProductEditions
                                on _p.ProductId equals _pe.ProductId
                                where _pe.EditionId == id
                                && _pt.Description == "PRODUCTO (TEXTO EDITORIAL)"
                                select _p).ToList();
            var _CountProductsTED = _ProductsTED.LongCount();
            ViewData["EDITORIAL"] = _CountProductsTED;
            if (_ProductEditionsCount.LongCount() == 0)
            {
                ViewData["CompanyNull"] = "";
            }
            ViewData["CountProductEditions"] = _CountProducts;
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            if (palabra != null)
            {
                page = 1;
            }
            else
            {
                palabra = currentFilter;
            }
            ViewBag.CurrentFilter = palabra;
            if (palabra != null)
            {
                if (palabra.LongCount() <= 3)
                {
                    if (!String.IsNullOrEmpty(palabra))
                    {
                        string _Palabra = palabra.ToUpper();
                        _storeprocedure = _storeprocedure.Where(j => j.ProductName.StartsWith(_Palabra)).ToList();
                    }
                }
                if (palabra.LongCount() > 3)
                {
                    if (!String.IsNullOrEmpty(palabra))
                    {
                        string _Palabra = palabra.ToUpper();
                        _storeprocedure = _storeprocedure.Where(j => j.ProductName.Contains(_Palabra)).ToList();
                    }
                }
            }
            ViewBag.CurrentFilter = palabra;
            int pageSize = 100;
            int pageNumber = (page ?? 1);
            return View(_storeprocedure.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult ReporteProductosSecciónPDF(int id, int ed)
        {
            SessionEditionId _SessionEditionId = new SessionEditionId(id, ed);
            Session["SessionEditionId"] = _SessionEditionId;
            var _Country = (from _ed in db.Editions
                            join _co in db.Countries
                            on _ed.CountryId equals _co.CountryId
                            where _ed.EditionId == id
                            select _co).ToList();
            ViewData["País"] = _Country.SingleOrDefault().CountryName;
            var _CountryBook = (from _ed in db.Editions
                                join _bo in db.Books
                                on _ed.BookId equals _bo.BookId
                                where _ed.EditionId == id
                                select _bo).ToList();
            ViewData["Obra"] = _CountryBook.SingleOrDefault().BookName;
            var _Editions = (from _ed in db.Editions
                             where _ed.EditionId == id
                             select _ed).ToList();
            ViewData["Edición"] = _Editions.SingleOrDefault().NumberEdition;
            ViewData["Number"] = _Editions.SingleOrDefault().EditionId;

            var _storeprocedure = db.Database.SqlQuery<plm_spGetProductsBySectionsByEditionS_Result>
                ("plm_spGetProductsBySectionsByEditionS @EditionId=" + id + "").OrderBy(model => model.ProductName);
            var _ProductEditionsCount = (from _pe in db.ProductEditions
                                         where _pe.EditionId == id
                                         select _pe).ToList();
            var _CountProducts = _ProductEditionsCount.LongCount();
            var _ProductsFT = (from _p in db.Products
                               join _pt in db.ProductTypes
                                   on _p.ProductTypeId equals _pt.ProductTypeId
                               join _pe in db.ProductEditions
                                   on _p.ProductId equals _pe.ProductId
                               where _pe.EditionId == id
                               && _pt.Description != "(PRODUCTO) en TERCERA"
                               && _pt.Description != "AGREGADO"
                               && _pt.Description != "PRODUCTO (TEXTO EDITORIAL)"
                               && _pt.Description != "MARCA"
                               select _p).ToList();
            var _CountProductsFT = _ProductsFT.LongCount();
            ViewData["FICHATÉCNICA"] = _CountProductsFT;
            var _ProductsAG = (from _p in db.Products
                               join _pt in db.ProductTypes
                                   on _p.ProductTypeId equals _pt.ProductTypeId
                               join _pe in db.ProductEditions
                                   on _p.ProductId equals _pe.ProductId
                               where _pe.EditionId == id
                               && _pt.Description == "AGREGADO"
                               select _p).ToList();
            var _CountProductsAG = _ProductsAG.LongCount();
            ViewData["AGREGADO"] = _CountProductsAG;
            var _ProductsTE = (from _p in db.Products
                               join _pt in db.ProductTypes
                                   on _p.ProductTypeId equals _pt.ProductTypeId
                               join _pe in db.ProductEditions
                                   on _p.ProductId equals _pe.ProductId
                               where _pe.EditionId == id
                               && _pt.Description == "(PRODUCTO) en TERCERA"
                               select _p).ToList();
            var _CountProductsTE = _ProductsTE.LongCount();
            ViewData["TERCERA"] = _CountProductsTE;
            var _ProductsTED = (from _p in db.Products
                                join _pt in db.ProductTypes
                                on _p.ProductTypeId equals _pt.ProductTypeId
                                join _pe in db.ProductEditions
                                on _p.ProductId equals _pe.ProductId
                                where _pe.EditionId == id
                                && _pt.Description == "PRODUCTO (TEXTO EDITORIAL)"
                                select _p).ToList();
            var _CountProductsTED = _ProductsTED.LongCount();
            ViewData["EDITORIAL"] = _CountProductsTED;
            if (_ProductEditionsCount.LongCount() == 0)
            {
                ViewData["CompanyNull"] = "";
            }
            ViewData["CountProductEditions"] = _CountProducts;
            return View(_storeprocedure);
        }
        public ActionResult ReporteProductosÍndice(int id, int ed, string palabra, string sortOrder, string currentFilter, string searchString, int? page)
        {
            SessionEditionId _SessionEditionId = new SessionEditionId(id, ed);
            Session["SessionEditionId"] = _SessionEditionId;
            var _Country = (from _ed in db.Editions
                            join _co in db.Countries
                            on _ed.CountryId equals _co.CountryId
                            where _ed.EditionId == id
                            select _co).ToList();
            ViewData["País"] = _Country.SingleOrDefault().CountryName;
            var _CountryBook = (from _ed in db.Editions
                                join _bo in db.Books
                                on _ed.BookId equals _bo.BookId
                                where _ed.EditionId == id
                                select _bo).ToList();
            ViewData["Obra"] = _CountryBook.SingleOrDefault().BookName;
            var _Editions = (from _ed in db.Editions
                             where _ed.EditionId == id
                             select _ed).ToList();
            ViewData["Edición"] = _Editions.SingleOrDefault().NumberEdition;
            ViewData["Number"] = _Editions.SingleOrDefault().EditionId;
            ViewData["CountryId"] = _Country.FirstOrDefault().CountryId;
            ViewData["BookId"] = _CountryBook.FirstOrDefault().BookId;
            var _ProductEditions = (from _p in db.Products
                                    join _pt in db.ProductTypes
                                    on _p.ProductTypeId equals _pt.ProductTypeId
                                    join _pe in db.ProductEditions
                                    on _p.ProductId equals _pe.ProductId
                                    join _c in db.Companies
                                    on _p.CompanyId equals _c.CompanyId
                                    where _pe.EditionId == id
                                    orderby _pt.Description == "PRODUCTO (TEXTO EDITORIAL)", _pt.Description == "(PRODUCTO) en TERCERA", _pt.Description == "AGREGADO", _c.CompanyName, _p.ProductName
                                    select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Companies = _c, ProductEditions = _pe, Products = _p, ProductTypes = _pt });
            var _ProductEditionsCount = (from _pe in db.ProductEditions
                                         where _pe.EditionId == id
                                         select _pe).ToList();
            var _CountProducts = _ProductEditionsCount.LongCount();
            var _ProductsFT = (from _p in db.Products
                               join _pt in db.ProductTypes
                                   on _p.ProductTypeId equals _pt.ProductTypeId
                               join _pe in db.ProductEditions
                                   on _p.ProductId equals _pe.ProductId
                               where _pe.EditionId == id
                               && _pt.Description != "(PRODUCTO) en TERCERA"
                               && _pt.Description != "AGREGADO"
                               && _pt.Description != "PRODUCTO (TEXTO EDITORIAL)"
                               && _pt.Description != "MARCA"
                               select _p).ToList();
            var _CountProductsFT = _ProductsFT.LongCount();
            ViewData["FICHATÉCNICA"] = _CountProductsFT;
            var _ProductsAG = (from _p in db.Products
                               join _pt in db.ProductTypes
                                   on _p.ProductTypeId equals _pt.ProductTypeId
                               join _pe in db.ProductEditions
                                   on _p.ProductId equals _pe.ProductId
                               where _pe.EditionId == id
                               && _pt.Description == "AGREGADO"
                               select _p).ToList();
            var _CountProductsAG = _ProductsAG.LongCount();
            ViewData["AGREGADO"] = _CountProductsAG;
            var _ProductsTE = (from _p in db.Products
                               join _pt in db.ProductTypes
                                   on _p.ProductTypeId equals _pt.ProductTypeId
                               join _pe in db.ProductEditions
                                   on _p.ProductId equals _pe.ProductId
                               where _pe.EditionId == id
                               && _pt.Description == "(PRODUCTO) en TERCERA"
                               select _p).ToList();
            var _CountProductsTE = _ProductsTE.LongCount();
            ViewData["TERCERA"] = _CountProductsTE;
            var _ProductsTED = (from _p in db.Products
                                join _pt in db.ProductTypes
                                on _p.ProductTypeId equals _pt.ProductTypeId
                                join _pe in db.ProductEditions
                                on _p.ProductId equals _pe.ProductId
                                where _pe.EditionId == id
                                && _pt.Description == "PRODUCTO (TEXTO EDITORIAL)"
                                select _p).ToList();
            var _CountProductsTED = _ProductsTED.LongCount();
            ViewData["EDITORIAL"] = _CountProductsTED;
            if (_ProductEditionsCount.LongCount() == 0)
            {
                ViewData["CompanyNull"] = "";
            }
            ViewData["CountProductEditions"] = _CountProducts;
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            if (palabra != null)
            {
                page = 1;
            }
            else
            {
                palabra = currentFilter;
            }
            ViewBag.CurrentFilter = palabra;
            if (palabra != null)
            {
                if (palabra.LongCount() <= 3)
                {
                    if (!String.IsNullOrEmpty(palabra))
                    {
                        _ProductEditions = _ProductEditions.Where(j => j.Products.ProductName.StartsWith(palabra));
                    }
                }
                if (palabra.LongCount() > 3)
                {
                    if (!String.IsNullOrEmpty(palabra))
                    {
                        _ProductEditions = _ProductEditions.Where(j => j.Products.ProductName.Contains(palabra));
                    }
                }
            }
            ViewBag.CurrentFilter = palabra;
            int pageSize = 100;
            int pageNumber = (page ?? 1);
            return View(_ProductEditions.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult ReporteProductosÍndicePDF(int id, int ed)
        {
            SessionEditionId _SessionEditionId = new SessionEditionId(id, ed);
            Session["SessionEditionId"] = _SessionEditionId;
            var _Country = (from _ed in db.Editions
                            join _co in db.Countries
                            on _ed.CountryId equals _co.CountryId
                            where _ed.EditionId == id
                            select _co).ToList();
            ViewData["País"] = _Country.SingleOrDefault().CountryName;
            var _CountryBook = (from _ed in db.Editions
                                join _bo in db.Books
                                on _ed.BookId equals _bo.BookId
                                where _ed.EditionId == id
                                select _bo).ToList();
            ViewData["Obra"] = _CountryBook.SingleOrDefault().BookName;
            var _Editions = (from _ed in db.Editions
                             where _ed.EditionId == id
                             select _ed).ToList();
            ViewData["Edición"] = _Editions.SingleOrDefault().NumberEdition;
            ViewData["Number"] = _Editions.SingleOrDefault().EditionId;
            var _ProductEditions = (from _p in db.Products
                                    join _pt in db.ProductTypes
                                    on _p.ProductTypeId equals _pt.ProductTypeId
                                    join _pe in db.ProductEditions
                                    on _p.ProductId equals _pe.ProductId
                                    join _c in db.Companies
                                    on _p.CompanyId equals _c.CompanyId
                                    where _pe.EditionId == id
                                    orderby _pt.Description == "PRODUCTO (TEXTO EDITORIAL)", _pt.Description == "(PRODUCTO) en TERCERA", _pt.Description == "AGREGADO", _c.CompanyName, _p.ProductName
                                    select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Companies = _c, ProductEditions = _pe, Products = _p, ProductTypes = _pt });
            var _ProductEditionsCount = (from _pe in db.ProductEditions
                                         where _pe.EditionId == id
                                         select _pe).ToList();
            var _CountProducts = _ProductEditionsCount.LongCount();
            var _ProductsFT = (from _p in db.Products
                               join _pt in db.ProductTypes
                                   on _p.ProductTypeId equals _pt.ProductTypeId
                               join _pe in db.ProductEditions
                                   on _p.ProductId equals _pe.ProductId
                               where _pe.EditionId == id
                               && _pt.Description != "(PRODUCTO) en TERCERA"
                               && _pt.Description != "AGREGADO"
                               && _pt.Description != "PRODUCTO (TEXTO EDITORIAL)"
                               && _pt.Description != "MARCA"
                               select _p).ToList();
            var _CountProductsFT = _ProductsFT.LongCount();
            ViewData["FICHATÉCNICA"] = _CountProductsFT;
            var _ProductsAG = (from _p in db.Products
                               join _pt in db.ProductTypes
                                   on _p.ProductTypeId equals _pt.ProductTypeId
                               join _pe in db.ProductEditions
                                   on _p.ProductId equals _pe.ProductId
                               where _pe.EditionId == id
                               && _pt.Description == "AGREGADO"
                               select _p).ToList();
            var _CountProductsAG = _ProductsAG.LongCount();
            ViewData["AGREGADO"] = _CountProductsAG;
            var _ProductsTE = (from _p in db.Products
                               join _pt in db.ProductTypes
                                   on _p.ProductTypeId equals _pt.ProductTypeId
                               join _pe in db.ProductEditions
                                   on _p.ProductId equals _pe.ProductId
                               where _pe.EditionId == id
                               && _pt.Description == "(PRODUCTO) en TERCERA"
                               select _p).ToList();
            var _CountProductsTE = _ProductsTE.LongCount();
            ViewData["TERCERA"] = _CountProductsTE;
            var _ProductsTED = (from _p in db.Products
                                join _pt in db.ProductTypes
                                on _p.ProductTypeId equals _pt.ProductTypeId
                                join _pe in db.ProductEditions
                                on _p.ProductId equals _pe.ProductId
                                where _pe.EditionId == id
                                && _pt.Description == "PRODUCTO (TEXTO EDITORIAL)"
                                select _p).ToList();
            var _CountProductsTED = _ProductsTED.LongCount();
            ViewData["EDITORIAL"] = _CountProductsTED;
            if (_ProductEditionsCount.LongCount() == 0)
            {
                ViewData["CompanyNull"] = "";
            }
            ViewData["CountProductEditions"] = _CountProducts;
            return View(_ProductEditions);
        }
        public ActionResult ReporteMarcasEdición(int id, int ed, string palabra, string sortOrder, string currentFilter, string searchString, int? page)
        {
            SessionEditionId _SessionEditionId = new SessionEditionId(id, ed);
            Session["SessionEditionId"] = _SessionEditionId;
            var _Country = (from _ed in db.Editions
                            join _co in db.Countries
                            on _ed.CountryId equals _co.CountryId
                            where _ed.EditionId == id
                            select _co).ToList();
            ViewData["País"] = _Country.SingleOrDefault().CountryName;
            var _CountryBook = (from _ed in db.Editions
                                join _bo in db.Books
                                on _ed.BookId equals _bo.BookId
                                where _ed.EditionId == id
                                select _bo).ToList();
            ViewData["Obra"] = _CountryBook.SingleOrDefault().BookName;
            var _Editions = (from _ed in db.Editions
                             where _ed.EditionId == id
                             select _ed).ToList();
            ViewData["Edición"] = _Editions.SingleOrDefault().NumberEdition;
            ViewData["Number"] = _Editions.SingleOrDefault().EditionId;
            ViewData["CountryId"] = _Country.FirstOrDefault().CountryId;
            ViewData["BookId"] = _CountryBook.FirstOrDefault().BookId;
            var _CompanyBrandsEdition = (from _c in db.Companies
                                         join _cbe in db.CompanyBrandEditions
                                         on _c.CompanyId equals _cbe.CompanyId
                                         join _b in db.Brands
                                         on _cbe.BrandId equals _b.BrandId
                                         where _cbe.EditionId == id
                                         orderby _c.CompanyName
                                         select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Companies = _c, CompanyBrandEditions = _cbe, Brands = _b });
            var _CompanyBrandsEditionCount = (from _cbe in db.CompanyBrandEditions
                                              where _cbe.EditionId == id
                                              select _cbe).ToList();
            var _CountBrands = _CompanyBrandsEditionCount.LongCount();
            if (_CompanyBrandsEditionCount.LongCount() == 0)
            {
                ViewData["CompanyNull"] = "";
            }
            ViewData["CountBrandsEditions"] = _CountBrands;
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            if (palabra != null)
            {
                page = 1;
            }
            else
            {
                palabra = currentFilter;
            }
            ViewBag.CurrentFilter = palabra;
            if (palabra != null)
            {
                if (palabra.LongCount() <= 3)
                {
                    if (!String.IsNullOrEmpty(palabra))
                    {
                        _CompanyBrandsEdition = _CompanyBrandsEdition.Where(j => j.Brands.BrandName.StartsWith(palabra));
                    }
                }
                if (palabra.LongCount() > 3)
                {
                    if (!String.IsNullOrEmpty(palabra))
                    {
                        _CompanyBrandsEdition = _CompanyBrandsEdition.Where(j => j.Brands.BrandName.Contains(palabra));
                    }
                }
            }
            ViewBag.CurrentFilter = palabra;
            int pageSize = 100;
            int pageNumber = (page ?? 1);
            return View(_CompanyBrandsEdition.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult ReporteMarcasEdiciónPDF(int id, int ed)
        {
            SessionEditionId _SessionEditionId = new SessionEditionId(id, ed);
            Session["SessionEditionId"] = _SessionEditionId;
            var _Country = (from _ed in db.Editions
                            join _co in db.Countries
                            on _ed.CountryId equals _co.CountryId
                            where _ed.EditionId == id
                            select _co).ToList();
            ViewData["País"] = _Country.SingleOrDefault().CountryName;
            var _CountryBook = (from _ed in db.Editions
                                join _bo in db.Books
                                on _ed.BookId equals _bo.BookId
                                where _ed.EditionId == id
                                select _bo).ToList();
            ViewData["Obra"] = _CountryBook.SingleOrDefault().BookName;
            var _Editions = (from _ed in db.Editions
                             where _ed.EditionId == id
                             select _ed).ToList();
            ViewData["Edición"] = _Editions.SingleOrDefault().NumberEdition;
            ViewData["Number"] = _Editions.SingleOrDefault().EditionId;
            var _CompanyBrandsEdition = (from _c in db.Companies
                                         join _cbe in db.CompanyBrandEditions
                                         on _c.CompanyId equals _cbe.CompanyId
                                         join _b in db.Brands
                                         on _cbe.BrandId equals _b.BrandId
                                         where _cbe.EditionId == id
                                         orderby _c.CompanyName
                                         select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Companies = _c, CompanyBrandEditions = _cbe, Brands = _b });
            var _CompanyBrandsEditionCount = (from _cbe in db.CompanyBrandEditions
                                              where _cbe.EditionId == id
                                              select _cbe).ToList();
            var _CountBrands = _CompanyBrandsEditionCount.LongCount();
            if (_CompanyBrandsEditionCount.LongCount() == 0)
            {
                ViewData["CompanyNull"] = "";
            }
            ViewData["CountBrandsEditions"] = _CountBrands;
            return View(_CompanyBrandsEdition);
        }
        public ActionResult ReporteAnunciosEdición(int id, int ed, string palabra, string sortOrder, string currentFilter, string searchString, int? page)
        {
            SessionEditionId _SessionEditionId = new SessionEditionId(id, ed);
            Session["SessionEditionId"] = _SessionEditionId;
            var _Country = (from _ed in db.Editions
                            join _co in db.Countries
                            on _ed.CountryId equals _co.CountryId
                            where _ed.EditionId == id
                            select _co).ToList();
            ViewData["País"] = _Country.SingleOrDefault().CountryName;
            var _CountryBook = (from _ed in db.Editions
                                join _bo in db.Books
                                on _ed.BookId equals _bo.BookId
                                where _ed.EditionId == id
                                select _bo).ToList();
            ViewData["Obra"] = _CountryBook.SingleOrDefault().BookName;
            var _Editions = (from _ed in db.Editions
                             where _ed.EditionId == id
                             select _ed).ToList();
            ViewData["Edición"] = _Editions.SingleOrDefault().NumberEdition;
            ViewData["Number"] = _Editions.SingleOrDefault().EditionId;
            ViewData["CountryId"] = _Country.FirstOrDefault().CountryId;
            ViewData["BookId"] = _CountryBook.FirstOrDefault().BookId;
            var _EditionAdver = (from _ade in db.EditionCompanySectionAdvers
                                 join _c in db.Companies
                                 on _ade.CompanyId equals _c.CompanyId
                                 where _ade.EditionId == id
                                 orderby _c.CompanyName
                                 select new Union_Companies_CompanyTypes_CompanyPhones_Cities { EditionCompanySectionAdvers = _ade, Companies = _c });
            var _AdversEditionCount = (from _ade in db.EditionCompanySectionAdvers
                                       where _ade.EditionId == id
                                       select _ade).ToList();
            var _CountBrands = _AdversEditionCount.LongCount();
            if (_AdversEditionCount.LongCount() == 0)
            {
                ViewData["CompanyNull"] = "";
            }
            ViewData["CountAdversEdition"] = _CountBrands;
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            if (palabra != null)
            {
                page = 1;
            }
            else
            {
                palabra = currentFilter;
            }
            ViewBag.CurrentFilter = palabra;
            if (palabra != null)
            {
                if (palabra.LongCount() <= 3)
                {
                    if (!String.IsNullOrEmpty(palabra))
                    {
                        _EditionAdver = _EditionAdver.Where(j => j.EditionCompanySectionAdvers.AdverFile.StartsWith(palabra));
                    }
                }
                if (palabra.LongCount() > 3)
                {
                    if (!String.IsNullOrEmpty(palabra))
                    {
                        _EditionAdver = _EditionAdver.Where(j => j.EditionCompanySectionAdvers.AdverFile.Contains(palabra));
                    }
                }
            }
            ViewBag.CurrentFilter = palabra;
            int pageSize = 100;
            int pageNumber = (page ?? 1);
            return View(_EditionAdver.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult ReporteAnunciosEdiciónPDF(int id, int ed)
        {
            SessionEditionId _SessionEditionId = new SessionEditionId(id, ed);
            Session["SessionEditionId"] = _SessionEditionId;
            var _Country = (from _ed in db.Editions
                            join _co in db.Countries
                            on _ed.CountryId equals _co.CountryId
                            where _ed.EditionId == id
                            select _co).ToList();
            ViewData["País"] = _Country.SingleOrDefault().CountryName;
            var _CountryBook = (from _ed in db.Editions
                                join _bo in db.Books
                                on _ed.BookId equals _bo.BookId
                                where _ed.EditionId == id
                                select _bo).ToList();
            ViewData["Obra"] = _CountryBook.SingleOrDefault().BookName;
            var _Editions = (from _ed in db.Editions
                             where _ed.EditionId == id
                             select _ed).ToList();
            ViewData["Edición"] = _Editions.SingleOrDefault().NumberEdition;
            ViewData["Number"] = _Editions.SingleOrDefault().EditionId;
            var _AdversEdition = (from _c in db.Companies
                                  join _ade in db.EditionCompanySectionAdvers on _c.CompanyId equals _ade.CompanyId
                                  where _ade.EditionId == id
                                  orderby _c.CompanyName
                                  select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Companies = _c, EditionCompanySectionAdvers = _ade });
            var _AdversEditionCount = (from _ade in db.EditionCompanySectionAdvers
                                       where _ade.EditionId == id
                                       select _ade).ToList();
            var _CountBrands = _AdversEditionCount.LongCount();
            if (_AdversEditionCount.LongCount() == 0)
            {
                ViewData["CompanyNull"] = "";
            }
            ViewData["CountAdversEdition"] = _CountBrands;
            return View(_AdversEdition);
        }
        public ActionResult ReporteLaboratoriosEdiciónPDF(int id, int ed, int ad, int ud)
        {
            SessionEditionId _SessionEditionId = new SessionEditionId(ad, ud);
            Session["SessionEditionId"] = _SessionEditionId;
            ViewData["CountryId"] = id;
            ViewData["BookId"] = ed;
            var _Country = (from _co in db.Countries
                            where _co.CountryId == id
                            select _co);
            ViewData["País"] = _Country.SingleOrDefault().CountryName;
            var _Book = (from _b in db.Books
                         where _b.BookId == ed
                         select _b);
            ViewData["Obra"] = _Book.SingleOrDefault().BookName;
            var _Edition = (from _e in db.Editions
                            where _e.EditionId == ad
                            select _e);
            ViewData["Edición"] = _Edition.SingleOrDefault().NumberEdition;
            ViewData["Number"] = ad;
            var _EditionCompanies = (from _c in db.Companies
                                     join _ce in db.CompanyEditions
                                     on _c.CompanyId equals _ce.CompanyId
                                     join _ct in db.CompanyTypes
                                     on _ce.CompanyTypeId equals _ct.CompanyTypeId
                                     join _cs in db.CompanySections
                                     on _ce.CompanyId equals _cs.CompanyId
                                     join _s in db.Sections
                                     on _cs.SectionId equals _s.SectionId
                                     where _ct.CompanyTypeId == ud
                                     && _ce.EditionId == ad
                                     && _s.Description == "ANÁLISIS CLÍNICOS, LABORATORIOS DE"
                                     orderby _c.CompanyName
                                     select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Companies = _c, CompanyTypes = _ct }).Distinct().OrderBy(model => model.Companies.CompanyName).AsQueryable();
            var _CompanyTypes = (from _ct in db.CompanyTypes
                                 where _ct.CompanyTypeId == ud
                                 select _ct).ToList();
            ViewData["TipoCliente"] = _CompanyTypes.FirstOrDefault().Description;
            var _Labs = _EditionCompanies.LongCount();
            ViewData["CountLabs"] = _Labs;
            if (_Labs == 0)
            {
                ViewData["LabsNull"] = "";
            }
            return View(_EditionCompanies);
        }
        public ActionResult ReporteLaboratoriosDiagnósticoPDF(int id, int ed, int ad, int ud)
        {
            SessionEditionId _SessionEditionId = new SessionEditionId(ad, ud);
            Session["SessionEditionId"] = _SessionEditionId;
            ViewData["CountryId"] = id;
            ViewData["BookId"] = ed;
            var _Country = (from _co in db.Countries
                            where _co.CountryId == id
                            select _co);
            ViewData["País"] = _Country.SingleOrDefault().CountryName;
            var _Book = (from _b in db.Books
                         where _b.BookId == ed
                         select _b);
            ViewData["Obra"] = _Book.SingleOrDefault().BookName;
            var _Edition = (from _e in db.Editions
                            where _e.EditionId == ad
                            select _e);
            ViewData["Edición"] = _Edition.SingleOrDefault().NumberEdition;
            ViewData["Number"] = ad;
            var _EditionCompanies = (from _c in db.Companies
                                     join _ce in db.CompanyEditions
                                     on _c.CompanyId equals _ce.CompanyId
                                     join _ct in db.CompanyTypes
                                     on _ce.CompanyTypeId equals _ct.CompanyTypeId
                                     join _cs in db.CompanySections
                                     on _ce.CompanyId equals _cs.CompanyId
                                     join _s in db.Sections
                                     on _cs.SectionId equals _s.SectionId
                                     where _ct.CompanyTypeId == ud
                                     && _ce.EditionId == ad
                                     && _s.Description == "DIAGNÓSTICO POR IMAGEN, SERVICIOS"
                                     orderby _c.CompanyName
                                     select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Companies = _c, CompanyTypes = _ct }).Distinct().OrderBy(model => model.Companies.CompanyName).AsQueryable();
            var _CompanyTypes = (from _ct in db.CompanyTypes
                                 where _ct.CompanyTypeId == ud
                                 select _ct).ToList();
            ViewData["TipoCliente"] = _CompanyTypes.FirstOrDefault().Description;
            var _Labs = _EditionCompanies.LongCount();
            ViewData["CountLabs"] = _Labs;
            if (_Labs == 0)
            {
                ViewData["LabsNull"] = "";
            }
            return View(_EditionCompanies);
        }
        public ActionResult GenerarReporteEdiciónPDF(int id, int ed)
        {
            return new Rotativa.ActionAsPdf("ReporteEdiciónPDF" + "/" + id + "/" + ed);
        }
        public ActionResult GenerarReporteProductosEdiciónPDF(int id, int ed)
        {
            return new Rotativa.ActionAsPdf("ReporteProductosEdiciónPDF" + "/" + id + "/" + ed);
        }
        public ActionResult GenerarReporteProductosSecciónPDF(int id, int ed)
        {
            return new Rotativa.ActionAsPdf("ReporteProductosSecciónPDF" + "/" + id + "/" + ed);
        }
        public ActionResult GenerarReporteProductosÍndicePDF(int id, int ed)
        {
            return new Rotativa.ActionAsPdf("ReporteProductosÍndicePDF" + "/" + id + "/" + ed);
        }
        public ActionResult GenerarReporteMarcasPDF(int id, int ed)
        {
            return new Rotativa.ActionAsPdf("ReporteMarcasEdiciónPDF" + "/" + id + "/" + ed);
        }
        public ActionResult GenerarReporteAnunciosEdiciónPDF(int id, int ed)
        {
            return new Rotativa.ActionAsPdf("ReporteAnunciosEdiciónPDF" + "/" + id + "/" + ed);
        }
        public ActionResult GenerarReporteLaboratoriosEdiciónPDF(int id, int ed, int ad, int ud)
        {
            return new Rotativa.ActionAsPdf("ReporteLaboratoriosEdiciónPDF" + "/" + id + "/" + ed + "/" + ad + "/" + ud);
        }
        public ActionResult GenerarReporteLaboratoriosDiagnósticoPDF(int id, int ed, int ad, int ud)
        {
            return new Rotativa.ActionAsPdf("ReporteLaboratoriosDiagnósticoPDF" + "/" + id + "/" + ed + "/" + ad + "/" + ud);
        }
        public ActionResult MismaInformación(int EditionId, byte CompanyTypeId)
        {
            // --- Obtenemos los identificadores para mandarlos al metodo --- //
            CountriesUsers p = (CountriesUsers)Session["CountriesUsers"];
            GeteditionValues.IdEdition = EditionId;
            GeteditionValues.IdCompanyType = CompanyTypeId;
            GeteditionValues.IdUser = p.userId;
            InformaciónAnteriorTiempo();
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        public void InformaciónAnteriorTiempo()
        {
            // --- Llevamos el metodo a una acción sincronica --- //
            System.Threading.Thread _Thread = new System.Threading.Thread(new System.Threading.ParameterizedThreadStart(InformaciónAnterior));
            _Thread.Start(1);
        }
        public void InformaciónAnterior(object t)
        {
            int EditionId = GeteditionValues.IdEdition;
            byte CompanyTypeId = GeteditionValues.IdCompanyType;
            int UserId = GeteditionValues.IdUser;
            // --- Acciones --- //
            var _LastEdition = EditionId - 1;
            var _LabsLast = (from _ce in db.CompanyEditions
                             where _ce.EditionId == EditionId
                             && _ce.CompanyTypeId == CompanyTypeId
                             select _ce).ToList();
            // --- Inserta en la base de datos los laboratorios que no esten en la nueva edición pero que vengan de la edición anterior
            db.plm_spInsertLabsByLastEditions(_LastEdition, EditionId, CompanyTypeId);

            var _LabsNew = (from _ce in db.CompanyEditions
                            where _ce.EditionId == EditionId
                            && _ce.CompanyTypeId == CompanyTypeId
                            select _ce).ToList();
            //  --- Total de laboratorios ingresados --- //
            var _LonCountLabs = _LabsNew.LongCount() - _LabsLast.LongCount();
            if (_LabsNew.LongCount() != _LabsLast.LongCount())
            {
                var _Aplication = (from _ap in plm.Applications
                                   where _ap.Description == "Sitio DEACI"
                                   select _ap).ToList();
                var _LastEditionValue = db.Editions.SingleOrDefault(model => model.EditionId == _LastEdition);
                var _EditionValue = db.Editions.SingleOrDefault(model => model.EditionId == EditionId);
                var _CompanySectionsCli = (from _ce in db.CompanyEditions
                                        join _cs in db.CompanySections
                                        on _ce.CompanyId equals _cs.CompanyId
                                        join _s in db.Sections
                                        on _cs.SectionId equals _s.SectionId
                                        where _ce.EditionId == EditionId
                                        && _ce.CompanyTypeId == CompanyTypeId
                                        && _s.Description == "ANÁLISIS CLÍNICOS, LABORATORIOS DE"
                                        select _s).ToList();
                // Análisis clínicos
                foreach (var _App in _Aplication)
                {
                    var _Tables = (from _tab in plm.Tables
                                   where _tab.ApplicationId == _App.ApplicationId
                                   && _tab.Description == "SameInformation"
                                   select _tab).ToList();
                    foreach (var _Tabs in _Tables)
                    {
                        var _ApplicationId = _App.ApplicationId;
                        ActivityLogs _ActivityLogs = new ActivityLogs();
                        int _OperationId = 1; // --- Agregar --- //
                        _ActivityLogs.UserId = UserId;
                        _ActivityLogs.TableId = _Tabs.TableId; // SameInformation
                        _ActivityLogs.OperationId = _OperationId;
                        _ActivityLogs.Date = DateTime.Now;
                        _ActivityLogs.PrimaryKeyAffected = ("(SectionId," + _CompanySectionsCli.FirstOrDefault().SectionIdParent + ");(SubSectionId," + _CompanySectionsCli.FirstOrDefault().SectionId + ");(CompanyTypeId," + CompanyTypeId + ");(EditionIdAnterior," + _LastEdition + ");(EditionIdNuevo," + EditionId + ")");
                        _ActivityLogs.FieldsAffected = _CompanySectionsCli.LongCount().ToString();
                        plm.Entry(_ActivityLogs).State = EntityState.Added;
                        plm.SaveChanges();
                    }
                }
                // -- Diagnóstico por imagen --- //
                var _CompanySectionsDia = (from _ce in db.CompanyEditions
                                           join _cs in db.CompanySections
                                            on _ce.CompanyId equals _cs.CompanyId
                                           join _s in db.Sections
                                           on _cs.SectionId equals _s.SectionId
                                           where _ce.EditionId == EditionId
                                           && _ce.CompanyTypeId == CompanyTypeId
                                           && _s.Description == "DIAGNÓSTICO POR IMAGEN, SERVICIOS"
                                           select _s).ToList();
                foreach (var _App in _Aplication)
                {
                    var _Tables = (from _tab in plm.Tables
                                   where _tab.ApplicationId == _App.ApplicationId
                                   && _tab.Description == "SameInformation"
                                   select _tab).ToList();
                    foreach (var _Tabs in _Tables)
                    {
                        var _ApplicationId = _App.ApplicationId;
                        ActivityLogs _ActivityLogs = new ActivityLogs();
                        int _OperationId = 1; // --- Agregar --- //
                        _ActivityLogs.UserId = UserId;
                        _ActivityLogs.TableId = _Tabs.TableId; // SameInformation
                        _ActivityLogs.OperationId = _OperationId;
                        _ActivityLogs.Date = DateTime.Now;
                        _ActivityLogs.PrimaryKeyAffected = ("(SectionId," + _CompanySectionsDia.FirstOrDefault().SectionIdParent + ");(SubSectionId," + _CompanySectionsDia.FirstOrDefault().SectionId + ");(CompanyTypeId," + CompanyTypeId + ");(EditionIdAnterior," + _LastEdition + ");(EditionIdNuevo," + EditionId + ")");
                        _ActivityLogs.FieldsAffected = _CompanySectionsDia.LongCount().ToString();
                        plm.Entry(_ActivityLogs).State = EntityState.Added;
                        plm.SaveChanges();
                    }
                }
            }
        }
    }
}