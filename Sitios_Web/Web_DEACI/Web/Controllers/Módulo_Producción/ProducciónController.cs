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
using Rotativa;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;
using OfficeOpenXml;
using OfficeOpenXml.Drawing;
using System.Drawing;
using System.Text;
namespace Web.Controllers.Módulo_Producción
{
    public class ProducciónController : Controller
    {
        private DataTable table = new DataTable();
        private DEACI_20150917Entities db = new DEACI_20150917Entities();
        PLMUsers plm = new PLMUsers();
        public ActionResult Clientes(int id, int ed, int ad)
        {
            SessionCompanyEditions _SessionCompanyEdition = new SessionCompanyEditions(id, ed, ad);
            Session["SessionCompanyEditions"] = _SessionCompanyEdition;
            if (Session["SessionCompanyEditions"] == null)
            {
                Session.Abandon();
                return RedirectToAction("Login", "Login");
            }
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
            var _storeprocedure = db.Database.SqlQuery<plm_spGetCompaniesByEditionIdByCompanyTypeId_Result>
                ("plm_spGetCompaniesByEditionIdByCompanyTypeId @EditionId=" + id + ", @CompanyTypeId=" + ad + "").ToList();
            var _details = (from _c in db.Companies
                            join _ce in db.CompanyEditions
                            on _c.CompanyId equals _ce.CompanyId
                            join _ct in db.CompanyTypes
                            on _ce.CompanyTypeId equals _ct.CompanyTypeId
                            where _ct.CompanyTypeId == ad
                            && _c.CompanyId == ed
                            select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Companies = _c, CompanyTypes = _ct }).Distinct();
            ViewData["TipoCliente"] = _details.SingleOrDefault().CompanyTypes.Description;
            ViewData["CompanyTypes.Description"] = _details.SingleOrDefault().CompanyTypes.Description;
            ViewData["CompanyName"] = _details.SingleOrDefault().Companies.CompanyName;
            ViewData["CompanyId"] = _details.SingleOrDefault().Companies.CompanyId;
            var _companies = (from _c in db.Companies
                              join _ce in db.CompanyEditions 
                              on _c.CompanyId equals _ce.CompanyId
                              join _ct in db.CompanyTypes
                              on _ce.CompanyTypeId equals _ct.CompanyTypeId
                              where _c.CompanyId == ed
                              && _ce.EditionId == id
                              select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Companies = _c, CompanyTypes = _ct });
            var _editions = (from _e in db.Editions
                             join _co in db.Countries
                             on _e.CountryId equals _co.CountryId
                             where _e.EditionId == id
                             select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Editions = _e, Countries = _co });
            ViewData["Edición"] = _editions.SingleOrDefault().Editions.NumberEdition;
            ViewData["Number"] = _editions.SingleOrDefault().Editions.EditionId;
            ViewData["CountryId"] = _editions.SingleOrDefault().Countries.CountryId;
            return View(_storeprocedure);
        }
        public ActionResult AsociarMarca(int id, int ed, int ad, string palabra, string sortOrder, string currentFilter, string searchString, int? page)
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
            var _editions = (from e in db.Editions
                             where e.EditionId == id
                             select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Editions = e });
            ViewData["Number"] = _editions.SingleOrDefault().Editions.EditionId;
            ViewData["Edición"] = _editions.SingleOrDefault().Editions.NumberEdition;
            var _CompanyTypesBrands = (from _c in db.Companies
                                       join _cbe in db.CompanyBrandEditions
                                       on _c.CompanyId equals _cbe.CompanyId
                                       join _b in db.Brands
                                       on _cbe.BrandId equals _b.BrandId
                                       where _c.CompanyId == ed
                                       && _cbe.EditionId == id
                                       orderby _b.BrandName
                                       select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Companies = _c, CompanyBrandEditions = _cbe, Brands = _b });
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
            var _companies = (from _c in db.Companies
                              join _ce in db.CompanyEditions 
                              on _c.CompanyId equals _ce.CompanyId
                              join _ct in db.CompanyTypes
                              on _ce.CompanyTypeId equals _ct.CompanyTypeId
                              where _c.CompanyId == ed
                              && _ct.CompanyTypeId == ad
                              select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Companies = _c, CompanyTypes = _ct }).Distinct();
            ViewData["CompanyName"] = _companies.FirstOrDefault().Companies.CompanyName;
            ViewData["CompanyId"] = _companies.FirstOrDefault().Companies.CompanyId;
            ViewData["CompanyShortName"] = _companies.SingleOrDefault().Companies.CompanyShortName;
            ViewData["TipoCliente"] = _companies.SingleOrDefault().CompanyTypes.Description;
            if (palabra != null)
            {
                if (palabra.LongCount() <= 3)
                {
                    if (!String.IsNullOrEmpty(palabra))
                    {
                        _CompanyTypesBrands = _CompanyTypesBrands.Where(j => j.Brands.BrandName.StartsWith(palabra));
                    }
                }
                if (palabra.LongCount() > 3)
                {
                    if (!String.IsNullOrEmpty(palabra))
                    {
                        _CompanyTypesBrands = _CompanyTypesBrands.Where(j => j.Brands.BrandName.Contains(palabra));
                    }
                }
            }
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(_CompanyTypesBrands.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult AsociarProductos(int id, int ed, int ad, string palabra, string sortOrder, string currentFilter, string searchString, int? page)
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
            var _editions = (from _e in db.Editions
                             where _e.EditionId == id
                             select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Editions = _e });
            ViewData["Number"] = _editions.SingleOrDefault().Editions.EditionId;
            ViewData["Edición"] = _editions.SingleOrDefault().Editions.NumberEdition;
            var _CompanyProducts = (from _c in db.Companies
                                           join _p in db.Products
                                           on _c.CompanyId equals _p.CompanyId
                                           join _pe in db.ProductEditions
                                           on _p.ProductId equals _pe.ProductId
                                           join _pt in db.ProductTypes
                                           on _p.ProductTypeId equals _pt.ProductTypeId
                                           where _c.CompanyId == ed
                                           && _pt.Description != "MARCA"
                                           && _pe.EditionId == id
                                           orderby _p.ProductName
                                           select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Companies = _c, Products = _p, ProductTypes = _pt, ProductEditions = _pe });
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
            var _companies = (from c in db.Companies
                              join _ce in db.CompanyEditions 
                              on c.CompanyId equals _ce.CompanyId
                              join _ct in db.CompanyTypes
                              on _ce.CompanyTypeId equals _ct.CompanyTypeId
                              where c.CompanyId == ed && _ct.CompanyTypeId == ad
                              select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Companies = c, CompanyTypes = _ct }).Distinct();
            ViewData["CompanyId"] = _companies.SingleOrDefault().Companies.CompanyId;
            ViewData["CompanyName"] = _companies.SingleOrDefault().Companies.CompanyName;
            ViewData["CompanyShortName"] = _companies.SingleOrDefault().Companies.CompanyShortName;
            ViewData["TipoCliente"] = _companies.SingleOrDefault().CompanyTypes.Description;
            if (palabra != null)
            {
                if (palabra.LongCount() <= 3)
                {
                    if (!String.IsNullOrEmpty(palabra))
                    {
                        _CompanyProducts = _CompanyProducts.Where(j => j.Products.ProductName.StartsWith(palabra));
                    }
                }
                if (palabra.LongCount() > 3)
                {
                    if (!String.IsNullOrEmpty(palabra))
                    {
                        _CompanyProducts = _CompanyProducts.Where(j => j.Products.ProductName.Contains(palabra));
                    }
                }
            }
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(_CompanyProducts.ToPagedList(pageNumber, pageSize));
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
        public ActionResult EditarEdicionesParticipantes(int id, int ed, int ad, int ud)
        {
            CompanyEditions _CompanyEditions = db.CompanyEditions.SingleOrDefault(x => x.CompanyId == ad && x.EditionId == ed);
            var _EditionsFile = (from _ed in db.Editions
                                 where _ed.EditionId == id
                                 select _ed).ToList();
            var _CompanyEditionsVal = (from _ce in db.CompanyEditions
                                    where _ce.EditionId == id
                                    && _ce.CompanyId == ad
                                    select _ce).ToList();
            foreach (var a in _CompanyEditionsVal)
            {
                if (a.HtmlContent == null)
                {
                    ViewData["FileNull"] = "";
                }
                
            }
            ViewBag.CompanyId = new SelectList(db.Companies, "CompanyId", "CompanyName", ad);
            ViewBag.EditionId = new SelectList(db.Editions, "EditionId", "NumberEdition", ed);
            var _Edits = (from _e in db.Editions
                          where _e.EditionId == ed
                          select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Editions = _e });
            ViewData["EdiciónParticipante"] = _Edits.SingleOrDefault().Editions.NumberEdition;
            return View(_CompanyEditions);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult EditarEdicionesParticipantes(int id, int ed, int ad, int ud, CompanyEditions Archivo)
        {
            CompanyEditions _CompanyEditions = new CompanyEditions();
            var _EditionsFile = (from _ed in db.Editions
                                 where _ed.EditionId == id
                                 select _ed).ToList();
            if (Archivo.File != null)
            {
                StreamReader SR = new StreamReader(Archivo.File.InputStream);
                string line = SR.ReadToEnd();
                _CompanyEditions.HtmlContent = line;
                _CompanyEditions.HtmlFile = Archivo.File.FileName;
            }
            else
            {
                _CompanyEditions.HtmlFile = Archivo.HtmlFile;
                _CompanyEditions.HtmlContent = Archivo.HtmlContent;
            }
            _CompanyEditions.CompanyId = Archivo.CompanyId;
            _CompanyEditions.EditionId = Archivo.EditionId;
            _CompanyEditions.CompanyTypeId = Archivo.CompanyTypeId;
            _CompanyEditions.CloseClient = Archivo.CloseClient;
            _CompanyEditions.Page = Archivo.Page;
            db.Entry(_CompanyEditions).State = EntityState.Modified;
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
                    _ActivityLogs.PrimaryKeyAffected = ("(CompanyId," + _CompanyEditions.CompanyId + "),(" + "EditionId," + _CompanyEditions.EditionId + "),(" + "CompanyTypeId," + _CompanyEditions.CompanyTypeId + ")");
                    _ActivityLogs.FieldsAffected = ("(HtmlFile," + _CompanyEditions.HtmlFile + ")");
                    plm.Entry(_ActivityLogs).State = EntityState.Added;
                    plm.SaveChanges();
                }
            }
            return RedirectToAction("/Clientes/" + id + "/" + Archivo.CompanyId + "/" + ud);
        }
        public ActionResult EditarEdicionesParticipantesProducto(int id, int ed, int ad, int ud, int od)
        {
            ProductEditions _producteditions = db.ProductEditions.SingleOrDefault(x => x.ProductId == ad && x.EditionId == ed);
            var _EditionsFile = (from _ed in db.Editions
                                 where _ed.EditionId == id
                                 select _ed).ToList();
            var _ProductEditions = (from _pe in db.ProductEditions
                                    where _pe.EditionId == id
                                    && _pe.ProductId == ad
                                    select _pe).ToList();
            foreach (var a in _ProductEditions)
            {
                if (a.HtmlContent == null)
                {
                    ViewData["FileNull"] = "";
                }
            }

            var _Products = (from _p in db.Products
                             where _p.ProductId == ad
                             select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Products = _p });
            ViewData["Pname"] = _Products.SingleOrDefault().Products.ProductName;
            ViewData["PP"] = _Products.SingleOrDefault().Products.ProductId;
            ViewBag.EditionId = new SelectList(db.Editions, "EditionId", "NumberEdition", ed);
            var _Edits = (from _ed in db.Editions
                          where _ed.EditionId == ed
                          select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Editions = _ed });
            ViewData["EdiciónParticipante"] = _Edits.SingleOrDefault().Editions.NumberEdition;
            return View(_producteditions);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult EditarEdicionesParticipantesProducto(int id, int ed, int ad, int ud, int od, ProductEditions Archivo)
        {
            ProductEditions _ProductEditions = new ProductEditions();
            var _EditionsFile = (from _ed in db.Editions
                                 where _ed.EditionId == id
                                 select _ed).ToList();
            if (Archivo.File != null)
            {
                StreamReader SR = new StreamReader(Archivo.File.InputStream);
                string line = SR.ReadToEnd();
                _ProductEditions.HtmlContent = line;
                _ProductEditions.HtmlFile = Archivo.File.FileName;
            }
            else
            {
                _ProductEditions.HtmlFile = Archivo.HtmlFile;
                _ProductEditions.HtmlContent = Archivo.HtmlContent;
            }
            _ProductEditions.ProductId = Archivo.ProductId;
            _ProductEditions.EditionId = Archivo.EditionId;
            _ProductEditions.Page = Archivo.Page;
            _ProductEditions.StatusId = Archivo.StatusId;
            _ProductEditions.Page = Archivo.Page;
            db.Entry(_ProductEditions).State = EntityState.Modified;
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
                    int _OperationId = 2; // Modificar
                    _ActivityLogs.UserId = p.userId;
                    _ActivityLogs.TableId = _Tabs.TableId; // ProductEditions
                    _ActivityLogs.OperationId = _OperationId;
                    _ActivityLogs.Date = DateTime.Now;
                    _ActivityLogs.PrimaryKeyAffected = ("(ProductId," + _ProductEditions.ProductId + "),(" + "EditionId," + _ProductEditions.EditionId + ")");
                    _ActivityLogs.FieldsAffected = ("(HtmlFile," + _ProductEditions.HtmlFile + ")");
                    plm.Entry(_ActivityLogs).State = EntityState.Added;
                    plm.SaveChanges();
                }
            }
            return RedirectToAction("/AsociarProductos/" + id + "/" + ud + "/" + od);
        }
        public ActionResult EditarAnuncio(int id, int ed, int ad, int ud, int od)
        {
            EditionCompanySectionAdvers _EditionCompanySectionAdvers = db.EditionCompanySectionAdvers.SingleOrDefault(model => model.EditionCompanySectionAdverId == id
            && model.EditionId == ed && model.CompanyId == ad && model.SectionId == ud);
            var _EditionsFile = (from _ed in db.Editions
                                 where _ed.EditionId == ed
                                 select _ed).ToList();
                var imageName = "";
                imageName = _EditionCompanySectionAdvers.AdverFile;
                string FullPath_DEACI = Request.MapPath("/Anuncios/DEACI_" + _EditionsFile.SingleOrDefault().NumberEdition + "/" + imageName);
                if (System.IO.File.Exists(FullPath_DEACI))
                { }
                else
                {
                    ViewData["FileNull"] = "";
                }
            return View(_EditionCompanySectionAdvers);
        }
        [HttpPost]
        public ActionResult EditarAnuncio(int id, int ed, int ad, int ud, int od, EditionCompanySectionAdvers Archivo)
        {
            var _Editions = db.Editions.SingleOrDefault(model => model.EditionId == ed);
            EditionCompanySectionAdvers _EditionCompanySectionAdvers = new EditionCompanySectionAdvers();
                if (Archivo.File != null)
                {
                    var imageName = "";
                    imageName = Archivo.AdverFile;
                    string FullPath_DEACI = Request.MapPath("/Anuncios/DEACI_" + _Editions.NumberEdition + "/" + imageName);
                    if (System.IO.File.Exists(FullPath_DEACI))
                    {
                        System.IO.File.Delete(FullPath_DEACI);
                        var _FileName = Path.GetFileName(Archivo.File.FileName);
                        var _path = Path.Combine(Server.MapPath("/Anuncios/DEACI_" + _Editions.NumberEdition), _FileName);
                        Archivo.File.SaveAs(_path);
                        string _BaseURL = _path;
                        _EditionCompanySectionAdvers.BaseURL = _BaseURL;
                    }
                    else
                    {
                        string Directory = Request.MapPath("/Anuncios/DEACI_" + _Editions.NumberEdition);
                        if (System.IO.Directory.Exists(Directory))
                        {
                            var _FileNameNew = Path.GetFileName(Archivo.File.FileName);
                            var _pathNew = Path.Combine(Server.MapPath("/Anuncios/DEACI_" + _Editions.NumberEdition), _FileNameNew);
                            Archivo.File.SaveAs(_pathNew);
                            string _BaseURL = _pathNew;
                            _EditionCompanySectionAdvers.BaseURL = _BaseURL;
                        }
                        else
                        {
                            System.IO.Directory.CreateDirectory(Directory);
                            var _FileNameNew = Path.GetFileName(Archivo.File.FileName);
                            var _pathNew = Path.Combine(Server.MapPath("/Anuncios/DEACI_" + _Editions.NumberEdition), _FileNameNew);
                            Archivo.File.SaveAs(_pathNew);
                            string _BaseURL = _pathNew;
                            _EditionCompanySectionAdvers.BaseURL = _BaseURL;
                        }


                    }
                }

            if (Archivo.File == null)
            {
                _EditionCompanySectionAdvers.AdverFile = Archivo.AdverFile;
            }
            else
            {
                _EditionCompanySectionAdvers.AdverFile = Archivo.File.FileName;
            }
            _EditionCompanySectionAdvers.EditionCompanySectionAdverId = Archivo.EditionCompanySectionAdverId;
            _EditionCompanySectionAdvers.EditionId = Archivo.EditionId;
            _EditionCompanySectionAdvers.CompanyId = Archivo.CompanyId;
            _EditionCompanySectionAdvers.SectionId = Archivo.SectionId;
            _EditionCompanySectionAdvers.HiredSpace = Archivo.HiredSpace;
            db.Entry(_EditionCompanySectionAdvers).State = EntityState.Modified;
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
            return RedirectToAction("/Clientes/" + Archivo.EditionId + "/" + Archivo.CompanyId + "/" + od);
        }
        public ActionResult EliminarAnuncioEdiciónCliente(int EditionId, int CompanyId, int AdvId, int CompanyTypeId)
        {
            var _ValueAdvrs = db.AdvertisementEditions.Where(model => model.EditionId == EditionId && model.AdvId == AdvId && model.CompanyId == CompanyId);
            if (_ValueAdvrs.LongCount() == 0)
            {
                return RedirectToAction("/Clientes/" + EditionId + "/" + CompanyId + "/" + CompanyTypeId);
            }
            AdvertisementEditions _AdvertisementEditions = new AdvertisementEditions();
            _AdvertisementEditions.EditionId = EditionId;
            _AdvertisementEditions.CompanyId = CompanyId;
            _AdvertisementEditions.AdvId = AdvId;
            db.Entry(_AdvertisementEditions).State = EntityState.Deleted;
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
                    _ActivityLogs.PrimaryKeyAffected = ("(AdvId," + _AdvertisementEditions.AdvId + "),(EditionId," + _AdvertisementEditions.EditionId + "),(CompanyId," + _AdvertisementEditions.CompanyId + ")");
                    _ActivityLogs.FieldsAffected = null;
                    plm.Entry(_ActivityLogs).State = EntityState.Added;
                    plm.SaveChanges();
                }
            }
            var _Advers = (from _ad in db.Advertisements
                           where _ad.AdvId == AdvId
                           select _ad).ToList();
            var imageName = "";
            imageName = _Advers.SingleOrDefault().AdvFile;
            var _EditionsAdver = (from _ed in db.Editions
                                  where _ed.EditionId == EditionId
                                  select _ed).ToList();
            if (_EditionsAdver.SingleOrDefault().ISBN == "DEACI 30")
            {
                string fullPath_DEACI30 = Request.MapPath("~/Anuncios/DEACI_30/" + imageName);
                if (System.IO.File.Exists(fullPath_DEACI30))
                {
                    System.IO.File.Delete(fullPath_DEACI30);
                }
            }
            if (_EditionsAdver.SingleOrDefault().ISBN == "DEACI 29")
            {
                string fullPath_DEACI29 = Request.MapPath("~/Anuncios/DEACI_29/" + imageName);
                if (System.IO.File.Exists(fullPath_DEACI29))
                {
                    System.IO.File.Delete(fullPath_DEACI29);
                }
            }
            if (_EditionsAdver.SingleOrDefault().ISBN == "DEACI 28")
            {
                string fullPath_DEACI28 = Request.MapPath("~/Anuncios/DEACI_28/" + imageName);
                if (System.IO.File.Exists(fullPath_DEACI28))
                {
                    System.IO.File.Delete(fullPath_DEACI28);
                }
            }
            if (_EditionsAdver.SingleOrDefault().ISBN == "DEACI 27")
            {
                string fullPath_DEACI27 = Request.MapPath("~/Anuncios/DEACI_27/" + imageName);
                if (System.IO.File.Exists(fullPath_DEACI27))
                {
                    System.IO.File.Delete(fullPath_DEACI27);
                }
            }
            if (_EditionsAdver.SingleOrDefault().ISBN == "DEACI 26")
            {
                string fullPath_DEACI26 = Request.MapPath("~/Anuncios/DEACI_26/" + imageName);
                if (System.IO.File.Exists(fullPath_DEACI26))
                {
                    System.IO.File.Delete(fullPath_DEACI26);
                }
            }
            if (_EditionsAdver.SingleOrDefault().ISBN == "DEACI 25")
            {
                string fullPath_DEACI25 = Request.MapPath("~/Anuncios/DEACI_25/" + imageName);
                if (System.IO.File.Exists(fullPath_DEACI25))
                {
                    System.IO.File.Delete(fullPath_DEACI25);
                }
            }
            if (_EditionsAdver.SingleOrDefault().ISBN == "DEACI 24")
            {
                string fullPath_DEACI24 = Request.MapPath("~/Anuncios/DEACI_24/" + imageName);
                if (System.IO.File.Exists(fullPath_DEACI24))
                {
                    System.IO.File.Delete(fullPath_DEACI24);
                }
            }
            if (_EditionsAdver.SingleOrDefault().ISBN == "DEACI 22")
            {
                string fullPath_DEACI22 = Request.MapPath("~/Anuncios/DEACI_22/" + imageName);
                if (System.IO.File.Exists(fullPath_DEACI22))
                {
                    System.IO.File.Delete(fullPath_DEACI22);
                }
            }
            return RedirectToAction("/Clientes/" + EditionId + "/" + CompanyId + "/" + CompanyTypeId);
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
                        int _OperationId = 1; // Modificar
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
            { _Addresses.Location = Location;}
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
        public ActionResult EliminarDirección(int id, string CompanyId, string AddressId, int CompanyTypeId)
        {
            int _CompanyId = int.Parse(CompanyId);
            int _AddressId = int.Parse(AddressId);
            var _deletecompanyaddress = db.CompanyAddresses.SingleOrDefault(model => model.CompanyId == _CompanyId && model.AddressId == _AddressId);
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
            return RedirectToAction("/Clientes/" + id + "/" + _CompanyId + "/" + CompanyTypeId);
        }
        public ActionResult Distribuidores(int id, int ed, int ad)
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
            var _Distributions = (from _c in db.EditionCompanyDistributions
                                  where _c.CompanyId == ed
                                  && _c.EditionId == id
                                  select new Union_Companies_CompanyTypes_CompanyPhones_Cities { EditionCompanyDistributions = _c });
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
            return View(_Distributions);
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
        public ActionResult PaginarClientes(int id, int ed, int ad, string palabra, string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewData["EditionId"] = id;
            ViewData["CompanyId"] = ed;
            ViewData["CompanyTypeId"] = ad;
            var _Companies = (from _c in db.Companies
                                  join _ce in db.CompanyEditions
                                  on _c.CompanyId equals _ce.CompanyId
                                  join _ct in db.CompanyTypes
                                  on _ce.CompanyTypeId equals _ct.CompanyTypeId
                                  where _ce.EditionId == id
                                  && _ct.Description == "CLIENTE"
                                  orderby _c.CompanyName
                                  select new Union_Companies_CompanyTypes_CompanyPhones_Cities{ Companies = _c, CompanyEditions = _ce, CompanyTypes = _ct });
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

            var _editions = (from _e in db.Editions
                             join _co in db.Countries
                             on _e.CountryId equals _co.CountryId
                             where _e.EditionId == id
                             select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Editions = _e, Countries = _co });
            ViewData["Edición"] = _editions.SingleOrDefault().Editions.NumberEdition;
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
                        _Companies = _Companies.Where(j => j.Companies.CompanyName.StartsWith(palabra));
                    }
                }
                if (palabra.LongCount() > 3)
                {
                    if (!String.IsNullOrEmpty(palabra))
                    {
                        _Companies = _Companies.Where(j => j.Companies.CompanyName.Contains(palabra));
                    }
                }
            }
            int pageSize = 30;
            int pageNumber = (page ?? 1);
            return View(_Companies.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult PaginarCompañias(int EditionId, int CompanyId, string Page)
        {
            var _CompanyEditions = (from _ce in db.CompanyEditions
                                    where _ce.CompanyId == CompanyId
                                    && _ce.EditionId == EditionId
                                    select _ce).ToList();
            foreach (var a in _CompanyEditions)
            {
                if (Page == "")
                {
                    a.Page = null;
                }
                else
                {
                    a.Page = Page;
                }
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
                        _ActivityLogs.PrimaryKeyAffected = ("(CompanyId," + a.CompanyId + "),(EditionId," + a.EditionId + "),(CompanyTypeId," + a.CompanyTypeId + ")");
                        _ActivityLogs.FieldsAffected = ("(Page," + a.Page + ")");
                        plm.Entry(_ActivityLogs).State = EntityState.Added;
                        plm.SaveChanges();
                    }
                }
            }
           return Json(true, JsonRequestBehavior.AllowGet);
        }
        public ActionResult PaginarProductos(int id, int ed, int ad, string palabra, string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewData["EditionId"] = id;
            ViewData["CompanyId"] = ed;
            ViewData["CompanyTypeId"] = ad;
            var _Products = (from _p in db.Products
                             join _pt in db.ProductTypes
                             on _p.ProductTypeId equals _pt.ProductTypeId
                             join _c in db.Companies
                             on _p.CompanyId equals _c.CompanyId
                             join _ce in db.CompanyEditions
                             on _c.CompanyId equals _ce.CompanyId
                             join _pe in db.ProductEditions
                             on _p.ProductId equals _pe.ProductId
                             where _pe.EditionId == _ce.EditionId
                             where _ce.EditionId == id
                             orderby _c.CompanyName, _pt.Description == "PRODUCTO (TEXTO EDITORIAL)", _pt.Description == "(PRODUCTO) en TERCERA", _pt.Description == "AGREGADO", _p.ProductName
                             select new Union_Companies_CompanyTypes_CompanyPhones_Cities { CompanyEditions = _ce, Companies = _c, Products = _p, ProductEditions = _pe, ProductTypes = _pt });
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

            var _editions = (from _e in db.Editions
                             join _co in db.Countries
                             on _e.CountryId equals _co.CountryId
                             where _e.EditionId == id
                             select new Union_Companies_CompanyTypes_CompanyPhones_Cities { Editions = _e, Countries = _co });
            ViewData["Edición"] = _editions.SingleOrDefault().Editions.NumberEdition;
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
                        _Products = _Products.Where(j => j.Products.ProductName.StartsWith(palabra));
                    }
                }
                if (palabra.LongCount() > 3)
                {
                    if (!String.IsNullOrEmpty(palabra))
                    {
                        _Products = _Products.Where(j => j.Products.ProductName.Contains(palabra));
                    }
                }
            }
            int pageSize = 30;
            int pageNumber = (page ?? 1);
            return View(_Products.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult PaginaciónProductos(int ProductId, int EditionId, string Page)
        {
                var _ProductContent = (from _pe in db.ProductEditions
                                       where _pe.ProductId == ProductId
                                       && _pe.EditionId == EditionId
                                       select _pe).ToList();
                foreach (var a in _ProductContent)
                {
                    if (Page == "")
                    {
                        a.Page = null;
                    }
                    else
                    {
                        a.Page = Page;
                    }
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
                            int _OperationId = 2; // Modificar
                            _ActivityLogs.UserId = p.userId;
                            _ActivityLogs.TableId = _Tabs.TableId; // ProductEditions
                            _ActivityLogs.OperationId = _OperationId;
                            _ActivityLogs.Date = DateTime.Now;
                            _ActivityLogs.PrimaryKeyAffected = ("(CompanyId," + a.ProductId + "),(EditionId," + a.EditionId + ")");
                            _ActivityLogs.FieldsAffected = ("(Page," + a.Page + ")");
                            plm.Entry(_ActivityLogs).State = EntityState.Added;
                            plm.SaveChanges();
                        }
                    }
                }
            return Json(true, JsonRequestBehavior.AllowGet);
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
        public FileResult ReporteÍndiceGeneral(int id, int ed)
        {
            var bind = new GridView();

            DataColumn Descrito = new DataColumn();
            Descrito.DataType = typeof(string);
            Descrito.ColumnName = "Descrito";

            DataColumn Producto = new DataColumn();
            Producto.DataType = typeof(string);
            Producto.ColumnName = "Nombre del producto";

            DataColumn Descripción = new DataColumn();
            Descripción.DataType = typeof(string);
            Descripción.ColumnName = "Descripción";

            DataColumn Cliente = new DataColumn();
            Cliente.DataType = typeof(string);
            Cliente.ColumnName = "Cliente";

            DataColumn Página = new DataColumn();
            Página.DataType = typeof(string);
            Página.ColumnName = "Página";

            table.Columns.Add(Descrito);
            table.Columns.Add(Producto);
            table.Columns.Add(Descripción);
            table.Columns.Add(Cliente);
            table.Columns.Add(Página);
            var _ReportIndexes = (from _c in db.Companies
                                      join _ce in db.CompanyEditions
                                      on _c.CompanyId equals _ce.CompanyId
                                      join _p in db.Products
                                      on _c.CompanyId equals _p.CompanyId
                                      join _pt in db.ProductTypes
                                      on _p.ProductTypeId equals _pt.ProductTypeId
                                      join _pe in db.ProductEditions
                                      on _p.ProductId equals _pe.ProductId
                                      where _ce.EditionId == _pe.EditionId
                                      join _pin in db.ProductIndexes
                                      on _p.ProductId equals _pin.ProductId
                                      join _ind in db.Indexes
                                      on _pin.IndexId equals _ind.IndexId
                                      where _ce.EditionId == id
                                      && _ind.IndexId == ed
                                      && _pt.Description != "MARCA"
                                      orderby _p.ProductName, _p.Description, _c.CompanyName, _pe.Page
                                      select new { _p, _c, _pe, _pt }).ToList();
            foreach (var a in _ReportIndexes)
            {
                ProductTypes _ProductTypes = new ProductTypes();
                if (a._pt.Description != "(PRODUCTO) en TERCERA" && a._pt.Description != "AGREGADO" && a._pt.Description != "PRODUCTO (TEXTO EDITORIAL)" && a._pt.Description != "MARCA")
                {
                    _ProductTypes.Description = "*";
                }
                else
                {
                    _ProductTypes.Description = null;
                }
                ProductEditions _ProductEditions = new ProductEditions();
                if (a._pe.HtmlFile == null)
                {
                    _ProductEditions.HtmlFile = "";
                }
                else
                {
                    _ProductEditions.HtmlFile = "*";
                }
                Products _Products = new Products();
                if (a._c.CompanyName == "DIAGNÓSTICA INTERNACIONAL, S.A. DE C.V. ")
                {
                    StringBuilder sb = new StringBuilder(a._p.Description);
                    sb.Replace("-", " ");
                    sb.Replace("–", " ");
                    _Products.Description = sb.ToString();
                }
                else
                {
                    _Products.Description = a._p.Description;
                }
                table.Rows.Add(
                    _ProductTypes.Description,
                    a._p.ProductName.Trim() + ".",
                    _Products.Description,
                    a._c.CompanyName,
                    a._pe.Page
                    );
            }
                var _Editions = (from _e in db.Editions
                                 where _e.EditionId == id
                                 select _e).ToList();
                string Directory = Request.MapPath("/Reportes_Excel/DEACI_" + _Editions.SingleOrDefault().NumberEdition);
                if (System.IO.Directory.Exists(Directory))
                {
                    //
                }
                else
                {   // Crea el directorio
                    System.IO.Directory.CreateDirectory(Directory);
                }
                bind.DataSource = table;
                FileInfo newFile = new FileInfo(Request.MapPath("/Reportes_Excel/DEACI_" + _Editions.SingleOrDefault().NumberEdition + "/Reporte índice general DEACI " + _Editions.SingleOrDefault().NumberEdition + ".xlsx"));
                ExcelPackage pkg = new ExcelPackage(newFile);
                if (newFile.Exists)
                {
                    System.IO.File.Delete(Server.MapPath("/Reportes_Excel/DEACI_" + _Editions.SingleOrDefault().NumberEdition + "/Reporte índice general DEACI " + _Editions.SingleOrDefault().NumberEdition + ".xlsx"));
                    newFile.Delete();
                    pkg.Workbook.Worksheets.Delete("Reporte índice general");
                    ExcelWorksheet worksheet = pkg.Workbook.Worksheets.Add("Reporte índice general");
                    worksheet.Cells["A1"].LoadFromDataTable(table, true);
                    using (ExcelRange range = worksheet.Cells["A1:E1"])
                    {
                        range.Style.Font.Bold = true;
                        range.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        range.Style.Fill.BackgroundColor.SetColor(Color.DimGray);
                        range.Style.Font.Color.SetColor(Color.White);
                        range.Style.ShrinkToFit = false;
                    }
                    worksheet.Cells.Style.Font.Size = 12;
                    worksheet.Cells.AutoFitColumns();
                    worksheet.Cells.Style.Font.Name = "Arial Narrow";
                }
                else
                {
                    ExcelWorksheet worksheet = pkg.Workbook.Worksheets.Add("Reporte índice general");
                    worksheet.Cells["A1"].LoadFromDataTable(table, true);
                    using (ExcelRange range = worksheet.Cells["A1:E1"])
                    {
                        range.Style.Font.Bold = true;
                        range.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        range.Style.Fill.BackgroundColor.SetColor(Color.DimGray);
                        range.Style.Font.Color.SetColor(Color.White);
                        range.Style.ShrinkToFit = false;
                    }
                    worksheet.Cells.Style.Font.Size = 12;
                    worksheet.Cells.AutoFitColumns();
                    worksheet.Cells.Style.Font.Name = "Arial Narrow";
                }
                pkg.SaveAs(newFile);
                var filepath = System.IO.Path.Combine(Server.MapPath("/Reportes_Excel/DEACI_" + _Editions.SingleOrDefault().NumberEdition), "Reporte índice general DEACI " + _Editions.SingleOrDefault().NumberEdition + ".xlsx");
                return File(filepath, "application/vnd.ms-excel", "Reporte índice general DEACI " + _Editions.SingleOrDefault().NumberEdition + ".xlsx");
        }
        public FileResult ReporteÍndiceImagenología(int id, int ed)
        {
            var bind = new GridView();

            DataColumn Sección = new DataColumn();
            Sección.DataType = typeof(string);
            Sección.ColumnName = "Sección";

            DataColumn Descrito = new DataColumn();
            Descrito.DataType = typeof(string);
            Descrito.ColumnName = "Descrito";

            DataColumn Producto = new DataColumn();
            Producto.DataType = typeof(string);
            Producto.ColumnName = "Nombre del producto";

            DataColumn Cliente = new DataColumn();
            Cliente.DataType = typeof(string);
            Cliente.ColumnName = "Cliente";

            DataColumn Página = new DataColumn();
            Página.DataType = typeof(string);
            Página.ColumnName = "Página";

            table.Columns.Add(Sección);
            table.Columns.Add(Descrito);
            table.Columns.Add(Producto);
            table.Columns.Add(Cliente);
            table.Columns.Add(Página);
            // Todos los productos que solo tengan contenido * y que esten asociados a nivel de (Sub Sección y Sub Sub Sección)
            var _ReportIndexes = (from _c in db.Companies
                                 join _ce in db.CompanyEditions
                                 on _c.CompanyId equals _ce.CompanyId
                                 join _p in db.Products
                                 on _c.CompanyId equals _p.CompanyId
                                 join _pt in db.ProductTypes
                                 on _p.ProductTypeId equals _pt.ProductTypeId
                                 join _pe in db.ProductEditions
                                 on _p.ProductId equals _pe.ProductId
                                 where _pe.EditionId == _ce.EditionId
                                 join _pi in db.ProductIndexes
                                 on _p.ProductId equals _pi.ProductId
                                 join _in in db.Indexes
                                 on _pi.IndexId equals _in.IndexId
                                 join _ps in db.ProductSections
                                 on _p.ProductId equals _ps.ProductId
                                 join _s in db.Sections
                                 on _ps.SectionId equals _s.SectionId
                                 where _ce.EditionId == id
                                 && _in.IndexId == ed
                                 && _s.SectionIdParent != null
                                 && _pt.Description != "(PRODUCTO) en TERCERA"
                                 && _pt.Description != "AGREGADO"
                                 && _pt.Description != "PRODUCTO (TEXTO EDITORIAL)"
                                 && _pt.Description != "MARCA"
                                 orderby _s.Description, _p.ProductName, _c.CompanyName
                                 select new { _s, _p, _c, _pe, _pt }).ToList();
            foreach (var a in _ReportIndexes)
            {
                ProductTypes _ProductTypes = new ProductTypes();
                _ProductTypes.Description = "*";
                table.Rows.Add(
                    a._s.Description,
                    _ProductTypes.Description,
                    a._p.ProductName.Trim() + ".",
                    a._c.CompanyName,
                    a._pe.Page);
            }
                var _Editions = (from _e in db.Editions
                                 where _e.EditionId == id
                                 select _e).ToList();
                string Directory = Request.MapPath("/Reportes_Excel/DEACI_" + _Editions.SingleOrDefault().NumberEdition);
                if (System.IO.Directory.Exists(Directory))
                {
                    //
                }
                else
                {   // Crea el directorio
                    System.IO.Directory.CreateDirectory(Directory);
                }
                bind.DataSource = table;
                FileInfo newFile = new FileInfo(Request.MapPath("/Reportes_Excel/DEACI_" + _Editions.SingleOrDefault().NumberEdition + "/Reporte índice imagenología DEACI " + _Editions.SingleOrDefault().NumberEdition + ".xlsx"));
                ExcelPackage pkg = new ExcelPackage(newFile);
                if (newFile.Exists)
                {
                    System.IO.File.Delete(Server.MapPath("/Reportes_Excel/DEACI_" + _Editions.SingleOrDefault().NumberEdition + "/Reporte índice imagenología DEACI " + _Editions.SingleOrDefault().NumberEdition + ".xlsx"));
                    newFile.Delete();
                    pkg.Workbook.Worksheets.Delete("Reporte índice imagenología");
                    ExcelWorksheet worksheet = pkg.Workbook.Worksheets.Add("Reporte índice imagenología");
                    worksheet.Cells["A1"].LoadFromDataTable(table, true);
                    using (ExcelRange range = worksheet.Cells["A1:E1"])
                    {
                        range.Style.Font.Bold = true;
                        range.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        range.Style.Fill.BackgroundColor.SetColor(Color.DimGray);
                        range.Style.Font.Color.SetColor(Color.White);
                        range.Style.ShrinkToFit = false;
                    }
                    worksheet.Cells.Style.Font.Size = 12;
                    worksheet.Cells.AutoFitColumns();
                    worksheet.Cells.Style.Font.Name = "Arial Narrow";
                }
                else
                {
                    ExcelWorksheet worksheet = pkg.Workbook.Worksheets.Add("Reporte índice imagenología");
                    worksheet.Cells["A1"].LoadFromDataTable(table, true);
                    using (ExcelRange range = worksheet.Cells["A1:E1"])
                    {
                        range.Style.Font.Bold = true;
                        range.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        range.Style.Fill.BackgroundColor.SetColor(Color.DimGray);
                        range.Style.Font.Color.SetColor(Color.White);
                        range.Style.ShrinkToFit = false;
                    }
                    worksheet.Cells.Style.Font.Size = 12;
                    worksheet.Cells.AutoFitColumns();
                    worksheet.Cells.Style.Font.Name = "Arial Narrow";
                }
                pkg.SaveAs(newFile);
                var filepath = System.IO.Path.Combine(Server.MapPath("/Reportes_Excel/DEACI_" + _Editions.SingleOrDefault().NumberEdition), "Reporte índice imagenología DEACI " + _Editions.SingleOrDefault().NumberEdition + ".xlsx");
                return File(filepath, "application/vnd.ms-excel", "Reporte índice imagenología DEACI " + _Editions.SingleOrDefault().NumberEdition + ".xlsx");
        }
        public FileResult ReporteÍndiceGenéricos(int id, int ed)
        {
            var bind = new GridView();


            DataColumn Sección = new DataColumn();
            Sección.DataType = typeof(string);
            Sección.ColumnName = "Sección";

            DataColumn Descrito = new DataColumn();
            Descrito.DataType = typeof(string);
            Descrito.ColumnName = "Descrito";

            DataColumn Producto = new DataColumn();
            Producto.DataType = typeof(string);
            Producto.ColumnName = "Nombre del producto";

            DataColumn Cliente = new DataColumn();
            Cliente.DataType = typeof(string);
            Cliente.ColumnName = "Cliente";

            DataColumn Página = new DataColumn();
            Página.DataType = typeof(string);
            Página.ColumnName = "Página";

            table.Columns.Add(Sección);
            table.Columns.Add(Descrito);
            table.Columns.Add(Producto);
            table.Columns.Add(Cliente);
            table.Columns.Add(Página);

            var _ReportIndexes = (from _c in db.Companies
                                      join _ce in db.CompanyEditions
                                      on _c.CompanyId equals _ce.CompanyId
                                      join _p in db.Products
                                      on _c.CompanyId equals _p.CompanyId
                                      join _pt in db.ProductTypes
                                      on _p.ProductTypeId equals _pt.ProductTypeId
                                      join _pe in db.ProductEditions
                                      on _p.ProductId equals _pe.ProductId
                                      where _pe.EditionId == _ce.EditionId
                                      join _pin in db.ProductIndexes
                                      on _p.ProductId equals _pin.ProductId
                                      join _ind in db.Indexes
                                      on _pin.IndexId equals _ind.IndexId
                                      join _ps in db.ProductSections
                                      on _p.ProductId equals _ps.ProductId
                                      join _s in db.Sections
                                      on _ps.SectionId equals _s.SectionId
                                      where _ce.EditionId == id
                                      && _ind.IndexId == ed
                                      && _s.SectionIdParent != null
                                      && _pt.Description != "(PRODUCTO) en TERCERA"
                                      && _pt.Description != "AGREGADO"
                                      && _pt.Description != "PRODUCTO (TEXTO EDITORIAL)"
                                      && _pt.Description != "MARCA"
                                      orderby _s.Description, _p.ProductName, _c.CompanyName
                                      select new {_s, _p, _c, _pe, _pt }).ToList();
            foreach (var a in _ReportIndexes)
            {
                ProductTypes _ProductTypes = new ProductTypes();
                _ProductTypes.Description = "*";
                table.Rows.Add(
                    a._s.Description,
                    _ProductTypes.Description,
                    a._p.ProductName.Trim() + ".",
                    a._c.CompanyName,
                    a._pe.Page);
            }
                var _Editions = (from _e in db.Editions
                                 where _e.EditionId == id
                                 select _e).ToList();
                string Directory = Request.MapPath("/Reportes_Excel/DEACI_" + _Editions.SingleOrDefault().NumberEdition);
                if (System.IO.Directory.Exists(Directory))
                {
                    //
                }
                else
                {   // Crea el directorio
                    System.IO.Directory.CreateDirectory(Directory);
                }
                bind.DataSource = table;
                FileInfo newFile = new FileInfo(Request.MapPath("/Reportes_Excel/DEACI_" + _Editions.SingleOrDefault().NumberEdition + "/Reporte índice genéricos DEACI " + _Editions.SingleOrDefault().NumberEdition + ".xlsx"));
                ExcelPackage pkg = new ExcelPackage(newFile);
                if (newFile.Exists)
                {
                    System.IO.File.Delete(Server.MapPath("/Reportes_Excel/DEACI_" + _Editions.SingleOrDefault().NumberEdition + "/Reporte índice genéricos DEACI " + _Editions.SingleOrDefault().NumberEdition + ".xlsx"));
                    newFile.Delete();
                    pkg.Workbook.Worksheets.Delete("Reporte índice genéricos");
                    ExcelWorksheet worksheet = pkg.Workbook.Worksheets.Add("Reporte índice genéricos");
                    worksheet.Cells["A1"].LoadFromDataTable(table, true);
                    using (ExcelRange range = worksheet.Cells["A1:E1"])
                    {
                        range.Style.Font.Bold = true;
                        range.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        range.Style.Fill.BackgroundColor.SetColor(Color.DimGray);
                        range.Style.Font.Color.SetColor(Color.White);
                        range.Style.ShrinkToFit = false;
                    }
                    worksheet.Cells.Style.Font.Size = 12;
                    worksheet.Cells.AutoFitColumns();
                    worksheet.Cells.Style.Font.Name = "Arial Narrow";
                }
                else
                {
                    ExcelWorksheet worksheet = pkg.Workbook.Worksheets.Add("Reporte índice genéricos");
                    worksheet.Cells["A1"].LoadFromDataTable(table, true);
                    using (ExcelRange range = worksheet.Cells["A1:E1"])
                    {
                        range.Style.Font.Bold = true;
                        range.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        range.Style.Fill.BackgroundColor.SetColor(Color.DimGray);
                        range.Style.Font.Color.SetColor(Color.White);
                        range.Style.ShrinkToFit = false;
                    }
                    worksheet.Cells.Style.Font.Size = 12;
                    worksheet.Cells.AutoFitColumns();
                    worksheet.Cells.Style.Font.Name = "Arial Narrow";
                }
                pkg.SaveAs(newFile);
                var filepath = System.IO.Path.Combine(Server.MapPath("/Reportes_Excel/DEACI_" + _Editions.SingleOrDefault().NumberEdition), "Reporte índice genéricos DEACI " + _Editions.SingleOrDefault().NumberEdition + ".xlsx");
                return File(filepath, "application/vnd.ms-excel", "Reporte índice genéricos DEACI " + _Editions.SingleOrDefault().NumberEdition + ".xlsx");
        }
        public FileResult ReporteÍndiceMarcas(int id, int ed)
        {
            var bind = new GridView();

            DataColumn Marca = new DataColumn();
            Marca.DataType = typeof(string);
            Marca.ColumnName = "Nombre de la marca";

            DataColumn Cliente = new DataColumn();
            Cliente.DataType = typeof(string);
            Cliente.ColumnName = "Cliente";

            DataColumn Página = new DataColumn();
            Página.DataType = typeof(string);
            Página.ColumnName = "Página";

            table.Columns.Add(Marca);
            table.Columns.Add(Cliente);
            table.Columns.Add(Página);

            var _ReportIndexes = (from _c in db.Companies
                                  join _ce in db.CompanyEditions
                                   on _c.CompanyId equals _ce.CompanyId
                                  join _cbe in db.CompanyBrandEditions
                                  on _c.CompanyId equals _cbe.CompanyId
                                  where _ce.EditionId == _cbe.EditionId
                                  join _cb in db.CompanyBrands
                                  on _c.CompanyId equals _cb.CompanyId
                                  where _cbe.BrandId == _cb.BrandId
                                  join _b in db.Brands
                                  on _cb.BrandId equals _b.BrandId
                                  join _cbi in db.CompanyBrandIndexes
                                  on _cb.BrandId equals _cbi.BrandId
                                  where _cb.CompanyId == _cbi.CompanyId
                                  join _ind in db.Indexes
                                  on _cbi.IndexId equals _ind.IndexId
                                  where _ce.EditionId == id
                                  && _ind.IndexId == ed
                                  orderby _b.BrandName, _c.CompanyName
                                  select new { _c, _b, _ce}).ToList();
            foreach (var a in _ReportIndexes)
            {
                table.Rows.Add(
                    a._b.BrandName,
                    a._c.CompanyName, 
                    a._ce.Page
                    );
            }
            var _Editions = (from _e in db.Editions
                             where _e.EditionId == id
                             select _e).ToList();
            string Directory = Request.MapPath("/Reportes_Excel/DEACI_" + _Editions.SingleOrDefault().NumberEdition);
            if (System.IO.Directory.Exists(Directory))
            {
                //
            }
            else
            {   // Crea el directorio
                System.IO.Directory.CreateDirectory(Directory);
            }
            bind.DataSource = table;
            FileInfo newFile = new FileInfo(Request.MapPath("/Reportes_Excel/DEACI_" + _Editions.SingleOrDefault().NumberEdition + "/Reporte índice marcas DEACI " + _Editions.SingleOrDefault().NumberEdition + ".xlsx"));
            ExcelPackage pkg = new ExcelPackage(newFile);
            if (newFile.Exists)
            {
                System.IO.File.Delete(Server.MapPath("/Reportes_Excel/DEACI_" + _Editions.SingleOrDefault().NumberEdition + "/Reporte índice marcas DEACI " + _Editions.SingleOrDefault().NumberEdition + ".xlsx"));
                newFile.Delete();
                pkg.Workbook.Worksheets.Delete("Reporte índice marcas");
                ExcelWorksheet worksheet = pkg.Workbook.Worksheets.Add("Reporte índice marcas");
                worksheet.Cells["A1"].LoadFromDataTable(table, true);
                using (ExcelRange range = worksheet.Cells["A1:C1"])
                {
                    range.Style.Font.Bold = true;
                    range.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    range.Style.Fill.BackgroundColor.SetColor(Color.DimGray);
                    range.Style.Font.Color.SetColor(Color.White);
                    range.Style.ShrinkToFit = false;
                }
                worksheet.Cells.Style.Font.Size = 12;
                worksheet.Cells.AutoFitColumns();
                worksheet.Cells.Style.Font.Name = "Arial Narrow";
            }
            else
            {
                ExcelWorksheet worksheet = pkg.Workbook.Worksheets.Add("Reporte índice marcas");
                worksheet.Cells["A1"].LoadFromDataTable(table, true);
                using (ExcelRange range = worksheet.Cells["A1:C1"])
                {
                    range.Style.Font.Bold = true;
                    range.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    range.Style.Fill.BackgroundColor.SetColor(Color.DimGray);
                    range.Style.Font.Color.SetColor(Color.White);
                    range.Style.ShrinkToFit = false;
                }
                worksheet.Cells.Style.Font.Size = 12;
                worksheet.Cells.AutoFitColumns();
                worksheet.Cells.Style.Font.Name = "Arial Narrow";
            }
            pkg.SaveAs(newFile);
            var filepath = System.IO.Path.Combine(Server.MapPath("/Reportes_Excel/DEACI_" + _Editions.SingleOrDefault().NumberEdition), "Reporte índice marcas DEACI " + _Editions.SingleOrDefault().NumberEdition + ".xlsx");
            return File(filepath, "application/vnd.ms-excel", "Reporte índice marcas DEACI " + _Editions.SingleOrDefault().NumberEdition + ".xlsx");
        }
        public FileResult Laboratorios_Excel(int id)
        {
            var bind = new GridView();

            DataColumn Laboratorio = new DataColumn();
            Laboratorio.DataType = typeof(string);
            Laboratorio.ColumnName = "Nombre del laboratorio";

            DataColumn Corto = new DataColumn();
            Corto.DataType = typeof(string);
            Corto.ColumnName = "Nombre corto";

            DataColumn País = new DataColumn();
            País.DataType = typeof(string);
            País.ColumnName = "País";

            DataColumn Estado = new DataColumn();
            Estado.DataType = typeof(string);
            Estado.ColumnName = "Estado";

            DataColumn Ciudad = new DataColumn();
            Ciudad.DataType = typeof(string);
            Ciudad.ColumnName = "Ciudad";

            DataColumn Dirección = new DataColumn();
            Dirección.DataType = typeof(string);
            Dirección.ColumnName = "Dirección";

            DataColumn Colonia = new DataColumn();
            Colonia.DataType = typeof(string);
            Colonia.ColumnName = "Colonia";

            DataColumn Código_Postal = new DataColumn();
            Código_Postal.DataType = typeof(string);
            Código_Postal.ColumnName = "Código postal";

            DataColumn Teléfonos = new DataColumn();
            Teléfonos.DataType = typeof(string);
            Teléfonos.ColumnName = "Teléfono(s)";

            DataColumn Teléfono_Fax = new DataColumn();
            Teléfono_Fax.DataType = typeof(string);
            Teléfono_Fax.ColumnName = "Teléfono/Fax";

            DataColumn Fax = new DataColumn();
            Fax.DataType = typeof(string);
            Fax.ColumnName = "Fax";

            DataColumn Oxidom = new DataColumn();
            Oxidom.DataType = typeof(string);
            Oxidom.ColumnName = "Oxidom";

            DataColumn Lada_sin_costo = new DataColumn();
            Lada_sin_costo.DataType = typeof(string);
            Lada_sin_costo.ColumnName = "Lada sin costo";

            DataColumn E_mail = new DataColumn();
            E_mail.DataType = typeof(string);
            E_mail.ColumnName = "E-mail";

            DataColumn Web = new DataColumn();
            Web.DataType = typeof(string);
            Web.ColumnName = "Web";

            DataColumn Delegación = new DataColumn();
            Delegación.DataType = typeof(string);
            Delegación.ColumnName = "Delegación";

            table.Columns.Add(Laboratorio);
            table.Columns.Add(Corto);
            table.Columns.Add(País);
            table.Columns.Add(Estado);
            table.Columns.Add(Ciudad);
            table.Columns.Add(Dirección);
            table.Columns.Add(Colonia);
            table.Columns.Add(Código_Postal);
            table.Columns.Add(Teléfonos);
            table.Columns.Add(Teléfono_Fax);
            table.Columns.Add(Fax);
            table.Columns.Add(Oxidom);
            table.Columns.Add(Lada_sin_costo);
            table.Columns.Add(E_mail);
            table.Columns.Add(Web);
            table.Columns.Add(Delegación);

            var _Labs = (from _c in db.Companies
                         join _ce in db.CompanyEditions
                         on _c.CompanyId equals _ce.CompanyId
                         join _ct in db.CompanyTypes
                         on _ce.CompanyTypeId equals _ct.CompanyTypeId
                         join _ca in db.CompanyAddresses
                         on _c.CompanyId equals _ca.CompanyId
                         join _ad in db.Addresses
                         on _ca.AddressId equals _ad.AddressId
                         join _ci in db.Cities
                         on _ad.CityId equals _ci.CityId
                         join _st in db.States
                         on _ci.StateId equals _st.StateId
                         join _co in db.Countries
                         on _st.CountryId equals _co.CountryId
                         where _ce.EditionId == id
                         && _ct.Description == "LABORATORIO"
                         orderby _c.CompanyName
                         select new { _c, _ad, _co, _st, _ci });
            foreach (var a in _Labs)
            {
                var _CompanyPhones = (from _cp in db.CompanyPhones
                                      join _pt in db.PhoneTypes
                                      on _cp.PhoneTypeId equals _pt.PhoneTypeId
                                      where _cp.CompanyId == a._c.CompanyId
                                      select new { _cp, _pt }).ToList();
                CompanyPhones _PhonesCompanyTel = new CompanyPhones();
                CompanyPhones _PhonesCompanyTelFax = new CompanyPhones();
                CompanyPhones _PhonesCompanyFax = new CompanyPhones();
                CompanyPhones _PhonesCompanyOxidom = new CompanyPhones();
                CompanyPhones _PhonesCompanyLada = new CompanyPhones();
                foreach (var x in _CompanyPhones)
                {
                    if (x._pt.Description.Trim() == "Teléfono(s)")
                    {
                        if (x._cp.PhoneValue == null)
                        { _PhonesCompanyTel.PhoneValue = ""; }
                        else
                        { _PhonesCompanyTel.PhoneValue = x._cp.PhoneValue; }
                    }
                    if (x._pt.Description.Trim() == "Teléfono:/Fax:")
                    {
                        if (x._cp.PhoneValue == null)
                        { _PhonesCompanyTelFax.PhoneValue = ""; }
                        else
                        { _PhonesCompanyTelFax.PhoneValue = x._cp.PhoneValue; }
                    }
                    if (x._pt.Description.Trim() == "Fax:")
                    {
                        if (x._cp.PhoneValue == null)
                        { _PhonesCompanyFax.PhoneValue = ""; }
                        else
                        { _PhonesCompanyFax.PhoneValue = x._cp.PhoneValue; }
                    }
                    if (x._pt.Description.Trim() == "Oxidom:")
                    {
                        if (x._cp.PhoneValue == null)
                        { _PhonesCompanyOxidom.PhoneValue = ""; }
                        else
                        { _PhonesCompanyOxidom.PhoneValue = x._cp.PhoneValue; }
                    }
                    if (x._pt.Description.Trim() == "Lada sin costo:")
                    {
                        if (x._cp.PhoneValue == null)
                        { _PhonesCompanyLada.PhoneValue = ""; }
                        else
                        { _PhonesCompanyLada.PhoneValue = x._cp.PhoneValue; }
                    }  
                }
                table.Rows.Add(
                    a._c.CompanyName,
                    a._c.CompanyShortName,
                    a._co.CountryName,
                    a._st.Name,
                    a._ci.Name,
                    a._ad.Address,
                    a._ad.Suburb,
                    a._ad.ZipCode,
                    _PhonesCompanyTel.PhoneValue,
                    _PhonesCompanyTelFax.PhoneValue,
                    _PhonesCompanyFax.PhoneValue,
                    _PhonesCompanyOxidom.PhoneValue,
                    _PhonesCompanyLada.PhoneValue,
                    a._ad.Email,
                    a._ad.Web,
                    a._ad.Location);
            }
            var _Editions = (from _e in db.Editions
                           where _e.EditionId == id
                           select _e).ToList();
            string Directory = Request.MapPath("/Reportes_Excel/DEACI_" + _Editions.SingleOrDefault().NumberEdition);
            if (System.IO.Directory.Exists(Directory))
            {
                //
            }
            else
            {   // Crea el directorio
                System.IO.Directory.CreateDirectory(Directory);
            }
            bind.DataSource = table;
            FileInfo newFile = new FileInfo(Request.MapPath("/Reportes_Excel/DEACI_" + _Editions.SingleOrDefault().NumberEdition + "/Laboratorios análisis clinicos DEACI " + _Editions.SingleOrDefault().NumberEdition + ".xlsx"));
            ExcelPackage pkg = new ExcelPackage(newFile);
            if (newFile.Exists)
            {
                System.IO.File.Delete(Server.MapPath("/Reportes_Excel/DEACI_" + _Editions.SingleOrDefault().NumberEdition + "/Laboratorios análisis clinicos DEACI " + _Editions.SingleOrDefault().NumberEdition + ".xlsx"));
                newFile.Delete();
                pkg.Workbook.Worksheets.Delete("Laboratorios análisis clínicos");
                ExcelWorksheet worksheet = pkg.Workbook.Worksheets.Add("Laboratorios análisis clínicos");
                worksheet.Cells["A1"].LoadFromDataTable(table, true);
                using (ExcelRange range = worksheet.Cells["A1:P1"])
                {
                    range.Style.Font.Bold = true;
                    range.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    range.Style.Fill.BackgroundColor.SetColor(Color.DimGray);
                    range.Style.Font.Color.SetColor(Color.White);
                    range.Style.ShrinkToFit = false;
                }
                    worksheet.Cells.Style.Font.Size = 12;
                    worksheet.Cells.AutoFitColumns();
                    worksheet.Cells.Style.Font.Name = "Arial Narrow";
            }
            else
            {
                ExcelWorksheet worksheet = pkg.Workbook.Worksheets.Add("Laboratorios análisis clínicos");
                worksheet.Cells["A1"].LoadFromDataTable(table, true);
                using (ExcelRange range = worksheet.Cells["A1:P1"])
                {
                    range.Style.Font.Bold = true;
                    range.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    range.Style.Fill.BackgroundColor.SetColor(Color.DimGray);
                    range.Style.Font.Color.SetColor(Color.White);
                    range.Style.ShrinkToFit = false;
                }
                worksheet.Cells.Style.Font.Size = 12;
                worksheet.Cells.AutoFitColumns();
                worksheet.Cells.Style.Font.Name = "Arial Narrow";
            }
            pkg.SaveAs(newFile);
            var filepath = System.IO.Path.Combine(Server.MapPath("/Reportes_Excel/DEACI_" + _Editions.SingleOrDefault().NumberEdition), "Laboratorios análisis clinicos DEACI " + _Editions.SingleOrDefault().NumberEdition + ".xlsx");
            return File(filepath, "application/vnd.ms-excel", "Laboratorios análisis clinicos DEACI " + _Editions.SingleOrDefault().NumberEdition + ".xlsx");
        }
        public FileResult ReporteSeccionesEdición(int id)
        {
            var bind = new GridView();

            DataColumn Sección = new DataColumn();
            Sección.DataType = typeof(string);
            Sección.ColumnName = "Sección";

            DataColumn Sub_sección = new DataColumn();
            Sub_sección.DataType = typeof(string);
            Sub_sección.ColumnName = "Sub sección";

            DataColumn Sub_sub_sección = new DataColumn();
            Sub_sub_sección.DataType = typeof(string);
            Sub_sub_sección.ColumnName = "Sub sub sección";

            DataColumn Tipo_Producto = new DataColumn();
            Tipo_Producto.DataType = typeof(string);
            Tipo_Producto.ColumnName = "Tipo de producto";

            DataColumn Producto = new DataColumn();
            Producto.DataType = typeof(string);
            Producto.ColumnName = "Producto";

            DataColumn Cliente = new DataColumn();
            Cliente.DataType = typeof(string);
            Cliente.ColumnName = "Cliente";

            table.Columns.Add(Sección);
            table.Columns.Add(Sub_sección);
            table.Columns.Add(Sub_sub_sección);
            table.Columns.Add(Tipo_Producto);
            table.Columns.Add(Producto);
            table.Columns.Add(Cliente);

            var _storeprocedure = db.Database.SqlQuery<plm_spGetProductsBySectionsByEditionS_Result>
            ("plm_spGetProductsBySectionsByEditionS @EditionId=" + id + "").ToList();
            var _orderby = (from _s in _storeprocedure
                            orderby _s.Section, _s.SubSection, _s.SubSubSection, _s.ProductName, _s.CompanyName
                            select _s).ToList();
            foreach (var a in _orderby)
            {
                table.Rows.Add(
                    a.Section, 
                    a.SubSection, 
                    a.SubSubSection,
                    a.ProductTypeDescription,
                    a.ProductName + ".",
                    a.CompanyName
                    );
            }
            var _Editions = (from _e in db.Editions
                             where _e.EditionId == id
                             select _e).ToList();
            string Directory = Request.MapPath("/Reportes_Excel/DEACI_" + _Editions.SingleOrDefault().NumberEdition);
            if (System.IO.Directory.Exists(Directory))
            {
                //
            }
            else
            {   // Crea el directorio
                System.IO.Directory.CreateDirectory(Directory);
            }
            bind.DataSource = table;
            FileInfo newFile = new FileInfo(Request.MapPath("/Reportes_Excel/DEACI_" + _Editions.SingleOrDefault().NumberEdition + "/Reporte secciones por edición DEACI " + _Editions.SingleOrDefault().NumberEdition + ".xlsx"));
            ExcelPackage pkg = new ExcelPackage(newFile);
            if (newFile.Exists)
            {
                System.IO.File.Delete(Server.MapPath("/Reportes_Excel/DEACI_" + _Editions.SingleOrDefault().NumberEdition + "/Reporte secciones por edición DEACI " + _Editions.SingleOrDefault().NumberEdition + ".xlsx"));
                newFile.Delete();
                pkg.Workbook.Worksheets.Delete("Reporte secciones por edición");
                ExcelWorksheet worksheet = pkg.Workbook.Worksheets.Add("Reporte secciones por edición");
                worksheet.Cells["A1"].LoadFromDataTable(table, true);
                using (ExcelRange range = worksheet.Cells["A1:F1"])
                {
                    range.Style.Font.Bold = true;
                    range.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    range.Style.Fill.BackgroundColor.SetColor(Color.DimGray);
                    range.Style.Font.Color.SetColor(Color.White);
                    range.Style.ShrinkToFit = false;
                }
                worksheet.Cells.Style.Font.Size = 12;
                worksheet.Cells.AutoFitColumns();
                worksheet.Cells.Style.Font.Name = "Arial Narrow";
            }
            else
            {
                ExcelWorksheet worksheet = pkg.Workbook.Worksheets.Add("Reporte secciones por edición");
                worksheet.Cells["A1"].LoadFromDataTable(table, true);
                using (ExcelRange range = worksheet.Cells["A1:F1"])
                {
                    range.Style.Font.Bold = true;
                    range.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    range.Style.Fill.BackgroundColor.SetColor(Color.DimGray);
                    range.Style.Font.Color.SetColor(Color.White);
                    range.Style.ShrinkToFit = false;
                }
                worksheet.Cells.Style.Font.Size = 12;
                worksheet.Cells.AutoFitColumns();
                worksheet.Cells.Style.Font.Name = "Arial Narrow";
            }
            pkg.SaveAs(newFile);
            var filepath = System.IO.Path.Combine(Server.MapPath("/Reportes_Excel/DEACI_" + _Editions.SingleOrDefault().NumberEdition), "Reporte secciones por edición DEACI " + _Editions.SingleOrDefault().NumberEdition + ".xlsx");
            return File(filepath, "application/vnd.ms-excel", "Reporte secciones por edición DEACI " + _Editions.SingleOrDefault().NumberEdition + ".xlsx");
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
            ViewData["HtmlFIle"] = _ProductEditions.SingleOrDefault().HtmlFile;
            return View();
        }
    }
}