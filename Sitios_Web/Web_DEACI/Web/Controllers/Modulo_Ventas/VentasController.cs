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
using System.IO;
using System.Web.UI.WebControls;
using System.Web.UI;
using Postal;
using Rotativa;
using System.Threading.Tasks;
using System.Threading;
namespace Web.Controllers
{
    public class VentasController : Controller
    {
        public GetValues GetValues = new Models.GetValues();
        public GeteditionValues GeteditionValues = new Models.GeteditionValues();
        public DEACI_20150917Entities db = new DEACI_20150917Entities();
        public PLMUsers plm = new PLMUsers();
        public ActionResult Clientes(int id, int ed, int ad)
        {
            SessionCompanyEditions _SessionCompanyEdition = new SessionCompanyEditions(id, ed, ad);
            Session["SessionCompanyEditions"] = _SessionCompanyEdition;
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
            var _storeprocedure = db.Database.SqlQuery<plm_spGetCompaniesByEditionIdByCompanyTypeId_Result>
                ("plm_spGetCompaniesByEditionIdByCompanyTypeId @EditionId=" + id + ", @CompanyTypeId=" + ad + "").ToList();
            var _Editions = (from _ed in db.Editions
                             where _ed.EditionId == id
                             select _ed).ToList();
            ViewData["Edición"] = _Editions.SingleOrDefault().NumberEdition;
            ViewData["Number"] = _Editions.SingleOrDefault().EditionId;
            var _CompanyTypes = (from _ct in db.CompanyTypes
                                 where _ct.CompanyTypeId == ad
                                 select _ct).ToList();
            ViewData["TipoCliente"] = _CompanyTypes.FirstOrDefault().Description;
            var _lastedition = (from _e in db.Editions
                                select _e).ToList();
            var _lasteditionvalue = db.Editions.SingleOrDefault(model => model.EditionId == id).NumberEdition;
            if (_lastedition.LastOrDefault().NumberEdition == _lasteditionvalue)
            {
                ViewData["CheckInformation"] = "True";
            }
            return View(_storeprocedure);
        }
        [ValidateInput(false)]
        public ActionResult EditarCliente(string CompanyId, string CompanyName, string CompanyShortName, string CompanytypeId, string CompanyIdParent,
                                          string EditionId, string HtmlFile, string HtmlContent, string Respaldo, string Page, string Active)
        {
            int _EditionId = int.Parse(EditionId);
            int _Respaldo = int.Parse(Respaldo);
            Companies _Companies = new Companies();
            CompanyEditions _CompanyEditions = new CompanyEditions();
            if (CompanyShortName == "")
            {
                CompanyShortName = null;
            }
            if (CompanyIdParent == "Seleccione...")
            {
                _Companies.CompanyIdParent = null;
            }
            else
            {
                int _CompanyIdParent = int.Parse(CompanyIdParent);
                _Companies.CompanyIdParent = _CompanyIdParent;
            }
            if (CompanytypeId == "Seleccione...")
            {
                CompanytypeId = null; 
            }
            int _CompanyId = int.Parse(CompanyId);
            byte _CompanyTypeId = byte.Parse(CompanytypeId);
            _Companies.CompanyId = _CompanyId;
            _Companies.CompanyName = CompanyName;
            _Companies.CompanyShortName = CompanyShortName;
            _Companies.Active = true;
            db.Entry(_Companies).State = EntityState.Modified;
            db.SaveChanges();
            CompanyEditions _CompanyEditionsmodified = new CompanyEditions();
            _CompanyEditionsmodified.EditionId = _EditionId;
            _CompanyEditionsmodified.CompanyId = _CompanyId;
            _CompanyEditionsmodified.CompanyTypeId = _CompanyTypeId;
            if (HtmlFile == "")
            { 
                _CompanyEditionsmodified.HtmlFile = null;
            }
            else
            { 
                _CompanyEditionsmodified.HtmlFile = HtmlFile;
            }
            if (HtmlContent == "")
            { 
                _CompanyEditionsmodified.HtmlContent = null;
            }
            else
            {
                _CompanyEditionsmodified.HtmlContent = HtmlContent;
            }
            if (Page == "")
            { 
                _CompanyEditionsmodified.Page = null;
            }
            else
            { 
                _CompanyEditionsmodified.Page = Page;
            }
            _CompanyEditionsmodified.CloseClient = true;
            db.Entry(_CompanyEditionsmodified).State = EntityState.Modified;
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
                    _ActivityLogs.FieldsAffected = ("(CompanyName," + _Companies.CompanyName + "),(" + "CompanyShortName," + _Companies.CompanyShortName + "),(" + "CompanyIdParent," + _Companies.CompanyIdParent + "),(" + "CompanyTypeId," + _CompanyEditionsmodified.CompanyTypeId + ")");
                    plm.Entry(_ActivityLogs).State = EntityState.Added;
                    plm.SaveChanges();
                }
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        public ActionResult EliminarTeléfono(int id, string CompanyId, string PhoneTypeId, int CompanyTypeId)
        {
            int _CompanyId = int.Parse(CompanyId);
            byte _PhoneTypeId = byte.Parse(PhoneTypeId);
            CompanyPhones _CompanyPhones = new CompanyPhones();
            _CompanyPhones.CompanyId = _CompanyId;
            _CompanyPhones.PhoneTypeId = _PhoneTypeId;
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
            return RedirectToAction("/Clientes/" + id + "/" + _CompanyPhones.CompanyId + "/" + CompanyTypeId);
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
        public ActionResult AgregarTeléfono(string EditionId, string CompanyId, string PhoneTypeId, string PhoneValue)
        {
            int _EditionId = int.Parse(EditionId);
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
        public ActionResult EliminarDirección(int id, string CompanyId, string AddressId, int CompanyTypeId)
        {
            int _CompanyId = int.Parse(CompanyId);
            int _AddressId = int.Parse(AddressId);
            var _deletecompanyaddress = db.CompanyAddresses.SingleOrDefault(model => model.CompanyId == _CompanyId && model.AddressId == _AddressId);
            db.Entry(_deletecompanyaddress).State = EntityState.Deleted;
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
                    int _OperationId = 4; // Eliminar
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
            return RedirectToAction("/Clientes/" + id + "/" + _CompanyId + "/" + CompanyTypeId);
        }
        public ActionResult EditarDirecciónes(int id, int ed)
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
        public ActionResult Productos(int id, int ed, int ad, string palabra, string sortOrder, string currentFilter, string searchString, int? page)
        {
            SessionCompanyEditions _SessionCompanyEdition = new SessionCompanyEditions(id, ed, ad);
            Session["SessionCompanyEditions"] = _SessionCompanyEdition;
            SessionEditionId SessionEditionId = new SessionEditionId(id, ed);
            Session["SessionEditionId"] = SessionEditionId;
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
            var _Country = (from _ed in db.Editions
                            join _co in db.Countries
                            on _ed.CountryId equals _co.CountryId
                            where _ed.EditionId == id
                            select _co);
            ViewData["País"] = _Country.SingleOrDefault().CountryName;
            var _CountryBook = (from _ed in db.Editions
                                join _bo in db.Books
                                on _ed.BookId equals _bo.BookId
                                where _ed.EditionId == id
                                select _bo);
            ViewData["Obra"] = _CountryBook.SingleOrDefault().BookName;
            Union_Products u = new Union_Products();
            var _Products = (from _p in db.Products
                             join _c in db.Companies
                             on _p.CompanyId equals _c.CompanyId
                             join _pt in db.ProductTypes
                             on _p.ProductTypeId equals _pt.ProductTypeId
                             where _p.CompanyId == ed
                             && _pt.Description != "MARCA"
                             orderby _p.ProductName
                             select new Union_Products { prod = _p, prodtypes = _pt, companies = _c });
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
                        _Products = _Products.Where(j => j.prod.ProductName.StartsWith(palabra));
                    }
                }
                if (palabra.LongCount() > 3)
                {
                    if (!String.IsNullOrEmpty(palabra))
                    {
                        _Products = _Products.Where(j => j.prod.ProductName.Contains(palabra));
                    }
                }
            }
            foreach (var Messagge in _Products)
            {
                if (Messagge.prodtypes.Description == "AGREGADO")
                {
                    ViewData["PRODUCTOAGREGADO"] = "";
                }
            }
            var _Companies = (from _c in db.Companies
                              join _ce in db.CompanyEditions
                              on _c.CompanyId equals _ce.CompanyId
                              join _ct in db.CompanyTypes
                                  on _ce.CompanyTypeId equals _ct.CompanyTypeId
                              where _c.CompanyId == ed
                              && _ct.CompanyTypeId == ad
                              select new { _c, _ct }).Distinct();
            ViewData["CompanyName"] = _Companies.FirstOrDefault()._c.CompanyName;
            ViewData["CompanyId"] = _Companies.FirstOrDefault()._c.CompanyId;
            ViewData["CompanyShortName"] = _Companies.SingleOrDefault()._c.CompanyShortName;
            ViewData["TipoCliente"] = _Companies.SingleOrDefault()._ct.Description;
            var _Editions = (from _e in db.Editions
                             where _e.EditionId == id
                             select _e).ToList();
            ViewData["Number"] = _Editions.SingleOrDefault().EditionId;
            ViewData["Edición"] = _Editions.SingleOrDefault().NumberEdition;
            int pageSize = 7;
            int pageNumber = (page ?? 1);
            return View(_Products.ToPagedList(pageNumber, pageSize));
        }
        [ValidateInput(false)]
        public ActionResult AgregarProductoEdición(string CompanyId, string ProductId, string HtmlFile, string HtmlContent, string EditionId)
        {
            int _EditionId = int.Parse(EditionId);
            int _ProductId = int.Parse(ProductId);
            ProductEditions _ProductEditions = new ProductEditions();
            var _ValueParticipantClient = (from _c in db.Companies
                                           join _ce in db.CompanyEditions
                                           on _c.CompanyId equals _ce.CompanyId
                                           join _p in db.Products
                                           on _c.CompanyId equals _p.CompanyId
                                           where _ce.EditionId == _EditionId
                                           && _p.ProductId == _ProductId
                                           select new { _c, _ce, _p }).ToList();
            if (_ValueParticipantClient.LongCount() == 0)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            var _ValueProductParent = (from _p in db.Products
                                       where _p.ProductId == _ProductId
                                       select _p).ToList();
            foreach (var _pp in _ValueProductParent)
            {
                if (_pp.ProductIdParent != null)
                {
                    var _valueParentProductParticipant = (from _p in db.Products
                                                          join _pe in db.ProductEditions
                                                          on _p.ProductId equals _pe.ProductId
                                                          where _p.ProductId == _pp.ProductIdParent
                                                          && _pe.EditionId == _EditionId
                                                          select new { _p, _pe }).ToList();
                    if (_valueParentProductParticipant.LongCount() > 0)
                    {
                        if (HtmlFile == "")
                        { _ProductEditions.HtmlFile = null; }
                        else
                        { _ProductEditions.HtmlFile = HtmlFile; }
                        if (HtmlContent == "")
                        { _ProductEditions.HtmlContent = null; }
                        else
                        { _ProductEditions.HtmlContent = HtmlContent; }
                        _ProductEditions.ProductId = _ProductId;
                        _ProductEditions.EditionId = _EditionId;
                        _ProductEditions.HtmlFile = HtmlFile;
                        _ProductEditions.HtmlContent = HtmlContent;
                        db.Entry(_ProductEditions).State = EntityState.Added;
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
                                int _OperationId = 1; // Agregar
                                _ActivityLogs.UserId = p.userId;
                                _ActivityLogs.TableId = _Tabs.TableId; // ProductEditions
                                _ActivityLogs.OperationId = _OperationId;
                                _ActivityLogs.Date = DateTime.Now;
                                _ActivityLogs.PrimaryKeyAffected = ("(ProductId," + _ProductEditions.ProductId + "),(" + "EditionId," + _ProductEditions.EditionId + ")");
                                _ActivityLogs.FieldsAffected = null;
                                plm.Entry(_ActivityLogs).State = EntityState.Added;
                                plm.SaveChanges();
                            }
                        }
                        return Json(true, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(false, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    if (HtmlFile == "")
                    { _ProductEditions.HtmlFile = null; }
                    else
                    { _ProductEditions.HtmlFile = HtmlFile; }
                    if (HtmlContent == "")
                    { _ProductEditions.HtmlContent = null; }
                    else
                    { _ProductEditions.HtmlContent = HtmlContent; }
                    _ProductEditions.ProductId = _ProductId;
                    _ProductEditions.EditionId = _EditionId;
                    _ProductEditions.HtmlFile = HtmlFile;
                    _ProductEditions.HtmlContent = HtmlContent;
                    db.Entry(_ProductEditions).State = EntityState.Added;
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
                            int _OperationId = 1; // Agregar
                            _ActivityLogs.UserId = p.userId;
                            _ActivityLogs.TableId = _Tabs.TableId; // ProductEditions
                            _ActivityLogs.OperationId = _OperationId;
                            _ActivityLogs.Date = DateTime.Now;
                            _ActivityLogs.PrimaryKeyAffected = ("(ProductId," + _ProductEditions.ProductId + "),(" + "EditionId," + _ProductEditions.EditionId + ")");
                            _ActivityLogs.FieldsAffected = null;
                            plm.Entry(_ActivityLogs).State = EntityState.Added;
                            plm.SaveChanges();
                        }
                    }
                    return Json(true, JsonRequestBehavior.AllowGet);
                }
            }
            return View();
        }
        [ValidateInput(false)]
        public ActionResult ProductoNuevo(string ProductId, string EditionId)
        {
            int _ProductId = int.Parse(ProductId);
            int _EditionId = int.Parse(EditionId);
            var _ProductEditions = (from _pe in db.ProductEditions
                                    where _pe.ProductId == _ProductId
                                    && _pe.EditionId == _EditionId
                                    select _pe).ToList();
            foreach (var a in _ProductEditions)
            {
                a.StatusId = 1;
                db.SaveChanges();
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ProductoConCambios(string ProductId, string EditionId)
        {
            int _ProductId = int.Parse(ProductId);
            int _EditionId = int.Parse(EditionId);
            var _ProductEditions = (from _pe in db.ProductEditions
                                    where _pe.ProductId == _ProductId
                                    && _pe.EditionId == _EditionId
                                    select _pe).ToList();
            foreach (var a in _ProductEditions)
            {
                a.StatusId = 2;
                db.SaveChanges();
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ProductoSinCambios(string ProductId, string EditionId)
        {
            int _ProductId = int.Parse(ProductId);
            int _EditionId = int.Parse(EditionId);
            var _ProductEditions = (from _pe in db.ProductEditions
                                    where _pe.ProductId == _ProductId
                                    && _pe.EditionId == _EditionId
                                    select _pe).ToList();
            foreach (var a in _ProductEditions)
            {
                a.StatusId = 3;
                db.SaveChanges();
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        public ActionResult AgregarProductoNuevaEdición(int id, int ed, int ad, int ud)
        {
            SessionProductSections _SessionProductSections = new SessionProductSections(ad);
            Session["SessionProductSections"] = _SessionProductSections;
            var _ProductDetails = (from _p in db.Products
                                   where _p.ProductId == ad
                                   select _p).ToList();
            ViewData["ProductName"] = _ProductDetails.SingleOrDefault().ProductName;
            ViewData["CompanyId"] = ed;
            ViewBag.EditionId = new SelectList(db.Editions, "EditionId", "NumberEdition");
            return View();
        }
        public ActionResult EliminarEdiciónProducto(int id, int EditionId, int ProductId, int CompanyId, int CompanyTypeId)
        {
            var _ValueProductParent = (from _p in db.Products
                                       where _p.ProductId == ProductId
                                       select _p).ToList();
            foreach (var _ProductValue in _ValueProductParent)
            {
                if (_ProductValue.ProductIdParent == null)
                {
                    ProductEditions _ProductEditions = new ProductEditions();
                    _ProductEditions.ProductId = ProductId;
                    _ProductEditions.EditionId = EditionId;
                    db.Entry(_ProductEditions).State = EntityState.Deleted;
                    db.SaveChanges();
                    var _ProductSon = (from _p in db.Products
                                       join _pe in db.ProductEditions
                                       on _p.ProductId equals _pe.ProductId
                                       where _p.ProductIdParent == _ProductValue.ProductId
                                       && _pe.EditionId == EditionId
                                       select _p).ToList();
                    foreach (var _ProductDeleteEditions in _ProductSon)
                    {
                        var _DeleteProductsEditionSon = db.ProductEditions.SingleOrDefault(model => model.EditionId == EditionId
                        && model.ProductId == _ProductDeleteEditions.ProductId);
                        db.ProductEditions.Remove(_DeleteProductsEditionSon);
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
                                _ActivityLogs.PrimaryKeyAffected = ("(ProductId," + _ProductEditions.ProductId + "),(" + "EditionId," + _ProductEditions.EditionId + ")");
                                _ActivityLogs.FieldsAffected = null;
                                plm.Entry(_ActivityLogs).State = EntityState.Added;
                                plm.SaveChanges();
                            }
                        }
                    }
                    return RedirectToAction("/Productos/" + id + "/" + CompanyId + "/" + CompanyTypeId);
                }
                else
                {
                    ProductEditions _ProductEditions = new ProductEditions();
                    _ProductEditions.EditionId = EditionId;
                    _ProductEditions.ProductId = ProductId;
                    db.Entry(_ProductEditions).State = EntityState.Deleted;
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
                            _ActivityLogs.TableId = _Tabs.TableId; // DeleteProductParticipant
                            _ActivityLogs.OperationId = _OperationId;
                            _ActivityLogs.Date = DateTime.Now;
                            _ActivityLogs.PrimaryKeyAffected = ("(ProductId," + _ProductEditions.ProductId + "),(" + "EditionId," + _ProductEditions.EditionId + ")");
                            _ActivityLogs.FieldsAffected = null;
                            plm.Entry(_ActivityLogs).State = EntityState.Added;
                            plm.SaveChanges();
                        }
                    }
                    return RedirectToAction("/Productos/" + id + "/" + CompanyId + "/" + CompanyTypeId);
                }
            }
            return View();
        }
        public ActionResult EditarProductos(int id, int ed, int ad)
        {
            SessionEditionId _SessionEditionId = new SessionEditionId(id, ed);
            Session["SessionEditionId"] = _SessionEditionId;
            var _Products = (from _p in db.Products
                             where _p.ProductId == ed
                             select _p).ToList();
            var _Value = (from _p in db.Products
                          join _pt in db.ProductTypes
                          on _p.ProductTypeId equals _pt.ProductTypeId
                          where _pt.Description == "AGREGADO"
                          && _p.ProductId == ed
                          select _p);
            foreach (var Parent in _Value)
            {
                var _ParentValue = (from _p in db.Products
                                    where _p.ProductIdParent == Parent.ProductId
                                    select _p).ToList();
                if (_ParentValue.LongCount() == 0)
                {
                    if (_Value.LongCount() > 0)
                    {
                        ViewData["Into"] = "";
                        var _ProductsTypess = (from _p in db.Products
                                               join _pe in db.ProductEditions
                                               on _p.ProductId equals _pe.ProductId
                                               join _pt in db.ProductTypes
                                               on _p.ProductTypeId equals _pt.ProductTypeId
                                               where
                                                _p.CompanyId == ad
                                               && _p.ProductId != ed
                                               && _p.ProductIdParent == null
                                               && _pe.EditionId == id
                                               select _p).ToList();
                        var _ProductsId = (from _p in db.Products
                                           where _p.ProductId == ed
                                           select _p);
                        ViewBag.ProductIdParent = new SelectList(_ProductsTypess, "ProductId", "ProductName",
                                                                 _Products.SingleOrDefault().ProductIdParent);
                    }
                }
            }
            var _ProductypesView = (from _pt in db.ProductTypes
                                    where _pt.Description != "MARCA"
                                    select _pt).ToList();
            ViewBag.CompanyId = new SelectList(db.Companies, "CompanyId", "CompanyName",
                                                     _Products.SingleOrDefault().CompanyId);
            ViewBag.ProductTypeId = new SelectList(_ProductypesView, "ProductTypeId", "Description",
                                                     _Products.SingleOrDefault().ProductTypeId);
            return View(_Products);
        }
        public ActionResult AgregarProductoNuevo(string CompanyId, string ProductName, string Description, string ProductTypeId, string ProductIdParent, string EditionId)
        {
            int _EditionId = int.Parse(EditionId);
            Products _Products = new Products();
            var _Value = (from _p in db.Products
                          where _p.ProductName.Trim() == ProductName.Trim()
                          select _p).ToList();
            if (_Value.LongCount() > 0)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            int _CompanyId = int.Parse(CompanyId);
            byte _ProductTypeId = byte.Parse(ProductTypeId);
            if (ProductIdParent == "Seleccione...")
            {
                ProductIdParent = null;
                _Products.ProductIdParent = null;
            }
            else
            {
                int _ProductIdParent = int.Parse(ProductIdParent);
                _Products.ProductIdParent = _ProductIdParent;
            }
            _Products.ProductName = ProductName.Trim();
            _Products.Description = Description;
            _Products.CompanyId = _CompanyId;
            _Products.ProductTypeId = _ProductTypeId;
            _Products.Active = true;
            var valueinsert = (from _pt in db.ProductTypes
                               where _pt.ProductTypeId == _Products.ProductTypeId
                               select _pt).ToList();
            foreach (var val in valueinsert)
            {
                if (val.Description != "AGREGADO" && _Products.ProductIdParent != null)
                {
                    return Json(false, JsonRequestBehavior.AllowGet);
                }
            }
            db.Products.Add(_Products);
            db.SaveChanges();
            var _Aplication = (from _ap in plm.Applications
                               where _ap.Description == "Sitio DEACI"
                               select _ap).ToList();
            foreach (var _App in _Aplication)
            {
                var _Tables = (from _tab in plm.Tables
                               where _tab.ApplicationId == _App.ApplicationId
                               && _tab.Description == "Products"
                               select _tab).ToList();
                foreach (var _Tabs in _Tables)
                {
                    var _ApplicationId = _App.ApplicationId;
                    CountriesUsers p = (CountriesUsers)Session["CountriesUsers"];
                    ActivityLogs _ActivityLogs = new ActivityLogs();
                    int _OperationId = 1; // Agregar
                    _ActivityLogs.UserId = p.userId;
                    _ActivityLogs.TableId = _Tabs.TableId; // Products
                    _ActivityLogs.OperationId = _OperationId;
                    _ActivityLogs.Date = DateTime.Now;
                    _ActivityLogs.PrimaryKeyAffected = ("(ProductId," + _Products.ProductId + ")");
                    _ActivityLogs.FieldsAffected = ("(ProductName," + _Products.ProductName + "),(Description," + _Products.Description + "),(ProductTyeId," + _Products.ProductTypeId + "),");
                    plm.Entry(_ActivityLogs).State = EntityState.Added;
                    plm.SaveChanges();
                }
            }
            var _ValueParticipantClient = (from _c in db.Companies
                                           join _ce in db.CompanyEditions
                                               on _c.CompanyId equals _ce.CompanyId
                                           where _c.CompanyId == _CompanyId
                                           && _ce.EditionId == _EditionId
                                           select new { _c, _ce }).ToList();
            if (_ValueParticipantClient.LongCount() == 0)
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            else
            {
                ProductEditions _ProductEditions = new ProductEditions();
                _ProductEditions.ProductId = _Products.ProductId;
                _ProductEditions.EditionId = _EditionId;
                _ProductEditions.HtmlFile = null;
                _ProductEditions.HtmlContent = null;
                _ProductEditions.StatusId = 3;
                db.Entry(_ProductEditions).State = EntityState.Added;
                db.SaveChanges();
                var _Aplications = (from _ap in plm.Applications
                                   where _ap.Description == "Sitio DEACI"
                                   select _ap).ToList();
                foreach (var _App in _Aplications)
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
                        int _OperationId = 1; // Agregar
                        _ActivityLogs.UserId = p.userId;
                        _ActivityLogs.TableId = _Tabs.TableId; // ProductEditions
                        _ActivityLogs.OperationId = _OperationId;
                        _ActivityLogs.Date = DateTime.Now;
                        _ActivityLogs.PrimaryKeyAffected = ("(ProductId," + _ProductEditions.ProductId + "),(" + "EditionId," + _ProductEditions.EditionId + ")");
                        _ActivityLogs.FieldsAffected = null;
                        plm.Entry(_ActivityLogs).State = EntityState.Added;
                        plm.SaveChanges();
                    }
                }
                return Json(true, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult EditarProducto(string CompanyId, string ProductId, string ProductName, string Description, string ProductTypeId, string ProductIdParent)
        {
            int _CompanyId = int.Parse(CompanyId);
            int _ProductId = int.Parse(ProductId);
            Products _Products = new Products();
            if (ProductIdParent == "Seleccione...")
            {
                _Products.ProductIdParent = null;
            }
            else if (ProductIdParent == null)
            {
                _Products.ProductIdParent = null;
            }
            else
            {
                int _ProductIdParent = int.Parse(ProductIdParent);
                _Products.ProductIdParent = _ProductIdParent;
            }
            if (Description == "")
            {
                _Products.Description = null;
            }
            else
            {
                _Products.Description = Description;
            }
            _Products.ProductName = ProductName.Trim();
            _Products.CompanyId = _CompanyId;
            _Products.ProductId = _ProductId;
            var _ProductTypeIds = db.ProductTypes.SingleOrDefault(model => model.Description == ProductTypeId);
            _Products.ProductTypeId = _ProductTypeIds.ProductTypeId;
            _Products.Active = true;
            var valueproductype = (from _pt in db.ProductTypes
                                   where _pt.ProductTypeId == _Products.ProductTypeId
                                   select _pt).ToList();
            foreach (var val in valueproductype)
            {
                if (val.Description != "AGREGADO" && _Products.ProductIdParent != null)
                {
                    _Products.ProductIdParent = null;
                }
            }
            db.Entry(_Products).State = EntityState.Modified;
            db.SaveChanges();
            var _Aplication = (from _ap in plm.Applications
                               where _ap.Description == "Sitio DEACI"
                               select _ap).ToList();
            foreach (var _App in _Aplication)
            {
                var _Tables = (from _tab in plm.Tables
                               where _tab.ApplicationId == _App.ApplicationId
                               && _tab.Description == "Products"
                               select _tab).ToList();
                foreach (var _Tabs in _Tables)
                {
                    var _ApplicationId = _App.ApplicationId;
                    CountriesUsers p = (CountriesUsers)Session["CountriesUsers"];
                    ActivityLogs _ActivityLogs = new ActivityLogs();
                    int _OperationId = 2; // Modificar
                    _ActivityLogs.UserId = p.userId;
                    _ActivityLogs.TableId = _Tabs.TableId; // Products
                    _ActivityLogs.OperationId = _OperationId;
                    _ActivityLogs.Date = DateTime.Now;
                    _ActivityLogs.PrimaryKeyAffected = ("(ProductId," + _Products.ProductId + ")");
                    _ActivityLogs.FieldsAffected = ("(ProductName," + _Products.ProductName + "),(Description," + _Products.Description + "),(ProductTyeId," + _Products.ProductTypeId + "),");
                    plm.Entry(_ActivityLogs).State = EntityState.Added;
                    plm.SaveChanges();
                }
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        public ActionResult AgregarÍndicesProducto(int id)
        {
            var _Product = (from _p in db.Products
                            join _pt in db.ProductTypes
                            on _p.ProductTypeId equals _pt.ProductTypeId
                            where _p.ProductId == id
                            select new { _p, _pt }).ToList();
            ViewData["ProductId"] = id;
            ViewData["ProductName"] = _Product.SingleOrDefault()._p.ProductName;
            ViewData["Description"] = _Product.SingleOrDefault()._p.Description;
            ViewData["ProductType"] = _Product.SingleOrDefault()._pt.Description;
            return View();
        }
        public ActionResult AgregarProductoÍndice(string IndexId, string ProductId)
        {
            byte _IndexId = byte.Parse(IndexId);
            int _ProductId = int.Parse(ProductId);
            var _Value = (from _pi in db.ProductIndexes
                          where _pi.ProductId == _ProductId && _pi.IndexId == _IndexId
                          select _pi).ToList();
            if (_Value.LongCount() > 0)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            ProductIndexes _ProductIdexes = new ProductIndexes();
            _ProductIdexes.ProductId = _ProductId;
            _ProductIdexes.IndexId = _IndexId;
            db.Entry(_ProductIdexes).State = EntityState.Added;
            db.SaveChanges();
            var _Aplication = (from _ap in plm.Applications
                               where _ap.Description == "Sitio DEACI"
                               select _ap).ToList();
            foreach (var _App in _Aplication)
            {
                var _Tables = (from _tab in plm.Tables
                               where _tab.ApplicationId == _App.ApplicationId
                               && _tab.Description == "ProductIndexes"
                               select _tab).ToList();
                foreach (var _Tabs in _Tables)
                {
                    var _ApplicationId = _App.ApplicationId;
                    CountriesUsers p = (CountriesUsers)Session["CountriesUsers"];
                    ActivityLogs _ActivityLogs = new ActivityLogs();
                    int _OperationId = 1; // Agregar
                    _ActivityLogs.UserId = p.userId;
                    _ActivityLogs.TableId = _Tabs.TableId; // ProductIndexes
                    _ActivityLogs.OperationId = _OperationId;
                    _ActivityLogs.Date = DateTime.Now;
                    _ActivityLogs.PrimaryKeyAffected = ("(ProductId," + _ProductIdexes.ProductId + "),(IndexId," + _ProductIdexes.IndexId + ")");
                    _ActivityLogs.FieldsAffected = null;
                    plm.Entry(_ActivityLogs).State = EntityState.Added;
                    plm.SaveChanges();
                }
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        public ActionResult EliminarProductoÍndice(byte IndexId, int ProductId)
        {
            ProductIndexes _ProductIndexes = new ProductIndexes();
            _ProductIndexes.ProductId = ProductId;
            _ProductIndexes.IndexId = IndexId;
            db.Entry(_ProductIndexes).State = EntityState.Deleted;
            db.SaveChanges();
            var _Aplication = (from _ap in plm.Applications
                               where _ap.Description == "Sitio DEACI"
                               select _ap).ToList();
            foreach (var _App in _Aplication)
            {
                var _Tables = (from _tab in plm.Tables
                               where _tab.ApplicationId == _App.ApplicationId
                               && _tab.Description == "ProductIndexes"
                               select _tab).ToList();
                foreach (var _Tabs in _Tables)
                {
                    var _ApplicationId = _App.ApplicationId;
                    CountriesUsers p = (CountriesUsers)Session["CountriesUsers"];
                    ActivityLogs _ActivityLogs = new ActivityLogs();
                    int _OperationId = 4; // Eliminar
                    _ActivityLogs.UserId = p.userId;
                    _ActivityLogs.TableId = _Tabs.TableId; // ProductIndexes
                    _ActivityLogs.OperationId = _OperationId;
                    _ActivityLogs.Date = DateTime.Now;
                    _ActivityLogs.PrimaryKeyAffected = ("(ProductId," + _ProductIndexes.ProductId + "),(IndexId," + _ProductIndexes.IndexId + ")");
                    _ActivityLogs.FieldsAffected = null;
                    plm.Entry(_ActivityLogs).State = EntityState.Added;
                    plm.SaveChanges();
                }
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        public ActionResult AgregarSeccionesProducto(int id)
        {
            var _Product = (from _p in db.Products
                            join _pt in db.ProductTypes
                            on _p.ProductTypeId equals _pt.ProductTypeId
                            where _p.ProductId == id
                            select new { _p, _pt }).ToList();
            ViewData["ProductName"] = _Product.SingleOrDefault()._p.ProductName;
            ViewData["Description"] = _Product.SingleOrDefault()._p.Description;
            ViewData["ProductType"] = _Product.SingleOrDefault()._pt.Description;
            ViewData["ProductId"] = id;
            return View();
        }
        public ActionResult EliminarSecciónProducto(int SectionId, int ProductId)
        {
            ProductSections _ProductSections = new ProductSections();
            _ProductSections.ProductId = ProductId;
            _ProductSections.SectionId = SectionId;
            db.Entry(_ProductSections).State = EntityState.Deleted;
            db.SaveChanges();
            var _Aplication = (from _ap in plm.Applications
                               where _ap.Description == "Sitio DEACI"
                               select _ap).ToList();
            foreach (var _App in _Aplication)
            {
                var _Tables = (from _tab in plm.Tables
                               where _tab.ApplicationId == _App.ApplicationId
                               && _tab.Description == "ProductSections"
                               select _tab).ToList();
                foreach (var _Tabs in _Tables)
                {
                    var _ApplicationId = _App.ApplicationId;
                    CountriesUsers p = (CountriesUsers)Session["CountriesUsers"];
                    ActivityLogs _ActivityLogs = new ActivityLogs();
                    int _OperationId = 4; // Eliminar
                    _ActivityLogs.UserId = p.userId;
                    _ActivityLogs.TableId = _Tabs.TableId; // ProductSections
                    _ActivityLogs.OperationId = _OperationId;
                    _ActivityLogs.Date = DateTime.Now;
                    _ActivityLogs.PrimaryKeyAffected = ("(ProductId," + _ProductSections.ProductId + "),(SectionId," + _ProductSections.SectionId + ")");
                    _ActivityLogs.FieldsAffected = null;
                    plm.Entry(_ActivityLogs).State = EntityState.Added;
                    plm.SaveChanges();
                }
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        public ActionResult AgregarProductoSección(string ProductId, string SectionId)
        {
            int _ProductId = int.Parse(ProductId);
            int _SectionId = int.Parse(SectionId);
            var _value = (from _ps in db.ProductSections
                          where _ps.ProductId == _ProductId && _ps.SectionId == _SectionId
                          select _ps).ToList();
            if (_value.LongCount() > 0)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            ProductSections _ProductSections = new ProductSections();
            _ProductSections.SectionId = _SectionId;
            _ProductSections.ProductId = _ProductId;
            db.Entry(_ProductSections).State = EntityState.Added;
            db.SaveChanges();
            var _Aplication = (from _ap in plm.Applications
                               where _ap.Description == "Sitio DEACI"
                               select _ap).ToList();
            foreach (var _App in _Aplication)
            {
                var _Tables = (from _tab in plm.Tables
                               where _tab.ApplicationId == _App.ApplicationId
                               && _tab.Description == "ProductSections"
                               select _tab).ToList();
                foreach (var _Tabs in _Tables)
                {
                    var _ApplicationId = _App.ApplicationId;
                    CountriesUsers p = (CountriesUsers)Session["CountriesUsers"];
                    ActivityLogs _ActivityLogs = new ActivityLogs();
                    int _OperationId = 4; // Eliminar
                    _ActivityLogs.UserId = p.userId;
                    _ActivityLogs.TableId = _Tabs.TableId; // ProductSections
                    _ActivityLogs.OperationId = _OperationId;
                    _ActivityLogs.Date = DateTime.Now;
                    _ActivityLogs.PrimaryKeyAffected = ("(ProductId," + _ProductSections.ProductId + "),(SectionId," + _ProductSections.SectionId + ")");
                    _ActivityLogs.FieldsAffected = null;
                    plm.Entry(_ActivityLogs).State = EntityState.Added;
                    plm.SaveChanges();
                }
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Marcas(int id, int ed, int ad, string palabra, string sortOrder, string currentFilter, string searchString, int? page)
        {
            SessionCompanyEditions _SessionCompanyEdition = new SessionCompanyEditions(id, ed, ad);
            Session["SessionCompanyEditions"] = _SessionCompanyEdition;
            SessionCompanyBrandEditions _SessionCompanyBrandEditions = new SessionCompanyBrandEditions(id, ed);
            Session["SessionCompanyBrandEditions"] = _SessionCompanyBrandEditions;
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
            var _Country = (from _ed in db.Editions
                            join _co in db.Countries
                            on _ed.CountryId equals _co.CountryId
                            where _ed.EditionId == id
                            select _co);
            ViewData["País"] = _Country.SingleOrDefault().CountryName;
            var _CountryBook = (from _ed in db.Editions
                                join _bo in db.Books
                                on _ed.BookId equals _bo.BookId
                                where _ed.EditionId == id
                                select _bo);
            ViewData["Obra"] = _CountryBook.SingleOrDefault().BookName;
            Union_companies_cb m = new Union_companies_cb();
            var _CompanyBrands = (from _c in db.Companies
                                  join _cb in db.CompanyBrands
                                  on _c.CompanyId equals _cb.CompanyId
                                  join _b in db.Brands
                                  on _cb.BrandId equals _b.BrandId
                                  where _cb.CompanyId == ed
                                  orderby _b.BrandName
                                  select new Union_companies_cb { Companies = _c, CompanyBrands = _cb, Brands = _b });
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
            var _Editions = (from _e in db.Editions
                             where _e.EditionId == id
                             select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Editions = _e });
            ViewData["Number"] = _Editions.SingleOrDefault().Editions.EditionId;
            ViewData["Edición"] = _Editions.SingleOrDefault().Editions.NumberEdition;
            var _Companies = (from _c in db.Companies
                              join _ce in db.CompanyEditions
                              on _c.CompanyId equals _ce.CompanyId
                              join _ct in db.CompanyTypes
                              on _ce.CompanyTypeId equals _ct.CompanyTypeId
                              where _c.CompanyId == ed
                              && _ct.CompanyTypeId == ad
                              select new { _c, _ct }).Distinct();
            ViewData["CompanyName"] = _Companies.FirstOrDefault()._c.CompanyName;
            ViewData["CompanyId"] = _Companies.FirstOrDefault()._c.CompanyId;
            ViewData["CompanyShortName"] = _Companies.SingleOrDefault()._c.CompanyShortName;
            ViewData["TipoCliente"] = _Companies.SingleOrDefault()._ct.Description;
            if (palabra != null)
            {
                if (palabra.LongCount() <= 3)
                {
                    if (!String.IsNullOrEmpty(palabra))
                    {
                        _CompanyBrands = _CompanyBrands.Where(j => j.Brands.BrandName.StartsWith(palabra));
                    }
                }
                if (palabra.LongCount() > 3)
                {
                    if (!String.IsNullOrEmpty(palabra))
                    {
                        _CompanyBrands = _CompanyBrands.Where(j => j.Brands.BrandName.Contains(palabra));
                    }
                }
            }
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(_CompanyBrands.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult EditarMarca(int id)
        {
            Brands _Brands = db.Brands.SingleOrDefault(model => model.BrandId == id);
            return View(_Brands);
        }
        public ActionResult AgregarMarcaCliente(string CompanyId, string BrandId, string EditionId)
        {
            int _CompanyId = int.Parse(CompanyId);
            int _BrandId = int.Parse(BrandId);
            int _EditionId = int.Parse(EditionId);
            var _value = (from _cb in db.CompanyBrands
                          where _cb.CompanyId == _CompanyId
                          && _cb.BrandId == _BrandId
                          select _cb).ToList();
            if (_value.LongCount() > 0)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            var ClientParticipant = (from _c in db.Companies
                                     join _ce in db.CompanyEditions
                                     on _c.CompanyId equals _ce.CompanyId
                                     where _c.CompanyId == _CompanyId
                                     && _ce.EditionId == _EditionId
                                     select new { _c, _ce }).ToList();
            if (ClientParticipant.LongCount() == 0)
            {
                CompanyBrands _CompanyBrands = new CompanyBrands();
                _CompanyBrands.CompanyId = _CompanyId;
                _CompanyBrands.BrandId = _BrandId;
                _CompanyBrands.Description = null;
                db.CompanyBrands.Add(_CompanyBrands);
                db.SaveChanges();
                var _Aplication = (from _ap in plm.Applications
                                   where _ap.Description == "Sitio DEACI"
                                   select _ap).ToList();
                foreach (var _App in _Aplication)
                {
                    var _Tables = (from _tab in plm.Tables
                                   where _tab.ApplicationId == _App.ApplicationId
                                   && _tab.Description == "CompanyBrands"
                                   select _tab).ToList();
                    foreach (var _Tabs in _Tables)
                    {
                        var _ApplicationId = _App.ApplicationId;
                        CountriesUsers p = (CountriesUsers)Session["CountriesUsers"];
                        ActivityLogs _ActivityLogs = new ActivityLogs();
                        int _OperationId = 1; // Agregar
                        _ActivityLogs.UserId = p.userId;
                        _ActivityLogs.TableId = _Tabs.TableId; // CompanyBrands
                        _ActivityLogs.OperationId = _OperationId;
                        _ActivityLogs.Date = DateTime.Now;
                        _ActivityLogs.PrimaryKeyAffected = ("(BrandId," + _CompanyBrands.BrandId + "),(CompanyId," + _CompanyBrands.CompanyId + ")");
                        _ActivityLogs.FieldsAffected = null;
                        plm.Entry(_ActivityLogs).State = EntityState.Added;
                        plm.SaveChanges();
                    }
                }
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            else
            {
                CompanyBrands _CompanyBrands = new CompanyBrands();
                _CompanyBrands.CompanyId = _CompanyId;
                _CompanyBrands.BrandId = _BrandId;
                _CompanyBrands.Description = null;
                db.CompanyBrands.Add(_CompanyBrands);
                db.SaveChanges();
                var _Aplication = (from _ap in plm.Applications
                                   where _ap.Description == "Sitio DEACI"
                                   select _ap).ToList();
                foreach (var _App in _Aplication)
                {
                    var _Tables = (from _tab in plm.Tables
                                   where _tab.ApplicationId == _App.ApplicationId
                                   && _tab.Description == "CompanyBrands"
                                   select _tab).ToList();
                    foreach (var _Tabs in _Tables)
                    {
                        var _ApplicationId = _App.ApplicationId;
                        CountriesUsers p = (CountriesUsers)Session["CountriesUsers"];
                        ActivityLogs _ActivityLogs = new ActivityLogs();
                        int _OperationId = 1; // Agregar
                        _ActivityLogs.UserId = p.userId;
                        _ActivityLogs.TableId = _Tabs.TableId; // CompanyBrands
                        _ActivityLogs.OperationId = _OperationId;
                        _ActivityLogs.Date = DateTime.Now;
                        _ActivityLogs.PrimaryKeyAffected = ("(BrandId," + _CompanyBrands.BrandId + "),(CompanyId," + _CompanyBrands.CompanyId + ")");
                        _ActivityLogs.FieldsAffected = null;
                        plm.Entry(_ActivityLogs).State = EntityState.Added;
                        plm.SaveChanges();
                    }
                }
                CompanyBrandEditions _CompanyBrandEditions = new CompanyBrandEditions();
                _CompanyBrandEditions.CompanyId = _CompanyId;
                _CompanyBrandEditions.BrandId = _BrandId;
                _CompanyBrandEditions.EditionId = _EditionId;
                db.Entry(_CompanyBrandEditions).State = EntityState.Added;
                db.SaveChanges();
                var _Aplications = (from _ap in plm.Applications
                                   where _ap.Description == "Sitio DEACI"
                                   select _ap).ToList();
                foreach (var _App in _Aplications)
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
                        int _OperationId = 1; // Agregar
                        _ActivityLogs.UserId = p.userId;
                        _ActivityLogs.TableId = _Tabs.TableId; // CompanyBrandEditions
                        _ActivityLogs.OperationId = _OperationId;
                        _ActivityLogs.Date = DateTime.Now;
                        _ActivityLogs.PrimaryKeyAffected = ("(BrandId," + _CompanyBrandEditions.BrandId + "),(CompanyId," + _CompanyBrandEditions.CompanyId + "),(EditionId," + _CompanyBrandEditions.EditionId + ")");
                        _ActivityLogs.FieldsAffected = null;
                        plm.Entry(_ActivityLogs).State = EntityState.Added;
                        plm.SaveChanges();
                    }
                }
                return Json(true, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult AgregarNuevaMarca(string _CompanyId, string _BrandName, string _EditionId)
        {
            int CompanyId = int.Parse(_CompanyId);
            int EditionId = int.Parse(_EditionId);
            var _Value = (from _b in db.Brands
                          where _b.BrandName.Trim() == _BrandName.Trim()
                          select _b);
            if (_Value.LongCount() > 0)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            else
            {
                Brands _Brands = new Brands();
                _Brands.BrandName = _BrandName;
                _Brands.Active = true;
                db.Brands.Add(_Brands);
                db.SaveChanges();
                var _Aplication = (from _ap in plm.Applications
                                   where _ap.Description == "Sitio DEACI"
                                   select _ap).ToList();
                foreach (var _App in _Aplication)
                {
                    var _Tables = (from _tab in plm.Tables
                                   where _tab.ApplicationId == _App.ApplicationId
                                   && _tab.Description == "Brands"
                                   select _tab).ToList();
                    foreach (var _Tabs in _Tables)
                    {
                        var _ApplicationId = _App.ApplicationId;
                        CountriesUsers p = (CountriesUsers)Session["CountriesUsers"];
                        ActivityLogs _ActivityLogs = new ActivityLogs();
                        int _OperationId = 1; // Agregar
                        _ActivityLogs.UserId = p.userId;
                        _ActivityLogs.TableId = _Tabs.TableId; // Brands
                        _ActivityLogs.OperationId = _OperationId;
                        _ActivityLogs.Date = DateTime.Now;
                        _ActivityLogs.PrimaryKeyAffected = ("(BrandId," + _Brands.BrandId + ")");
                        _ActivityLogs.FieldsAffected = null;
                        plm.Entry(_ActivityLogs).State = EntityState.Added;
                        plm.SaveChanges();
                    }
                }
                CompanyBrands _CompanyBrands = new CompanyBrands();
                _CompanyBrands.BrandId = _Brands.BrandId;
                _CompanyBrands.CompanyId = CompanyId;
                _CompanyBrands.Description = null;
                db.CompanyBrands.Add(_CompanyBrands);
                db.SaveChanges();
                var _Aplications = (from _ap in plm.Applications
                                   where _ap.Description == "Sitio DEACI"
                                   select _ap).ToList();
                foreach (var _App in _Aplications)
                {
                    var _Tables = (from _tab in plm.Tables
                                   where _tab.ApplicationId == _App.ApplicationId
                                   && _tab.Description == "CompanyBrands"
                                   select _tab).ToList();
                    foreach (var _Tabs in _Tables)
                    {
                        var _ApplicationId = _App.ApplicationId;
                        CountriesUsers p = (CountriesUsers)Session["CountriesUsers"];
                        ActivityLogs _ActivityLogs = new ActivityLogs();
                        int _OperationId = 1; // Agregar
                        _ActivityLogs.UserId = p.userId;
                        _ActivityLogs.TableId = _Tabs.TableId; // CompanyBrands
                        _ActivityLogs.OperationId = _OperationId;
                        _ActivityLogs.Date = DateTime.Now;
                        _ActivityLogs.PrimaryKeyAffected = ("(BrandId," + _CompanyBrands.BrandId + "),(CompanyId," + _CompanyBrands.CompanyId + ")");
                        _ActivityLogs.FieldsAffected = null;
                        plm.Entry(_ActivityLogs).State = EntityState.Added;
                        plm.SaveChanges();
                    }
                }
                var _ValueParticipantClient = (from _c in db.Companies
                                               join _ce in db.CompanyEditions
                                                   on _c.CompanyId equals _ce.CompanyId
                                               where _c.CompanyId == CompanyId
                                               && _ce.EditionId == EditionId
                                               select new { _c, _ce }).ToList();
                if (_ValueParticipantClient.LongCount() == 0)
                {
                    return Json(true, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    CompanyBrandEditions _CompanyBrandEditions = new CompanyBrandEditions();
                    _CompanyBrandEditions.CompanyId = CompanyId;
                    _CompanyBrandEditions.BrandId = _CompanyBrands.BrandId;
                    _CompanyBrandEditions.EditionId = EditionId;
                    db.Entry(_CompanyBrandEditions).State = EntityState.Added;
                    db.SaveChanges();
                    var _Aplications1 = (from _ap in plm.Applications
                                       where _ap.Description == "Sitio DEACI"
                                       select _ap).ToList();
                    foreach (var _App in _Aplications1)
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
                            int _OperationId = 1; // Agregar
                            _ActivityLogs.UserId = p.userId;
                            _ActivityLogs.TableId = _Tabs.TableId; // CompanyBrandEditions
                            _ActivityLogs.OperationId = _OperationId;
                            _ActivityLogs.Date = DateTime.Now;
                            _ActivityLogs.PrimaryKeyAffected = ("(BrandId," + _CompanyBrandEditions.BrandId + "),(CompanyId," + _CompanyBrandEditions.CompanyId + "),(EditionId," + _CompanyBrandEditions.EditionId + ")");
                            _ActivityLogs.FieldsAffected = null;
                            plm.Entry(_ActivityLogs).State = EntityState.Added;
                            plm.SaveChanges();
                        }
                    }
                    return Json(true, JsonRequestBehavior.AllowGet);
                }

            }
        }
        public ActionResult AgregarNuevaMarcaEdición(int id, int ed, int ad, int ud)
        {
            SessionBrandId _SessionBrandId = new SessionBrandId(ed, ad);
            Session["SessionBrandId"] = _SessionBrandId;
            var _BrandDetails = (from _b in db.Brands
                                 where _b.BrandId == ad
                                 select _b).ToList();
            ViewData["BrandName"] = _BrandDetails.SingleOrDefault().BrandName;
            ViewData["CompanyId"] = ed;
            return View();
        }
        public ActionResult AgregarMarcaEdición(string _EditionId, string _CompanyId, string _BrandId)
        {
            int EditionId = int.Parse(_EditionId);
            int CompanyId = int.Parse(_CompanyId);
            int BrandId = int.Parse(_BrandId);
            var _Value = (from _ce in db.CompanyEditions
                          where _ce.EditionId == EditionId
                          && _ce.CompanyId == CompanyId
                          select _ce);
            if (_Value.LongCount() == 0)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            var _Value2 = (from _ce in db.CompanyBrandEditions
                           where _ce.EditionId == EditionId
                           && _ce.CompanyId == CompanyId
                           && _ce.BrandId == BrandId
                           select _ce);
            if (_Value2.LongCount() > 0)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            CompanyBrandEditions _CompanyBrandEditions = new CompanyBrandEditions();
            _CompanyBrandEditions.EditionId = EditionId;
            _CompanyBrandEditions.CompanyId = CompanyId;
            _CompanyBrandEditions.BrandId = BrandId;
            db.CompanyBrandEditions.Add(_CompanyBrandEditions);
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
                    int _OperationId = 1; // Agregar
                    _ActivityLogs.UserId = p.userId;
                    _ActivityLogs.TableId = _Tabs.TableId; // CompanyBrandEditions
                    _ActivityLogs.OperationId = _OperationId;
                    _ActivityLogs.Date = DateTime.Now;
                    _ActivityLogs.PrimaryKeyAffected = ("(BrandId," + _CompanyBrandEditions.BrandId + "),(CompanyId," + _CompanyBrandEditions.CompanyId + "),(EditionId," + _CompanyBrandEditions.EditionId + ")");
                    _ActivityLogs.FieldsAffected = null;
                    plm.Entry(_ActivityLogs).State = EntityState.Added;
                    plm.SaveChanges();
                }
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ModificarMarca(string BrandId, string BrandName)
        {
            int _BrandId = int.Parse(BrandId);
            string _BrandName = BrandName;
            Brands _Brands = new Brands();
            _Brands.BrandId = _BrandId;
            _Brands.BrandName = BrandName.Trim();
            _Brands.Active = true;
            db.Entry(_Brands).State = EntityState.Modified;
            db.SaveChanges();
            var _Aplication = (from _ap in plm.Applications
                               where _ap.Description == "Sitio DEACI"
                               select _ap).ToList();
            foreach (var _App in _Aplication)
            {
                var _Tables = (from _tab in plm.Tables
                               where _tab.ApplicationId == _App.ApplicationId
                               && _tab.Description == "Brands"
                               select _tab).ToList();
                foreach (var _Tabs in _Tables)
                {
                    var _ApplicationId = _App.ApplicationId;
                    CountriesUsers p = (CountriesUsers)Session["CountriesUsers"];
                    ActivityLogs _ActivityLogs = new ActivityLogs();
                    int _OperationId = 2; // Actualizar
                    _ActivityLogs.UserId = p.userId;
                    _ActivityLogs.TableId = _Tabs.TableId; // ModifyBrands
                    _ActivityLogs.OperationId = _OperationId;
                    _ActivityLogs.Date = DateTime.Now;
                    _ActivityLogs.PrimaryKeyAffected = ("(BrandId," + _Brands.BrandId + ")");
                    _ActivityLogs.FieldsAffected = ("(BrandName," + _Brands.BrandName + ")");
                    plm.Entry(_ActivityLogs).State = EntityState.Added;
                    plm.SaveChanges();
                }
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        public ActionResult EliminarEdiciónMarca(int id, int EditionId, int CompanyId, int BrandId, int CompanyTypeId)
        {
            var _Editions = (from _e in db.Editions
                             where _e.EditionId == id
                             select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Editions = _e });
            CompanyBrandEditions _CompanyBrandEditions = new CompanyBrandEditions();
            var _ValueParticipantBrand = db.CompanyBrandEditions.Where(model => model.EditionId == EditionId && model.CompanyId == CompanyId && model.BrandId == BrandId);
            if (_ValueParticipantBrand.LongCount() == 0)
            {
                return RedirectToAction("/Marcas/" + id + "/" + _CompanyBrandEditions.CompanyId + "/" + CompanyTypeId);
            }
            _CompanyBrandEditions.EditionId = EditionId;
            _CompanyBrandEditions.CompanyId = CompanyId;
            _CompanyBrandEditions.BrandId = BrandId;
            db.Entry(_CompanyBrandEditions).State = EntityState.Deleted;
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
                    _ActivityLogs.PrimaryKeyAffected = ("(BrandId," + _CompanyBrandEditions.BrandId + "),(EditionId," + _CompanyBrandEditions.EditionId + "),(CompanyId," + _CompanyBrandEditions.CompanyId + ")");
                    _ActivityLogs.FieldsAffected = null;
                    plm.Entry(_ActivityLogs).State = EntityState.Added;
                    plm.SaveChanges();
                }
            }
            return RedirectToAction("/Marcas/" + id + "/" + _CompanyBrandEditions.CompanyId + "/" + CompanyTypeId);
        }
        public ActionResult AgregarÍndicesMarca(int id, int ed)
        {
            var _Brand = (from _b in db.Brands
                          where _b.BrandId == ed
                          select _b).ToList();
            ViewData["BrandName"] = _Brand.SingleOrDefault().BrandName;
            ViewData["BrandId"] = _Brand.SingleOrDefault().BrandId;
            ViewData["CompanyId"] = id;
            return View();
        }
        public ActionResult AgregarMarcaÍndice(string CompanyId, string IndexId, string BrandId)
        {
            int _CompanyId = int.Parse(CompanyId);
            byte _IndexId = byte.Parse(IndexId);
            int _BrandId = int.Parse(BrandId);
            var _value = (from _cbi in db.CompanyBrandIndexes
                          where _cbi.IndexId == _IndexId && _cbi.CompanyId == _CompanyId && _cbi.BrandId == _BrandId
                          select _cbi).ToList();
            if (_value.LongCount() > 0)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            CompanyBrandIndexes _CompanyBrandIndexes = new CompanyBrandIndexes();
            _CompanyBrandIndexes.CompanyId = _CompanyId;
            _CompanyBrandIndexes.BrandId = _BrandId;
            _CompanyBrandIndexes.IndexId = _IndexId;
            db.Entry(_CompanyBrandIndexes).State = EntityState.Added;
            db.SaveChanges();
            var _Aplication = (from _ap in plm.Applications
                               where _ap.Description == "Sitio DEACI"
                               select _ap).ToList();
            foreach (var _App in _Aplication)
            {
                var _Tables = (from _tab in plm.Tables
                               where _tab.ApplicationId == _App.ApplicationId
                               && _tab.Description == "CompanyBrandIndexes"
                               select _tab).ToList();
                foreach (var _Tabs in _Tables)
                {
                    var _ApplicationId = _App.ApplicationId;
                    CountriesUsers p = (CountriesUsers)Session["CountriesUsers"];
                    ActivityLogs _ActivityLogs = new ActivityLogs();
                    int _OperationId = 1; // Agregar
                    _ActivityLogs.UserId = p.userId;
                    _ActivityLogs.TableId = _Tabs.TableId; // CompanyBrandIndexes
                    _ActivityLogs.OperationId = _OperationId;
                    _ActivityLogs.Date = DateTime.Now;
                    _ActivityLogs.PrimaryKeyAffected = ("(BrandId," + _CompanyBrandIndexes.BrandId + "),(IndexId," + _CompanyBrandIndexes.IndexId + "),(CompanyId," + _CompanyBrandIndexes.CompanyId + ")");
                    _ActivityLogs.FieldsAffected = null;
                    plm.Entry(_ActivityLogs).State = EntityState.Added;
                    plm.SaveChanges();
                }
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        public ActionResult EliminarMarcaÍndice(int id, int CompanyId, byte IndexId, int BrandId, int CompanyTypeId)
        {
            CompanyBrandIndexes _CompanyBrandIndexes = new CompanyBrandIndexes();
            _CompanyBrandIndexes.IndexId = IndexId;
            _CompanyBrandIndexes.CompanyId = CompanyId;
            _CompanyBrandIndexes.BrandId = BrandId;
            db.Entry(_CompanyBrandIndexes).State = EntityState.Deleted;
            db.SaveChanges();
            var _Aplication = (from _ap in plm.Applications
                               where _ap.Description == "Sitio DEACI"
                               select _ap).ToList();
            foreach (var _App in _Aplication)
            {
                var _Tables = (from _tab in plm.Tables
                               where _tab.ApplicationId == _App.ApplicationId
                               && _tab.Description == "CompanyBrandIndexes"
                               select _tab).ToList();
                foreach (var _Tabs in _Tables)
                {
                    var _ApplicationId = _App.ApplicationId;
                    CountriesUsers p = (CountriesUsers)Session["CountriesUsers"];
                    ActivityLogs _ActivityLogs = new ActivityLogs();
                    int _OperationId = 4; // Eliminar
                    _ActivityLogs.UserId = p.userId;
                    _ActivityLogs.TableId = _Tabs.TableId; // CompanyBrandIndexes
                    _ActivityLogs.OperationId = _OperationId;
                    _ActivityLogs.Date = DateTime.Now;
                    _ActivityLogs.PrimaryKeyAffected = ("(BrandId," + _CompanyBrandIndexes.BrandId + "),(IndexId," + _CompanyBrandIndexes.IndexId + "),(CompanyId," + _CompanyBrandIndexes.CompanyId + ")");
                    _ActivityLogs.FieldsAffected = null;
                    plm.Entry(_ActivityLogs).State = EntityState.Added;
                    plm.SaveChanges();
                }
            }
            return RedirectToAction("/Marcas/" + id + "/" + _CompanyBrandIndexes.CompanyId + "/" + CompanyTypeId);
        }
        public ActionResult AgregarSeccionesMarca(int id, int ed)
        {
            var _Brand = (from _b in db.Brands
                          where _b.BrandId == ed
                          select _b).ToList();
            ViewData["BrandName"] = _Brand.SingleOrDefault().BrandName;
            ViewData["BrandId"] = _Brand.SingleOrDefault().BrandId;
            ViewData["CompanyId"] = id;
            return View();
        }
        public ActionResult AgregarMarcaSección(string CompanyId, string SectionId, string BrandId)
        {
            int _CompanyId = int.Parse(CompanyId);
            int _SectionId = int.Parse(SectionId);
            int _BrandId = int.Parse(BrandId);
            var _value = (from _cbs in db.CompanyBrandSections
                          where _cbs.CompanyId == _CompanyId && _cbs.SectionId == _SectionId && _cbs.BrandId == _BrandId
                          select _cbs).ToList();
            if (_value.LongCount() > 0)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            CompanyBrandSections _CompanyBrandSections = new CompanyBrandSections();
            _CompanyBrandSections.CompanyId = _CompanyId;
            _CompanyBrandSections.BrandId = _BrandId;
            _CompanyBrandSections.SectionId = _SectionId;
            db.Entry(_CompanyBrandSections).State = EntityState.Added;
            db.SaveChanges();
            var _Aplication = (from _ap in plm.Applications
                               where _ap.Description == "Sitio DEACI"
                               select _ap).ToList();
            foreach (var _App in _Aplication)
            {
                var _Tables = (from _tab in plm.Tables
                               where _tab.ApplicationId == _App.ApplicationId
                               && _tab.Description == "CompanyBrandSections"
                               select _tab).ToList();
                foreach (var _Tabs in _Tables)
                {
                    var _ApplicationId = _App.ApplicationId;
                    CountriesUsers p = (CountriesUsers)Session["CountriesUsers"];
                    ActivityLogs _ActivityLogs = new ActivityLogs();
                    int _OperationId = 1; // Agregar
                    _ActivityLogs.UserId = p.userId;
                    _ActivityLogs.TableId = _Tabs.TableId; // CompanyBrandSections
                    _ActivityLogs.OperationId = _OperationId;
                    _ActivityLogs.Date = DateTime.Now;
                    _ActivityLogs.PrimaryKeyAffected = ("(BrandId," + _CompanyBrandSections.BrandId + "),(SectionId," + _CompanyBrandSections.SectionId + "),(CompanyId," + _CompanyBrandSections.CompanyId + ")");
                    _ActivityLogs.FieldsAffected = null;
                    plm.Entry(_ActivityLogs).State = EntityState.Added;
                    plm.SaveChanges();
                }
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        public ActionResult EliminarMarcaSección(int id, int CompanyId, int SectionId, int BrandId, int CompanyTypeId)
        {
            CompanyBrandSections _CompanyBrandSections = new CompanyBrandSections();
            _CompanyBrandSections.SectionId = SectionId;
            _CompanyBrandSections.CompanyId = CompanyId;
            _CompanyBrandSections.BrandId = BrandId;
            db.Entry(_CompanyBrandSections).State = EntityState.Deleted;
            db.SaveChanges();
            var _Aplication = (from _ap in plm.Applications
                               where _ap.Description == "Sitio DEACI"
                               select _ap).ToList();
            foreach (var _App in _Aplication)
            {
                var _Tables = (from _tab in plm.Tables
                               where _tab.ApplicationId == _App.ApplicationId
                               && _tab.Description == "CompanyBrandSections"
                               select _tab).ToList();
                foreach (var _Tabs in _Tables)
                {
                    var _ApplicationId = _App.ApplicationId;
                    CountriesUsers p = (CountriesUsers)Session["CountriesUsers"];
                    ActivityLogs _ActivityLogs = new ActivityLogs();
                    int _OperationId = 4; // Eliminar
                    _ActivityLogs.UserId = p.userId;
                    _ActivityLogs.TableId = _Tabs.TableId; // CompanyBrandSections
                    _ActivityLogs.OperationId = _OperationId;
                    _ActivityLogs.Date = DateTime.Now;
                    _ActivityLogs.PrimaryKeyAffected = ("(BrandId," + _CompanyBrandSections.BrandId + "),(SectionId," + _CompanyBrandSections.SectionId + "),(CompanyId," + _CompanyBrandSections.CompanyId + ")");
                    _ActivityLogs.FieldsAffected = null;
                    plm.Entry(_ActivityLogs).State = EntityState.Added;
                    plm.SaveChanges();
                }
            }
            return RedirectToAction("/Marcas/" + id + "/" + _CompanyBrandSections.CompanyId + "/" + CompanyTypeId);
        }
        public ActionResult Secciones(int id, int ed, int ad, int? ud)
        {
            SessionCompanyEditions _SessionCompanyEdition = new SessionCompanyEditions(id, ed, ad);
            Session["SessionCompanyEditions"] = _SessionCompanyEdition;
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
            if (ud == null)
            {
                ViewData["SectionNull"] = "";
            }
            else
            {
                var _SectionSelected = (from _s in db.Sections
                                        where _s.SectionId == ud
                                        select _s).ToList();
                ViewData["SectionLevel"] = _SectionSelected.SingleOrDefault().Description;
                ViewData["SectionLevelId"] = _SectionSelected.SingleOrDefault().SectionId;
                Sections _Section = new Sections();
                _Section.SectionId = _SectionSelected.SingleOrDefault().SectionId;
                SessionSectionId _SessionSectionId = new SessionSectionId(_Section.SectionId);
                Session["SessionSectionId"] = _SessionSectionId;
                if (_SectionSelected.SingleOrDefault().SectionIdParent == null)
                {
                    ViewData["SectionValueNull"] = "";
                }
                else
                {
                    ViewData["True"] = "";
                    _Section.SectionIdParent = _SectionSelected.SingleOrDefault().SectionIdParent;
                    var _SectionSelected2 = (from _s in db.Sections
                                             where _s.SectionId == _Section.SectionIdParent
                                             select _s).ToList();
                    if (_SectionSelected2.LongCount() == 0)
                    {
                        ViewData["SectionValueNull2"] = "";
                    }
                    else
                    {
                        ViewData["SectionLevel2"] = _SectionSelected2.FirstOrDefault().Description;
                        ViewData["SectionLevel2Id"] = _SectionSelected2.FirstOrDefault().SectionId;
                        Sections _Section2 = new Sections();
                        _Section2.SectionIdParent = _SectionSelected2.SingleOrDefault().SectionIdParent;
                        if (_SectionSelected2.SingleOrDefault().SectionIdParent == null)
                        {
                            ViewData["SectionValueNull2"] = "";
                        }
                        else
                        {
                            var _SectionSelected3 = (from _s in db.Sections
                                                     where _s.SectionId == _Section2.SectionIdParent
                                                     select _s).ToList();
                            ViewData["SectionLevel3"] = _SectionSelected3.FirstOrDefault().Description;
                            ViewData["SectionLevel3Id"] = _SectionSelected3.FirstOrDefault().SectionId;
                        }
                    }
                }
            }
            var _Country = (from _ed in db.Editions
                            join _co in db.Countries
                            on _ed.CountryId equals _co.CountryId
                            where _ed.EditionId == id
                            select _co);
            ViewData["País"] = _Country.SingleOrDefault().CountryName;
            var _CountryBook = (from _ed in db.Editions
                                join _bo in db.Books
                                on _ed.BookId equals _bo.BookId
                                where _ed.EditionId == id
                                select _bo);
            ViewData["Obra"] = _CountryBook.SingleOrDefault().BookName;
            var _CompanyTypesSections = (from _c in db.Companies
                                         join _cs in db.CompanySections
                                         on _c.CompanyId equals _cs.CompanyId
                                         join _s in db.Sections
                                         on _cs.SectionId equals _s.SectionId
                                         where _c.CompanyId == ed
                                         select new { _c, _s }).ToList();
            if (_CompanyTypesSections.LongCount() == 0)
            {
                ViewData["ErrorSections"] = "";
            }
            else
            {
                ViewData["SectionDescrition"] = _CompanyTypesSections.FirstOrDefault()._s.Description;
                ViewData["SectionDescriptionId"] = _CompanyTypesSections.FirstOrDefault()._s.SectionId;
                SessionSectionId2 _SessionSectionId2 = new SessionSectionId2(_CompanyTypesSections.SingleOrDefault()._s.SectionId);
                Session["SessionSectionId2"] = _SessionSectionId2;
            }
            var _Companies = (from _c in db.Companies
                              join _ce in db.CompanyEditions
                              on _c.CompanyId equals _ce.CompanyId
                              join _ct in db.CompanyTypes
                              on _ce.CompanyTypeId equals _ct.CompanyTypeId
                              where _c.CompanyId == ed
                              && _ct.CompanyTypeId == ad
                              && _ce.EditionId == id
                              select new { _c, _ct }).ToList();
            ViewData["CompanyName"] = _Companies.FirstOrDefault()._c.CompanyName;
            ViewData["CompanyId"] = _Companies.FirstOrDefault()._c.CompanyId;
            ViewData["CompanyShortName"] = _Companies.FirstOrDefault()._c.CompanyShortName;
            ViewData["TipoCliente"] = _Companies.SingleOrDefault()._ct.Description;
            var _Editions = (from _e in db.Editions
                             where _e.EditionId == id
                             select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Editions = _e });
            ViewData["Number"] = _Editions.SingleOrDefault().Editions.EditionId;
            ViewData["Edición"] = _Editions.SingleOrDefault().Editions.NumberEdition;
            return View();
        }
        public ActionResult EditarSecciones(string SectionName, string SectionId, string SectionIdParent)
        {
            Sections _Sections = new Sections();
            if (SectionIdParent == "")
            {
                _Sections.SectionIdParent = null;
            }
            else
            {
                int _SectionIdParent = int.Parse(SectionIdParent);
                _Sections.SectionIdParent = _SectionIdParent;
            }
            int _SectionId = int.Parse(SectionId);
            _Sections.SectionId = _SectionId;
            _Sections.Description = SectionName;
            _Sections.Active = true;
            db.Entry(_Sections).State = EntityState.Modified;
            db.SaveChanges();
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        public ActionResult EditarSecciones2(string SectionName, string SectionId, string SectionIdParent)
        {
            Sections _Sections = new Sections();
            if (SectionIdParent == "")
            {
                _Sections.SectionIdParent = null;
            }
            else
            {
                int _SectionIdParent = int.Parse(SectionIdParent);
                _Sections.SectionIdParent = int.Parse(SectionIdParent);
            }
            int _SectionId = int.Parse(SectionId);
            _Sections.SectionId = _SectionId;
            _Sections.Description = SectionName;
            _Sections.Active = true;
            db.Entry(_Sections).State = EntityState.Modified;
            db.SaveChanges();
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        public ActionResult AgregarNuevaSección(string SectionName, string SectionName2, string SectionName3, string CompanyId, string EditionId)
        {
            int _CompanyId = int.Parse(CompanyId);
            int _EditionId = int.Parse(EditionId);
            var _Value = (from _s in db.Sections
                          where _s.Description.Trim() == SectionName.Trim()
                          select _s).ToList();
            if (_Value.LongCount() > 0)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            Sections _Sections = new Sections();
            _Sections.Description = SectionName;
            _Sections.SectionIdParent = null;
            _Sections.Active = true;
            db.Sections.Add(_Sections);
            db.SaveChanges();
            if (SectionName2 == "")
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            else
            {
                Sections _Sections2 = new Sections();
                _Sections2.SectionIdParent = _Sections.SectionId;
                _Sections2.Description = SectionName2;
                _Sections2.Active = true;
                db.Sections.Add(_Sections2);
                db.SaveChanges();
                if (SectionName3 == "")
                {
                    return Json(true, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    Sections _Sections3 = new Sections();
                    _Sections3.SectionIdParent = _Sections2.SectionId;
                    _Sections3.Description = SectionName3;
                    _Sections3.Active = true;
                    db.Sections.Add(_Sections3);
                    db.SaveChanges();
                    return Json(true, JsonRequestBehavior.AllowGet);
                }
            }
        }
        public ActionResult AsociarSección(string SectionName, string SectionIdParent)
        {
            var _valueName = (from _s in db.Sections
                              where _s.Description.Trim() == SectionName.Trim()
                              select _s).ToList();
            if (_valueName.LongCount() > 0)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            int _SectionIdParent = int.Parse(SectionIdParent);
            Sections _Sections = new Sections();
            var _Value = (from _s in db.Sections
                          where _s.SectionId == _SectionIdParent
                          select _s).ToList();
            if (_Value.SingleOrDefault().SectionIdParent != null)
            {
                foreach (var _v in _Value)
                {
                    var _value2 = (from _s in db.Sections
                                   where _s.SectionId == _v.SectionIdParent
                                   select _s).ToList();
                    if (_value2.FirstOrDefault().SectionIdParent != null)
                    {
                        return Json(false, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        _Sections.Description = SectionName;
                        _Sections.SectionIdParent = _SectionIdParent;
                        _Sections.Active = true;
                        db.Entry(_Sections).State = EntityState.Added;
                        db.SaveChanges();
                        return Json(true, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            else
            {
                _Sections.Description = SectionName;
                _Sections.SectionIdParent = _SectionIdParent;
                _Sections.Active = true;
                db.Entry(_Sections).State = EntityState.Added;
                db.SaveChanges();
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            return View();
        }
        public ActionResult DesasociarSección(int id, int CompanyId, int SectionId, string Description, int SectionIdParent, int CompanyTypeId)
        {
            Sections _Sections = new Sections();
            _Sections.SectionId = SectionId;
            _Sections.Description = Description;
            _Sections.SectionIdParent = null;
            db.Entry(_Sections).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("/Secciones/" + id + "/" + CompanyId + "/" + CompanyTypeId + "/" + SectionIdParent);
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
        public ActionResult EliminarSecciónCliente(int id, int CompanyId, int SectionId, int CompanyTypeId)
        {
            var _ValueAdver = db.EditionCompanySectionAdvers.Where(model => model.EditionId == id &&
                model.CompanyId == CompanyId && model.SectionId == SectionId);
            foreach (var Detele in _ValueAdver)
            {
                db.Entry(Detele).State = EntityState.Deleted;
                db.SaveChanges();
                var _EditionNumber = db.Editions.SingleOrDefault(model => model.EditionId == id);
                var imageName = "";
                imageName = Detele.AdverFile;
                string FullPathEditionId = Request.MapPath("~/Anuncios/DEACI_" + _EditionNumber.NumberEdition + "/" + imageName);
                if (System.IO.File.Exists(FullPathEditionId))
                {
                    System.IO.File.Delete(FullPathEditionId);
                }
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
                        _ActivityLogs.PrimaryKeyAffected = ("(SectionId," + Detele.SectionId + "),(EditionId," + Detele.EditionId + "),(CompanyId," + Detele.CompanyId + ")");
                        _ActivityLogs.FieldsAffected = null;
                        plm.Entry(_ActivityLogs).State = EntityState.Added;
                        plm.SaveChanges();
                    }
                }
            }
            
            CompanySections _CompanySections = new CompanySections();
            _CompanySections.CompanyId = CompanyId;
            _CompanySections.SectionId = SectionId;
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
            return RedirectToAction("/Clientes/" + id + "/" + _CompanySections.CompanyId + "/" + CompanyTypeId);
        }
        public ActionResult Ediciones(int id, int ed, int _CompanyTypeId)
        {
            SessionCompanyEditions _SessionCompanyEdition = new SessionCompanyEditions(id, ed, _CompanyTypeId);
            Session["SessionCompanyEditions"] = _SessionCompanyEdition;
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
            var _Country = (from _ed in db.Editions
                            join _co in db.Countries
                            on _ed.CountryId equals _co.CountryId
                            where _ed.EditionId == id
                            select _co);
            ViewData["País"] = _Country.SingleOrDefault().CountryName;
            var _CountryBook = (from _ed in db.Editions
                                join _bo in db.Books
                                on _ed.BookId equals _bo.BookId
                                where _ed.EditionId == id
                                select _bo);
            ViewData["Obra"] = _CountryBook.SingleOrDefault().BookName;
            var _CompanyEditions = (from _c in db.Companies
                                    join _ce in db.CompanyEditions
                                    on _c.CompanyId equals _ce.CompanyId
                                    join _edr in db.Editions
                                    on _ce.EditionId equals _edr.EditionId
                                    where _c.CompanyId == ed
                                    select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Companies = _c, CompanyEditions = _ce, Editions = _edr });
            var _Editions = (from _e in db.Editions
                             where _e.EditionId == id
                             select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Editions = _e });
            ViewData["Number"] = _Editions.SingleOrDefault().Editions.EditionId;
            ViewData["Edición"] = _Editions.SingleOrDefault().Editions.NumberEdition;
            var _Companies = (from _c in db.Companies
                              where _c.CompanyId == ed
                              select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Companies = _c });
            ViewData["CompanyName"] = _Companies.SingleOrDefault().Companies.CompanyName;
            ViewData["CompanyId"] = _Companies.SingleOrDefault().Companies.CompanyId;
            return View(_CompanyEditions);
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
                _CompanyEditions.Page = null;
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
                        var _DeleteCompanyDistributions = db.EditionCompanyDistributions.Where(model => model.EditionId == EditionId && model.CompanyId == ClientSon.CompanyId);
                        foreach (var _DeleteDistributionsCompanies in _DeleteCompanyDistributions)
                        {
                            db.EditionCompanyDistributions.Remove(_DeleteDistributionsCompanies);
                            db.SaveChanges();
                            var _Distributions = db.CompanyDistributions.SingleOrDefault(model => model.CompanyId == _DeleteDistributionsCompanies.CompanyId && model.DistributionId == _DeleteDistributionsCompanies.DistributionId);
                            db.CompanyDistributions.Remove(_Distributions);
                            db.SaveChanges();
                            var _ProductsDistributions = (from _p in db.Products
                                                          join _pe in db.ProductEditions
                                                          on _p.ProductId equals _pe.ProductId
                                                          where _p.ProductId == _DeleteDistributionsCompanies.DistributionId
                                                          && _pe.EditionId == _DeleteDistributionsCompanies.EditionId
                                                          select _p).ToList();
                            var _BrandsDistributions = (from _cbe in db.CompanyBrandEditions
                                                        where _cbe.CompanyId == _DeleteDistributionsCompanies.DistributionId
                                                        && _cbe.EditionId == _DeleteDistributionsCompanies.EditionId
                                                        select _cbe).ToList();
                            var _AdversEditions = (from _eds in db.EditionCompanySectionAdvers
                                                   where _eds.CompanyId == _DeleteDistributionsCompanies.DistributionId
                                                   && _eds.EditionId == _DeleteDistributionsCompanies.EditionId
                                                   select _eds).ToList();
                            var _EditionsDist = (from _ced in db.EditionCompanyDistributions
                                                where _ced.CompanyId == _DeleteDistributionsCompanies.DistributionId
                                                && _ced.EditionId == _DeleteDistributionsCompanies.EditionId
                                                select _ced).ToList();
                            if (_ProductsDistributions.LongCount() == 0 && _BrandsDistributions.LongCount() == 0 && _AdversEditions.LongCount() == 0 && _EditionsDist.LongCount() == 0)
                            {
                                var _DeleteEditionDistribution = db.CompanyEditions.SingleOrDefault(model => model.EditionId == _DeleteDistributionsCompanies.EditionId && model.CompanyId == _DeleteDistributionsCompanies.DistributionId);
                                db.CompanyEditions.Remove(_DeleteEditionDistribution);
                                db.SaveChanges();
                            }
                        }
                        var _deleteClient = db.CompanyEditions.SingleOrDefault(model => model.CompanyId == ClientSon.CompanyId && model.EditionId == EditionId);
                        var _ValueAdver = db.EditionCompanySectionAdvers.Where(model => model.EditionId == _deleteClient.EditionId &&    model.CompanyId == _deleteClient.CompanyId);
                        foreach (var _Ade in _ValueAdver)
                        {
                            db.Entry(_Ade).State = EntityState.Deleted;
                            db.SaveChanges();
                            var _EditionNumber = db.Editions.SingleOrDefault(model => model.EditionId == _Ade.EditionId);
                            var imageName = "";
                            imageName = _Ade.AdverFile;
                            string FullPathEditionId = Request.MapPath("~/Anuncios/DEACI_" + _EditionNumber.NumberEdition + "/" + imageName);
                            if (System.IO.File.Exists(FullPathEditionId))
                            {
                                System.IO.File.Delete(FullPathEditionId);
                            }
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
                                    _ActivityLogs.PrimaryKeyAffected = ("(SectionId," + _Ade.SectionId + "),(EditionId," + _Ade.EditionId + "),(CompanyId," + _Ade.CompanyId + ")");
                                    _ActivityLogs.FieldsAffected = null;
                                    plm.Entry(_ActivityLogs).State = EntityState.Added;
                                    plm.SaveChanges();
                                }
                            }
                        }
                        db.CompanyEditions.Remove(_deleteClient);
                        db.SaveChanges();
                        var _Aplications = (from _ap in plm.Applications
                                           where _ap.Description == "Sitio DEACI"
                                           select _ap).ToList();
                        foreach (var _App in _Aplications)
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
                                var _Aplication2 = (from _ap in plm.Applications
                                                    where _ap.Description == "Sitio DEACI"
                                                    select _ap).ToList();
                                foreach (var _App in _Aplication2)
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
                                var _Aplication3 = (from _ap in plm.Applications
                                                    where _ap.Description == "Sitio DEACI"
                                                    select _ap).ToList();
                                foreach (var _App in _Aplication3)
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
                    }
                }
            }
            var _ValueAdvers = db.EditionCompanySectionAdvers.Where(model => model.EditionId == EditionId &&
                model.CompanyId == CompanyId);
            foreach (var _Ade in _ValueAdvers)
            {
                db.Entry(_Ade).State = EntityState.Deleted;
                db.SaveChanges();
                var _EditionNumber = db.Editions.SingleOrDefault(model => model.EditionId == _Ade.EditionId);
                var imageName = "";
                imageName = _Ade.AdverFile;
                string FullPathEditionId = Request.MapPath("~/Anuncios/DEACI_" + _EditionNumber.NumberEdition + "/" + imageName);
                if (System.IO.File.Exists(FullPathEditionId))
                {
                    System.IO.File.Delete(FullPathEditionId);
                }
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
                        _ActivityLogs.PrimaryKeyAffected = ("(SectionId," + _Ade.SectionId + "),(EditionId," + _Ade.EditionId + "),(CompanyId," + _Ade.CompanyId + ")");
                        _ActivityLogs.FieldsAffected = null;
                        plm.Entry(_ActivityLogs).State = EntityState.Added;
                        plm.SaveChanges();
                    }
                }
            }
            CompanyEditions _CompanyEditions = new CompanyEditions();
            _CompanyEditions.EditionId = EditionId;
            _CompanyEditions.CompanyId = CompanyId;
            var _eliminarCompanyEditions = db.CompanyEditions.SingleOrDefault(model => model.EditionId == _CompanyEditions.EditionId
            && model.CompanyId == _CompanyEditions.CompanyId);

            var _CompanyDistributions = db.EditionCompanyDistributions.Where(model => model.EditionId == EditionId && model.CompanyId == CompanyId).ToList();

            foreach (var _CompanyDistributionsDelete in _CompanyDistributions)
            {
                db.EditionCompanyDistributions.Remove(_CompanyDistributionsDelete);
                db.SaveChanges();
            }
            db.CompanyEditions.Remove(_eliminarCompanyEditions);
            db.SaveChanges();
            var _Aplication5 = (from _ap in plm.Applications
                               where _ap.Description == "Sitio DEACI"
                               select _ap).ToList();
            foreach (var _App in _Aplication5)
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
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        public ActionResult InsertarProductoParticipante(string EditionId, string ProductId)
        {
            int _EditionId = int.Parse(EditionId);
            int _ProductId = int.Parse(ProductId);
            ProductEditions _ProductEditions = new ProductEditions();
            var _ValueParticipantClient = (from _c in db.Companies
                                           join _ce in db.CompanyEditions
                                           on _c.CompanyId equals _ce.CompanyId
                                           join _p in db.Products
                                           on _c.CompanyId equals _p.CompanyId
                                           where _ce.EditionId == _EditionId
                                           && _p.ProductId == _ProductId
                                           select new { _c, _ce, _p }).ToList();
            if (_ValueParticipantClient.LongCount() == 0)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            var _ValueProductParent = (from _p in db.Products
                                       where _p.ProductId == _ProductId
                                       select _p).ToList();
            foreach (var _pp in _ValueProductParent)
            {
                if (_pp.ProductIdParent != null)
                {
                    var _valueParentProductParticipant = (from _p in db.Products
                                                          join _pe in db.ProductEditions
                                                          on _p.ProductId equals _pe.ProductId
                                                          where _p.ProductId == _pp.ProductIdParent
                                                          && _pe.EditionId == _EditionId
                                                          select new { _p, _pe }).ToList();
                    if (_valueParentProductParticipant.LongCount() > 0)
                    {
                        _ProductEditions.ProductId = _ProductId;
                        _ProductEditions.EditionId = _EditionId;
                        _ProductEditions.HtmlFile = null;
                        _ProductEditions.HtmlContent = null;
                        _ProductEditions.Page = null;
                        _ProductEditions.StatusId = 3;
                        db.Entry(_ProductEditions).State = EntityState.Added;
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
                                int _OperationId = 1; // Agregar
                                _ActivityLogs.UserId = p.userId;
                                _ActivityLogs.TableId = _Tabs.TableId; // ProductEditions
                                _ActivityLogs.OperationId = _OperationId;
                                _ActivityLogs.Date = DateTime.Now;
                                _ActivityLogs.PrimaryKeyAffected = ("(ProductId," + _ProductEditions.ProductId + "),(EditionId," + _ProductEditions.EditionId + ")");
                                _ActivityLogs.FieldsAffected = null;
                                plm.Entry(_ActivityLogs).State = EntityState.Added;
                                plm.SaveChanges();
                            }
                        }
                        return Json(true, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(false, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    _ProductEditions.ProductId = _ProductId;
                    _ProductEditions.EditionId = _EditionId;
                    _ProductEditions.HtmlFile = null;
                    _ProductEditions.HtmlContent = null;
                    _ProductEditions.Page = null;
                    _ProductEditions.StatusId = 3;
                    db.Entry(_ProductEditions).State = EntityState.Added;
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
                            int _OperationId = 1; // Agregar
                            _ActivityLogs.UserId = p.userId;
                            _ActivityLogs.TableId = _Tabs.TableId; // ProductEditions
                            _ActivityLogs.OperationId = _OperationId;
                            _ActivityLogs.Date = DateTime.Now;
                            _ActivityLogs.PrimaryKeyAffected = ("(ProductId," + _ProductEditions.ProductId + "),(EditionId," + _ProductEditions.EditionId + ")");
                            _ActivityLogs.FieldsAffected = null;
                            plm.Entry(_ActivityLogs).State = EntityState.Added;
                            plm.SaveChanges();
                        }
                    }
                    return Json(true, JsonRequestBehavior.AllowGet);
                }
            }
            return View();
        }
        public ActionResult EliminarProductoParticipante(string EditionId, string ProductId)
        {
            int _EditionId = int.Parse(EditionId);
            int _Productid = int.Parse(ProductId);
            var _ValueParticipantproduct = db.ProductEditions.Where(model => model.EditionId == _EditionId && model.ProductId == _Productid);
            if (_ValueParticipantproduct.LongCount() == 0)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            var _ValueProductParent = (from _p in db.Products
                                       where _p.ProductId == _Productid
                                       select _p).ToList();
            foreach (var _ProductValue in _ValueProductParent)
            {
                if (_ProductValue.ProductIdParent == null)
                {
                    var _DeleteHtmlFile = db.ProductEditions.Where(model => model.ProductId == _Productid && model.EditionId == _EditionId);
                    foreach (var _ProductEditions in _DeleteHtmlFile)
                    {
                        var _EditionsFile = (from _ed in db.Editions
                                             where _ed.EditionId == _EditionId
                                             select _ed).ToList();
                        if (_EditionsFile.SingleOrDefault().ISBN == "DEACI 30")
                        {
                            if (_ProductEditions.HtmlFile != null)
                            {
                                var htmlfile = "";
                                htmlfile = _ProductEditions.HtmlFile;
                                string fullPath_DEACI30 = Request.MapPath("/Productos/DEACI_30/" + htmlfile);
                                if (System.IO.File.Exists(fullPath_DEACI30))
                                {
                                    System.IO.File.Delete(fullPath_DEACI30);
                                }
                            }
                        }
                        if (_EditionsFile.SingleOrDefault().ISBN == "DEACI 29")
                        {
                            if (_ProductEditions.HtmlFile != null)
                            {
                                var htmlfile = "";
                                htmlfile = _ProductEditions.HtmlFile;
                                string fullPath_DEACI29 = Request.MapPath("/Productos/DEACI_29/" + htmlfile);
                                if (System.IO.File.Exists(fullPath_DEACI29))
                                {
                                    System.IO.File.Delete(fullPath_DEACI29);
                                }
                            }
                        }
                        if (_EditionsFile.SingleOrDefault().ISBN == "DEACI 28")
                        {
                            if (_ProductEditions.HtmlFile != null)
                            {
                                var htmlfile = "";
                                htmlfile = _ProductEditions.HtmlFile;
                                string fullPath_DEACI29 = Request.MapPath("/Productos/DEACI_28/" + htmlfile);
                                if (System.IO.File.Exists(fullPath_DEACI29))
                                {
                                    System.IO.File.Delete(fullPath_DEACI29);
                                }
                            }
                        }
                        if (_EditionsFile.SingleOrDefault().ISBN == "DEACI 27")
                        {
                            if (_ProductEditions.HtmlFile != null)
                            {
                                var htmlfile = "";
                                htmlfile = _ProductEditions.HtmlFile;
                                string fullPath_DEACI29 = Request.MapPath("/Productos/DEACI_27/" + htmlfile);
                                if (System.IO.File.Exists(fullPath_DEACI29))
                                {
                                    System.IO.File.Delete(fullPath_DEACI29);
                                }
                            }
                        }
                        if (_EditionsFile.SingleOrDefault().ISBN == "DEACI 26")
                        {
                            if (_ProductEditions.HtmlFile != null)
                            {
                                var htmlfile = "";
                                htmlfile = _ProductEditions.HtmlFile;
                                string fullPath_DEACI29 = Request.MapPath("/Productos/DEACI_26/" + htmlfile);
                                if (System.IO.File.Exists(fullPath_DEACI29))
                                {
                                    System.IO.File.Delete(fullPath_DEACI29);
                                }
                            }
                        }
                        if (_EditionsFile.SingleOrDefault().ISBN == "DEACI 25")
                        {
                            if (_ProductEditions.HtmlFile != null)
                            {
                                var htmlfile = "";
                                htmlfile = _ProductEditions.HtmlFile;
                                string fullPath_DEACI29 = Request.MapPath("/Productos/DEACI_25/" + htmlfile);
                                if (System.IO.File.Exists(fullPath_DEACI29))
                                {
                                    System.IO.File.Delete(fullPath_DEACI29);
                                }
                            }
                        }
                        if (_EditionsFile.SingleOrDefault().ISBN == "DEACI 24")
                        {
                            if (_ProductEditions.HtmlFile != null)
                            {
                                var htmlfile = "";
                                htmlfile = _ProductEditions.HtmlFile;
                                string fullPath_DEACI29 = Request.MapPath("/Productos/DEACI_24/" + htmlfile);
                                if (System.IO.File.Exists(fullPath_DEACI29))
                                {
                                    System.IO.File.Delete(fullPath_DEACI29);
                                }
                            }
                        }
                        if (_EditionsFile.SingleOrDefault().ISBN == "DEACI 22")
                        {
                            if (_ProductEditions.HtmlFile != null)
                            {
                                var htmlfile = "";
                                htmlfile = _ProductEditions.HtmlFile;
                                string fullPath_DEACI29 = Request.MapPath("/Productos/DEACI_22/" + htmlfile);
                                if (System.IO.File.Exists(fullPath_DEACI29))
                                {
                                    System.IO.File.Delete(fullPath_DEACI29);
                                }
                            }
                        }
                        db.ProductEditions.Remove(_ProductEditions);
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
                                _ActivityLogs.PrimaryKeyAffected = ("(ProductId," + _ProductEditions.ProductId + "),(" + "EditionId," + _ProductEditions.EditionId + ")");
                                _ActivityLogs.FieldsAffected = null;
                                plm.Entry(_ActivityLogs).State = EntityState.Added;
                                plm.SaveChanges();
                            }
                        }
                    }
                    var _ProductSon = (from _p in db.Products
                                       join _pe in db.ProductEditions
                                       on _p.ProductId equals _pe.ProductId
                                       where _p.ProductIdParent == _ProductValue.ProductId
                                       && _pe.EditionId == _EditionId
                                       select _p).ToList();
                    foreach (var _ProductDeleteEditions in _ProductSon)
                    {
                        var _DeleteProductsEditionSon = db.ProductEditions.SingleOrDefault(model => model.EditionId == _EditionId
                        && model.ProductId == _ProductDeleteEditions.ProductId);
                        var _EditionsFileSon = (from _ed in db.Editions
                                             where _ed.EditionId == _EditionId
                                             select _ed).ToList();
                        if (_EditionsFileSon.SingleOrDefault().ISBN == "DEACI 30")
                        {
                            if (_DeleteProductsEditionSon.HtmlFile != null)
                            {
                                var htmlfile = "";
                                htmlfile = _DeleteProductsEditionSon.HtmlFile;
                                string fullPath_DEACI30 = Request.MapPath("/Productos/DEACI_30/" + htmlfile);
                                if (System.IO.File.Exists(fullPath_DEACI30))
                                {
                                    System.IO.File.Delete(fullPath_DEACI30);
                                }
                            }
                        }
                        if (_EditionsFileSon.SingleOrDefault().ISBN == "DEACI 29")
                        {
                            if (_DeleteProductsEditionSon.HtmlFile != null)
                            {
                                var htmlfile = "";
                                htmlfile = _DeleteProductsEditionSon.HtmlFile;
                                string fullPath_DEACI29 = Request.MapPath("/Productos/DEACI_29/" + htmlfile);
                                if (System.IO.File.Exists(fullPath_DEACI29))
                                {
                                    System.IO.File.Delete(fullPath_DEACI29);
                                }
                            }
                        }
                        if (_EditionsFileSon.SingleOrDefault().ISBN == "DEACI 28")
                        {
                            if (_DeleteProductsEditionSon.HtmlFile != null)
                            {
                                var htmlfile = "";
                                htmlfile = _DeleteProductsEditionSon.HtmlFile;
                                string fullPath_DEACI29 = Request.MapPath("/Productos/DEACI_28/" + htmlfile);
                                if (System.IO.File.Exists(fullPath_DEACI29))
                                {
                                    System.IO.File.Delete(fullPath_DEACI29);
                                }
                            }
                        }
                        if (_EditionsFileSon.SingleOrDefault().ISBN == "DEACI 27")
                        {
                            if (_DeleteProductsEditionSon.HtmlFile != null)
                            {
                                var htmlfile = "";
                                htmlfile = _DeleteProductsEditionSon.HtmlFile;
                                string fullPath_DEACI29 = Request.MapPath("/Productos/DEACI_27/" + htmlfile);
                                if (System.IO.File.Exists(fullPath_DEACI29))
                                {
                                    System.IO.File.Delete(fullPath_DEACI29);
                                }
                            }
                        }
                        if (_EditionsFileSon.SingleOrDefault().ISBN == "DEACI 26")
                        {
                            if (_DeleteProductsEditionSon.HtmlFile != null)
                            {
                                var htmlfile = "";
                                htmlfile = _DeleteProductsEditionSon.HtmlFile;
                                string fullPath_DEACI29 = Request.MapPath("/Productos/DEACI_26/" + htmlfile);
                                if (System.IO.File.Exists(fullPath_DEACI29))
                                {
                                    System.IO.File.Delete(fullPath_DEACI29);
                                }
                            }
                        }
                        if (_EditionsFileSon.SingleOrDefault().ISBN == "DEACI 25")
                        {
                            if (_DeleteProductsEditionSon.HtmlFile != null)
                            {
                                var htmlfile = "";
                                htmlfile = _DeleteProductsEditionSon.HtmlFile;
                                string fullPath_DEACI29 = Request.MapPath("/Productos/DEACI_25/" + htmlfile);
                                if (System.IO.File.Exists(fullPath_DEACI29))
                                {
                                    System.IO.File.Delete(fullPath_DEACI29);
                                }
                            }
                        }
                        if (_EditionsFileSon.SingleOrDefault().ISBN == "DEACI 24")
                        {
                            if (_DeleteProductsEditionSon.HtmlFile != null)
                            {
                                var htmlfile = "";
                                htmlfile = _DeleteProductsEditionSon.HtmlFile;
                                string fullPath_DEACI29 = Request.MapPath("/Productos/DEACI_24/" + htmlfile);
                                if (System.IO.File.Exists(fullPath_DEACI29))
                                {
                                    System.IO.File.Delete(fullPath_DEACI29);
                                }
                            }
                        }
                        if (_EditionsFileSon.SingleOrDefault().ISBN == "DEACI 22")
                        {
                            if (_DeleteProductsEditionSon.HtmlFile != null)
                            {
                                var htmlfile = "";
                                htmlfile = _DeleteProductsEditionSon.HtmlFile;
                                string fullPath_DEACI29 = Request.MapPath("/Productos/DEACI_22/" + htmlfile);
                                if (System.IO.File.Exists(fullPath_DEACI29))
                                {
                                    System.IO.File.Delete(fullPath_DEACI29);
                                }
                            }
                        }
                        db.ProductEditions.Remove(_DeleteProductsEditionSon);
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
                                _ActivityLogs.PrimaryKeyAffected = ("(ProductId," + _DeleteProductsEditionSon.ProductId + "),(" + "EditionId," + _DeleteProductsEditionSon.EditionId + ")");
                                _ActivityLogs.FieldsAffected = null;
                                plm.Entry(_ActivityLogs).State = EntityState.Added;
                                plm.SaveChanges();
                            }
                        }
                    }
                    return Json(true, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var _DeleteHtmlFile = db.ProductEditions.Where(model => model.ProductId == _Productid && model.EditionId == _EditionId);
                    foreach (var _ProductEditions in _DeleteHtmlFile)
                    {
                        var _EditionsFile = (from _ed in db.Editions
                                             where _ed.EditionId == _EditionId
                                             select _ed).ToList();
                        if (_EditionsFile.SingleOrDefault().ISBN == "DEACI 30")
                        {
                            if (_ProductEditions.HtmlFile != null)
                            {
                                var htmlfile = "";
                                htmlfile = _ProductEditions.HtmlFile;
                                string fullPath_DEACI30 = Request.MapPath("/Productos/DEACI_30/" + htmlfile);
                                if (System.IO.File.Exists(fullPath_DEACI30))
                                {
                                    System.IO.File.Delete(fullPath_DEACI30);
                                }
                            }
                        }
                        if (_EditionsFile.SingleOrDefault().ISBN == "DEACI 29")
                        {
                            if (_ProductEditions.HtmlFile != null)
                            {
                                var htmlfile = "";
                                htmlfile = _ProductEditions.HtmlFile;
                                string fullPath_DEACI29 = Request.MapPath("/Productos/DEACI_29/" + htmlfile);
                                if (System.IO.File.Exists(fullPath_DEACI29))
                                {
                                    System.IO.File.Delete(fullPath_DEACI29);
                                }
                            }
                        }
                        if (_EditionsFile.SingleOrDefault().ISBN == "DEACI 28")
                        {
                            if (_ProductEditions.HtmlFile != null)
                            {
                                var htmlfile = "";
                                htmlfile = _ProductEditions.HtmlFile;
                                string fullPath_DEACI29 = Request.MapPath("/Productos/DEACI_28/" + htmlfile);
                                if (System.IO.File.Exists(fullPath_DEACI29))
                                {
                                    System.IO.File.Delete(fullPath_DEACI29);
                                }
                            }
                        }
                        if (_EditionsFile.SingleOrDefault().ISBN == "DEACI 27")
                        {
                            if (_ProductEditions.HtmlFile != null)
                            {
                                var htmlfile = "";
                                htmlfile = _ProductEditions.HtmlFile;
                                string fullPath_DEACI29 = Request.MapPath("/Productos/DEACI_27/" + htmlfile);
                                if (System.IO.File.Exists(fullPath_DEACI29))
                                {
                                    System.IO.File.Delete(fullPath_DEACI29);
                                }
                            }
                        }
                        if (_EditionsFile.SingleOrDefault().ISBN == "DEACI 26")
                        {
                            if (_ProductEditions.HtmlFile != null)
                            {
                                var htmlfile = "";
                                htmlfile = _ProductEditions.HtmlFile;
                                string fullPath_DEACI29 = Request.MapPath("/Productos/DEACI_26/" + htmlfile);
                                if (System.IO.File.Exists(fullPath_DEACI29))
                                {
                                    System.IO.File.Delete(fullPath_DEACI29);
                                }
                            }
                        }
                        if (_EditionsFile.SingleOrDefault().ISBN == "DEACI 25")
                        {
                            if (_ProductEditions.HtmlFile != null)
                            {
                                var htmlfile = "";
                                htmlfile = _ProductEditions.HtmlFile;
                                string fullPath_DEACI29 = Request.MapPath("/Productos/DEACI_25/" + htmlfile);
                                if (System.IO.File.Exists(fullPath_DEACI29))
                                {
                                    System.IO.File.Delete(fullPath_DEACI29);
                                }
                            }
                        }
                        if (_EditionsFile.SingleOrDefault().ISBN == "DEACI 24")
                        {
                            if (_ProductEditions.HtmlFile != null)
                            {
                                var htmlfile = "";
                                htmlfile = _ProductEditions.HtmlFile;
                                string fullPath_DEACI29 = Request.MapPath("/Productos/DEACI_24/" + htmlfile);
                                if (System.IO.File.Exists(fullPath_DEACI29))
                                {
                                    System.IO.File.Delete(fullPath_DEACI29);
                                }
                            }
                        }
                        if (_EditionsFile.SingleOrDefault().ISBN == "DEACI 22")
                        {
                            if (_ProductEditions.HtmlFile != null)
                            {
                                var htmlfile = "";
                                htmlfile = _ProductEditions.HtmlFile;
                                string fullPath_DEACI29 = Request.MapPath("/Productos/DEACI_22/" + htmlfile);
                                if (System.IO.File.Exists(fullPath_DEACI29))
                                {
                                    System.IO.File.Delete(fullPath_DEACI29);
                                }
                            }
                        }
                        db.ProductEditions.Remove(_ProductEditions);
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
                                _ActivityLogs.PrimaryKeyAffected = ("(ProductId," + _ProductEditions.ProductId + "),(" + "EditionId," + _ProductEditions.EditionId + ")");
                                _ActivityLogs.FieldsAffected = null;
                                plm.Entry(_ActivityLogs).State = EntityState.Added;
                                plm.SaveChanges();
                            }
                        }
                    }
                    return Json(true, JsonRequestBehavior.AllowGet);
                }
            }
            return View();
        }
        public ActionResult InsertarMarcaParticipante(string EditionId, string CompanyId, string BrandId)
        {
            int _EditionId = int.Parse(EditionId);
            int _CompanyId = int.Parse(CompanyId);
            int _BrandId = int.Parse(BrandId);
            var _ValueParticipantClient = (from _c in db.Companies
                                           join _ce in db.CompanyEditions
                                           on _c.CompanyId equals _ce.CompanyId
                                           where _c.CompanyId == _CompanyId
                                           && _ce.EditionId == _EditionId
                                           select new { _c, _ce }).ToList();
            if (_ValueParticipantClient.LongCount() == 0)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            CompanyBrandEditions _CompanyBrandEditions = new CompanyBrandEditions();
            _CompanyBrandEditions.EditionId = _EditionId;
            _CompanyBrandEditions.CompanyId = _CompanyId;
            _CompanyBrandEditions.BrandId = _BrandId;
            db.Entry(_CompanyBrandEditions).State = EntityState.Added;
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
                    int _OperationId = 1; // Agregar
                    _ActivityLogs.UserId = p.userId;
                    _ActivityLogs.TableId = _Tabs.TableId; // CompanyBrandEditions
                    _ActivityLogs.OperationId = _OperationId;
                    _ActivityLogs.Date = DateTime.Now;
                    _ActivityLogs.PrimaryKeyAffected = ("(BrandId," + _CompanyBrandEditions.BrandId + "),(CompanyId," + _CompanyBrandEditions.CompanyId + "),(EditionId," + _CompanyBrandEditions.EditionId + ")");
                    _ActivityLogs.FieldsAffected = null;
                    plm.Entry(_ActivityLogs).State = EntityState.Added;
                    plm.SaveChanges();
                }
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        public ActionResult EliminarMarcaParticipante(string EditionId, string CompanyId, string BrandId)
        {
            int _EditionId = int.Parse(EditionId);
            int _CompanyId = int.Parse(CompanyId);
            int _BrandId = int.Parse(BrandId);
            var _ValueParticipantBrand = db.CompanyBrandEditions.Where(model => model.CompanyId == _CompanyId && model.EditionId == _EditionId && model.BrandId == _BrandId);
            if (_ValueParticipantBrand.LongCount() == 0)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            var _ValueParticipant = (from _cbe in db.CompanyBrandEditions
                                     where _cbe.BrandId == _BrandId && _cbe.CompanyId == _CompanyId
                                     select _cbe).ToList();
            if (_ValueParticipant.LongCount() < 2)
            {
                var _CompanyBrandEditions = (from _cbe in db.CompanyBrandEditions
                                             where _cbe.CompanyId == _CompanyId && _cbe.BrandId == _BrandId && _cbe.EditionId == _EditionId
                                             select _cbe).ToList();
                foreach (var _Values in _CompanyBrandEditions)
                {
                    var _DeleteCompanyBrandEditions = db.CompanyBrandEditions.SingleOrDefault(model => model.CompanyId == _Values.CompanyId
                        && model.EditionId == _Values.EditionId && model.BrandId == _Values.BrandId);
                    db.CompanyBrandEditions.Remove(_DeleteCompanyBrandEditions);
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
                            _ActivityLogs.PrimaryKeyAffected = ("(BrandId," + _DeleteCompanyBrandEditions.BrandId + "),(EditionId," + _DeleteCompanyBrandEditions.EditionId + "),(CompanyId," + _DeleteCompanyBrandEditions.CompanyId + ")");
                            _ActivityLogs.FieldsAffected = null;
                            plm.Entry(_ActivityLogs).State = EntityState.Added;
                            plm.SaveChanges();
                        }
                    }
                }
                var _CompanyBrandIndexes = (from _cbi in db.CompanyBrandIndexes
                                            where _cbi.CompanyId == _CompanyId && _cbi.BrandId == _BrandId
                                            select _cbi).ToList();
                foreach (var _Value in _CompanyBrandIndexes)
                {
                    var _DeleteCompanyBrandIndexes = db.CompanyBrandIndexes.SingleOrDefault(model => model.CompanyId == _Value.CompanyId
                        && model.BrandId == _Value.BrandId && model.IndexId == _Value.IndexId);
                    db.CompanyBrandIndexes.Remove(_DeleteCompanyBrandIndexes);
                    db.SaveChanges();
                    var _Aplication = (from _ap in plm.Applications
                                       where _ap.Description == "Sitio DEACI"
                                       select _ap).ToList();
                    foreach (var _App in _Aplication)
                    {
                        var _Tables = (from _tab in plm.Tables
                                       where _tab.ApplicationId == _App.ApplicationId
                                       && _tab.Description == "CompanyBrandIndexes"
                                       select _tab).ToList();
                        foreach (var _Tabs in _Tables)
                        {
                            var _ApplicationId = _App.ApplicationId;
                            CountriesUsers p = (CountriesUsers)Session["CountriesUsers"];
                            ActivityLogs _ActivityLogs = new ActivityLogs();
                            int _OperationId = 4; // Eliminar
                            _ActivityLogs.UserId = p.userId;
                            _ActivityLogs.TableId = _Tabs.TableId; // CompanyBrandIndexes
                            _ActivityLogs.OperationId = _OperationId;
                            _ActivityLogs.Date = DateTime.Now;
                            _ActivityLogs.PrimaryKeyAffected = ("(BrandId," + _DeleteCompanyBrandIndexes.BrandId + "),(IndexId," + _DeleteCompanyBrandIndexes.IndexId + "),(CompanyId," + _DeleteCompanyBrandIndexes.CompanyId + ")");
                            _ActivityLogs.FieldsAffected = null;
                            plm.Entry(_ActivityLogs).State = EntityState.Added;
                            plm.SaveChanges();
                        }
                    }
                }
                var _CompanyBrandSections = (from _cbs in db.CompanyBrandSections
                                             where _cbs.CompanyId == _CompanyId && _cbs.BrandId == _BrandId
                                             select _cbs).ToList();
                foreach (var _Value in _CompanyBrandSections)
                {
                    var _DeleteCompanyBrandSections = db.CompanyBrandSections.SingleOrDefault(model => model.CompanyId == _Value.CompanyId
                        && model.BrandId == _Value.BrandId && model.SectionId == _Value.SectionId);
                    db.CompanyBrandSections.Remove(_DeleteCompanyBrandSections);
                    db.SaveChanges();
                    var _Aplication = (from _ap in plm.Applications
                                       where _ap.Description == "Sitio DEACI"
                                       select _ap).ToList();
                    foreach (var _App in _Aplication)
                    {
                        var _Tables = (from _tab in plm.Tables
                                       where _tab.ApplicationId == _App.ApplicationId
                                       && _tab.Description == "CompanyBrandSections"
                                       select _tab).ToList();
                        foreach (var _Tabs in _Tables)
                        {
                            var _ApplicationId = _App.ApplicationId;
                            CountriesUsers p = (CountriesUsers)Session["CountriesUsers"];
                            ActivityLogs _ActivityLogs = new ActivityLogs();
                            int _OperationId = 4; // Eliminar
                            _ActivityLogs.UserId = p.userId;
                            _ActivityLogs.TableId = _Tabs.TableId; // CompanyBrandSections
                            _ActivityLogs.OperationId = _OperationId;
                            _ActivityLogs.Date = DateTime.Now;
                            _ActivityLogs.PrimaryKeyAffected = ("(BrandId," + _DeleteCompanyBrandSections.BrandId + "),(SectionId," + _DeleteCompanyBrandSections.SectionId + "),(CompanyId," + _DeleteCompanyBrandSections.CompanyId + ")");
                            _ActivityLogs.FieldsAffected = null;
                            plm.Entry(_ActivityLogs).State = EntityState.Added;
                            plm.SaveChanges();
                        }
                    }
                }
                var _CompanyBrands = (from _cb in db.CompanyBrands
                                      where _cb.CompanyId == _CompanyId && _cb.BrandId == _BrandId
                                      select _cb).ToList();
                foreach (var _Value in _CompanyBrands)
                {
                    var _DeleteCompanyBrans = db.CompanyBrands.SingleOrDefault(model => model.CompanyId == _Value.CompanyId
                        && model.BrandId == _Value.BrandId);
                    db.CompanyBrands.Remove(_DeleteCompanyBrans);
                    db.SaveChanges();
                    var _Aplication = (from _ap in plm.Applications
                                       where _ap.Description == "Sitio DEACI"
                                       select _ap).ToList();
                    foreach (var _App in _Aplication)
                    {
                        var _Tables = (from _tab in plm.Tables
                                       where _tab.ApplicationId == _App.ApplicationId
                                       && _tab.Description == "CompanyBrands"
                                       select _tab).ToList();
                        foreach (var _Tabs in _Tables)
                        {
                            var _ApplicationId = _App.ApplicationId;
                            CountriesUsers p = (CountriesUsers)Session["CountriesUsers"];
                            ActivityLogs _ActivityLogs = new ActivityLogs();
                            int _OperationId = 4; // Eliminar
                            _ActivityLogs.UserId = p.userId;
                            _ActivityLogs.TableId = _Tabs.TableId; // CompanyBrands
                            _ActivityLogs.OperationId = _OperationId;
                            _ActivityLogs.Date = DateTime.Now;
                            _ActivityLogs.PrimaryKeyAffected = ("(BrandId," + _DeleteCompanyBrans.BrandId + "),(CompanyId," + _DeleteCompanyBrans.CompanyId + ")");
                            _ActivityLogs.FieldsAffected = null;
                            plm.Entry(_ActivityLogs).State = EntityState.Added;
                            plm.SaveChanges();
                        }
                    }
                }
            }
            else
            {
                var _CompanyBrandEditions = (from _cbe in db.CompanyBrandEditions
                                             where _cbe.CompanyId == _CompanyId && _cbe.EditionId == _EditionId && _cbe.BrandId == _BrandId
                                             select _cbe).ToList();
                foreach (var _Value in _CompanyBrandEditions)
                {
                    var _DeleteCompanyBranEditions = db.CompanyBrandEditions.SingleOrDefault(model => model.CompanyId == _Value.CompanyId
                        && model.EditionId == _Value.EditionId && model.BrandId == _Value.BrandId);
                    db.CompanyBrandEditions.Remove(_DeleteCompanyBranEditions);
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
                            _ActivityLogs.PrimaryKeyAffected = ("(BrandId," + _DeleteCompanyBranEditions.BrandId + "),(EditionId," + _DeleteCompanyBranEditions.EditionId + "),(CompanyId," + _DeleteCompanyBranEditions.CompanyId + ")");
                            _ActivityLogs.FieldsAffected = null;
                            plm.Entry(_ActivityLogs).State = EntityState.Added;
                            plm.SaveChanges();
                        }
                    }
                }
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        public ActionResult AgregarAnuncioEdición(string EditionCompanySectionAdverId, string EditionId, string SectionId, string CompanyId)
        {
            int _EditionCompanySectionAdverId = int.Parse(EditionCompanySectionAdverId);
            int _EditionId = int.Parse(EditionId);
            int _CompanyId = int.Parse(CompanyId);
            int _SectionId = int.Parse(SectionId);
            EditionCompanySectionAdvers _EditionCompanySectionAdvers = new EditionCompanySectionAdvers();
            var _Adver = db.EditionCompanySectionAdvers.Where(model => model.EditionCompanySectionAdverId == _EditionCompanySectionAdverId);
            foreach (var _Add in _Adver)
            {
                var _EditionExist = (from _e in db.Editions
                                     where _e.EditionId == _Add.EditionId
                                     select _e).ToList();
                var imageName = "";
                imageName = _Add.AdverFile;
                string FullPathDEACI = Request.MapPath("/Anuncios/DEACI_" + _EditionExist.SingleOrDefault().NumberEdition + "/" + imageName);

                    if (System.IO.File.Exists(FullPathDEACI))
                    {
                var _EditionNotExist = (from _ed in db.Editions
                                        where _ed.EditionId == _EditionId
                                        select _ed).ToList();
                        foreach (var Complement in _EditionNotExist)
                        {
                            string Directory = Request.MapPath("/Anuncios/DEACI_" + Complement.NumberEdition);
                            if (System.IO.Directory.Exists(Directory))
                            {
                                string DestFile = System.IO.Path.Combine(Directory, imageName);
                                System.IO.File.Copy(FullPathDEACI, DestFile, true);
                                _EditionCompanySectionAdvers.BaseURL = DestFile;
                            }
                            else
                            {
                                System.IO.Directory.CreateDirectory(Directory);
                                string DestFile = System.IO.Path.Combine(Directory, imageName);
                                System.IO.File.Copy(FullPathDEACI, DestFile, true);
                                _EditionCompanySectionAdvers.BaseURL = DestFile;
                            }
                        }
                    }
                    _EditionCompanySectionAdvers.EditionId = _EditionId;
                    _EditionCompanySectionAdvers.CompanyId = _CompanyId;
                    _EditionCompanySectionAdvers.SectionId = _SectionId;
                    _EditionCompanySectionAdvers.AdverFile = _Add.AdverFile;
                    _EditionCompanySectionAdvers.HiredSpace = _Add.HiredSpace;
                    db.Entry(_EditionCompanySectionAdvers).State = EntityState.Added;
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
                        int _OperationId = 1; // Agregar
                        _ActivityLogs.UserId = p.userId;
                        _ActivityLogs.TableId = _Tabs.TableId; // AdvertisementEditions
                        _ActivityLogs.OperationId = _OperationId;
                        _ActivityLogs.Date = DateTime.Now;
                        _ActivityLogs.PrimaryKeyAffected = ("(AdvId," + _EditionCompanySectionAdvers.EditionCompanySectionAdverId 
                            + "),(EditionId," + _EditionCompanySectionAdvers.EditionId + "),(CompanyId," + _EditionCompanySectionAdvers.CompanyId 
                            + "),(SectionId," + _EditionCompanySectionAdvers.SectionId + ")");
                        _ActivityLogs.FieldsAffected = ("(AdverFile," + _EditionCompanySectionAdvers.AdverFile + "),(HiredSpace," + _EditionCompanySectionAdvers.HiredSpace 
                            + "),(BaseURL," + _EditionCompanySectionAdvers.BaseURL + "),");
                        plm.Entry(_ActivityLogs).State = EntityState.Added;
                        plm.SaveChanges();
                    }
                }
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        public ActionResult EliminarAnuncioEdición(string EditionCompanySectionAdverId, string EditionId, string CompanyId, string SectionId)
        {
            int _EditionCompanySectionAdverId = int.Parse(EditionCompanySectionAdverId);
            int _EditionId = int.Parse(EditionId);
            int _CompanyId = int.Parse(CompanyId);
            int _SectionId = int.Parse(SectionId);
            var _EditionNumber = db.Editions.SingleOrDefault(model => model.EditionId == _EditionId);
            var _DeteleEditionCompanySectionAdver = db.EditionCompanySectionAdvers.SingleOrDefault(model => model.EditionId == _EditionId && model.CompanyId == _CompanyId
                && model.SectionId == _SectionId);
            var imageName = "";
            imageName = _DeteleEditionCompanySectionAdver.AdverFile;
            string FullPathDEACI = Request.MapPath("~/Anuncios/DEACI_/" + _EditionNumber.NumberEdition + "/" + imageName);
            if (System.IO.File.Exists(FullPathDEACI))
            {
                System.IO.File.Delete(FullPathDEACI);
            }
            db.Entry(_DeteleEditionCompanySectionAdver).State = EntityState.Deleted;
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
                    _ActivityLogs.PrimaryKeyAffected = ("(EditionCompanySectionAdverId," + _DeteleEditionCompanySectionAdver.EditionCompanySectionAdverId 
                        + "),(EditionId," + _DeteleEditionCompanySectionAdver.EditionId + "),(CompanyId," + _DeteleEditionCompanySectionAdver.CompanyId 
                        + "),(SectionId," + _DeteleEditionCompanySectionAdver.SectionId + ")");
                    _ActivityLogs.FieldsAffected = null;
                    plm.Entry(_ActivityLogs).State = EntityState.Added;
                    plm.SaveChanges();
                }
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        public ActionResult EliminarAnuncioEdiciónCliente(int EditionCompanySectionAdverId, int EditionId, int CompanyId, int SectionId, int CompanyTypeId, string AdverFile)
        {
            EditionCompanySectionAdvers _EditionCompanySectionAdvers = new EditionCompanySectionAdvers();
            _EditionCompanySectionAdvers.EditionCompanySectionAdverId = EditionCompanySectionAdverId;
            _EditionCompanySectionAdvers.EditionId = EditionId;
            _EditionCompanySectionAdvers.CompanyId = CompanyId;
            _EditionCompanySectionAdvers.SectionId = SectionId;
            db.Entry(_EditionCompanySectionAdvers).State = EntityState.Deleted;
            db.SaveChanges();
            var _EditionNumber = db.Editions.SingleOrDefault(model => model.EditionId == EditionId);
            var imageName = "";
            imageName = AdverFile;
            string FullPathEditionId = Request.MapPath("~/Anuncios/DEACI_" + _EditionNumber.NumberEdition + "/" + imageName);
            if (System.IO.File.Exists(FullPathEditionId))
            {
                System.IO.File.Delete(FullPathEditionId);
            }
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
                    _ActivityLogs.PrimaryKeyAffected = ("(SectionId," + _EditionCompanySectionAdvers.SectionId + "),(EditionId," + _EditionCompanySectionAdvers.EditionId + "),(CompanyId," + _EditionCompanySectionAdvers.CompanyId + ")");
                    _ActivityLogs.FieldsAffected = null;
                    plm.Entry(_ActivityLogs).State = EntityState.Added;
                    plm.SaveChanges();
                }
            }
            return RedirectToAction("/Clientes/" + EditionId + "/" + CompanyId + "/" + CompanyTypeId);
        }
        public ActionResult EditarAnuncio(int id, int ed, int ad, int ud)
        {
            EditionCompanySectionAdvers _EditionCompanySectionAdvers = db.EditionCompanySectionAdvers.SingleOrDefault(model => model.EditionCompanySectionAdverId == id
                && model.EditionId == ed && model.CompanyId == ad && model.SectionId == ud);
            return View(_EditionCompanySectionAdvers);
        }
        public ActionResult ModificarAnuncio(string EditionCompanySectionAdverId, string EditionId, string CompanyId, string SectionId, string AdverFile, string HiredSpace, string BaseURL)
        {
            int _EditionCompanySectionAdverId = int.Parse(EditionCompanySectionAdverId);
            int _EditionId = int.Parse(EditionId);
            int _CompanyId = int.Parse(CompanyId);
            int _SectionId = int.Parse(SectionId);

            EditionCompanySectionAdvers _EditionCompanySectionAdvers = new EditionCompanySectionAdvers();
            _EditionCompanySectionAdvers.EditionCompanySectionAdverId = _EditionCompanySectionAdverId;
            _EditionCompanySectionAdvers.EditionId = _EditionId;
            _EditionCompanySectionAdvers.CompanyId = _CompanyId;
            _EditionCompanySectionAdvers.SectionId = _SectionId;
            if (AdverFile == "")
            { _EditionCompanySectionAdvers.AdverFile = null; }
            else
            { _EditionCompanySectionAdvers.AdverFile = AdverFile; }
            if (BaseURL == "")
            { _EditionCompanySectionAdvers.BaseURL = null; }
            else
            { _EditionCompanySectionAdvers.BaseURL = BaseURL; }
            _EditionCompanySectionAdvers.HiredSpace = HiredSpace;
            db.Entry(_EditionCompanySectionAdvers).State = EntityState.Modified;
            db.SaveChanges();

            var _Aplication = (from _ap in plm.Applications
                               where _ap.Description == "Sitio DEACI"
                               select _ap).ToList();
            foreach (var _App in _Aplication)
            {
                var _Tables = (from _tab in plm.Tables
                               where _tab.ApplicationId == _App.ApplicationId
                               && _tab.Description == "Advertisements"
                               select _tab).ToList();
                foreach (var _Tabs in _Tables)
                {
                    var _ApplicationId = _App.ApplicationId;
                    CountriesUsers p = (CountriesUsers)Session["CountriesUsers"];
                    ActivityLogs _ActivityLogs = new ActivityLogs();
                    int _OperationId = 2; // Modificar
                    _ActivityLogs.UserId = p.userId;
                    _ActivityLogs.TableId = _Tabs.TableId; // Advertisements
                    _ActivityLogs.OperationId = _OperationId;
                    _ActivityLogs.Date = DateTime.Now;
                    _ActivityLogs.PrimaryKeyAffected = ("(SectionId," + _EditionCompanySectionAdvers.SectionId + "),(EditionId," + _EditionCompanySectionAdvers.EditionId + "),(CompanyId," + _EditionCompanySectionAdvers.CompanyId + ")");
                    _ActivityLogs.FieldsAffected = ("(HiredSpace," + _EditionCompanySectionAdvers.HiredSpace + ")");
                    plm.Entry(_ActivityLogs).State = EntityState.Added;
                    plm.SaveChanges();
                }
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        public ActionResult AgregarAnuncioExistente(string EditionId, string CompanyId, string AdvId)
        {
            int _EditionId = int.Parse(EditionId);
            int _CompanyId = int.Parse(CompanyId);
            int _AdvId = int.Parse(AdvId);
            var _ValueClientAdvs = (from _ade in db.AdvertisementEditions
                                    where _ade.CompanyId == _CompanyId && _ade.AdvId == _AdvId
                                    select _ade).ToList();
            if (_ValueClientAdvs.LongCount() > 0)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            AdvertisementEditions _AdvertisementEditions = new AdvertisementEditions();
            _AdvertisementEditions.EditionId = _EditionId;
            _AdvertisementEditions.CompanyId = _CompanyId;
            _AdvertisementEditions.AdvId = _AdvId;
            db.Entry(_AdvertisementEditions).State = EntityState.Added;
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
                    int _OperationId = 1; // Eliminar
                    _ActivityLogs.UserId = p.userId;
                    _ActivityLogs.TableId = _Tabs.TableId; // AdvertisementEditions
                    _ActivityLogs.OperationId = _OperationId;
                    _ActivityLogs.Date = DateTime.Now;
                    _ActivityLogs.PrimaryKeyAffected = ("(AdvId," + _AdvertisementEditions.AdvId + "),(EditionId," + _AdvertisementEditions.EditionId + "),(CompanyId," + _AdvertisementEditions.CompanyId + ")");
                    _ActivityLogs.FieldsAffected = null;
                    plm.Entry(_ActivityLogs).State = EntityState.Added;
                    plm.SaveChanges();
                }
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        public ActionResult AgregarNuevoAnuncioSección(int id, int ed, int ad) 
        {
            ViewData["EditionId"] = id;
            ViewData["CompanyId"] = ed;
            ViewData["SectionId"] = ad;
            return View();
        }
        public ActionResult AsociarAnuncioSección(int id, int ed, int ad)
        {
            var _Editions = (from _ed in db.Editions
                             where _ed.EditionId == id
                             select _ed).ToList();
            ViewData["Edición"] = _Editions.SingleOrDefault().NumberEdition;
            SessionCompanyEditions _SessionCompanyEdition = new SessionCompanyEditions(id, ed, ad);
            Session["SessionCompanyEditions"] = _SessionCompanyEdition;
            return View();
        }
        public ActionResult AgregarAnuncio(string EditionId, string CompanyId, string SectionId, string HiredSpace)
        {
            int _SectionId = int.Parse(SectionId);
            int _EditionId = int.Parse(EditionId);
            int _CompanyId = int.Parse(CompanyId);
            EditionCompanySectionAdvers _EditionCompanySectionAdvers = new EditionCompanySectionAdvers();
            _EditionCompanySectionAdvers.EditionId = _EditionId;
            _EditionCompanySectionAdvers.CompanyId = _CompanyId;
            _EditionCompanySectionAdvers.SectionId = _SectionId;
            _EditionCompanySectionAdvers.HiredSpace = HiredSpace;
            _EditionCompanySectionAdvers.AdverFile = null;
            _EditionCompanySectionAdvers.BaseURL = null;
            db.Entry(_EditionCompanySectionAdvers).State = EntityState.Added;
            db.SaveChanges();
            var _Aplications = (from _ap in plm.Applications
                               where _ap.Description == "Sitio DEACI"
                               select _ap).ToList();
            foreach (var _App in _Aplications)
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
                    int _OperationId = 1; // Agregar
                    _ActivityLogs.UserId = p.userId;
                    _ActivityLogs.TableId = _Tabs.TableId; // AdvertisementEditions
                    _ActivityLogs.OperationId = _OperationId;
                    _ActivityLogs.Date = DateTime.Now;
                    _ActivityLogs.PrimaryKeyAffected = ("(SectionId," + _EditionCompanySectionAdvers.SectionId + "),(EditionId," + _EditionCompanySectionAdvers.EditionId + "),(CompanyId," + _EditionCompanySectionAdvers.CompanyId + ")");
                    _ActivityLogs.FieldsAffected = ("(HiredSpace," + _EditionCompanySectionAdvers.HiredSpace + ")");
                    plm.Entry(_ActivityLogs).State = EntityState.Added;
                    plm.SaveChanges();
                }
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Distribuidores(int id, int ed, int ad, string palabra, string sortOrder, string currentFilter, string searchString, int? page)
        {
            SessionCompanyEditions _SessionCompanyEdition = new SessionCompanyEditions(id, ed, ad);
            Session["SessionCompanyEditions"] = _SessionCompanyEdition;
            SessionEditionId SessionEditionId = new SessionEditionId(id, ed);
            Session["SessionEditionId"] = SessionEditionId;
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
            var _Country = (from _ed in db.Editions
                            join _co in db.Countries
                            on _ed.CountryId equals _co.CountryId
                            where _ed.EditionId == id
                            select _co);
            ViewData["País"] = _Country.SingleOrDefault().CountryName;
            var _CountryBook = (from _ed in db.Editions
                                join _bo in db.Books
                                on _ed.BookId equals _bo.BookId
                                where _ed.EditionId == id
                                select _bo);
            ViewData["Obra"] = _CountryBook.SingleOrDefault().BookName;
            Union_Products u = new Union_Products();
            List<plm_spGetCompaniesGropupBy_Result> _storeprocedure = db.Database.SqlQuery<plm_spGetCompaniesGropupBy_Result>
                ("plm_spGetCompaniesGropupBy").ToList();
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
                        _storeprocedure = _storeprocedure.Where(j => j.CompanyName.StartsWith(_Palabra)).ToList();
                    }
                }
                if (palabra.LongCount() > 3)
                {
                    if (!String.IsNullOrEmpty(palabra))
                    {
                        string _Palabra = palabra.ToUpper();
                        _storeprocedure = _storeprocedure.Where(j => j.CompanyName.Contains(_Palabra)).ToList();
                    }
                }
            }
            var _Companies = (from _c in db.Companies
                              join _ce in db.CompanyEditions
                              on _c.CompanyId equals _ce.CompanyId
                              join _ct in db.CompanyTypes
                              on _ce.CompanyTypeId equals _ct.CompanyTypeId
                              where _c.CompanyId == ed
                              && _ct.CompanyTypeId == ad
                              select new { _c, _ct }).Distinct();
            ViewData["CompanyName"] = _Companies.FirstOrDefault()._c.CompanyName;
            ViewData["CompanyId"] = _Companies.FirstOrDefault()._c.CompanyId;
            ViewData["CompanyShortName"] = _Companies.SingleOrDefault()._c.CompanyShortName;
            ViewData["TipoCliente"] = _Companies.SingleOrDefault()._ct.Description;
            var _Editions = (from _e in db.Editions
                             where _e.EditionId == id
                             select _e).ToList();
            ViewData["Number"] = _Editions.SingleOrDefault().EditionId;
            ViewData["Edición"] = _Editions.SingleOrDefault().NumberEdition;
            int pageSize = 50;
            int pageNumber = (page ?? 1);
            return View(_storeprocedure.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult DistribuidorParticipante(string EditionId, string CompanyId, string DistributionId)
        {
            int _EditionId = int.Parse(EditionId);
            int _CompanyId = int.Parse(CompanyId);
            int _DistributionId = int.Parse(DistributionId);

            var _CompanyEditionsValueParticipant = (from _ce in db.CompanyEditions
                                         where _ce.EditionId == _EditionId
                                         && _ce.CompanyId == _CompanyId
                                         select _ce).ToList();
            if (_CompanyEditionsValueParticipant.LongCount() == 0)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }

            var _CompanyEditionsValue = (from _ce in db.CompanyEditions
                                         where _ce.CompanyId == _DistributionId
                                         && _ce.EditionId == _EditionId
                                         select _ce).ToList();
            if (_CompanyEditionsValue.LongCount() == 0)
            {
                var _CompanyTypes = (from _ct in db.CompanyTypes
                                     where _ct.Description == "DISTRIBUIDOR"
                                     select _ct).ToList();
                CompanyEditions _CompanyEditions = new CompanyEditions();
                _CompanyEditions.CompanyId = _DistributionId;
                _CompanyEditions.EditionId = _EditionId;
                _CompanyEditions.HtmlFile = null;
                _CompanyEditions.HtmlContent = null;
                _CompanyEditions.CompanyTypeId = _CompanyTypes.SingleOrDefault().CompanyTypeId;
                _CompanyEditions.Page = null;
                _CompanyEditions.CloseClient = true;
                db.Entry(_CompanyEditions).State = EntityState.Added;
                db.SaveChanges();
                var _ValueCompanyDistributions = (from _cb in db.CompanyDistributions
                                                  where _cb.CompanyId == _CompanyId
                                                  && _cb.DistributionId == _DistributionId
                                                  select _cb).ToList();
                if (_ValueCompanyDistributions.LongCount() == 0)
                {
                    CompanyDistributions _CompanyDistributions = new CompanyDistributions();
                    _CompanyDistributions.CompanyId = _CompanyId;
                    _CompanyDistributions.DistributionId = _DistributionId;
                    db.Entry(_CompanyDistributions).State = EntityState.Added;
                    db.SaveChanges();
                    EditionCompanyDistributions _EditionCompanyDistributions = new EditionCompanyDistributions();
                    _EditionCompanyDistributions.CompanyId = _CompanyId;
                    _EditionCompanyDistributions.EditionId = _EditionId;
                    _EditionCompanyDistributions.DistributionId = _DistributionId;
                    db.Entry(_EditionCompanyDistributions).State = EntityState.Added;
                    db.SaveChanges();
                }
                else
                {
                    EditionCompanyDistributions _EditionCompanyDistributions = new EditionCompanyDistributions();
                    _EditionCompanyDistributions.CompanyId = _CompanyId;
                    _EditionCompanyDistributions.EditionId = _EditionId;
                    _EditionCompanyDistributions.DistributionId = _DistributionId;
                    db.Entry(_EditionCompanyDistributions).State = EntityState.Added;
                    db.SaveChanges();
                }
            }
            else
            {
                var _ValueCompanyDistributions = (from _cb in db.CompanyDistributions
                                                  where _cb.CompanyId == _CompanyId
                                                  && _cb.DistributionId == _DistributionId
                                                  select _cb).ToList();
                if (_ValueCompanyDistributions.LongCount() == 0)
                {
                    CompanyDistributions _CompanyDistributions = new CompanyDistributions();
                    _CompanyDistributions.CompanyId = _CompanyId;
                    _CompanyDistributions.DistributionId = _DistributionId;
                    db.Entry(_CompanyDistributions).State = EntityState.Added;
                    db.SaveChanges();
                    EditionCompanyDistributions _EditionCompanyDistributions = new EditionCompanyDistributions();
                    _EditionCompanyDistributions.CompanyId = _CompanyId;
                    _EditionCompanyDistributions.EditionId = _EditionId;
                    _EditionCompanyDistributions.DistributionId = _DistributionId;
                    db.Entry(_EditionCompanyDistributions).State = EntityState.Added;
                    db.SaveChanges();
                }
                else
                {
                    EditionCompanyDistributions _EditionCompanyDistributions = new EditionCompanyDistributions();
                    _EditionCompanyDistributions.CompanyId = _CompanyId;
                    _EditionCompanyDistributions.EditionId = _EditionId;
                    _EditionCompanyDistributions.DistributionId = _DistributionId;
                    db.Entry(_EditionCompanyDistributions).State = EntityState.Added;
                    db.SaveChanges();
                }
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        public ActionResult EliminarDistribuidorParticipante(string EditionId, string CompanyId, string DistributionId)
        {
            int _EditionId = int.Parse(EditionId);
            int _CompanyId = int.Parse(CompanyId);
            int _DistributionId = int.Parse(DistributionId);
            EditionCompanyDistributions _EditionCompanyDistributions = new EditionCompanyDistributions();
            _EditionCompanyDistributions.CompanyId = _CompanyId;
            _EditionCompanyDistributions.EditionId = _EditionId;
            _EditionCompanyDistributions.DistributionId = _DistributionId;
            db.Entry(_EditionCompanyDistributions).State = EntityState.Deleted;
            db.SaveChanges();
            CompanyDistributions _CompanyDistributions = new CompanyDistributions();
            _CompanyDistributions.CompanyId = _CompanyId;
            _CompanyDistributions.DistributionId = _DistributionId;
            db.Entry(_CompanyDistributions).State = EntityState.Deleted;
            db.SaveChanges();
            var _CompanyBrandEditonsValue = (from _cbe in db.CompanyBrandEditions
                                             where _cbe.CompanyId == _DistributionId
                                             && _cbe.EditionId == _EditionId
                                             select _cbe).ToList();
            if (_CompanyBrandEditonsValue.LongCount() != 0)
            {
                return Json(true, JsonRequestBehavior.AllowGet); 
            }
            var _AdversEditionSectionsValue = (from _ad in db.EditionCompanySectionAdvers
                                          where _ad.CompanyId == _DistributionId
                                          && _ad.EditionId == _EditionId
                                          select _ad).ToList();
            if (_AdversEditionSectionsValue.LongCount() != 0)
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            var _ProductEditionsValue = (from _p in db.Products
                                    join _pe in db.ProductEditions
                                        on _p.ProductId equals _pe.ProductId
                                    where _pe.EditionId == _EditionId
                                    && _p.CompanyId == _DistributionId
                                    select _p).ToList();
            if (_ProductEditionsValue.LongCount() != 0)
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            CompanyEditions _CompanyEditions = new CompanyEditions();
            _CompanyEditions.CompanyId = _DistributionId;
            _CompanyEditions.EditionId = _EditionId;
            db.Entry(_CompanyEditions).State = EntityState.Deleted;
            db.SaveChanges();
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
            var _Edition = (from _ed in db.Editions
                            join _ce in db.CompanyEditions
                            on _ed.EditionId equals _ce.EditionId
                            join _c in db.Companies
                            on _ce.CompanyId equals _c.CompanyId
                            where _ed.EditionId == id
                            && _ce.EditionId == id
                            orderby _ce.CompanyTypes.Description, _c.CompanyName
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
                            orderby _ce.CompanyTypes.Description, _c.CompanyName
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
                       _ProductEditions.Where(j => j.Products.ProductName.StartsWith(palabra));
                    }
                }
                if (palabra.LongCount() > 3)
                {
                    if (!String.IsNullOrEmpty(palabra))
                    {
                       _ProductEditions.Where(j => j.Products.ProductName.Contains(palabra));
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
            List<plm_spGetProductsBySectionsByEditionS_Result> _storeprocedure = db.Database.SqlQuery<plm_spGetProductsBySectionsByEditionS_Result>
                ("plm_spGetProductsBySectionsByEditionS @EditionId=" + id + "").ToList();
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
                ("plm_spGetProductsBySectionsByEditionS @EditionId=" + id + "");
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
        public ActionResult ReporteCliente(int id, int ed, string palabra, string sortOrder, string currentFilter, string searchString, int? page)
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
                            && _c.CompanyId == ed
                            orderby _ce.CompanyTypes.Description
                            select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Companies = _c, CompanyEditions = _ce, Editions = _ed });
            var _CompanyEditions = (from _ce in db.CompanyEditions
                                    where _ce.EditionId == id
                                    && _ce.CompanyId == ed
                                    select _ce).ToList();
            if (_CompanyEditions.LongCount() == 0)
            {
                ViewData["CompanyNull"] = "";
            }
            var _Count = _CompanyEditions.LongCount();
            ViewData["CountClients"] = _Count;
            var _Products = (from _pe in db.ProductEditions
                             join _p in db.Products
                             on _pe.ProductId equals _p.ProductId
                             where _pe.EditionId == id
                             && _p.CompanyId == ed
                             select _pe).ToList();
            var _CountProducts = _Products.LongCount();
            ViewData["CountProducts"] = _CountProducts;
            var _Brands = (from _cbe in db.CompanyBrandEditions
                           where _cbe.EditionId == id
                           && _cbe.CompanyId == ed
                           select _cbe).ToList();
            var _CountBrands = _Brands.LongCount();
            ViewData["CountBrands"] = _CountBrands;
            var _Advers = (from _ade in db.AdvertisementEditions
                           where _ade.EditionId == id
                           && _ade.CompanyId == ed
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
        public ActionResult ReporteClientePDF(int id, int ed)
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
                            && _c.CompanyId == ed
                            orderby _ce.CompanyTypes.Description
                            select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Companies = _c, CompanyEditions = _ce, Editions = _ed });
            var _CompanyEditions = (from _ce in db.CompanyEditions
                                    where _ce.EditionId == id
                                    && _ce.CompanyId == ed
                                    select _ce).ToList();
            if (_CompanyEditions.LongCount() == 0)
            {
                ViewData["CompanyNull"] = "";
            }
            var _Count = _CompanyEditions.LongCount();
            ViewData["CountClients"] = _Count;
            var _Products = (from _pe in db.ProductEditions
                             join _p in db.Products
                             on _pe.ProductId equals _p.ProductId
                             where _pe.EditionId == id
                             && _p.CompanyId == ed
                             select _pe).ToList();
            var _CountProducts = _Products.LongCount();
            ViewData["CountProducts"] = _CountProducts;
            var _Brands = (from _cbe in db.CompanyBrandEditions
                           where _cbe.EditionId == id
                           && _cbe.CompanyId == ed
                           select _cbe).ToList();
            var _CountBrands = _Brands.LongCount();
            ViewData["CountBrands"] = _CountBrands;
            var _Advers = (from _ade in db.AdvertisementEditions
                           where _ade.EditionId == id
                           && _ade.CompanyId == ed
                           select _ade).ToList();
            var _CountAdver = _Advers.LongCount();
            ViewData["CountAdvers"] = _CountAdver;
            return View(_Edition);
        }
        public ActionResult ReporteProductosCliente(int id, int ed, string palabra, string sortOrder, string currentFilter, string searchString, int? page)
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
            var _ProductEditions = (from _c in db.Companies
                                    join _p in db.Products
                                    on _c.CompanyId equals _p.CompanyId
                                    join _pt in db.ProductTypes
                                    on _p.ProductTypeId equals _pt.ProductTypeId
                                    join _pe in db.ProductEditions
                                    on _p.ProductId equals _pe.ProductId
                                    where _pe.EditionId == id
                                    && _c.CompanyId == ed
                                    orderby _pt.Description == "PRODUCTO (TEXTO EDITORIAL)", _pt.Description == "(PRODUCTO) en TERCERA", _pt.Description == "AGREGADO", _c.CompanyName, _p.ProductName
                                    select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Companies = _c, Products = _p, ProductEditions = _pe, ProductTypes = _pt });
            var _ProductEditionsCount = (from _p in db.Products
                                         join _pe in db.ProductEditions
                                         on _p.ProductId equals _pe.ProductId
                                         where _pe.EditionId == id
                                         && _p.CompanyId == ed
                                         select _p).ToList();
            var _Count = _ProductEditionsCount.LongCount();
            ViewData["CountProductsCompany"] = _Count;
            var _ValueParticipantClient = (from _ce in db.CompanyEditions
                                           where _ce.CompanyId == ed
                                           && _ce.EditionId == id
                                           select _ce).ToList();
            if (_ValueParticipantClient.LongCount() == 0)
            {
                ViewData["CompanyNull"] = "";
            }
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
                               && _p.CompanyId == ed
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
                               && _p.CompanyId == ed
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
                               && _p.CompanyId == ed
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
                                && _p.CompanyId == ed
                                select _p).ToList();
            var _CountProductsTED = _ProductsTED.LongCount();
            ViewData["EDITORIAL"] = _CountProductsTED;
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
        public ActionResult ReporteProductosPDF(int id, int ed)
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
            var _ProductEditions = (from _c in db.Companies
                                    join _p in db.Products
                                    on _c.CompanyId equals _p.CompanyId
                                    join _pt in db.ProductTypes
                                    on _p.ProductTypeId equals _pt.ProductTypeId
                                    join _pe in db.ProductEditions
                                    on _p.ProductId equals _pe.ProductId
                                    where _pe.EditionId == id
                                    && _c.CompanyId == ed
                                    orderby _pt.Description == "PRODUCTO (TEXTO EDITORIAL)", _pt.Description == "(PRODUCTO) en TERCERA", _pt.Description == "AGREGADO", _c.CompanyName, _p.ProductName
                                    select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Companies = _c, Products = _p, ProductEditions = _pe, ProductTypes = _pt });
            var _ProductEditionsCount = (from _p in db.Products
                                         join _pe in db.ProductEditions
                                         on _p.ProductId equals _pe.ProductId
                                         where _pe.EditionId == id
                                         && _p.CompanyId == ed
                                         select _p).ToList();
            var _Count = _ProductEditionsCount.LongCount();
            ViewData["CountProductsCompany"] = _Count;
            var _ValueParticipantClient = (from _ce in db.CompanyEditions
                                           where _ce.CompanyId == ed
                                           && _ce.EditionId == id
                                           select _ce).ToList();
            if (_ValueParticipantClient.LongCount() == 0)
            {
                ViewData["CompanyNull"] = "";
            }
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
                               && _p.CompanyId == ed
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
                               && _p.CompanyId == ed
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
                               && _p.CompanyId == ed
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
                                && _p.CompanyId == ed
                                select _p).ToList();
            var _CountProductsTED = _ProductsTED.LongCount();
            ViewData["EDITORIAL"] = _CountProductsTED;
            var _CompanyName = (from _c in db.Companies
                                where _c.CompanyId == ed
                                select _c).ToList();
            ViewData["CompanyName"] = _CompanyName.SingleOrDefault().CompanyName;
            return View(_ProductEditions);
        }
        public ActionResult ReporteProductosSecciónCliente(int id, int ed, string palabra, string sortOrder, string currentFilter, string searchString, int? page)
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
            List<plm_spGetProductsBySectionsByCompaniesByEditionS_Result> _storeprocedure = db.Database.SqlQuery<plm_spGetProductsBySectionsByCompaniesByEditionS_Result>
                ("plm_spGetProductsBySectionsByCompaniesByEditionS @EditionId=" + id + ", @CompanyId=" + ed + "").ToList();
            var _CountProducts = _storeprocedure.LongCount();
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
                               && _p.CompanyId == ed
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
                               && _p.CompanyId == ed
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
                               && _p.CompanyId == ed
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
                                && _p.CompanyId == ed
                                select _p).ToList();
            var _CountProductsTED = _ProductsTED.LongCount();
            ViewData["EDITORIAL"] = _CountProductsTED;
            if (_storeprocedure.LongCount() == 0)
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
            var _CompanyName = (from _c in db.Companies
                                where _c.CompanyId == ed
                                select _c).ToList();
            ViewData["CompanyName"] = _CompanyName.SingleOrDefault().CompanyName;
            ViewBag.CurrentFilter = palabra;
            int pageSize = 100;
            int pageNumber = (page ?? 1);
            return View(_storeprocedure.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult ReporteProductosSecciónClientePDF(int id, int ed)
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

            var _storeprocedure = db.Database.SqlQuery<plm_spGetProductsBySectionsByCompaniesByEditionS_Result>
                ("plm_spGetProductsBySectionsByCompaniesByEditionS @EditionId=" + id + ", @CompanyId=" + ed + "").ToList();
            var _CountProducts = _storeprocedure.LongCount();
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
                               && _p.CompanyId == ed
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
                               && _p.CompanyId == ed
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
                               && _p.CompanyId == ed
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
                                && _p.CompanyId == ed
                                select _p).ToList();
            var _CountProductsTED = _ProductsTED.LongCount();
            ViewData["EDITORIAL"] = _CountProductsTED;
            if (_storeprocedure.LongCount() == 0)
            {
                ViewData["CompanyNull"] = "";
            }
            ViewData["CountProductEditions"] = _CountProducts;
            var _CompanyName = (from _c in db.Companies
                                where _c.CompanyId == ed
                                select _c).ToList();
            ViewData["CompanyName"] = _CompanyName.SingleOrDefault().CompanyName;
            return View(_storeprocedure);
        }
        public ActionResult ReporteProductosÍndiceCliente(int id, int ed, string palabra, string sortOrder, string currentFilter, string searchString, int? page)
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
            var _ProductEditions = (from _c in db.Companies
                                    join _p in db.Products
                                    on _c.CompanyId equals _p.CompanyId
                                    join _pt in db.ProductTypes
                                    on _p.ProductTypeId equals _pt.ProductTypeId
                                    join _pe in db.ProductEditions
                                    on _p.ProductId equals _pe.ProductId
                                    where _pe.EditionId == id
                                    && _c.CompanyId == ed
                                    orderby _pt.Description == "PRODUCTO (TEXTO EDITORIAL)", _pt.Description == "(PRODUCTO) en TERCERA", _pt.Description == "AGREGADO", _c.CompanyName, _p.ProductName
                                    select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Companies = _c, Products = _p, ProductEditions = _pe, ProductTypes = _pt });
            var _CountProducts = _ProductEditions.LongCount();
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
                               && _p.CompanyId == ed
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
                               && _p.CompanyId == ed
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
                               && _p.CompanyId == ed
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
                                && _p.CompanyId == ed
                                select _p).ToList();
            var _CountProductsTED = _ProductsTED.LongCount();
            ViewData["EDITORIAL"] = _CountProductsTED;
            if (_ProductEditions.LongCount() == 0)
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
            var _CompanyName = (from _c in db.Companies
                                where _c.CompanyId == ed
                                select _c).ToList();
            ViewData["CompanyName"] = _CompanyName.SingleOrDefault().CompanyName;
            ViewBag.CurrentFilter = palabra;
            int pageSize = 100;
            int pageNumber = (page ?? 1);
            return View(_ProductEditions.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult ReporteProductosÍndiceClientePDF(int id, int ed)
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

            var _ProductEditions = (from _c in db.Companies
                                    join _p in db.Products
                                    on _c.CompanyId equals _p.CompanyId
                                    join _pt in db.ProductTypes
                                    on _p.ProductTypeId equals _pt.ProductTypeId
                                    join _pe in db.ProductEditions
                                    on _p.ProductId equals _pe.ProductId
                                    where _pe.EditionId == id
                                    && _c.CompanyId == ed
                                    orderby _pt.Description == "PRODUCTO (TEXTO EDITORIAL)", _pt.Description == "(PRODUCTO) en TERCERA", _pt.Description == "AGREGADO", _c.CompanyName, _p.ProductName
                                    select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Companies = _c, Products = _p, ProductEditions = _pe, ProductTypes = _pt });
            var _CountProducts = _ProductEditions.LongCount();
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
                               && _p.CompanyId == ed
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
                               && _p.CompanyId == ed
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
                               && _p.CompanyId == ed
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
                                && _p.CompanyId == ed
                                select _p).ToList();
            var _CountProductsTED = _ProductsTED.LongCount();
            ViewData["EDITORIAL"] = _CountProductsTED;
            if (_ProductEditions.LongCount() == 0)
            {
                ViewData["CompanyNull"] = "";
            }
            ViewData["CountProductEditions"] = _CountProducts;
            var _CompanyName = (from _c in db.Companies
                                where _c.CompanyId == ed
                                select _c).ToList();
            ViewData["CompanyName"] = _CompanyName.SingleOrDefault().CompanyName;
            return View(_ProductEditions);
        }
        public ActionResult ReporteMarcasCliente(int id, int ed, string palabra, string sortOrder, string currentFilter, string searchString, int? page)
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
            var _CompanyBrandsEditions = (from _c in db.Companies
                                          join _cbe in db.CompanyBrandEditions
                                              on _c.CompanyId equals _cbe.CompanyId
                                          join _b in db.Brands
                                              on _cbe.BrandId equals _b.BrandId
                                          where _c.CompanyId == ed
                                          && _cbe.EditionId == id
                                          orderby _b.BrandName
                                          select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Companies = _c, CompanyBrandEditions = _cbe, Brands = _b });
            var _CompanyBrandEditionsCount = (from _cbe in db.CompanyBrandEditions
                                              where _cbe.CompanyId == ed
                                              && _cbe.EditionId == id
                                              select _cbe).ToList();
            var _Count = _CompanyBrandEditionsCount.LongCount();
            var _ValueClienParticipant = (from _ce in db.CompanyEditions
                                          where _ce.CompanyId == ed
                                          && _ce.EditionId == id
                                          select _ce).ToList();
            if (_ValueClienParticipant.LongCount() == 0)
            {
                ViewData["CompanyNull"] = "";
            }
            ViewData["CountBrandsCompany"] = _Count;
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
                        _CompanyBrandsEditions = _CompanyBrandsEditions.Where(j => j.Brands.BrandName.StartsWith(palabra));
                    }
                }
                if (palabra.LongCount() > 3)
                {
                    if (!String.IsNullOrEmpty(palabra))
                    {
                        _CompanyBrandsEditions = _CompanyBrandsEditions.Where(j => j.Brands.BrandName.Contains(palabra));
                    }
                }
            }
            ViewBag.CurrentFilter = palabra;
            int pageSize = 100;
            int pageNumber = (page ?? 1);
            return View(_CompanyBrandsEditions.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult ReporteMarcasPDF(int id, int ed)
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
            var _CompanyBrandsEditions = (from _c in db.Companies
                                          join _cbe in db.CompanyBrandEditions
                                              on _c.CompanyId equals _cbe.CompanyId
                                          join _b in db.Brands
                                              on _cbe.BrandId equals _b.BrandId
                                          where _c.CompanyId == ed
                                          && _cbe.EditionId == id
                                          orderby _b.BrandName
                                          select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Companies = _c, CompanyBrandEditions = _cbe, Brands = _b });
            var _CompanyBrandEditionsCount = (from _cbe in db.CompanyBrandEditions
                                              where _cbe.CompanyId == ed
                                              && _cbe.EditionId == id
                                              select _cbe).ToList();
            var _Count = _CompanyBrandEditionsCount.LongCount();
            var _ValueClienParticipant = (from _ce in db.CompanyEditions
                                          where _ce.CompanyId == ed
                                          && _ce.EditionId == id
                                          select _ce).ToList();
            if (_ValueClienParticipant.LongCount() == 0)
            {
                ViewData["CompanyNull"] = "";
            }
            ViewData["CountBrandsCompany"] = _Count;
            var _CompanyName = (from _c in db.Companies
                                where _c.CompanyId == ed
                                select _c).ToList();
            ViewData["CompanyName"] = _CompanyName.SingleOrDefault().CompanyName;
            return View(_CompanyBrandsEditions);
        }
        public ActionResult ReporteAnunciosCliente(int id, int ed, string palabra, string sortOrder, string currentFilter, string searchString, int? page)
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
            var _AdversEditionCompany = (from _c in db.Companies
                                         join _ade in db.EditionCompanySectionAdvers
                                         on _c.CompanyId equals _ade.CompanyId
                                         where _c.CompanyId == ed
                                         && _ade.EditionId == id
                                         orderby _ade.AdverFile
                                         select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Companies = _c, EditionCompanySectionAdvers = _ade});
            var _AdversEditionCompanyCount = (from _ade in db.EditionCompanySectionAdvers
                                              where _ade.CompanyId == ed
                                              && _ade.EditionId == id
                                              select _ade).ToList();
            var _Count = _AdversEditionCompanyCount.LongCount();
            var _ValueClientParticipant = (from _ce in db.CompanyEditions
                                           where _ce.CompanyId == ed
                                           && _ce.EditionId == id
                                           select _ce).ToList();
            if (_ValueClientParticipant.LongCount() == 0)
            {
                ViewData["CompanyNull"] = "";
            }
            ViewData["CountAdversCompany"] = _Count;
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
                        _AdversEditionCompany = _AdversEditionCompany.Where(j => j.Advertisements.AdvFile.StartsWith(palabra));
                    }
                }
                if (palabra.LongCount() > 3)
                {
                    if (!String.IsNullOrEmpty(palabra))
                    {
                        _AdversEditionCompany = _AdversEditionCompany.Where(j => j.Advertisements.AdvFile.Contains(palabra));
                    }
                }
            }
            ViewBag.CurrentFilter = palabra;
            int pageSize = 100;
            int pageNumber = (page ?? 1);
            return View(_AdversEditionCompany.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult ReporteAnunciosClientePDF(int id, int ed)
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
            var _AdversEditionCompany = (from _c in db.Companies
                                         join _ade in db.EditionCompanySectionAdvers
                                         on _c.CompanyId equals _ade.CompanyId
                                         where _c.CompanyId == ed
                                         && _ade.EditionId == id
                                         orderby _ade.AdverFile
                                         select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Companies = _c, EditionCompanySectionAdvers = _ade });
            var _AdversEditionCompanyCount = (from _ade in db.EditionCompanySectionAdvers
                                              where _ade.CompanyId == ed
                                              && _ade.EditionId == id
                                              select _ade).ToList();
            var _Count = _AdversEditionCompanyCount.LongCount();
            var _ValueClientParticipant = (from _ce in db.CompanyEditions
                                           where _ce.CompanyId == ed
                                           && _ce.EditionId == id
                                           select _ce).ToList();
            if (_ValueClientParticipant.LongCount() == 0)
            {
                ViewData["CompanyNull"] = "";
            }
            ViewData["CountAdversCompany"] = _Count;
            var _CompanyName = (from _c in db.Companies
                                where _c.CompanyId == ed
                                select _c).ToList();
            ViewData["CompanyName"] = _CompanyName.SingleOrDefault().CompanyName;
            return View(_AdversEditionCompany);
        }
        public ActionResult ReporteSucursalesPDF(int id, int ed)
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
            var _Edition = (from _e in db.Editions
                            where _e.EditionId == id
                            select _e);
            ViewData["Edición"] = _Edition.SingleOrDefault().NumberEdition;
            var _EditionCompanies = (from _c in db.Companies
                                     join _ce in db.CompanyEditions
                                     on _c.CompanyId equals _ce.CompanyId
                                     join _ct in db.CompanyTypes
                                     on _ce.CompanyTypeId equals _ct.CompanyTypeId
                                     where _ct.CompanyTypeId == ed
                                     && _ce.EditionId == id
                                     orderby _c.CompanyName
                                     select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Companies = _c, CompanyTypes = _ct }).Distinct().OrderBy(model => model.Companies.CompanyName).AsQueryable();
            var _CompanyTypes = (from _ct in db.CompanyTypes
                                 where _ct.CompanyTypeId == ed
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
        public ActionResult AperturaDatos(int EditionId, int CompanyId, int UserId)
        {
            SendEmail _SendEmail = new SendEmail();
            var _Editions = (from _e in db.Editions
                             where _e.EditionId == EditionId
                             select _e).ToList();
            var _Companies = (from _c in db.Companies
                              where _c.CompanyId == CompanyId
                              select _c).ToList();
            var _UserName = (from _us in plm.Users
                             where _us.UserId == UserId
                             select _us).ToList();
            var _CompanyEditions = (from _ce in db.CompanyEditions
                                    where _ce.EditionId == EditionId
                                    && _ce.CompanyId == CompanyId
                                    select _ce).ToList();
            foreach (var _CompanyEditionsModify in _CompanyEditions)
            {
                _CompanyEditionsModify.CompanyId = _CompanyEditionsModify.CompanyId;
                _CompanyEditionsModify.EditionId = _CompanyEditionsModify.EditionId;
                _CompanyEditionsModify.HtmlContent = _CompanyEditionsModify.HtmlContent;
                _CompanyEditionsModify.HtmlContent = _CompanyEditionsModify.HtmlContent;
                _CompanyEditionsModify.CompanyTypeId = _CompanyEditionsModify.CompanyTypeId;
                _CompanyEditionsModify.Page = _CompanyEditionsModify.Page;
                _CompanyEditionsModify.CloseClient = true;
                db.Entry(_CompanyEditionsModify).State = EntityState.Modified;
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
                        int _OperationId = 2; // Modificar
                        _ActivityLogs.UserId = p.userId;
                        _ActivityLogs.TableId = _Tabs.TableId; // CompanyEditions
                        _ActivityLogs.OperationId = _OperationId;
                        _ActivityLogs.Date = DateTime.Now;
                        _ActivityLogs.PrimaryKeyAffected = ("(CompanyId," + _CompanyEditionsModify.CompanyId + "),(EditionId," + _CompanyEditionsModify.EditionId + ")");
                        _ActivityLogs.FieldsAffected = ("(Active," + _CompanyEditionsModify.CloseClient + ")");
                        plm.Entry(_ActivityLogs).State = EntityState.Added;
                        plm.SaveChanges();
                    }
                }
            }
            GetValues.CId = CompanyId;
            GetValues.EId = EditionId;
            GetValues.UId = UserId;
            OpenEmails();
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        public void OpenEmailMessagge(object t)
        {
            int? eid = GetValues.EId;
            int? cid = GetValues.CId;
            int? uid = GetValues.UId;

            DEACI_20150917Entities db = new DEACI_20150917Entities();
            PLMUsers plm = new PLMUsers();
            var _Editions = (from _e in db.Editions
                             where _e.EditionId == eid
                             select _e).ToList();
            var _Companies = (from _c in db.Companies
                              where _c.CompanyId == cid
                              select _c).ToList();
            var _UserName = (from _us in plm.Users
                             where _us.UserId == uid
                             select _us).ToList();
            System.Net.Mail.MailMessage mmsg = new System.Net.Mail.MailMessage();
            foreach (var a in _UserName)
            {
            mmsg.To.Add(a.Email);
            }
            mmsg.To.Add("carlos.carrillo@plmlatina.com");
            mmsg.Subject = "Sistema DEACI. Apertura de ingreso de datos del cliente: " + _Companies.SingleOrDefault().CompanyName + " de la edición " + _Editions.SingleOrDefault().NumberEdition;
            mmsg.SubjectEncoding = System.Text.Encoding.UTF8;
            mmsg.CC.Add("erick.morales@plmlatina.com");
            mmsg.CC.Add("beatriz.vazquez@plmlatina.com");
            mmsg.CC.Add("juan.betancourt@plmlatina.com");
            mmsg.CC.Add("angel.soto@plmlatina.com");
            mmsg.CC.Add("monica.medina@plmlatina.com");
            var _MessageBody = "<p>Buen día,</p>" + "<p>El motivo de este correo, es para hacer de su conocimiento que el usuario: "
                + "<strong>" + _UserName.SingleOrDefault().Name + " " + _UserName.SingleOrDefault().LastName + "</strong>"
                + " abrio el cliente: " + "<strong>" + _Companies.SingleOrDefault().CompanyName + "</strong>" + " que participa en la edición: "
                + "<strong>" + _Editions.SingleOrDefault().ISBN + "</strong>"
                + "</p>" + "<p>La apertura de este cliente implica que se pueden realizar modificaciones.</p>"
                + "<p>La apertura se realizo el: " + DateTime.Now + "</p>" + "<p>Saludos.</p>" + "<p>Sistema DEACI.</p>";
            mmsg.Body = _MessageBody;
            mmsg.BodyEncoding = System.Text.Encoding.UTF8;
            mmsg.IsBodyHtml = true;
            mmsg.From = new System.Net.Mail.MailAddress("SistemaDEACI@plmlatina.com");
            System.Net.Mail.SmtpClient cliente = new System.Net.Mail.SmtpClient();
            cliente.Credentials = new System.Net.NetworkCredential("SistemaDEACI@plmlatina.com", "SistemaDEACI");
            cliente.Host = "plmlatina.com";
            try
            {
                cliente.Send(mmsg);
            }
            catch (System.Net.Mail.SmtpException ex)
            {
                string msg = ex.InnerException.Message;
            }
        }
        public void OpenEmails()
        {
            System.Threading.Thread t = new System.Threading.Thread(new System.Threading.ParameterizedThreadStart(OpenEmailMessagge));
            t.Start(1);
        }
        public ActionResult CierreDatos(int EditionId, int CompanyId, int UserId)
        {
            var _Editions = (from _e in db.Editions
                             where _e.EditionId == EditionId
                             select _e).ToList();
            var _Companies = (from _c in db.Companies
                              where _c.CompanyId == CompanyId
                              select _c).ToList();
            var _UserName = (from _us in plm.Users
                             where _us.UserId == UserId
                             select _us).ToList();
            var _CompanyEditions = (from _ce in db.CompanyEditions
                                    where _ce.EditionId == EditionId
                                    && _ce.CompanyId == CompanyId
                                    select _ce).ToList();
            foreach (var _CompanyEditionsModify in _CompanyEditions)
            {
                _CompanyEditionsModify.CompanyId = _CompanyEditionsModify.CompanyId;
                _CompanyEditionsModify.EditionId = _CompanyEditionsModify.EditionId;
                _CompanyEditionsModify.HtmlContent = _CompanyEditionsModify.HtmlContent;
                _CompanyEditionsModify.HtmlContent = _CompanyEditionsModify.HtmlContent;
                _CompanyEditionsModify.CompanyTypeId = _CompanyEditionsModify.CompanyTypeId;
                _CompanyEditionsModify.Page = _CompanyEditionsModify.Page;
                _CompanyEditionsModify.CloseClient = false;
                db.Entry(_CompanyEditionsModify).State = EntityState.Modified;
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
                        int _OperationId = 2; // Modificar
                        _ActivityLogs.UserId = p.userId;
                        _ActivityLogs.TableId = _Tabs.TableId; // CompanyEditions
                        _ActivityLogs.OperationId = _OperationId;
                        _ActivityLogs.Date = DateTime.Now;
                        _ActivityLogs.PrimaryKeyAffected = ("(CompanyId," + _CompanyEditionsModify.CompanyId + "),(EditionId," + _CompanyEditionsModify.EditionId + ")");
                        _ActivityLogs.FieldsAffected = ("(Active," + _CompanyEditionsModify.CloseClient + ")");
                        plm.Entry(_ActivityLogs).State = EntityState.Added;
                        plm.SaveChanges();
                    }
                }
            }
            GetValues.CId = CompanyId;
            GetValues.EId = EditionId;
            GetValues.UId = UserId;
            CloseEmails();
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        public void CloseEmails()
        {
            System.Threading.Thread t = new System.Threading.Thread(new System.Threading.ParameterizedThreadStart(CloseEmailMessagge));
            t.Start(1);
        }
        public void CloseEmailMessagge(object r)
        {
            int? eid = GetValues.EId;
            int? cid = GetValues.CId;
            int? uid = GetValues.UId;

            var _Editions = (from _e in db.Editions
                             where _e.EditionId == eid
                             select _e).ToList();
            var _Companies = (from _c in db.Companies
                              where _c.CompanyId == cid
                              select _c).ToList();
            var _UserName = (from _us in plm.Users
                             where _us.UserId == uid
                             select _us).ToList();
            var _CompanyEditions = (from _ce in db.CompanyEditions
                                    where _ce.EditionId == eid
                                    && _ce.CompanyId == cid
                                    select _ce).ToList();
            System.Net.Mail.MailMessage mmsg = new System.Net.Mail.MailMessage();
            var _UserNamecc = (from _us in plm.Users
                               where _us.UserId == uid
                               select _us).ToList();
            foreach (var a in _UserName)
            {
                mmsg.To.Add(a.Email);
            }
            mmsg.To.Add("carlos.carrillo@plmlatina.com");
            mmsg.Subject = "Sistema DEACI. Cierre de ingreso de datos del cliente: " + _Companies.SingleOrDefault().CompanyName + " de la edición " + _Editions.SingleOrDefault().NumberEdition;
            mmsg.SubjectEncoding = System.Text.Encoding.UTF8;
            mmsg.CC.Add("erick.morales@plmlatina.com");
            mmsg.CC.Add("beatriz.vazquez@plmlatina.com");
            mmsg.CC.Add("juan.betancourt@plmlatina.com");
            mmsg.CC.Add("angel.soto@plmlatina.com");
            mmsg.CC.Add("monica.medina@plmlatina.com");
            var _MessageBody = "<p>Buen día,</p>" + "<p>El motivo de este correo, es para hacer de su conocimiento que el usuario: "
                + "<strong>" + _UserName.SingleOrDefault().Name + " " + _UserName.SingleOrDefault().LastName + "</strong>"
                + " cerró el cliente: " + "<strong>" + _Companies.SingleOrDefault().CompanyName + "</strong>" + " que participa en la edición: "
                + "<strong>" + _Editions.SingleOrDefault().ISBN + "</strong>" + "</p>"
                + "<p>El cierre de este cliente implica que ya no se pueden realizar modificaciones.</p>"
                + "<p>El cierre se realizó el: " + DateTime.Now + "</p>" + "<p>Saludos.</p>" + "<p>Sistema DEACI.</p>";
            mmsg.Body = _MessageBody;
            mmsg.BodyEncoding = System.Text.Encoding.UTF8;
            mmsg.IsBodyHtml = true;
            mmsg.From = new System.Net.Mail.MailAddress("SistemaDEACI@plmlatina.com");
            System.Net.Mail.SmtpClient cliente = new System.Net.Mail.SmtpClient();
            cliente.Credentials = new System.Net.NetworkCredential("SistemaDEACI@plmlatina.com", "SistemaDEACI");
            cliente.Host = "plmlatina.com";
            try
            {
                cliente.Send(mmsg);
            }
            catch (System.Net.Mail.SmtpException ex)
            {
                string msg = ex.InnerException.Message;
            }
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
        public ActionResult GenerarReporteClientePDF(int id, int ed)
        {
            return new Rotativa.ActionAsPdf("ReporteClientePDF" + "/" + id + "/" + ed);
        }
        public ActionResult GenerarReporteProductosPDF(int id, int ed)
        {
            return new Rotativa.ActionAsPdf("ReporteProductosPDF" + "/" + id + "/" + ed);
        }
        public ActionResult GenerarReporteProductosSecciónClientePDF(int id, int ed)
        {
            return new Rotativa.ActionAsPdf("ReporteProductosSecciónClientePDF" + "/" + id + "/" + ed);
        }
        public ActionResult GenerarReporteProductosÍndiceClientePDF(int id, int ed)
        {
            return new Rotativa.ActionAsPdf("ReporteProductosÍndiceClientePDF" + "/" + id + "/" + ed);
        }
        public ActionResult GenerarReporteMarcasClientePDF(int id, int ed)
        {
            return new Rotativa.ActionAsPdf("ReporteMarcasPDF" + "/" + id + "/" + ed);
        }
        public ActionResult GenerarReporteAnunciosClientePDF(int id, int ed)
        {
            return new Rotativa.ActionAsPdf("ReporteAnunciosClientePDF" + "/" + id + "/" + ed);
        }
        public ActionResult GenerarReporteSucursales(int id, int ed)
        {
            return new Rotativa.ActionAsPdf("ReporteSucursalesPDF" + "/" + id + "/" + ed);
        }
        public ActionResult HTML(int id, int ed)
        {
            var _CompanyEditions = (from _ce in db.CompanyEditions
                                    where _ce.EditionId == id
                                    && _ce.CompanyId == ed
                                    select _ce).ToList();
            ViewData["Line"] = _CompanyEditions.SingleOrDefault().HtmlContent;
            ViewData["HtmlFile"] = _CompanyEditions.SingleOrDefault().HtmlFile;
            return View();
        }
        public ActionResult HTMLProducts(int id, int ed)
        {
            var _ProductEditions = (from _pe in db.ProductEditions
                                    where _pe.EditionId == id
                                    && _pe.ProductId == ed
                                    select _pe).ToList();
            ViewData["Line"] = _ProductEditions.SingleOrDefault().HtmlContent;
            ViewData["HtmlFile"] = _ProductEditions.SingleOrDefault().HtmlFile;
            return View();
        }
        public ActionResult MismaInformación(int EditionId, int CompanyId, byte CompanyTypeId)
        {
            var _EditionLastParticipant = EditionId - 1;
            var _valueeditionlast = (from _ce in db.CompanyEditions
                                     where _ce.EditionId == _EditionLastParticipant
                                     && _ce.CompanyId == CompanyId
                                     select _ce).ToList();
            if (_valueeditionlast.LongCount() == 0)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            else
            {
                CountriesUsers p = (CountriesUsers)Session["CountriesUsers"];
                GeteditionValues.IdEdition = EditionId;
                GeteditionValues.IdCompany = CompanyId;
                GeteditionValues.IdCompanyType = CompanyTypeId;
                GeteditionValues.IdUser = p.userId;
                InformaciónAnteriorTiempo();
                return Json(true, JsonRequestBehavior.AllowGet);
            }
        }
        public void InformaciónAnteriorTiempo()
        {
            System.Threading.Thread _thread = new System.Threading.Thread(new System.Threading.ParameterizedThreadStart(InformaciónAnterior));
            _thread.Start(1);
        }
        public void InformaciónAnterior(object t)
        {
            int EditionId = GeteditionValues.IdEdition;
            int CompanyId = GeteditionValues.IdCompany;
            byte CompanyTypeId = GeteditionValues.IdCompanyType;
            int UserId = GeteditionValues.IdUser;
            var _EditionLastParticipant = EditionId - 1;
            var _CompanyEditionsValue = (from _ce in db.CompanyEditions
                                         where _ce.EditionId == EditionId
                                         && _ce.CompanyId == CompanyId
                                         select _ce).ToList();
            var _CompanyEditionsValueLast = (from _ce in db.CompanyEditions
                                             where _ce.EditionId == EditionId
                                             && _ce.CompanyId == CompanyId
                                             select _ce).ToList();
            db.plm_spInsertLastInformationByEditionClient(_EditionLastParticipant, EditionId, CompanyId);
            var _CompanyEditionsValueNew = (from _ce in db.CompanyEditions
                                             where _ce.EditionId == EditionId
                                             && _ce.CompanyId == CompanyId
                                             select _ce).ToList();
            if (_CompanyEditionsValueLast.LongCount() != _CompanyEditionsValueNew.LongCount())
            {
                var _EditioncompanySectionAdvers = (from _ec in db.EditionCompanySectionAdvers
                                                    where _ec.EditionId == EditionId
                                                    && _ec.CompanyId == CompanyId
                                                    select _ec).ToList();
                var _EditionExist = (from _ed in db.Editions
                                     where _ed.EditionId == _EditionLastParticipant
                                     select _ed).ToList();
                foreach (var _Advers in _EditioncompanySectionAdvers)
                {
                    var imageName = "";
                    imageName = _Advers.AdverFile;
                    string FullPathDEACI = Request.MapPath("/Anuncios/DEACI_" + _EditionExist.SingleOrDefault().NumberEdition + "/" + imageName);

                    if (System.IO.File.Exists(FullPathDEACI))
                    {
                        var _EditionNotExist = (from _ed in db.Editions
                                                where _ed.EditionId == EditionId
                                                select _ed).ToList();
                        foreach (var Complement in _EditionNotExist)
                        {
                            string Directory = Request.MapPath("/Anuncios/DEACI_" + Complement.NumberEdition);
                            if (System.IO.Directory.Exists(Directory))
                            {
                                string DestFile = System.IO.Path.Combine(Directory, imageName);
                                System.IO.File.Copy(FullPathDEACI, DestFile, true);
                                _Advers.BaseURL = DestFile;
                                db.SaveChanges();
                            }
                            else
                            {
                                System.IO.Directory.CreateDirectory(Directory);
                                string DestFile = System.IO.Path.Combine(Directory, imageName);
                                System.IO.File.Copy(FullPathDEACI, DestFile, true);
                                _Advers.BaseURL = DestFile;
                                db.SaveChanges();
                            }
                        }
                    }
                }
                var _Aplication = (from _ap in plm.Applications
                                   where _ap.Description == "Sitio DEACI"
                                   select _ap).ToList();
                var _LastEditionValue = db.Editions.SingleOrDefault(model => model.EditionId == _EditionLastParticipant);
                var _EditionValue = db.Editions.SingleOrDefault(model => model.EditionId == EditionId);
                var _ClientValue = db.Companies.SingleOrDefault(model => model.CompanyId == CompanyId);
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
                        _ActivityLogs.TableId = _Tabs.TableId; // --- SameInformation --- //
                        _ActivityLogs.OperationId = _OperationId;
                        _ActivityLogs.Date = DateTime.Now;
                        _ActivityLogs.PrimaryKeyAffected = ("(CompanyId," + CompanyId + ");(EditionIdAnterior," + _EditionLastParticipant + ");(EditionIdNuevo," + EditionId + ")");
                        _ActivityLogs.FieldsAffected = null;
                        plm.Entry(_ActivityLogs).State = EntityState.Added;
                        plm.SaveChanges();
                    }
                }
            }
        }
                 // --- Anexos --- // 
        // <------ No son parte del sistema ------>
        public ActionResult Styles()
        {
            return View();
        }
        public ActionResult StylesProducts()
        {
            return View();
        }
        public ActionResult LabsStyles()
        {
            return View();
        }
        // <------ No son parte del sistema ------>
    }
}