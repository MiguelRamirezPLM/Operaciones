using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using Web.Models;
namespace Web.Controllers
{
    public class OffMarketsController : Controller
    {
        public Medinet md = new Medinet();
        public PLMUsersM plm = new PLMUsersM();
        public ActionResult SelectDataBases()
        {
            CountriesUsers _counTries = (CountriesUsers)Session["CountriesUsers"];
            if (Session["CountriesUsers"] == null)
            {
                Session.Abandon();
                return RedirectToAction("Logout", "Login");
            }
            string Medinet = "Medinet";
            ViewData["Medinet"] = Medinet;
            return View();
        }
        public ActionResult Countries(int id)
        {
            CountriesUsers _counTries = (CountriesUsers)Session["CountriesUsers"];
            if (Session["CountriesUsers"] == null)
            {
                Session.Abandon();
                return RedirectToAction("Logout", "Login");
            }
            if (id == 1)
            {
                ViewData["DataBase"] = "Medinet";
                ViewData["IdDataBase"] = id;
                return View();
            }
            return View();
        }
        public ActionResult Divisions(int id, int ed)
        {
            CountriesUsers _counTries = (CountriesUsers)Session["CountriesUsers"];
            if (Session["CountriesUsers"] == null)
            {
                Session.Abandon();
                return RedirectToAction("Logout", "Login");
            }
            if (id == 1)
            {
                var _roWsdivision = md.Database.SqlQuery<plm_spGetDivisionsByCountry_Result>("plm_spGetDivisionsByCountry @countryId=" + ed).ToList();
                var _counTryname = md.Countries.SingleOrDefault(_model => _model.CountryId == ed);
                ViewData["CountryName"] = _counTryname.CountryName;
                ViewData["DataBase"] = "Medinet";
                ViewData["IdDataBase"] = id;
                return View(_roWsdivision);
            }
            return View();
        } 
        public ActionResult ProductsDivisions(int id, int ed, int ad)
        {
            CountriesUsers _counTries = (CountriesUsers)Session["CountriesUsers"];
            if (Session["CountriesUsers"] == null)
            {
                Session.Abandon();
                return RedirectToAction("Logout", "Login");
            }
            if (id == 1)
            {
                SessionCountryByDivision _SessionCountryByDivision = new SessionCountryByDivision(id, ed, ad);
                Session["SessionCountryByDivision"] = _SessionCountryByDivision;
                var _counTryname = md.Countries.SingleOrDefault(_model => _model.CountryId == ed);
                ViewData["CountryName"] = _counTryname.CountryName;
                ViewData["DataBase"] = "Medinet";
                List<plm_spGetProductsbyDivisionbyCountry_Result> _roWsProducts = md.Database.SqlQuery<plm_spGetProductsbyDivisionbyCountry_Result>("plm_spGetProductsbyDivisionbyCountry @CountryId=" + ed + ", @DivisionId=" + ad + "").ToList();
                var _getdivision = md.Divisions.SingleOrDefault(_model => _model.DivisionId == ad);
                ViewData["Laboratory"] = _getdivision.Description;
                return View(_roWsProducts);
            }
            return View();
        }
        public ActionResult InsertOffMarkets(int DivisionId, int CategoryId, int PharmaFormId, int ProductId)
        {
            CountriesUsers _counTries = (CountriesUsers)Session["CountriesUsers"];
            if (Session["CountriesUsers"] == null)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            OffMarkets _OffMarkets = new OffMarkets();
            _OffMarkets.DivisionId = DivisionId;
            _OffMarkets.CategoryId = CategoryId;
            _OffMarkets.PharmaFormId = PharmaFormId;
            _OffMarkets.ProductId = ProductId;
            _OffMarkets.OffDate = DateTime.Now;
            md.Entry(_OffMarkets).State = EntityState.Added;
            md.SaveChanges();
            var _productpharmaform = (from _pe in md.ProductPharmaForms
                                      where _pe.ProductId == ProductId
                                      && _pe.PharmaFormId == PharmaFormId
                                      select _pe).ToList();
            foreach (var _update in _productpharmaform)
            {
                _update.Active = false;
                md.SaveChanges();
            }
            var _Aplication = (from _ap in plm.Applications
                               where _ap.Description == "OffMarkets"
                               select _ap).ToList();
            foreach (var _App in _Aplication)
            {
                var _Tables = (from _tab in plm.Tables
                               where _tab.ApplicationId == _App.ApplicationId
                               && _tab.Description == "OffMarkets"
                               select _tab).ToList();
                foreach (var _Tabs in _Tables)
                {
                    var _ApplicationId = _App.ApplicationId;
                    ActivityLogs _ActivityLogs = new ActivityLogs();
                    int _OperationId = 1; // Add
                    _ActivityLogs.UserId = _counTries.userId;
                    _ActivityLogs.TableId = _Tabs.TableId; // OffMarkets
                    _ActivityLogs.OperationId = _OperationId;
                    _ActivityLogs.Date = DateTime.Now;
                    _ActivityLogs.PrimaryKeyAffected = ("(DivisionId," + _OffMarkets.DivisionId + "),(CategoryId," + _OffMarkets.CategoryId + "),(PharmaFormId," + _OffMarkets.PharmaFormId + "),(ProductId," + _OffMarkets.ProductId + ")");
                    _ActivityLogs.FieldsAffected = null;
                    plm.Entry(_ActivityLogs).State = EntityState.Added;
                    //plm.SaveChanges();
                }
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeleteOffMarkets(int DivisionId, int CategoryId, int PharmaFormId, int ProductId)
        {
            CountriesUsers _counTries = (CountriesUsers)Session["CountriesUsers"];
            if (Session["CountriesUsers"] == null)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            OffMarkets _OffMarkets = new OffMarkets();
            _OffMarkets.DivisionId = DivisionId;
            _OffMarkets.CategoryId = CategoryId;
            _OffMarkets.PharmaFormId = PharmaFormId;
            _OffMarkets.ProductId = ProductId;
            md.Entry(_OffMarkets).State = EntityState.Deleted;
            md.SaveChanges();
            var _productpharmaform = (from _pe in md.ProductPharmaForms
                                      where _pe.ProductId == ProductId
                                      && _pe.PharmaFormId == PharmaFormId
                                      select _pe).ToList();
            foreach (var _update in _productpharmaform)
            {
                _update.Active = true;
                md.SaveChanges();
            }
            var _Aplication = (from _ap in plm.Applications
                               where _ap.Description == "OffMarkets"
                               select _ap).ToList();
            foreach (var _App in _Aplication)
            {
                var _Tables = (from _tab in plm.Tables
                               where _tab.ApplicationId == _App.ApplicationId
                               && _tab.Description == "OffMarkets"
                               select _tab).ToList();
                foreach (var _Tabs in _Tables)
                {
                    var _ApplicationId = _App.ApplicationId;
                    ActivityLogs _ActivityLogs = new ActivityLogs();
                    int _OperationId = 4; // Delete
                    _ActivityLogs.UserId = _counTries.userId;
                    _ActivityLogs.TableId = _Tabs.TableId; // OffMarkets
                    _ActivityLogs.OperationId = _OperationId;
                    _ActivityLogs.Date = DateTime.Now;
                    _ActivityLogs.PrimaryKeyAffected = ("(DivisionId," + _OffMarkets.DivisionId + "),(CategoryId," + _OffMarkets.CategoryId + "),(PharmaFormId," + _OffMarkets.PharmaFormId + "),(ProductId," + _OffMarkets.ProductId + ")");
                    _ActivityLogs.FieldsAffected = null;
                    plm.Entry(_ActivityLogs).State = EntityState.Added;
                    //plm.SaveChanges();
                }
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        public ActionResult UserManual()
        {
            CountriesUsers _counTries = (CountriesUsers)Session["CountriesUsers"];
            if (Session["CountriesUsers"] == null)
            {
                Session.Abandon();
                return RedirectToAction("Logout", "Login");
            }
            return View();
        }
    }
}